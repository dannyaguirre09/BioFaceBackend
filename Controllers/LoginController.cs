using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class LoginController : ApiController
	{
		[HttpPost]
		public int login([FromBody] Usuario model)
		{
			LoginDAO dao = new LoginDAO();
			return dao.iniciarSesion(model);
		}

		[HttpPut]
		public int actualizar([FromBody] Usuario model)
		{
			UsuarioDAO dao = new UsuarioDAO();
			return dao.actualizarPasswordUsuario(model);
		}

	}
}
