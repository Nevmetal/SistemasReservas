using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SistemaReservaCitas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SistemaReservaCitas.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar solo las citas pendientes
        public async Task<IActionResult> Index()
        {
            var citasPendientes = await _context.Citas
                .Where(c => c.Estado == EstadoCita.Pendiente)
                .OrderBy(c => c.Fecha)
                .ToListAsync();
            return View(citasPendientes);
        }
        // Acción para mostrar todas las citas con opción de filtrar por estado
        public async Task<IActionResult> Consulta(EstadoCita? estadoFiltro)
        {
            var citas = _context.Citas.AsQueryable();

            // Aplica el filtro de estado si está seleccionado
            if (estadoFiltro.HasValue)
            {
                citas = citas.Where(c => c.Estado == estadoFiltro.Value);
            }

            // Pasa el estado seleccionado a la vista para mantener el filtro
            ViewData["EstadoFiltro"] = estadoFiltro?.ToString();
            return View(await citas.OrderBy(c => c.Fecha).ToListAsync());
        }

        // Acción para crear una nueva cita
        public IActionResult Crear()
        {
            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult> Crear(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Fecha = DateTime.SpecifyKind(cita.Fecha, DateTimeKind.Utc);
                _context.Citas.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cita);
        }
        //Editar
        public async Task<IActionResult> Editar(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            return View(cita);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Fecha = DateTime.SpecifyKind(cita.Fecha, DateTimeKind.Utc);
                _context.Citas.Update(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cita);
        }
        public async Task<IActionResult> MarcarCumplido(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            cita.Estado = EstadoCita.Cumplido;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancelar(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            cita.Estado = EstadoCita.Cancelado;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Calendario
        public async Task<IActionResult> ObtenerCitas()
        {
            var citas = await _context.Citas.ToListAsync();

            var eventos = citas.Select(c => new
            {
                title = $"{c.ClienteNombre} - {c.Servicio}",
                start = c.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                color = c.Estado == EstadoCita.Pendiente ? "#FFA500" : c.Estado == EstadoCita.Cumplido ? "#008000" : "#FF0000", // Colores por estado
                clienteNombre = c.ClienteNombre,
                clienteEmail = c.ClienteEmail,
                servicio = c.Servicio,
                estado = c.Estado.ToString()
            });

            return Json(eventos);
        }
        public IActionResult Calendario()
        {
            return View();
        }
    }
}
