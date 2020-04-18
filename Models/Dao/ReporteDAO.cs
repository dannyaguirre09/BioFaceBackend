using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ApiFace.Models.Dao
{
	public class ReporteDAO
	{

		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataReader dr = null;

		public List<string> obtenerReporteLinealFecha(int idPersona)
		{
			List<string> listado = new List<string>();
			string fecha;

			try
			{
				string query = "select fecha_ingreso from registro_persona where id_persona = " + idPersona + " and estado = true and fecha_salida is null order by id_registro desc limit 10 ;";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					fecha = dr["fecha_ingreso"].ToString();
					fecha = fecha.Replace("0:00:00", "");
					listado.Add(fecha);
				}
			}
			catch (Exception)
			{
				fecha = "-1";
				listado.Add(fecha);
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<string> obtenerReporteLinealHora(int idPersona)
		{
			List<string> listado = new List<string>();
			string hora;

			try
			{
				string query = "select hora_ingreso from registro_persona where id_persona = " + idPersona + " and estado = true and fecha_salida is null order by id_registro desc limit 10 ;";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					hora = dr["hora_ingreso"].ToString();
					hora = hora.Replace(":00", "");
					hora = hora.Replace(":", ".");
					listado.Add(hora);
				}
			}
			catch (Exception)
			{
				hora = "-1";
				listado.Add(hora);
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<string> obtenerReporteLinealFechaSalida(int idPersona)
		{
			List<string> listado = new List<string>();
			string fecha;

			try
			{
				string query = "select fecha_salida from registro_persona where id_persona = " + idPersona + " and estado = true and fecha_ingreso is null order by id_registro desc limit 10 ;";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					fecha = dr["fecha_salida"].ToString();
					fecha = fecha.Replace("0:00:00", "");
					listado.Add(fecha);
				}
			}
			catch (Exception)
			{
				fecha = "-1";
				listado.Add(fecha);
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<string> obtenerReporteLinealHoraSalida(int idPersona)
		{
			List<string> listado = new List<string>();
			string hora;

			try
			{
				string query = "select hora_salida from registro_persona where id_persona = " + idPersona + " and estado = true and fecha_ingreso is null order by id_registro desc limit 10 ;";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					hora = dr["hora_salida"].ToString();
					hora = hora.Replace(":00", "");
					hora = hora.Replace(":", ".");
					listado.Add(hora);
				}
			}
			catch (Exception)
			{
				hora = "-1";
				listado.Add(hora);
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerReporteLinealConteoIngreso(int idPersona)
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = "select count(id_persona) 'contador', fecha_ingreso from registro_persona where id_persona = " + idPersona +
								" and estado = true and fecha_salida is null " +
								" group by(month(fecha_ingreso)); ";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
					objRegistro.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerReporteLinealConteoSalida(int idPersona)
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = "select count(id_persona) 'contador', fecha_salida from registro_persona where id_persona = " + idPersona +
								" and estado = true and fecha_ingreso is null " +
								" group by(month(fecha_salida)); ";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr["fecha_salida"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerReporteConteoIngresoGeneral()
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = "select count(id_persona) 'contador', fecha_ingreso from registro_persona where " +
								" estado = true and fecha_salida is null " +
								" group by(month(fecha_ingreso)); ";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
					objRegistro.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerReporteConteoSalidaGeneral()
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = "select count(id_persona) 'contador', fecha_salida from registro_persona where " +
								" estado = true and fecha_ingreso is null " +
								" group by(month(fecha_salida)); ";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr["fecha_salida"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerPromedioIngresoGeneral()
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(hora_ingreso))) promedio,  fecha_ingreso
								from registro_persona 
								where estado = true and hora_salida is null
								group by month(fecha_ingreso);";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.Promedio = dr[0].ToString();
					objRegistro.Promedio = objRegistro.Promedio.Substring(0, 5);
					objRegistro.Promedio = objRegistro.Promedio.Replace(":", ".");
					objRegistro.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerPromedioSalidaGeneral()
		{
			List<Registro> listado = new List<Registro>();
			Registro objRegistro = null;

			try
			{
				string query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(hora_salida))) promedio,  fecha_salida
								from registro_persona 
								where estado = true and hora_ingreso is null
								group by month(fecha_salida);";
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objRegistro.Promedio = dr[0].ToString();
					objRegistro.Promedio = objRegistro.Promedio.Substring(0, 5);
					objRegistro.Promedio = objRegistro.Promedio.Replace(":", ".");
					objRegistro.FechaSalida = Convert.ToDateTime(dr["fecha_salida"].ToString());
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerTop10PromedioIngreso(int mejor)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string query;

				if (mejor == 1)
				{
					query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(RP.hora_ingreso))) promedio, RP.id_persona, P.nombre_persona, P.apellido_persona
							from registro_persona RP inner join persona P 
							on RP.id_persona = P.id_persona 
							where RP.estado = true and RP.hora_salida is null and P.estado = true
							group by RP.id_persona 
							order by 1 asc
							limit 10;";
				}
				else
				{
					query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(RP.hora_ingreso))) promedio, RP.id_persona, P.nombre_persona, P.apellido_persona
							from registro_persona RP inner join persona P 
							on RP.id_persona = P.id_persona 
							where RP.estado = true and RP.hora_salida is null and P.estado = true
							group by RP.id_persona 
							order by 1 desc
							limit 10;";
				}


				Registro objRegistro = null;
				Persona objPersona = null;

				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr[2].ToString();
					objPersona.ApellidoPersona = dr[3].ToString();
					objPersona.IdPersona = Convert.ToInt32(dr[1].ToString());
					objRegistro.Promedio = dr[0].ToString();
					objRegistro.Promedio = objRegistro.Promedio.Substring(0, 5);
					objRegistro.Promedio = objRegistro.Promedio.Replace(":", ".");
					objRegistro.Persona = objPersona;
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

		public List<Registro> obtenerTop10PromedioSalida(int mejor)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string query;

				if (mejor == 1)
				{
					query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(RP.hora_salida))) promedio, RP.id_persona, P.nombre_persona, P.apellido_persona
							from registro_persona RP inner join persona P 
							on RP.id_persona = P.id_persona 
							where RP.estado = true and RP.hora_ingreso is null and P.estado = true
							group by RP.id_persona 
							order by 1 desc 
							limit 10;";
				}
				else
				{
					query = @"SELECT SEC_TO_TIME(AVG(TIME_TO_SEC(RP.hora_salida))) promedio, RP.id_persona, P.nombre_persona, P.apellido_persona
							from registro_persona RP inner join persona P 
							on RP.id_persona = P.id_persona 
							where RP.estado = true and RP.hora_ingreso is null and P.estado = true
							group by RP.id_persona 
							order by 1 asc 
							limit 10;";
				}


				Registro objRegistro = null;
				Persona objPersona = null;

				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr[2].ToString();
					objPersona.ApellidoPersona = dr[3].ToString();
					objPersona.IdPersona = Convert.ToInt32(dr[1].ToString());
					objRegistro.Promedio = dr[0].ToString();
					objRegistro.Promedio = objRegistro.Promedio.Substring(0, 5);
					objRegistro.Promedio = objRegistro.Promedio.Replace(":", ".");
					objRegistro.Persona = objPersona;
					listado.Add(objRegistro);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return listado;
		}

	}
}