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
        public Librarian() : base()
        {
            if(new Random().Next(2) == 0)
                 userThread = new Thread(() => AddBookTransaction(30));
            else
                 userThread = new Thread(() => RemoveBookTransaction());

            userThread.Start();
        }
        public void AddBookTransaction(int HowManyDays) {
            OracleCommand cmd;
            cmd = new OracleCommand();
            cmd.Connection = oraconn;
            oraconn.Open();
            tran = oraconn.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                RandomlyLeaveConnectionOpen(10000, 100000);
                string query = @"INSERT into loans(TAKENDATE, BROUGHTDATE, USERS_USER_ID, BOOKS_ID) VALUES(:taken, :brought, :usid, :boid)";
                cmd.CommandText = query;
                cmd.Parameters.Add(new OracleParameter("taken", OracleDbType.Date)).Value = DateTime.Now;
                cmd.Parameters.Add(new OracleParameter("brought", OracleDbType.Date)).Value = DateTime.Now.AddDays(HowManyDays);
                cmd.Parameters.Add(":usid", OracleDbType.Int32, GetRandomUserFromLibrary(), ParameterDirection.Input);
                cmd.Parameters.Add(":boid", OracleDbType.Int32, GetRandomBookFromLibrary(), ParameterDirection.Input);
                long rows = cmd.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("Commit complete, {0} ", rows + " Rows updated");
            }
            catch (Exception e)
            {
                tran.Rollback();
                Console.WriteLine("Transaction failed - Rolled Back!");
                Console.WriteLine(e.Message);
            }
            finally
            {
                base.DisconnectFromDatabase();
                userThread.Join();
            }
        }
        public void RemoveBookTransaction()
        {
            OracleCommand cmd;
            cmd = new OracleCommand();
            cmd.Connection = oraconn;
            oraconn.Open();
            tran = oraconn.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                RandomlyLeaveConnectionOpen(10000, 100000);
                string query = @"delete from loans where ID = :id";
                cmd.CommandText = query;
                cmd.Parameters.Add(":id", OracleDbType.Int32, GetRandomLoan(), ParameterDirection.Input);
                long rows = cmd.ExecuteNonQuery();
                tran.Commit();
                Console.WriteLine("Commit complete, {0} ", rows + " Rows updated");
            }
            catch (Exception e)
            {
                tran.Rollback();
                Console.WriteLine("Transaction failed - Rolled Back!");
                Console.WriteLine(e.Message);
            }
            finally
            {
                base.DisconnectFromDatabase();
                userThread.Join();
            }
        }
        public Int32 GetRandomUserFromLibrary()
        {
            string myQuery = "SELECT USER_ID FROM   ( SELECT * FROM   USERS ORDER BY DBMS_RANDOM.VALUE) WHERE  rownum = 1";
            OracleConnection oraconn = new OracleConnection(cs);
            OracleCommand cmd = new OracleCommand(myQuery, oraconn);
            oraconn.Open();
            //System.NullReferenceException occurs when their is no data/result
            var getValue = int.Parse(cmd.ExecuteScalar().ToString());

            oraconn.Close();
            return getValue;
        }
        public Int32 GetRandomBookFromLibrary()
        {
            string myQuery = "SELECT ID FROM   ( SELECT * FROM  BOOKS ORDER BY DBMS_RANDOM.VALUE) WHERE  rownum = 1 ";
            OracleConnection oraconn = new OracleConnection(cs);
            OracleCommand cmd = new OracleCommand(myQuery, oraconn);
            oraconn.Open();
            //System.NullReferenceException occurs when their is no data/result
            var getValue = int.Parse(cmd.ExecuteScalar().ToString());

            oraconn.Close();
            return getValue;
        }
        public Int32 GetRandomLoan()
        {
            string myQuery = "SELECT ID FROM   ( SELECT * FROM  LOANS ORDER BY DBMS_RANDOM.VALUE) WHERE  rownum = 1 ";
            OracleConnection oraconn = new OracleConnection(cs);
            OracleCommand cmd = new OracleCommand(myQuery, oraconn);
            oraconn.Open();
            //System.NullReferenceException occurs when their is no data/result
            var getValue = int.Parse(cmd.ExecuteScalar().ToString());

            oraconn.Close();
            return getValue;
        }
    }
}
