using System.Data;
using System.Data.SQLite;


namespace AnimalTracker
{
    class Connection
    {
        // declare connection variables
        private static SQLiteConnection con;
        private static SQLiteCommand cmd;
        private static SQLiteDataReader dr;
        private static DataSet DS = new DataSet();
        private static DataTable DT = new DataTable();

        // connection to database file
        private static void setConnection()
        {
            con = new SQLiteConnection
                ("Data Source=Animaldb.db;Version=3;New=Flase;Compress=True;");
        }

        // execute query function
        public static void ExecuteQuery(string txtQuery)
        {
            setConnection();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandText = txtQuery;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static SQLiteConnection GetConnection()
        {
            setConnection();
            con.Open();
            return con;
        }
    }
}
