using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Ciudad, CiudadDto>().ReverseMap();
            CreateMap<ContactoPersona, ContactoPersonaDto>().ReverseMap();
            CreateMap<Contrato, ContratoDto>().ReverseMap();
            CreateMap<DirPersona, DirPersonaDto>().ReverseMap();
            CreateMap<Estado, EstadoDto>().ReverseMap();
            CreateMap<Pais, PaisDto>().ReverseMap();
            CreateMap<Persona, PersonaDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<Turno, TurnoDto>().ReverseMap();
            CreateMap<Persona, PersonaWithContactoDto>().ReverseMap();
            CreateMap<Contrato, ContratoWithEmployeeAndClient>()
            .ForMember(e => e.Empleado , dest => dest.MapFrom(e => e.Empleado.Nombre))
            .ForMember(e => e.Cliente , dest => dest.MapFrom(e => e.Cliente.Nombre))
            .ReverseMap();

        }
    }