using ApiFace.Models.Dao;
using ApiFace.Models.Entidades;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiFace.Controllers
{
	[EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
	public class ReporteGeneralController : ApiController
	{
		ReporteDAO dao = null;
		RegistroDAO daoRegistro = null;

		[HttpGet]
		public List<Registro> obtenerTop10Ingreso(int mayorAMenorIngresos)
		{
			daoRegistro = new RegistroDAO();
			return daoRegistro.obtenerTop10Ingreso(mayorAMenorIngresos);
		}

		[HttpGet]
		public List<Registro> obtenerTop10Salidas(int mayorAMenorSalidas)
		{
			daoRegistro = new RegistroDAO();
			return daoRegistro.obtenerTop10Salidas(mayorAMenorSalidas);
		}

		[HttpGet]
		public List<Registro> obtenerPromedioSalidaGeneral(int esEntrada)
		{
			dao = new ReporteDAO();
			if (esEntrada == 1)
				return dao.obtenerPromedioIngresoGeneral();
			else
				return dao.obtenerPromedioSalidaGeneral();
		}

		[HttpGet]
		public List<Registro> obtenerReporteConteoGeneral(int entrada)
		{
			dao = new ReporteDAO();
			if (entrada == 1)
				return dao.obtenerReporteConteoIngresoGeneral();
			else
				return dao.obtenerReporteConteoSalidaGeneral();
		}

		[HttpGet]
		public List<Registro> obtenerTop10PromedioIngreso(int mejor)
		{
			dao = new ReporteDAO();
			return dao.obtenerTop10PromedioIngreso(mejor);
		}

		[HttpGet]
		public List<Registro> obtenerTop10PromedioSalida(int mejorPromedioSalidas)
		{
			dao = new ReporteDAO();
			return dao.obtenerTop10PromedioSalida(mejorPromedioSalidas);
		}

		[HttpPost]
		public List<Registro> obtenerRegistroPersona([FromBody] Registro modelo)
		{
			daoRegistro = new RegistroDAO();
			return daoRegistro.obtenerRegistrosPorFecha(modelo);
		}
	}
}
