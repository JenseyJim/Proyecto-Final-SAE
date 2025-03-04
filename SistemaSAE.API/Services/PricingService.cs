using Microsoft.EntityFrameworkCore;
using SistemaSAE.API.Context;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;
using SistemaSAE.API.Models;
using SistemaSAE.API.Repositories.Interfaces;
using SistemaSAE.API.Utils;

namespace SistemaSAE.API.Services
{
    public class PricingService : IPricing
    {
        private readonly ITarifaRepository _tarifaRepository;
        private readonly ITicketRepository _ticketRepository;

        public PricingService(ITarifaRepository tarifaRepository, ITicketRepository ticketRepository)
        {
            _tarifaRepository = tarifaRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<decimal> CalcularPago(string codigoTicket, DateTime horaSalida)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigoTicket);

            if (ticket == null)
                throw new Exception("Ticket no encontrado.");

            var duracionEnMinutos = TimeHelper.CalcularDuracionEnMinutos(ticket.HoraIngreso, horaSalida);

            var tarifa = await _tarifaRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo.ToString());

            if (tarifa == null)
                throw new Exception("No se encontró tarifa para este tipo de vehículo.");

            var totalPago = TimeHelper.CalcularCobro(duracionEnMinutos, tarifa.TarifaPorHora, tarifa.TiempoCortesia);

            return totalPago;
        }

        public async Task<IEnumerable<PricingDTO>> ObtenerTarifas()
        {
            var tarifas = await _tarifaRepository.ObtenerTodas();

            return tarifas.Select(t => new PricingDTO
            {
                IDTarifa = t.IDTarifa,
                TipoVehiculo = t.TipoVehiculo,
                TarifaPorHora = t.TarifaPorHora,
                TiempoCortesia = t.TiempoCortesia
            });
        }

        public async Task<PricingDTO> ObtenerTarifaPorTipoVehiculo(string tipoVehiculo)
        {
            var tarifa = await _tarifaRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (tarifa == null)
                return null;

            return new PricingDTO
            {
                IDTarifa = tarifa.IDTarifa,
                TipoVehiculo = tarifa.TipoVehiculo,
                TarifaPorHora = tarifa.TarifaPorHora,
                TiempoCortesia = tarifa.TiempoCortesia
            };
        }

        public async Task ActualizarTarifa(PricingDTO tarifaDTO)
        {
            var tarifa = await _tarifaRepository.ObtenerPorTipoVehiculo(tarifaDTO.TipoVehiculo);

            if (tarifa == null)
                throw new Exception("Tarifa no encontrada.");

            tarifa.TarifaPorHora = tarifaDTO.TarifaPorHora;
            tarifa.TiempoCortesia = tarifaDTO.TiempoCortesia;

            await _tarifaRepository.Actualizar(tarifa);
        }

        public async Task CrearTarifa(PricingDTO tarifaDTO)
        {
            var tarifa = new Tarifa
            {
                TipoVehiculo = tarifaDTO.TipoVehiculo,
                TarifaPorHora = tarifaDTO.TarifaPorHora,
                TiempoCortesia = tarifaDTO.TiempoCortesia
            };

            await _tarifaRepository.Crear(tarifa);
        }

        public async Task EliminarTarifa(int idTarifa)
        {
            var tarifa = await _tarifaRepository.ObtenerTodas()
                .ContinueWith(t => t.Result.FirstOrDefault(tarifa => tarifa.IDTarifa == idTarifa));

            if (tarifa != null)
            {
                await _tarifaRepository.Eliminar(tarifa);
            }
        }
    }

}
