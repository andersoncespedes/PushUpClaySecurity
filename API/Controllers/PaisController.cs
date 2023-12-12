
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class PaisController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisDto>> Add(PaisDto genero)
    {
        var entity = _mapper.Map<Pais>(genero);
        _unitOfWork.Paises.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PaisDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.Paises.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<PaisDto>>(labs.registros);
        return new Pager<PaisDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisDto>> GetById(int id)
    {
        var genero = await _unitOfWork.Paises.GetById(id);
        return _mapper.Map<PaisDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaisDto>>> GetAll()
    {
        var generos = await _unitOfWork.Paises.GetAll();
        return _mapper.Map<List<PaisDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Paises.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.Paises.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] PaisDto ciudad)
    {
        var genero = await _unitOfWork.Paises.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<Pais>(ciudad);
        _unitOfWork.Paises.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
