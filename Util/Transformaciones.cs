using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;

namespace ApiFace.Util
{
	public class Transformaciones
	{
		public byte[] convertToByte(string cadena)
		{
			string convert = cadena.Replace("data:image/png;base64,", String.Empty);
			var cadenaByte = Convert.FromBase64String(convert);
			return cadenaByte;
		}

		public Image ConvertFrameToImg(byte[] frame)
		{
			Image Imagen;
			byte[] img = frame;
			MemoryStream Memoria = new MemoryStream(img);
			Imagen = Image.FromStream(Memoria);
			Memoria.Close();
			return Imagen;
		}

		public Emgu.CV.Image<Bgr, Byte> imageToEmguImage(System.Drawing.Image imageIn)
		{
			Bitmap bmpImage = new Bitmap(imageIn);
			Emgu.CV.Image<Bgr, Byte> imageOut = new Emgu.CV.Image<Bgr, Byte>(bmpImage);

			return imageOut.Resize(320, 240, Inter.Cubic);
		}
	}
}