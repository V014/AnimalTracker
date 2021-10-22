using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace AnimalTracker
{
    class AnimalControls
    {   
        // Inserts animal details into the database
        public static void Querydb(string txtQuery)
        {
            // links query to connection page
            Connection.ExecuteQuery(txtQuery);
        }
    }
}
