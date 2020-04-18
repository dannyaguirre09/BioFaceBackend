using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ApiFace.Models.Dao
{
	public class RegistroDAO
	{
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataReader dr = null;

		public int[] guardarRegistroIngreso(Registro objRegistro)
		{
			int[] respuesta = { 0, 0 };
			conexion = Conexion.obtenerInstancia().ConexionBD();
			try
			{
				cmd = new MySqlCommand("insert into registro_persona (id_persona, hora_ingreso, fecha_ingreso) values (?, ?, ?)", conexion);

				MySqlParameter parIdPersona = new MySqlParameter("@idPersona", MySqlDbType.Int32, objRegistro.Persona.IdPersona);
				MySqlParameter parHoraIngreso = new MySqlParameter("@horaIngreso", MySqlDbType.VarChar, 100);
				MySqlParameter parFechaIngreso = new MySqlParameter("@fechaIngreso", MySqlDbType.Date, 100);

				parIdPersona.Value = objRegistro.Persona.IdPersona;
				parHoraIngreso.Value = objRegistro.HoraIngreso.ToShortTimeString();
				parFechaIngreso.Value = objRegistro.FechaIngreso;

				cmd.Parameters.Add(parIdPersona);
				cmd.Parameters.Add(parHoraIngreso);
				cmd.Parameters.Add(parFechaIngreso);

				conexion.Open();
				int filas = cmd.ExecuteNonQuery();

				if (filas > 0)
				{
					respuesta[0] = 1;
					respuesta[1] = objRegistro.Persona.IdPersona;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}


			return respuesta;

		}

		public int[] guardarRegistroSalida(Registro objRegistro)
		{
			int[] respuesta = { 0, 0 };
			conexion = Conexion.obtenerInstancia().ConexionBD();
			try
			{
				cmd = new MySqlCommand("insert into registro_persona (id_persona, hora_salida, fecha_salida) values (?, ?, ?)", conexion);

				MySqlParameter parIdPersona = new MySqlParameter("@idPersona", MySqlDbType.Int32, objRegistro.Persona.IdPersona);
				MySqlParameter parHoraSalida = new MySqlParameter("@horaSalida", MySqlDbType.VarChar, 100);
				MySqlParameter parFechaSalida = new MySqlParameter("@fechaSalida", MySqlDbType.Date, 100);

				parIdPersona.Value = objRegistro.Persona.IdPersona;
				parHoraSalida.Value = objRegistro.HoraSalida.ToShortTimeString();
				parFechaSalida.Value = objRegistro.FechaSalida;

				cmd.Parameters.Add(parIdPersona);
				cmd.Parameters.Add(parHoraSalida);
				cmd.Parameters.Add(parFechaSalida);

				conexion.Open();
				int filas = cmd.ExecuteNonQuery();

				if (filas > 0)
				{
					respuesta[0] = 1;
					respuesta[1] = objRegistro.Persona.IdPersona;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}


			return respuesta;

		}

		public Registro otenerRegistroPorPersona(int idPersona)
		{
			string query = "select id_registro from registro_persona where id_persona = " + idPersona + " and estado = true limit 1;";
			Registro objRegistro = new Registro();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objRegistro;
		}

		public int eliminarRegistroIdRegistro(int idRegistro)
		{
			int respuesta = 0;

			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("update registro_persona set estado = ? " +
										"where id_registro = ?", conexion);

				MySqlParameter parEstado = new MySqlParameter("@estadoRegistro", MySqlDbType.Bit);
				MySqlParameter parId = new MySqlParameter("@idRegistro", MySqlDbType.Int32);

				parEstado.Value = false;
				parId.Value = idRegistro;

				cmd.Parameters.Add(parEstado);
				cmd.Parameters.Add(parId);

				conexion.Open();
				int filas = cmd.ExecuteNonQuery();

				if (filas > 0)
					respuesta = 1;
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}


			return respuesta;
		}

		public int eliminarRegistroIdPersona(int idPersona)
		{
			Registro objRegistro = otenerRegistroPorPersona(idPersona);
			int respuesta = 0;

			if (objRegistro.IdRegistro != 0)
			{

				try
				{
					conexion = Conexion.obtenerInstancia().ConexionBD();
					cmd = new MySqlCommand("update registro_persona set estado = ? " +
											"where id_persona = ?", conexion);

					MySqlParameter parEstado = new MySqlParameter("@estadoRegistro", MySqlDbType.Bit);
					MySqlParameter parId = new MySqlParameter("@idPersona", MySqlDbType.Int32);

					parEstado.Value = false;
					parId.Value = idPersona;

					cmd.Parameters.Add(parEstado);
					cmd.Parameters.Add(parId);

					conexion.Open();
					int filas = cmd.ExecuteNonQuery();


					if (filas > 0)
						respuesta = 1;
				}
				catch (Exception)
				{
				}
				finally
				{
					conexion.Close();
				}
			}
			else
			{
				respuesta = 1;
			}


			return respuesta;
		}

		public Registro buscarRegistroIngresoPorId(int idPersona)
		{
			string query = "select RP.id_registro, P.nombre_persona, P.apellido_persona, RP.hora_ingreso, RP.fecha_ingreso " +
							" from registro_persona RP inner " +
							" join persona P " +
							" on RP.id_persona = P.id_persona " +
							" where P.id_persona = " + idPersona + " and P.estado = true and RP.estado = true " +
							" order by RP.id_registro desc " +
							" limit 1;";
			Registro objRegistro = null;
			Persona objPersona = null;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();

					objRegistro.Persona = objPersona;
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraIngreso = Convert.ToDateTime(dr["hora_ingreso"].ToString());
					objRegistro.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
				}
			}
			catch (Exception)
			{

			}
			finally
			{
				conexion.Close();
			}

			return objRegistro;
		}

		public Registro buscarRegistroSalidaPorId(int idPersona)
		{
			string query = "select RP.id_registro, P.nombre_persona, P.apellido_persona, RP.hora_salida, RP.fecha_salida " +
							" from registro_persona RP inner " +
							" join persona P " +
							" on RP.id_persona = P.id_persona " +
							" where P.id_persona = " + idPersona + " and P.estado = true and RP.estado = true " +
							" order by RP.id_registro desc " +
							" limit 1;";
			Registro objRegistro = null;
			Persona objPersona = null;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();

					objRegistro.Persona = objPersona;
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraSalida = Convert.ToDateTime(dr["hora_salida"].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr["fecha_salida"].ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objRegistro;
		}

		public List<Registro> obtenerTop5RegistroIngresos()
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				string query = @"select RP.id_registro, P.nombre_persona, P.apellido_persona, P.cedula_persona, RP.hora_ingreso, RP.fecha_ingreso 
							from registro_persona RP 
							inner join persona P on P.id_persona = RP.id_persona 
							where RP.estado = true and P.estado = true and RP.fecha_salida is null order by 1 desc limit 5";

				Registro objRegistro = null;
				Persona objPersona = null;

				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();
					objPersona.CedulaPersona = dr["cedula_persona"].ToString();

					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraIngreso = Convert.ToDateTime(dr["hora_ingreso"].ToString());
					objRegistro.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
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

		public List<Registro> obtenerTop5RegistroSalidas()
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				string query = @"select RP.id_registro, P.nombre_persona, P.apellido_persona, P.cedula_persona, RP.hora_salida, RP.fecha_salida 
							from registro_persona RP 
							inner join persona P on P.id_persona = RP.id_persona 
							where RP.estado = true and P.estado = true and RP.fecha_ingreso is null order by 1 desc limit 5";

				Registro objRegistro = null;
				Persona objPersona = null;
				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();
					objPersona.CedulaPersona = dr["cedula_persona"].ToString();

					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraSalida = Convert.ToDateTime(dr["hora_salida"].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr["fecha_salida"].ToString());
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

		public List<Registro> obtenerRegistrosPersona(Registro modelo)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string fechaInicio = modelo.FechaIngreso.ToString("yyyy-MM-dd 00:00:00");
				string fechaFin = modelo.FechaSalida.ToString("yyyy-MM-dd 23:59:59");

				string query = "select RP.id_registro, P.nombre_persona, P.apellido_persona, ifnull(RP.hora_ingreso, '00:00:00'), ifnull(RP.fecha_ingreso, '00:00:00'), " +
							" ifnull(RP.hora_salida,'00:00:00'), ifnull(RP.fecha_salida, '00:00:00')   " +
							" from registro_persona as RP inner join persona P " +
							"on RP.id_persona = P.id_persona where RP.id_persona = " + modelo.Persona.IdPersona + " and RP.estado = true " +
							" and RP.fecha_creacion " +
								" BETWEEN '" + fechaInicio + "' AND '" + fechaFin + "' " +
							" order by RP.fecha_creacion desc; ";

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

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();

					objRegistro.tipo = 1;
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraIngreso = Convert.ToDateTime(dr[3].ToString());
					if (objRegistro.HoraIngreso.Minute == 0 && objRegistro.HoraIngreso.Hour == 0 && objRegistro.HoraIngreso.Second == 0)
						objRegistro.tipo = 0;
					objRegistro.FechaIngreso = Convert.ToDateTime(dr[4].ToString());
					objRegistro.HoraSalida = Convert.ToDateTime(dr[5].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr[6].ToString());
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

		public List<Registro> obtenerTop5Registros()
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				string query = "select RP.id_registro, P.nombre_persona, P.apellido_persona, ifnull(RP.hora_ingreso, '00:00:00'), " +
								" ifnull(RP.fecha_ingreso, '00:00:00'), ifnull(RP.fecha_salida, '00:00:00'), ifnull(RP.hora_salida, '00:00:00') " +
								"from registro_persona RP " +
								" inner join persona P on P.id_persona = RP.id_persona  " +
								" where RP.estado = true and P.estado = true order by 1 desc limit 5";

				Registro objRegistro = null;
				Persona objPersona = null;

				cmd = new MySqlCommand(query, conexion);

				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objRegistro = new Registro();
					objPersona = new Persona();

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();

					objRegistro.tipo = 1;
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraIngreso = Convert.ToDateTime(dr[3].ToString());
					if (objRegistro.HoraIngreso.Minute == 0 && objRegistro.HoraIngreso.Hour == 0 && objRegistro.HoraIngreso.Second == 0)
						objRegistro.tipo = 0;
					objRegistro.FechaIngreso = Convert.ToDateTime(dr[4].ToString());
					objRegistro.HoraSalida = Convert.ToDateTime(dr[6].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr[5].ToString());
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

		public List<Registro> obtenerRegistrosPorFecha(Registro modelo)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string fechaInicio = modelo.FechaIngreso.ToString("yyyy-MM-dd 00:00:00");
				string fechaFin = modelo.FechaSalida.ToString("yyyy-MM-dd 23:59:59");

				string query = "select RP.id_registro, P.nombre_persona, P.apellido_persona, ifnull(RP.hora_ingreso, '00:00:00'), ifnull(RP.fecha_ingreso, '00:00:00'), " +
							" ifnull(RP.hora_salida,'00:00:00'), ifnull(RP.fecha_salida, '00:00:00')   " +
							" from registro_persona as RP inner join persona P " +
							"on RP.id_persona = P.id_persona where RP.estado = true " +
							" and RP.fecha_creacion " +
								" BETWEEN '" + fechaInicio + "' AND '" + fechaFin + "' " +
							" order by RP.fecha_creacion desc; ";

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

					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();

					objRegistro.tipo = 1;
					objRegistro.IdRegistro = Convert.ToInt32(dr["id_registro"].ToString());
					objRegistro.HoraIngreso = Convert.ToDateTime(dr[3].ToString());
					if (objRegistro.HoraIngreso.Minute == 0 && objRegistro.HoraIngreso.Hour == 0 && objRegistro.HoraIngreso.Second == 0)
						objRegistro.tipo = 0;
					objRegistro.FechaIngreso = Convert.ToDateTime(dr[4].ToString());
					objRegistro.HoraSalida = Convert.ToDateTime(dr[5].ToString());
					objRegistro.FechaSalida = Convert.ToDateTime(dr[6].ToString());
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

		public List<Registro> obtenerTop10Ingreso(int mayor)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string query;

				if (mayor == 1)
				{
					query = @"select count(rp.id_registro), rp.id_persona, p.nombre_persona, p.apellido_persona 
							from registro_persona rp inner join persona p
							on rp.id_persona = p.id_persona
							where rp.estado = true and p.estado = true and rp.hora_salida is null
							group by rp.id_persona
							order by 1 desc
							limit 10;";
				}
				else
				{
					query = @"select count(rp.id_registro), rp.id_persona, p.nombre_persona, p.apellido_persona 
							from registro_persona rp inner join persona p
							on rp.id_persona = p.id_persona
							where rp.estado = true and p.estado = true and rp.hora_salida is null
							group by rp.id_persona
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
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
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

		public List<Registro> obtenerTop10Salidas(int mayor)
		{
			List<Registro> listado = new List<Registro>();
			try
			{
				string query;

				if (mayor == 1)
				{
					query = @"select count(rp.id_registro), rp.id_persona, p.nombre_persona, p.apellido_persona 
							from registro_persona rp inner join persona p
							on rp.id_persona = p.id_persona
							where rp.estado = true and p.estado = true and rp.hora_ingreso is null
							group by rp.id_persona
							order by 1 desc
							limit 10;";
				}
				else
				{
					query = @"select count(rp.id_registro), rp.id_persona, p.nombre_persona, p.apellido_persona 
							from registro_persona rp inner join persona p
							on rp.id_persona = p.id_persona
							where rp.estado = true and p.estado = true and rp.hora_ingreso is null
							group by rp.id_persona
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
					objRegistro.IdRegistro = Convert.ToInt32(dr[0].ToString());
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