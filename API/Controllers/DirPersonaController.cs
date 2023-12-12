using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
public class DirPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public DirPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DirPersonaDto>> Add(DirPersonaDto genero)
    {
        var entity = _mapper.Map<DirPersona>(genero);
        _unitOfWork.DirPersonas.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DirPersonaDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.DirPersonas.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<DirPersonaDto>>(labs.registros);
        return new Pager<DirPersonaDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DirPersonaDto>> GetById(int id)
    {
        var genero = await _unitOfWork.DirPersonas.GetById(id);
        return _mapper.Map<DirPersonaDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DirPersonaDto>>> GetAll()
    {
        var generos = await _unitOfWork.DirPersonas.GetAll();
        return _mapper.Map<List<DirPersonaDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.DirPersonas.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.DirPersonas.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] DirPersonaDto ciudad)
    {
        var genero = await _unitOfWork.DirPersonas.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<DirPersona>(ciudad);
        _unitOfWork.DirPersonas.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}