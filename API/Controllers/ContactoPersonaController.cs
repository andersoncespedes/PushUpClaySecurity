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
public class ContactoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public ContactoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactoPersonaDto>> Add(ContactoPersonaDto genero)
    {
        var entity = _mapper.Map<ContactoPersona>(genero);
        _unitOfWork.ContactoPersona.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ContactoPersonaDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.ContactoPersona.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<ContactoPersonaDto>>(labs.registros);
        return new Pager<ContactoPersonaDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactoPersonaDto>> GetById(int id)
    {
        var genero = await _unitOfWork.ContactoPersona.GetById(id);
        return _mapper.Map<ContactoPersonaDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContactoPersonaDto>>> GetAll()
    {
        var generos = await _unitOfWork.ContactoPersona.GetAll();
        return _mapper.Map<List<ContactoPersonaDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.ContactoPersona.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.ContactoPersona.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] ContactoPersonaDto ciudad)
    {
        var genero = await _unitOfWork.ContactoPersona.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<ContactoPersona>(ciudad);
        _unitOfWork.ContactoPersona.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}