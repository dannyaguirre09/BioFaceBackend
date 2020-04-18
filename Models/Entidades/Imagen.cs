using System;

namespace ApiFace.Models.Entidades
{
	public class Imagen
	{
		public int IdImagen { get; set; }

		public Persona Persona { get; set; }

		public string ImagenPersona { get; set; }

		public DateTime FechaCreacion { get; set; }

		public DateTime FechaModificacion { get; set; }

		public bool Estado { get; set; }

		public int IdUsuario { get; set; }

		public byte[] ImagenPersonaByte { get; set; }
	}
}