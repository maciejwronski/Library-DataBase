using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace Library_Database
{
    class Librarian : User
    {
        public Librarian()
        {
            base.ConnectToDatabase();
            Console.WriteLine(oraconn.State);
        }
        ~Librarian()
        {
            base.DisconnectFromDatabase();
        }
    }
}
