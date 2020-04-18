using MySql.Data.MySqlClient;

namespace ApiFace.Util
{
	public class Conexion
	{
		#region Patron Singleton
		private static Conexion conexion = null;
		private Conexion() { }

		public static Conexion obtenerInstancia()
		{
			if (conexion == null)
			{
				conexion = new Conexion();
			}

			return conexion;
		}

		#endregion

		public MySqlConnection ConexionBD()
		{
			string servidor = "localhost";
			string puerto = "3306";
			string user = "root";
			string pass = "root";

			MySqlConnection conexion = new MySqlConnection();
			conexion.ConnectionString = @"server = " + servidor + "; port = " + puerto + "; user id = " + user + "; password = " + pass + "; " +
				"						database = face_prueba; max pool size=1;";

			return conexion;
		}
	}
}