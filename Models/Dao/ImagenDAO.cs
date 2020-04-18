using ApiFace.Models.Entidades;
using ApiFace.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ApiFace.Models.Dao
{
	public class ImagenDAO
	{
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataAdapter da = null;
		Transformaciones util = null;
		MySqlDataReader dr = null;

		public int guardarImagen(Imagen objImagen)
		{
			util = new Transformaciones();
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("insert into imagen (id_persona, imagen) values (?, ?)", conexion);

				MySqlParameter parIdPersona = new MySqlParameter("@idPersona", MySqlDbType.Int32, objImagen.Persona.IdPersona);
				MySqlParameter parImagen = new MySqlParameter("@imagenPersona", MySqlDbType.VarBinary, objImagen.ImagenPersona.Length);

				parIdPersona.Value = objImagen.Persona.IdPersona;
				parImagen.Value = util.convertToByte(objImagen.ImagenPersona);

				cmd.Parameters.Add(parIdPersona);
				cmd.Parameters.Add(parImagen);

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

		public List<Imagen> buscarImagen(int idPersona)
		{
			string query = "select * from imagen where estado = true and id_persona = " + idPersona;
			List<Imagen> listado = new List<Imagen>();
			conexion = Conexion.obtenerInstancia().ConexionBD();
			try
			{
				cmd = new MySqlCommand(query, conexion);
				da = new MySqlDataAdapter(cmd);
				conexion.Open();
				DataTable dt = new DataTable();
				da.Fill(dt);
				int cont = dt.Rows.Count;

				for (int i = 0; i < cont; i++)
				{
					Imagen objImagen = new Imagen();
					objImagen.IdImagen = Convert.ToInt32(dt.Rows[i]["id_imagen"].ToString());
					objImagen.ImagenPersonaByte = (byte[])dt.Rows[i]["imagen"];
					objImagen.ImagenPersona = Convert.ToBase64String(objImagen.ImagenPersonaByte);
					listado.Add(objImagen);
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

		public Imagen otenerImagenPersona(int idPersona)
		{
			string query = "select imagen from imagen where id_persona = " + idPersona + " and estado = true limit 1;";
			Imagen objImagen = new Imagen();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objImagen.ImagenPersonaByte = (byte[])dr["imagen"];
					objImagen.ImagenPersona = Convert.ToBase64String(objImagen.ImagenPersonaByte);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objImagen;
		}

		public Imagen otenerImagen(int idImagen)
		{
			string query = "select imagen from imagen where id_imagen = " + idImagen + " and estado = true;";
			Imagen objImagen = new Imagen();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand(query, conexion);
				conexion.Open();
				dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					objImagen.ImagenPersonaByte = (byte[])dr["imagen"];
					objImagen.ImagenPersona = Convert.ToBase64String(objImagen.ImagenPersonaByte);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}

			return objImagen;
		}

		public int eliminarImagen(int idImagen)
		{
			int respuesta = 0;
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				cmd = new MySqlCommand("update imagen set estado = ? " +
										"where id_imagen = ?", conexion);

				MySqlParameter parEstado = new MySqlParameter("@estadoImagen", MySqlDbType.Bit);
				MySqlParameter parId = new MySqlParameter("@idImagen", MySqlDbType.Int32);

				parEstado.Value = false;
				parId.Value = idImagen;

				cmd.Parameters.Add(parEstado);
				cmd.Parameters.Add(parId);

				conexion.Open();
				int filas = cmd.ExecuteNonQuery();

				if (filas > 0)
					respuesta = 1;


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

		public int eliminarImagenIdPersona(int idPersona)
		{
			Imagen objImagen = otenerImagenPersona(idPersona);
			int respuesta = 0;

			if (objImagen.ImagenPersona != null)
			{

				try
				{
					conexion = Conexion.obtenerInstancia().ConexionBD();
					cmd = new MySqlCommand("update imagen set estado = ? " +
											"where id_persona = ?", conexion);

					MySqlParameter parEstado = new MySqlParameter("@estadoImagen", MySqlDbType.Bit);
					MySqlParameter parId = new MySqlParameter("@idImagen", MySqlDbType.Int32);

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

	}
}