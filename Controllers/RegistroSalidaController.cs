using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class RegistroSalidaController : ApiController
	{
		#region Variables
		RegistroDAO dao = null;
		#endregion


		[HttpGet]
		public List<Registro> get()
		{
			dao = new RegistroDAO();
			return dao.obtenerTop5RegistroSalidas();
		}

		[HttpGet]
		public Registro get(int idPersona)
		{
			dao = new RegistroDAO();
			return dao.buscarRegistroSalidaPorId(idPersona);
		}




	}
}
