using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ICategoria _categorias;

    private ICiudad _ciudad;

    private IContactoPersona _contactoPersona ;

    private IContrato _contratos ;

    private IDepartamento _departamentos ;

    private IDirPersona _dirPersonas;

    private IEstado _estados;

    private IPais _paises ;

    private IPersona _personas;

    private IProgramacion _programaciones;

    private ITipoContacto _tipoContactos ;

    private ITipoDireccion _tipoDirecciones;

    private ITipoPersona _tipoPersonas ;
    private IUsuario _usuarios;
    private IRol _rol;


    private ITurno _turnos ;
    private readonly APIContext _context;

    public UnitOfWork(APIContext context){
        _context = context;
    }
    public ICategoria Categorias
    {
        get
        {
            _categorias ??= new CategoriaRepository(_context);
            return _categorias;
        }
    }

    public ICiudad Ciudades
    {
        get
        {
            _ciudad ??= new CiudadRepository(_context);
            return _ciudad;
        }
    }

    public IContactoPersona ContactoPersona
    {
        get
        {
            _contactoPersona ??= new ContactoPersonaRepository(_context);
            return _contactoPersona;
        }
    }

    public IContrato Contratos
    {
        get
        {
            _contratos ??= new ContratoRepository(_context);
            return _contratos;
        }
    }

    public IDepartamento Departamentos
    {
        get
        {
            _departamentos ??= new DepartamentoRepository(_context);
            return _departamentos;
        }
    }

    public IDirPersona DirPersonas
    {
        get
        {
            _dirPersonas ??= new DirPersonaRepository(_context);
            return _dirPersonas;
        }
    }

    public IEstado Estados
    {
        get
        {
            _estados ??= new EstadoRepository(_context);
            return _estados;
        }
    }

    public IPais Paises
    {
        get{
            _paises ??= new PaisRepository(_context);
            return _paises;
        }
    }

    public IPersona Personas
    {
        get
        {
            _personas ??= new PersonaRepository(_context);
            return _personas;
        }
    }

    public IProgramacion Programaciones
    {
        get
        {
            _programaciones ??= new ProgramacionRepository(_context);
            return _programaciones;
        }
    }

    public ITipoContacto TipoContactos
    {
        get
        {
            _tipoContactos ??= new TipoContactoRepository(_context);
            return _tipoContactos;
        }
    }

    public ITipoDireccion TipoDirecciones
    {
        get
        {
            _tipoDirecciones ??= new TipoDireccionRepository(_context);
            return _tipoDirecciones;
        }
    }

    public ITipoPersona TipoPersonas
    {
        get
        {
            _tipoPersonas ??= new TipoPersonaRepository(_context);
            return _tipoPersonas;
        }
    }

    public ITurno Turnos 
    {
        get
        {
            _turnos ??= new TurnoRepository(_context);
            return _turnos;
        }
    }

    public IUsuario Usuarios
    {
        get
        {
            _usuarios ??= new UsuarioRepository(_context);
            return _usuarios;
        }
    }

    public IRol Roles {
        get
        {
            _rol ??= new RolRepository(_context);
            return _rol;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}