using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class ReporteController : ApiController
	{
		ReporteDAO dao = null;
		RegistroDAO daoRegistro = null;

		[HttpPost]
		public List<Registro> obtenerRegistroPersona([FromBody] Registro modelo)
		{
			daoRegistro = new RegistroDAO();
			return daoRegistro.obtenerRegistrosPersona(modelo);
		}

		[HttpGet]
		public List<string> obtenerReporteLinealFecha(int idPersona)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealFecha(idPersona);
		}

		[HttpGet]
		public List<string> obtenerReporteLinealHora(int idPersonaHora)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealHora(idPersonaHora);
		}

		[HttpGet]
		public List<string> obtenerReporteLinealFechaSalida(int idPersonaFechaSalida)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealFechaSalida(idPersonaFechaSalida);
		}

		[HttpGet]
		public List<string> obtenerReporteLinealHoraSalida(int idPersonaHoraSalida)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealHoraSalida(idPersonaHoraSalida);
		}

		[HttpGet]
		public List<Registro> obtenerReporteLinealConteoIngreso(int idPersonaConteoIngreso)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealConteoIngreso(idPersonaConteoIngreso);
		}

		[HttpGet]
		public List<Registro> obtenerReporteLinealConteoSalida(int idPersonaConteoSalida)
		{
			dao = new ReporteDAO();
			return dao.obtenerReporteLinealConteoSalida(idPersonaConteoSalida);
		}

	}
}
