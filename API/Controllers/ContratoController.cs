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
public class ContratoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public ContratoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContratoDto>> Add(ContratoDto genero)
    {
        var entity = _mapper.Map<Contrato>(genero);
        _unitOfWork.Contratos.Add(entity);
        await _unitOfWork.SaveAsync();
        return genero;
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ContratoDto>>> Paginacion([FromQuery] Params Params)
    {
        var labs = await _unitOfWork.Contratos.Paginacion(Params.PageIndex, Params.PageSize, Params.Search);
        var mapeo = _mapper.Map<List<ContratoDto>>(labs.registros);
        return new Pager<ContratoDto>(mapeo, labs.totalRegistros, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContratoDto>> GetById(int id)
    {
        var genero = await _unitOfWork.Contratos.GetById(id);
        return _mapper.Map<ContratoDto>(genero);
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [MapToApiVersion("1.1")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContratoDto>>> GetAll()
    {
        var generos = await _unitOfWork.Contratos.GetAll();
        return _mapper.Map<List<ContratoDto>>(generos);
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Contratos.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        _unitOfWork.Contratos.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador")]
    [HttpDelete("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, [FromBody] ContratoDto ciudad)
    {
        var genero = await _unitOfWork.Contratos.GetById(id);
        if (genero == null)
        {
            return BadRequest();
        }
        var dato = _mapper.Map<Contrato>(ciudad);
        _unitOfWork.Contratos.Update(dato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [Authorize(Roles = "Administrador, Empleado")]
    [HttpGet("GetContratsWithClientAndEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ContratoWithEmployeeAndClient>>> GetContratsWithClientAndEmployee()
    {
        var datos = await _unitOfWork.Contratos.GetActivadedContrats();
        return _mapper.Map<List<ContratoWithEmployeeAndClient>>(datos);
    }
}