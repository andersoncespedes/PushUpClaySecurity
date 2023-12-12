
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
public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Add(PersonaDto genero)
    {
        var entity = _mapper.Map<Persona>(genero);
        _unitOfWork.Personas.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.Personas.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<PersonaDto>>(labs.registros);
        return new Pager<PersonaDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> GetById(int id)
    {
        var genero = await _unitOfWork.Personas.GetById(id);
        return _mapper.Map<PersonaDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetAll()
    {
        var generos = await _unitOfWork.Personas.GetAll();
        return _mapper.Map<List<PersonaDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Personas.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.Personas.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] PersonaDto ciudad)
    {
        var genero = await _unitOfWork.Personas.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<Persona>(ciudad);
        _unitOfWork.Personas.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetAllEmployees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<PersonaDto>> GetEmployees()
    {
        var datos = await _unitOfWork.Personas.GetAllEmpleados();
        return _mapper.Map<List<PersonaDto>>(datos);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetWatchMen")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetWatchMen()
    {
        var datos = await _unitOfWork.Personas.GetWatchMen();
        return _mapper.Map<List<PersonaDto>>(datos);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetWatchMenWithContact")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaWithContactoDto>>> GetWatchMenWithContact()
    {
        var datos = await _unitOfWork.Personas.GetWatchMenWithContacts();
        return _mapper.Map<List<PersonaWithContactoDto>>(datos);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetClientFromBucaramanga")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetClientFromBucaramanga()
    {
        var datos = await _unitOfWork.Personas.GetClientsFromBucaramanga();
        return _mapper.Map<List<PersonaDto>>(datos);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetEmployeeFromBucaramangaOrGiron")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetEmployeeFromBucaramangaOrGiron()
    {
        var datos = await _unitOfWork.Personas.GetEmployeesFromGironOrBucaramanga();
        return _mapper.Map<List<PersonaDto>>(datos);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetClients5YearOlder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetClients5YearOlder()
    {
        var datos = await _unitOfWork.Personas.GetClients5YearOlder();
        return _mapper.Map<List<PersonaDto>>(datos);
    }
}