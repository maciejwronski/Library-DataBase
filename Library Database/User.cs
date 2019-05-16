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
    class User
    {
        protected string cs = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            (HOST=dbserver.mif.pg.gda.pl)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
            (SERVICE_NAME=ORACLEMIF)));User Id=S161307_S;Password=;";
        protected OracleConnection oraconn;
        protected Thread userThread;
        protected OracleTransaction tran;
        public User()
        {
            oraconn = new OracleConnection(cs);
        }
        ~User()
        {
            DisconnectFromDatabase();
        }
        public virtual void DisconnectFromDatabase()
        {
            oraconn.Close();
            
        }
        public virtual void RandomlyLeaveConnectionOpen(int min, int max)
        {
            Random rand = new Random();
            int time = rand.Next(min, max);
            Thread.Sleep(time);
        }
        public virtual void LoseConnectionWithDatabase(float timeToLoseConnection = 0, bool tryToReconnect = true)
        {

            Task.Delay(1000).ContinueWith(t => DisconnectFromDatabase());
            if (tryToReconnect)
            {
                Task.Delay(1001).ContinueWith(t => TryToReconnect());
            }
        }
        public virtual void TryToReconnect(int numberOfTries = 3)
        {
            int tempTries = numberOfTries;
            while (oraconn.State == ConnectionState.Closed && numberOfTries > 0)
            {
                ConnectToDatabase();
            }
        }
        public virtual void ConnectToDatabase()
        {
            oraconn.Open();
        }
    }
}
