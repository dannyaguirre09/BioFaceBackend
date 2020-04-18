using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class InicioController : ApiController
	{
		RegistroDAO daoRegistro = null;
		PersonaDAO daoPersona = null;
		UsuarioDAO daoUsuario = null;

		[HttpGet]
		public List<Registro> obtenerTop5Registros()
		{
			daoRegistro = new RegistroDAO();
			return daoRegistro.obtenerTop5Registros();
		}

		[HttpGet]
		public int obtenerNumeroPersonas(int creacion)
		{
			daoPersona = new PersonaDAO();
			return daoPersona.obtenerContadorNumeroPersona(creacion);
		}

		[HttpGet]
		public int obtenerNumeroUsuarios(string fechaActual)
		{
			daoUsuario = new UsuarioDAO();
			return daoUsuario.obtenerContadorNumeroUsuarios();
		}

	}
}
