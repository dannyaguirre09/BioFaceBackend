using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ApiFace.Models.Dao
{
	public class UsuarioDAO
	{
		//Variables
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataReader dr = null;

		//Métodos
		public int crearUsuario(Usuario objUsuario)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("insert into usuario(nombre_usuario, apellido_usuario, correo_usuario, username_usuario,  " +
										"password_usuario, es_administrador) values (? ,? ,? ,? , ?, ?)", conexion);


				MySqlParameter parNombre = new MySqlParameter("@nombreUsuario", MySqlDbType.VarChar, objUsuario.NombreUsuario.Length);
				MySqlParameter parApellido = new MySqlParameter("@apellidoUsuario", MySqlDbType.VarChar, objUsuario.ApellidoUsuario.Length);
				MySqlParameter parCorreo = new MySqlParameter("@correoUsuario", MySqlDbType.VarChar, objUsuario.CorreoUsuario.Length);
				MySqlParameter parUsername = new MySqlParameter("@usernameUsuario", MySqlDbType.VarChar, objUsuario.UsernameUsuario.Length);
				MySqlParameter parPassword = new MySqlParameter("@passwordUsuario", MySqlDbType.VarChar, objUsuario.PasswordUsuario.Length);
				MySqlParameter parAdministrador = new MySqlParameter("@AdministradorUsuario", MySqlDbType.Bit);

				parNombre.Value = objUsuario.NombreUsuario;
				parApellido.Value = objUsuario.ApellidoUsuario;
				parCorreo.Value = objUsuario.CorreoUsuario;
				parUsername.Value = objUsuario.UsernameUsuario;
				parPassword.Value = Util.Encriptacion.encriptar(objUsuario.PasswordUsuario);
				parAdministrador.Value = objUsuario.EsAdministrador;

				cmd.Parameters.Add(parNombre);
				cmd.Parameters.Add(parApellido);
				cmd.Parameters.Add(parCorreo);
				cmd.Parameters.Add(parUsername);
				cmd.Parameters.Add(parPassword);
				cmd.Parameters.Add(parAdministrador);

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

		public List<Usuario> listadoUsuarios()
		{
			string query = "select * from usuario where estado = true";
			List<Usuario> listado = new List<Usuario>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Usuario objUsuario = new Usuario();
					objUsuario.IdUsuario = Convert.ToInt32(dr["id_usuario"].ToString());
					objUsuario.NombreUsuario = dr["nombre_usuario"].ToString();
					objUsuario.ApellidoUsuario = dr["apellido_usuario"].ToString();
					objUsuario.CorreoUsuario = dr["correo_usuario"].ToString();
					objUsuario.UsernameUsuario = dr["username_usuario"].ToString();
					objUsuario.EsAdministrador = Convert.ToBoolean(dr["es_administrador"].ToString());
					listado.Add(objUsuario);
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

		public List<Usuario> buscarUsuarios(string nombreUsuario)
		{
			string query = "select * from usuario where estado = true and nombre_usuario LIKE '%" + nombreUsuario + "%' or apellido_usuario like '%" + nombreUsuario + "%' and estado = true";
			List<Usuario> listado = new List<Usuario>();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Usuario objUsuario = new Usuario();
					objUsuario.IdUsuario = Convert.ToInt32(dr["id_usuario"].ToString());
					objUsuario.NombreUsuario = dr["nombre_usuario"].ToString();
					objUsuario.ApellidoUsuario = dr["apellido_usuario"].ToString();
					objUsuario.CorreoUsuario = dr["correo_usuario"].ToString();
					objUsuario.UsernameUsuario = dr["username_usuario"].ToString();
					objUsuario.EsAdministrador = Convert.ToBoolean(dr["es_administrador"].ToString());
					listado.Add(objUsuario);
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

		public Usuario buscarUsuario(int idUsuario)
		{
			string query = "select * from usuario where id_usuario = " + idUsuario + " and estado = true ";
			Usuario objUsuario = new Usuario();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objUsuario.IdUsuario = Convert.ToInt32(dr["id_usuario"].ToString());
					objUsuario.NombreUsuario = dr["nombre_usuario"].ToString();
					objUsuario.ApellidoUsuario = dr["apellido_usuario"].ToString();
					objUsuario.CorreoUsuario = dr["correo_usuario"].ToString();
					objUsuario.UsernameUsuario = dr["username_usuario"].ToString();
					objUsuario.EsAdministrador = Convert.ToBoolean(dr["es_administrador"].ToString());
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objUsuario;
		}

		public int actualizarUsuario(Usuario objUsuario)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();

				if (objUsuario.UsernameUsuario == null)
				{
					cmd = new MySqlCommand("update usuario set nombre_usuario = ? , apellido_usuario = ?, " +
									 " correo_usuario = ?, es_administrador = ?,  " +
									 " fecha_modificacion = current_timestamp() " +
									"where id_usuario = ?", conexion);
				}
				else
				{
					cmd = new MySqlCommand("update usuario set nombre_usuario = ? , apellido_usuario = ?, " +
									 " correo_usuario = ?, es_administrador = ?, username_usuario = '" + objUsuario.UsernameUsuario + "', " +
									 " fecha_modificacion = current_timestamp() " +
									"where id_usuario = ?", conexion);
				}


				MySqlParameter parNombre = new MySqlParameter("@nombreUsuario", MySqlDbType.VarChar, objUsuario.NombreUsuario.Length);
				MySqlParameter parApellido = new MySqlParameter("@apellidoUsuario", MySqlDbType.VarChar, objUsuario.ApellidoUsuario.Length);
				MySqlParameter parCorreo = new MySqlParameter("@correoUsuario", MySqlDbType.VarChar, objUsuario.CorreoUsuario.Length);
				MySqlParameter parAdministrador = new MySqlParameter("@administradorUsuario", MySqlDbType.Bit);
				MySqlParameter parId = new MySqlParameter("@idUsuario", MySqlDbType.Int32);



				parNombre.Value = objUsuario.NombreUsuario;
				parApellido.Value = objUsuario.ApellidoUsuario;
				parCorreo.Value = objUsuario.CorreoUsuario;
				parAdministrador.Value = objUsuario.EsAdministrador;
				parId.Value = objUsuario.IdUsuario;

				cmd.Parameters.Add(parNombre);
				cmd.Parameters.Add(parApellido);
				cmd.Parameters.Add(parCorreo);
				cmd.Parameters.Add(parAdministrador);
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

		public int eliminarUsuario(int idUsuario)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("update usuario set estado = ? " +
										"where id_usuario = ?", conexion);

				MySqlParameter parEstado = new MySqlParameter("@estadoPersona", MySqlDbType.Bit);
				MySqlParameter parId = new MySqlParameter("@idPersona", MySqlDbType.Int32);

				parEstado.Value = false;
				parId.Value = idUsuario;

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

		public int obtenerContadorNumeroUsuarios()
		{
			string query = @"select count(*) from usuario 
							where estado = true 
							and fecha_creacion BETWEEN CONCAT(CURDATE(), ' 00:00:00')   AND CONCAT(CURDATE(), ' 23:59:59');";
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

		public int actualizarPasswordUsuario(Usuario objUsuario)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("update usuario set password_usuario = ? " +
										"where id_usuario = ?", conexion);

				MySqlParameter parPassword = new MySqlParameter("@PassPersona", MySqlDbType.VarChar);
				MySqlParameter parId = new MySqlParameter("@idUsuario", MySqlDbType.Int32);

				parPassword.Value = Util.Encriptacion.encriptar(objUsuario.PasswordUsuario);
				parId.Value = objUsuario.IdUsuario;

				cmd.Parameters.Add(parPassword);
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


	}
}