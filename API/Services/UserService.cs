using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Dtos;
using API.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace API.Services
{
    public class UserService : IUserService
{
    private readonly API.Helpers.JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    public UserService(IUnitOfWork unitOfWork, IOptions<API.Helpers.JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var user = new Usuario
        {
            UserName = registerDto.UserName,
            Email = registerDto.UserEmail,
        };

        user.PassName = _passwordHasher.HashPassword(user, registerDto.UserPassword); //Encrypt password

        var existingUser = _unitOfWork.Usuarios
                                    .Find(u => u.UserName.ToLower() == registerDto.UserName.ToLower())
                                    .FirstOrDefault();


        if (existingUser == null)
        {
            Rol rolDefault = _unitOfWork.Roles
                                    .Find(u => u.Descripcion == Authorization.rol_default.ToString())
                                    .First();
            try
            {
                user.Roles.Add(rolDefault);
                _unitOfWork.Usuarios.Add(user);
                await _unitOfWork.SaveAsync();

                return $"Usuario Registrado Correctamente";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"Usuario ya tiene Registro";
        }
    }








    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto dataUserDto = new DataUserDto();
        var user = await _unitOfWork.Usuarios
                    .GetByUsernameAsync(model.Username);

        if (user == null)
        {
            dataUserDto.RefreshToken = "";
            dataUserDto.RefreshTokenExpiry = DateTime.Now;
            dataUserDto.UserToken = "";
            dataUserDto.UserEmail = "";
            dataUserDto.UserName = "";
            dataUserDto.UserRoles = null;
            dataUserDto.EstadoAutenticado = false;
            dataUserDto.Mensaje = $"Usuario No Existe";
            return dataUserDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PassName, model.UserPassword);

        if (result == PasswordVerificationResult.Success)
        {
            dataUserDto.EstadoAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
            dataUserDto.UserToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            dataUserDto.UserEmail = user.Email;
            dataUserDto.UserName = user.UserName;
            dataUserDto.UserRoles = user.Roles
                                            .Select(u => u.Descripcion)
                                            .ToList();

            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                dataUserDto.Mensaje = "Usuario Existente";
                dataUserDto.RefreshToken = activeRefreshToken.Token;
                dataUserDto.RefreshTokenExpiry = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                dataUserDto.RefreshToken = refreshToken.Token;
                dataUserDto.RefreshTokenExpiry = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);
                _unitOfWork.Usuarios.Update(user);
                await _unitOfWork.SaveAsync();
            }

            return dataUserDto;
        }
        dataUserDto.RefreshToken = "";
            dataUserDto.RefreshTokenExpiry = DateTime.Now;
            dataUserDto.UserToken = "";
            dataUserDto.UserEmail = "";
            dataUserDto.UserName = "";
            dataUserDto.UserRoles = null;
        dataUserDto.EstadoAutenticado = false;
        dataUserDto.Mensaje = $"Credenciales incorrectas para el usuario";
        return dataUserDto;
    }




    public async Task<string> AddRoleAsync(AddRoleDto model)
    {

        var user = await _unitOfWork.Usuarios
                    .GetByUsernameAsync(model.UserName);
        if (user == null)
        {
            return $"User {model.UserName} does not exists.";
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PassName, model.UserPassword);

        if (result == PasswordVerificationResult.Success)
        {
            var rolExists = _unitOfWork.Roles
                                        .Find(u => u.Descripcion.ToLower() == model.UserRol.ToLower())
                                        .FirstOrDefault();

            if (rolExists != null)
            {
                var userHasRole = user.Roles
                                            .Any(u => u.Id == rolExists.Id);

                if (userHasRole == false)
                {
                    user.Roles.Add(rolExists);
                    _unitOfWork.Usuarios.Update(user);
                    await _unitOfWork.SaveAsync();
                }

                return $"Role {model.UserRol} added to user {model.UserName} successfully.";
            }

            return $"Role {model.UserRol} was not found.";
        }
        return $"Invalid Credentials";
    }








    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var dataUserDto = new DataUserDto();

        var usuario = await _unitOfWork.Usuarios
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataUserDto.EstadoAutenticado = false;
            dataUserDto.Mensaje = $"Token is not assigned to any user.";
            return dataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataUserDto.EstadoAutenticado = false;
            dataUserDto.Mensaje = $"Token is not active.";
            return dataUserDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        dataUserDto.EstadoAutenticado = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        dataUserDto.UserToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        dataUserDto.UserEmail = usuario.Email;
        dataUserDto.UserName = usuario.UserName;
        dataUserDto.UserRoles = usuario.Roles
                                        .Select(u => u.Descripcion)
                                        .ToList();
        dataUserDto.RefreshToken = newRefreshToken.Token;
        dataUserDto.RefreshTokenExpiry = newRefreshToken.Expires;
        return dataUserDto;
    }














    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }







    private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        var roles = usuario.Roles;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Descripcion));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
    }
}
