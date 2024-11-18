using System.ComponentModel.DataAnnotations;

namespace SistemaReservaCitas.Models
{
    public enum EstadoCita
    {
        Pendiente,
        Cumplido,
        Cancelado
    }

    public class Cita
    {
        public int Id { get; set; }

        [Required]
        public string ClienteNombre { get; set; }

        [Required]
        public string ClienteEmail { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        public string Servicio { get; set; }
        [Required]
        public EstadoCita Estado { get; set; } = EstadoCita.Pendiente;
    }
}
