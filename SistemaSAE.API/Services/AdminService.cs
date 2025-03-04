using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Enums;
using SistemaSAE.API.Interfaces;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;

namespace SistemaSAE.API.Services
{
    public class AdminService : IAdmin
    {
        private readonly IAdministradorRepository _administradorRepository;
        private readonly IEstacionamientoRepository _estacionamientoRepository;

        public AdminService(IAdministradorRepository administradorRepository, IEstacionamientoRepository estacionamientoRepository)
        {
            _administradorRepository = administradorRepository;
            _estacionamientoRepository = estacionamientoRepository;
        }

        public async Task<AdminDTO> AutenticarAdministrador(string nombreUsuario, string contrasena)
        {
            var admin = await _administradorRepository.AutenticarAdministrador(nombreUsuario, contrasena);

            if (admin == null)
                throw new Exception("Credenciales incorrectas.");

            return new AdminDTO
            {
                IDAdministrador = admin.IDAdministrador,
                NombreUsuario = admin.NombreUsuario
            };
        }

        public async Task<IEnumerable<EstacionamientoDTO>> ObtenerEstadoEstacionamientos()
        {
            var estacionamientos = await _estacionamientoRepository.ObtenerTodos();

            return estacionamientos.Select(e => new EstacionamientoDTO
            {
                IDEstacionamiento = e.IDEstacionamiento,
                TipoVehiculo = e.TipoVehiculo.ToString(),
                CapacidadTotal = e.CapacidadTotal,
                Ocupados = e.Ocupados
            });
        }

        public async Task<EstacionamientoDTO> ObtenerEstacionamientoPorTipoVehiculo(string tipoVehiculo)
        {
            if (!Enum.TryParse(tipoVehiculo, out TipoVehiculo tipoVehiculoEnum))
            {
                throw new Exception("El tipo de vehículo proporcionado no es válido.");
            }

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculoEnum);

            if (estacionamiento == null)
                throw new Exception("Estacionamiento no encontrado.");

            return new EstacionamientoDTO
            {
                IDEstacionamiento = estacionamiento.IDEstacionamiento,
                TipoVehiculo = estacionamiento.TipoVehiculo.ToString(),
                CapacidadTotal = estacionamiento.CapacidadTotal,
                Ocupados = estacionamiento.Ocupados
            };
        }

        public async Task ActualizarEstacionamiento(EstacionamientoDTO estacionamientoDTO)
        {
            if (!Enum.TryParse(estacionamientoDTO.TipoVehiculo, out TipoVehiculo tipoVehiculo))
            {
                throw new Exception("El tipo de vehículo proporcionado no es válido.");
            }

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Estacionamiento no encontrado.");

            estacionamiento.CapacidadTotal = estacionamientoDTO.CapacidadTotal;
           
            await _estacionamientoRepository.Actualizar(estacionamiento);
        }

        public async Task CrearEstacionamiento(EstacionamientoDTO estacionamientoDTO)
        {
            if (!Enum.TryParse<TipoVehiculo>(estacionamientoDTO.TipoVehiculo, out var tipoVehiculo))
                throw new Exception("Tipo de vehículo no válido.");

            var existingEstacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (existingEstacionamiento != null)
                throw new Exception("Ya existe un estacionamiento para este tipo de vehículo.");

            var estacionamiento = new Estacionamiento
            {
                TipoVehiculo = tipoVehiculo,
                CapacidadTotal = estacionamientoDTO.CapacidadTotal,
                Ocupados = estacionamientoDTO.Ocupados
            };

            await _estacionamientoRepository.Crear(estacionamiento);
        }

        public async Task AdministrarCapacidad(TicketViewModel vehiculo, int nuevaCapacidad)
        {
            if (!Enum.TryParse(vehiculo.TipoVehiculo, out TipoVehiculo tipoVehiculo))
                throw new Exception("Tipo de vehículo no válido.");

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Tipo de vehículo no soportado.");

            estacionamiento.CapacidadTotal = nuevaCapacidad;

            await _estacionamientoRepository.Actualizar(estacionamiento);
        }
    }
}
