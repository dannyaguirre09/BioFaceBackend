using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class ImagenController : ApiController
	{
		ImagenDAO dao = null;

		[HttpPost]
		public int post([FromBody] Imagen model)
		{
			dao = new ImagenDAO();
			return dao.guardarImagen(model);
		}

		[HttpGet]
		public List<Imagen> buscarImagen(int idPersona)
		{
			dao = new ImagenDAO();
			return dao.buscarImagen(idPersona);
		}

		[HttpGet]
		public Imagen obtenerPersona(int idPersonaImagen)
		{
			dao = new ImagenDAO();
			return dao.otenerImagenPersona(idPersonaImagen);
		}

		[HttpGet]
		public Imagen obtenerImagen(int idImagen)
		{
			dao = new ImagenDAO();
			return dao.otenerImagen(idImagen);
		}

		[HttpDelete]
		public int eliminarImagen(int idImagen)
		{
			dao = new ImagenDAO();
			return dao.eliminarImagen(idImagen);
		}

	}
}
