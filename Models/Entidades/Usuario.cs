using System;

namespace ApiFace.Models.Entidades
{
	public class Usuario
	{
		public int IdUsuario { get; set; }

		public string NombreUsuario { get; set; }

		public string ApellidoUsuario { get; set; }

		public string CorreoUsuario { get; set; }

		public string UsernameUsuario { get; set; }

		public string PasswordUsuario { get; set; }

		public bool EsAdministrador { get; set; }

		public DateTime FechaCreacion { get; set; }

		public DateTime FechaModificacion { get; set; }

		public bool Estado { get; set; }

	}
}