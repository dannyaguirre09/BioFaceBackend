using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ApiFace.Models.Dao
{
	public class PersonaDAO
	{
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataReader dr = null;

		public int guardarPersona(Persona objPersona)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("insert into persona(nombre_persona, apellido_persona, direccion_persona, correo_persona,  " +
										"cedula_persona, fecha_nacimiento) values (? ,? ,? ,? , ?, ?)", conexion);

				MySqlParameter parNombre = new MySqlParameter("@nombrePersona", MySqlDbType.VarChar, objPersona.NombrePersona.Length);
				MySqlParameter parApellido = new MySqlParameter("@apellidoPersona", MySqlDbType.VarChar, objPersona.ApellidoPersona.Length);
				MySqlParameter parDireccion = new MySqlParameter("@direccionPersona", MySqlDbType.VarChar, objPersona.DireccionPersona.Length);
				MySqlParameter parCorreo = new MySqlParameter("@correoPersona", MySqlDbType.VarChar, objPersona.CorreoPersona.Length);
				MySqlParameter parCedula = new MySqlParameter("@cedulaPersona", MySqlDbType.VarChar, objPersona.CedulaPersona.Length);
				MySqlParameter parFecha = new MySqlParameter("@fechaPersona", MySqlDbType.Date);

				parNombre.Value = objPersona.NombrePersona;
				parApellido.Value = objPersona.ApellidoPersona;
				parDireccion.Value = objPersona.DireccionPersona;
				parCorreo.Value = objPersona.CorreoPersona;
				parCedula.Value = objPersona.CedulaPersona;
				parFecha.Value = objPersona.FechaNacimiento;

				cmd.Parameters.Add(parNombre);
				cmd.Parameters.Add(parApellido);
				cmd.Parameters.Add(parDireccion);
				cmd.Parameters.Add(parCorreo);
				cmd.Parameters.Add(parCedula);
				cmd.Parameters.Add(parFecha);

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

		public List<Persona> listadoPersonas()
		{
			string query = "select * from persona where estado = true";
			List<Persona> listado = new List<Persona>();
			conexion = Conexion.obtenerInstancia().ConexionBD();
			try
			{
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Persona objPersona = new Persona();
					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();
					objPersona.DireccionPersona = dr["direccion_persona"].ToString();
					objPersona.CorreoPersona = dr["correo_persona"].ToString();
					objPersona.CedulaPersona = dr["cedula_persona"].ToString();
					objPersona.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
					objPersona.IdPersona = Convert.ToInt32(dr["id_persona"]);
					listado.Add(objPersona);
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

		public List<Persona> buscarPersonas(string nombrePersona)
		{
			string query = "select * from persona where estado = true and nombre_persona LIKE '%" + nombrePersona + "%' or apellido_persona like '%" + nombrePersona + "%' and estado = true";
			List<Persona> listado = new List<Persona>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Persona objPersona = new Persona();
					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();
					objPersona.DireccionPersona = dr["direccion_persona"].ToString();
					objPersona.CorreoPersona = dr["correo_persona"].ToString();
					objPersona.CedulaPersona = dr["cedula_persona"].ToString();
					objPersona.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
					objPersona.IdPersona = Convert.ToInt32(dr["id_persona"]);
					listado.Add(objPersona);
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

		public Persona buscarPersona(int idPersona)
		{
			string query = "select * from persona where id_persona = " + idPersona + " and estado = true ";
			Persona objPersona = new Persona();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objPersona.NombrePersona = dr["nombre_persona"].ToString();
					objPersona.ApellidoPersona = dr["apellido_persona"].ToString();
					objPersona.DireccionPersona = dr["direccion_persona"].ToString();
					objPersona.CorreoPersona = dr["correo_persona"].ToString();
					objPersona.CedulaPersona = dr["cedula_persona"].ToString();
					objPersona.FechaNacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]);
					objPersona.IdPersona = Convert.ToInt32(dr["id_persona"]);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objPersona;
		}

		public int actualizarPersona(Persona objPersona)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("update persona set nombre_persona = ? , apellido_persona = ?, " +
									 " direccion_persona = ?, correo_persona = ?, cedula_persona = ?,  " +
									 " fecha_nacimiento = ?, fecha_modificacion = current_timestamp() " +
									"where id_persona = ?", conexion);

				MySqlParameter parNombre = new MySqlParameter("@nombrePersona", MySqlDbType.VarChar, objPersona.NombrePersona.Length);
				MySqlParameter parApellido = new MySqlParameter("@apellidoPersona", MySqlDbType.VarChar, objPersona.ApellidoPersona.Length);
				MySqlParameter parDireccion = new MySqlParameter("@direccionPersona", MySqlDbType.VarChar, objPersona.DireccionPersona.Length);
				MySqlParameter parCorreo = new MySqlParameter("@correoPersona", MySqlDbType.VarChar, objPersona.CorreoPersona.Length);
				MySqlParameter parCedula = new MySqlParameter("@cedulaPersona", MySqlDbType.VarChar, objPersona.CedulaPersona.Length);
				MySqlParameter parFecha = new MySqlParameter("@fechaPersona", MySqlDbType.Date);
				MySqlParameter parId = new MySqlParameter("@idPersona", MySqlDbType.Int32);

				parNombre.Value = objPersona.NombrePersona;
				parApellido.Value = objPersona.ApellidoPersona;
				parDireccion.Value = objPersona.DireccionPersona;
				parCorreo.Value = objPersona.CorreoPersona;
				parCedula.Value = objPersona.CedulaPersona;
				parFecha.Value = objPersona.FechaNacimiento;
				parId.Value = objPersona.IdPersona;

				cmd.Parameters.Add(parNombre);
				cmd.Parameters.Add(parApellido);
				cmd.Parameters.Add(parDireccion);
				cmd.Parameters.Add(parCorreo);
				cmd.Parameters.Add(parCedula);
				cmd.Parameters.Add(parFecha);
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

		public int eliminarPersona(int idPersona)
		{
			ImagenDAO daoImagen = new ImagenDAO();
			RegistroDAO daoRegistro = new RegistroDAO();
			int respuesta = 0;
			conexion = Conexion.obtenerInstancia().ConexionBD();
			cmd = new MySqlCommand("update persona set estado = ?, fecha_modificacion = current_timestamp() " +
									"where id_persona = ?", conexion);

			MySqlParameter parEstado = new MySqlParameter("@estadoPersona", MySqlDbType.Bit);
			MySqlParameter parId = new MySqlParameter("@idPersona", MySqlDbType.Int32);

			parEstado.Value = false;
			parId.Value = idPersona;

			cmd.Parameters.Add(parEstado);
			cmd.Parameters.Add(parId);
			int eliminarImagen = daoImagen.eliminarImagenIdPersona(idPersona);
			int eliminarRegistro = daoRegistro.eliminarRegistroIdPersona(idPersona);

			if (eliminarImagen == 1 && eliminarRegistro == 1)
			{
				conexion.Open();
				int filas = cmd.ExecuteNonQuery();
				conexion.Close();

				if (filas > 0)
					respuesta = 1;
			}


			return respuesta;

		}

		public int obtenerContadorNumeroPersona(int creacion)
		{
			string query;
			if (creacion == 1)
			{
				query = @"select count(*) from persona 
							where estado = true 
							and fecha_creacion BETWEEN CONCAT(CURDATE(), ' 00:00:00')   AND CONCAT(CURDATE(), ' 23:59:59');";
			}
			else
			{
				query = @"select count(*) from persona 
							where estado = false 
							and fecha_modificacion BETWEEN CONCAT(CURDATE(), ' 00:00:00')   AND CONCAT(CURDATE(), ' 23:59:59');";
			}

			int respuesta = -1;

			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					respuesta = Convert.ToInt32(dr[0].ToString());
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


	}
}