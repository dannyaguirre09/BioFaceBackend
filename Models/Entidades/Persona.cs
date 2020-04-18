using System;

namespace ApiFace.Models.Entidades
{
	public class Persona
	{
		public int IdPersona { get; set; }

		public string NombrePersona { get; set; }

		public string ApellidoPersona { get; set; }

		public string DireccionPersona { get; set; }

		public string CorreoPersona { get; set; }

		public string CedulaPersona { get; set; }

		public string NombreUsuario { get; set; }

		public DateTime FechaNacimiento { get; set; }

		public DateTime FechaCreacion { get; set; }

		public DateTime FechaModificacion { get; set; }

		public bool Estado { get; set; }

		public int IdEstado { get; set; }

	}
}