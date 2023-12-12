
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
public class TurnoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public TurnoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TurnoDto>> Add(TurnoDto genero)
    {
        var entity = _mapper.Map<Turno>(genero);
        _unitOfWork.Turnos.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TurnoDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.Turnos.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<TurnoDto>>(labs.registros);
        return new Pager<TurnoDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TurnoDto>> GetById(int id)
    {
        var genero = await _unitOfWork.Turnos.GetById(id);
        return _mapper.Map<TurnoDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TurnoDto>>> GetAll()
    {
        var generos = await _unitOfWork.Turnos.GetAll();
        return _mapper.Map<List<TurnoDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Turnos.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.Turnos.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] TurnoDto ciudad)
    {
        var genero = await _unitOfWork.Turnos.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<Turno>(ciudad);
        _unitOfWork.Turnos.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}