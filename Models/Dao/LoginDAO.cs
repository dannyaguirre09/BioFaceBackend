using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;

namespace ApiFace.Models.Dao
{
	public class LoginDAO
	{
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataReader dr = null;

		public int iniciarSesion(Usuario objUser)
		{
			string pass = Encriptacion.encriptar(objUser.PasswordUsuario);
			string query = "select * from usuario where username_usuario = '" + objUser.UsernameUsuario + "' and password_usuario = '" + pass + "' and estado = true ";
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					respuesta = Convert.ToInt32(dr["id_usuario"].ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				conexion.Close();
			}
			return respuesta;
		}

	}
}