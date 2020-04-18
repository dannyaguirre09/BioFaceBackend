using System;

namespace ApiFace.Models.Entidades
{
	public class Registro
	{
		public int IdRegistro { get; set; }

		public Persona Persona { get; set; }

		public DateTime HoraIngreso { get; set; }

		public DateTime HoraSalida { get; set; }

		public DateTime FechaIngreso { get; set; }

		public DateTime FechaSalida { get; set; }

		public DateTime FechaCreacion { get; set; }

		public DateTime FechaModificacion { get; set; }

		public bool Estado { get; set; }

		public int IdUsuario { get; set; }

		public int tipo { get; set; }

		public string Promedio { get; set; }
	}
}