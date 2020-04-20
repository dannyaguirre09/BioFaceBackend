using ApiFace.Models.Entidades;
using ApiFace.Util;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace ApiFace.Models.Dao
{
	public class ReconocimientoDAO
	{
		#region Variables
		MySqlConnection conexion = null;
		MySqlCommand cmd = null;
		MySqlDataAdapter da = null;
		Reconocimiento objReconocimiento = null;
		public List<byte[]> listadoRostros = new List<byte[]>();
		List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
		int contrain;
		List<int> IDS = new List<int>();
		public int[] idPersona;
		public static byte[] rostros;
		#endregion

		public void obtenerRostros()
		{
			objReconocimiento = new Reconocimiento();
			try
			{
				conexion = Conexion.obtenerInstancia().ConexionBD();
				conexion.Open();
				cmd = new MySqlCommand("select id_persona, imagen from imagen where estado = true ", conexion);
				da = new MySqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);
				int cont = dt.Rows.Count;
				idPersona = new int[cont];

				for (int i = 0; i < cont; i++)
				{
					idPersona[i] = Convert.ToInt32(dt.Rows[i]["id_persona"].ToString());
					rostros = (byte[])dt.Rows[i]["imagen"];
					listadoRostros.Add(rostros);
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				conexion.Close();
			}
		}

		public Image ConvertBinaryToImg(int C)
		{
			Image Imagen = null;
			try
			{
				byte[] img = listadoRostros[C];
				MemoryStream Memoria = new MemoryStream(img);
				Imagen = Image.FromStream(Memoria);
				Memoria.Close();
			}
			catch (Exception)
			{
			}
			return Imagen;
		}

		public int[] deteccion(Imagen objImagen, bool ingreso)
		{
			int[] respuesta = { 0, 0 };
			Transformaciones conversiones = new Transformaciones();
			byte[] cuadroVideo = conversiones.convertToByte(objImagen.ImagenPersona);
			Image cuadroImagen = conversiones.ConvertFrameToImg(cuadroVideo);
			Image<Bgr, byte> cuadro = conversiones.imageToEmguImage(cuadroImagen);
			obtenerRostros();
			int[] Ids = idPersona;
			contrain = Ids.Length;
			for (int i = 0; i < Ids.Length; i++)
			{
				Bitmap bmp = new Bitmap(ConvertBinaryToImg(i), new Size(200, 200));
				trainingImages.Add(new Image<Gray, byte>(bmp));
				IDS.Add(Ids[i]);
			}
			Image<Gray, byte> result = cuadro.Copy().Convert<Gray, byte>().Resize(200, 200, Inter.Cubic);

			Mat[] faceImages = new Mat[trainingImages.Count];
			int[] faceLabels = new int[IDS.Count];

			for (int i = 0; i < trainingImages.Count; i++)
			{
				faceImages[i] = trainingImages[i].Mat;
			}

			for (int i = 0; i < IDS.Count; i++)
			{
				faceLabels[i] = IDS[i];
			}

			if (trainingImages.ToArray().Length != 0)
			{
				EigenFaceRecognizer recognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);			
				recognizer.Train(faceImages, faceLabels);
				FaceRecognizer.PredictionResult result1 = recognizer.Predict(result);
				var a = result1.Label;
				var b = result1.Distance;
				if (a != -1)
					respuesta = guardarRegistro(Convert.ToInt32(a), ingreso);
			}
			return respuesta;
		}

		public int[] guardarRegistro(int idPersona, bool ingreso)
		{
			RegistroDAO daoRegistro = new RegistroDAO();
			Registro objRegistro = new Registro();
			Persona objPersona = new Persona();
			objPersona.IdPersona = idPersona;
			objRegistro.Persona = objPersona;
			if (ingreso)
			{
				objRegistro.HoraIngreso = DateTime.Now;
				objRegistro.FechaIngreso = DateTime.Now;
				return daoRegistro.guardarRegistroIngreso(objRegistro);
			}
			else
			{
				objRegistro.HoraSalida = DateTime.Now;
				objRegistro.FechaSalida = DateTime.Now;
				return daoRegistro.guardarRegistroSalida(objRegistro);

			}

		}


	}
}