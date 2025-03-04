using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Enums;
using SistemaSAE.API.Interfaces;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;
using SistemaSAE.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSAE.API.Services
{
    public class VehicleService : IVehicle
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEstacionamientoRepository _estacionamientoRepository;
        private readonly ITarifaRepository _tarifaRepository;
        private readonly IRegistroPagoRepository _registroPagoRepository;

        public VehicleService(
            ITicketRepository ticketRepository,
            IEstacionamientoRepository estacionamientoRepository,
            ITarifaRepository tarifaRepository,
            IRegistroPagoRepository registroPagoRepository)
        {
            _ticketRepository = ticketRepository;
            _estacionamientoRepository = estacionamientoRepository;
            _tarifaRepository = tarifaRepository;
            _registroPagoRepository = registroPagoRepository;
        }

        public async Task<TicketDTO> GenerarTicket(TicketViewModel vehiculo)
        {
            if (!Enum.TryParse<TipoVehiculo>(vehiculo.TipoVehiculo, true, out var tipoVehiculo))
                throw new Exception("Tipo de vehículo no válido.");

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Tipo de vehículo no soportado.");

            if (estacionamiento.Ocupados >= estacionamiento.CapacidadTotal)
                throw new Exception("No hay disponibilidad para este tipo de vehículo.");

            var codigo = stringGenerator.GenerarCodigoUnico();

            var ticket = new Ticket
            {
                Codigo = codigo,
                TipoVehiculo = tipoVehiculo,
                HoraIngreso = DateTime.Now
            };

            await _ticketRepository.Crear(ticket);

            estacionamiento.Ocupados += 1;
            await _estacionamientoRepository.Actualizar(estacionamiento);

            return new TicketDTO
            {
                Codigo = ticket.Codigo,
                TipoVehiculo = ticket.TipoVehiculo.ToString(),
                HoraIngreso = ticket.HoraIngreso
            };
        }

        public async Task<SalidaResponseDTO> RegistrarSalida(string codigoTicket)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigoTicket);

            if (ticket == null)
                throw new Exception("Ticket no encontrado.");

            if (ticket.HoraSalida != null)
                throw new Exception("La salida ya ha sido registrada para este ticket.");

            ticket.HoraSalida = DateTime.Now;

            var duracionEnMinutos = TimeHelper.CalcularDuracionEnMinutos(ticket.HoraIngreso, ticket.HoraSalida.Value);

            var tarifa = await _tarifaRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo.ToString());

            if (tarifa == null)
                throw new Exception("No se encontró tarifa para este tipo de vehículo.");

            ticket.TotalPago = TimeHelper.CalcularCobro(duracionEnMinutos, tarifa.TarifaPorHora, tarifa.TiempoCortesia);

            await _ticketRepository.Actualizar(ticket);

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Estacionamiento no encontrado para este tipo de vehículo.");

            if (estacionamiento.Ocupados <= 0)
                throw new Exception("No hay vehículos ocupando espacios para este tipo.");

            estacionamiento.Ocupados -= 1; 
            await _estacionamientoRepository.Actualizar(estacionamiento);

            var registroPago = new RegistroPago
            {
                CodigoTicket = ticket.Codigo,
                TipoVehiculo = ticket.TipoVehiculo.ToString(),
                HoraIngreso = ticket.HoraIngreso,
                HoraSalida = ticket.HoraSalida.Value,
                MontoCobrado = ticket.TotalPago,
                DuracionEstadia = duracionEnMinutos
            };

            await _registroPagoRepository.Crear(registroPago);

            var response = new SalidaResponseDTO
            {
                Message = "Salida registrada exitosamente.",
                TotalPago = ticket.TotalPago
            };

            return response;
        }




        public async Task<IEnumerable<TicketDTO>> ObtenerTicketsActivos()
        {
            var tickets = await _ticketRepository.ObtenerActivos();

            return tickets.Select(t => new TicketDTO
            {
                Codigo = t.Codigo,
                TipoVehiculo = t.TipoVehiculo.ToString(),
                HoraIngreso = t.HoraIngreso
            });
        }

        public async Task<TicketDTO> ObtenerTicketPorCodigo(string codigo)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigo);

            if (ticket == null)
                return null;

            return new TicketDTO
            {
                Codigo = ticket.Codigo,
                TipoVehiculo = ticket.TipoVehiculo.ToString(),
                HoraIngreso = ticket.HoraIngreso,
                HoraSalida = ticket.HoraSalida,
                TotalPago = ticket.TotalPago
            };
        }

        public async Task ActualizarTicket(TicketDTO ticketDTO)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(ticketDTO.Codigo);

            if (ticket == null)
                throw new Exception("Ticket no encontrado.");

            ticket.TipoVehiculo = (TipoVehiculo)Enum.Parse(typeof(TipoVehiculo), ticketDTO.TipoVehiculo);
            ticket.HoraIngreso = ticketDTO.HoraIngreso;
            ticket.HoraSalida = ticketDTO.HoraSalida;
            ticket.TotalPago = ticketDTO.TotalPago;

            await _ticketRepository.Actualizar(ticket);
        }

        public async Task<bool> EliminarTicket(string codigo)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigo);

            if (ticket != null)
            {
                var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo);

                if (estacionamiento != null && estacionamiento.Ocupados > 0)
                {
                    estacionamiento.Ocupados -= 1;
                    await _estacionamientoRepository.Actualizar(estacionamiento);
                }

                await _ticketRepository.Eliminar(ticket);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
