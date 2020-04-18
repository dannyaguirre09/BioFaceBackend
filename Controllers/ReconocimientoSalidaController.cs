using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class ReconocimientoSalidaController : ApiController
	{
		ReconocimientoDAO dao = null;

		[HttpPost]
		public int[] Post([FromBody]Imagen model)
		{
			dao = new ReconocimientoDAO();
			return dao.deteccion(model, false);
		}


	}
}
