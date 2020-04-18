using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class PersonaController : ApiController
	{

		PersonaDAO dao;

		[HttpPost]
		public int post([FromBody] Persona model)
		{
			dao = new PersonaDAO();
			return dao.guardarPersona(model);
		}

		[HttpGet]
		public List<Persona> get()
		{
			dao = new PersonaDAO();
			return dao.listadoPersonas();
		}

		[HttpGet]
		public List<Persona> get(string nombrePersona)
		{
			dao = new PersonaDAO();
			return dao.buscarPersonas(nombrePersona);
		}

		[HttpGet]
		public Persona get(int idPersona)
		{
			dao = new PersonaDAO();
			return dao.buscarPersona(idPersona);
		}

		[HttpPut]
		public int put([FromBody] Persona model)
		{
			dao = new PersonaDAO();
			return dao.actualizarPersona(model);
		}

		[HttpDelete]
		public int delete(int idPersona)
		{
			dao = new PersonaDAO();
			return dao.eliminarPersona(idPersona);
		}

	}
}
