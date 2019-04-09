using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace Library_Database
{
    class Database
    {
        string cs = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            (HOST=dbserver.mif.pg.gda.pl)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
            (SERVICE_NAME=ORACLEMIF)));User Id=S161307_S;Password=b6H2D;";
        public Int32 AddLibraryToDatabase(Library library)
        {
            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                oraconn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = oraconn;
                    cmd.CommandText = "insert into library(libraryname)VALUES(:libName) RETURNING LIBRARY_ID INTO :my_id_param";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("libName", OracleDbType.Varchar2))
                            .Value = library.LibraryName;
                    OracleParameter outputParameter = new OracleParameter("my_id_param", OracleDbType.Int32);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);
                    cmd.ExecuteNonQuery();
                    return int.Parse(outputParameter.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu biblioteki:");
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public void AddBooksToDataBase(Library lib, int ID)
        {
            int[] years = lib.BookList.Select(c => c.Year).ToArray();
            string[] names = lib.BookList.Select(c => c.Name).ToArray();
            int[] pagecount = lib.BookList.Select(c => c.PageCount).ToArray();
            int[] test = new int[lib.BookList.Count];
            for (int i = 0; i < test.Length; i++)
                test[i] = ID;
            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                string sql = @"INSERT INTO BOOKS (NAME, PAGECOUNT, YEAR, LIBRARY_LIBRARY_ID) values (:name, :pagecount, :year, :lib_id)";

                OracleConnection cnn = new OracleConnection(cs);
                cnn.Open();
                OracleCommand cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.ArrayBindCount = lib.BookList.Count;
                cmd.Parameters.Add(":name", OracleDbType.Varchar2, names.Length,
                                   names, ParameterDirection.Input);
                cmd.Parameters.Add(":pagecount", OracleDbType.Int32, pagecount.Length,
                                   pagecount, ParameterDirection.Input);
                cmd.Parameters.Add(":year", OracleDbType.Int32, years.Length,
                                   years, ParameterDirection.Input);
                cmd.Parameters.Add(":lib_id", OracleDbType.Int32, test.Length,
                                   test, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu insert Books:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void AddUsersToDataBase(Library lib)
        {
            var tempLib = lib.MenList.Concat(lib.WomenList);
            string[] names =  tempLib.Select(c => c.Name).ToArray();
            string[] surnames = tempLib.Select(c => c.Surname).ToArray();
            string[] pesels = tempLib.Select(c => c.Pesel).ToArray();
            char[] gender = tempLib.Select(c => c.Gender).ToArray();
            Console.WriteLine(names.Length);
            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                string sql = @"INSERT INTO USERS (NAME, SURNAME, PESEL, GENDER) values (:name, :surname, :pesel, :gender)";

                OracleConnection cnn = new OracleConnection(cs);
                cnn.Open();
                OracleCommand cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.ArrayBindCount = names.Length;
                cmd.Parameters.Add(":name", OracleDbType.Varchar2, names.Length,
                                   names, ParameterDirection.Input);
                cmd.Parameters.Add(":surname", OracleDbType.Varchar2, surnames.Length,
                                   surnames, ParameterDirection.Input);
                cmd.Parameters.Add(":pesel", OracleDbType.Varchar2, pesels.Length,
                                   pesels, ParameterDirection.Input);
                cmd.Parameters.Add(":gender", OracleDbType.Varchar2, gender.Length,
                                   gender, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu insert Users:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void AddAuthorsToDataBase(Library lib, int ID)
        {
            int[] years = lib.BookList.Select(c => c.Year).ToArray();
            string[] names = lib.BookList.Select(c => c.Name).ToArray();
            int[] pagecount = lib.BookList.Select(c => c.PageCount).ToArray();
            int[] test = new int[lib.BookList.Count];
            for (int i = 0; i < test.Length; i++)
                test[i] = ID;
            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                string sql = @"INSERT INTO BOOKS (NAME, PAGECOUNT, YEAR, LIBRARY_LIBRARY_ID) values (:name, :pagecount, :year, :lib_id)";

                OracleConnection cnn = new OracleConnection(cs);
                cnn.Open();
                OracleCommand cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.ArrayBindCount = lib.BookList.Count;
                cmd.Parameters.Add(":name", OracleDbType.Varchar2, names.Length,
                                   names, ParameterDirection.Input);
                cmd.Parameters.Add(":pagecount", OracleDbType.Int32, pagecount.Length,
                                   pagecount, ParameterDirection.Input);
                cmd.Parameters.Add(":year", OracleDbType.Int32, years.Length,
                                   years, ParameterDirection.Input);
                cmd.Parameters.Add(":lib_id", OracleDbType.Int32, test.Length,
                                   test, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu insertaBooks:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
