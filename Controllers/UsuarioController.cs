using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class UsuarioController : ApiController
	{
		UsuarioDAO dao;

		[HttpPost]
		public int post([FromBody] Usuario model)
		{
			dao = new UsuarioDAO();
			return dao.crearUsuario(model);
		}

		[HttpGet]
		public List<Usuario> get()
		{
			dao = new UsuarioDAO();
			return dao.listadoUsuarios();
		}

		[HttpGet]
		public List<Usuario> get(string nombreUsuario)
		{
			dao = new UsuarioDAO();
			return dao.buscarUsuarios(nombreUsuario);
		}

		[HttpGet]
		public Usuario get(int idUsuario)
		{
			dao = new UsuarioDAO();
			return dao.buscarUsuario(idUsuario);
		}

		[HttpPut]
		public int put([FromBody] Usuario model)
		{
			dao = new UsuarioDAO();
			return dao.actualizarUsuario(model);
		}

		[HttpDelete]
		public int delete(int idUsuario)
		{
			dao = new UsuarioDAO();
			return dao.eliminarUsuario(idUsuario);
		}
	}
}
