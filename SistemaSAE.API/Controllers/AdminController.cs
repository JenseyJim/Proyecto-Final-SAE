using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaSAE.API.DTOs;
using SistemaSAE.API.Interfaces;

namespace SistemaSAE.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _adminService;

        public AdminController(IAdmin adminService)
        {
            _adminService = adminService;
        }

        // POST /api/admin/login
        [HttpPost("login")]
        public async Task<ActionResult<AdminDTO>> Login([FromBody] AdminLoginDTO loginDTO)
        {
            try
            {
                var admin = await _adminService.AutenticarAdministrador(loginDTO.NombreUsuario, loginDTO.Contrasena);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // GET /api/admin/spaces
        [HttpGet("spaces")]
        public async Task<ActionResult<IEnumerable<EstacionamientoDTO>>> GetSpaces()
        {
            var espacios = await _adminService.ObtenerEstadoEstacionamientos();
            return Ok(espacios);
        }

        // GET /api/admin/spaces/{tipoVehiculo}
        [HttpGet("spaces/{tipoVehiculo}")]
        public async Task<ActionResult<EstacionamientoDTO>> GetEstacionamientoByTipoVehiculo(string tipoVehiculo)
        {
            try
            {
                var estacionamiento = await _adminService.ObtenerEstacionamientoPorTipoVehiculo(tipoVehiculo);
                return Ok(estacionamiento);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // PUT /api/admin/spaces/update
        [HttpPut("spaces/update")]
        public async Task<ActionResult> UpdateEstacionamiento([FromBody] EstacionamientoDTO estacionamientoDTO)
        {
            try
            {
                await _adminService.ActualizarEstacionamiento(estacionamientoDTO);
                return Ok(new { message = "Estacionamiento actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/admin/capacity
        [HttpPost("capacity")]
        public async Task<ActionResult> AdministrarCapacidad([FromBody] CapacidadDTO capacidadDTO)
        {
            try
            {
                await _adminService.AdministrarCapacidad(capacidadDTO.Vehiculo, capacidadDTO.NuevaCapacidad);
                return Ok(new { message = "Capacidad actualizada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("spaces/create")]
        public async Task<ActionResult> CreateEstacionamiento([FromBody] EstacionamientoDTO estacionamientoDTO)
        {
            try
            {
                await _adminService.CrearEstacionamiento(estacionamientoDTO);
                return Ok(new { message = "Estacionamiento creado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
