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
            (SERVICE_NAME=ORACLEMIF)));User Id=S161307_S;Password=q5SAr;";
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
        public List<int> AddBooksToDataBase(Library lib, int ID)
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
                List<int> count = new List<int>();
                cmd.CommandText = @"SELECT * FROM(SELECT id FROM books ORDER BY id desc) WHERE ROWNUM <= " + names.Length + " ORDER BY id";
                cmd.CommandType = CommandType.Text;

                OracleDataReader oraReader = null;
                oraReader = cmd.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        count.Add(oraReader.GetInt32(0));
                    }
                }
                cnn.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu insert Books:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
        public void CreateConnectionsBetweenAuthorsAndBooks(Library lib, int[] bookIDS, int[] authIDS)
        {
            var authorList = lib.BookList.Select(c => c.Authors).ToList();
            List<int> authList = new List<int>();
            foreach (List<Person> myList in authorList)
                authList.Add(myList.Count);
            int[] authorsArray = authList.ToArray();

            List<int> FinalBookIDList = new List<int>();
            for (int i = 0; i < authorsArray.Length; i++)
            {
                for (int j = 0; j < authorsArray[i]; j++)
                {
                    FinalBookIDList.Add(bookIDS[i]);
                }
            }
            int[] FinalBookIDArray = FinalBookIDList.ToArray();

            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                string sql = @"INSERT INTO RELATION_8(BOOKS_ID, AUTHORS_ID) VALUES(:firstParam, :secondParam)";

                OracleConnection cnn = new OracleConnection(cs);
                cnn.Open();
                OracleCommand cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.ArrayBindCount = FinalBookIDArray.Length;
                Console.WriteLine(FinalBookIDArray.Length + " " + authIDS.Length);
                cmd.Parameters.Add(":firstParam", OracleDbType.Int32, FinalBookIDArray.Length,
                                   FinalBookIDArray, ParameterDirection.Input);
                cmd.Parameters.Add(":secondParam", OracleDbType.Int32, authIDS.Length,
                                   authIDS, ParameterDirection.Input);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Blad przy dodawaniu insert Relation_8:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
            public List<int> AddAuthorsToBooks(Library lib)
        {
            var authorList = lib.BookList.Select(c => c.Authors).ToList();
            List<string> nameList = new List<string>();
            List<string> surnameList = new List<string>();
            List<string> peselList = new List<string>();
            List<char> genderList = new List<char>();
            foreach (List<Person> list in authorList)
            {
                foreach(Person p in list)
                {
                    nameList.Add(p.Name);
                    surnameList.Add(p.Surname);
                    peselList.Add(p.Pesel);
                    genderList.Add(p.Gender);
                }
            }
            string[] names = nameList.ToArray();
            string[] surnames = surnameList.ToArray();
            string[] pesels = peselList.ToArray();
            char[] gender = genderList.ToArray();

            OracleConnection oraconn = new OracleConnection(cs);
            try
            {
                string sql = @"INSERT INTO AUTHORS (NAME, SURNAME, PESEL, GENDER) values (:name, :surname, :pesel, :gender)";

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

                List<int> count = new List<int>();
                cmd.CommandText = @"SELECT * FROM(SELECT id FROM AUTHORS ORDER BY id desc) WHERE ROWNUM <= " + names.Length + " ORDER BY id";
                cmd.CommandType = CommandType.Text;

                OracleDataReader oraReader = null;
                oraReader = cmd.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        count.Add(oraReader.GetInt32(0));
                    }
                }
                cnn.Close();
                return count;

            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Blad przy dodawaniu insert Authors:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
        public void AddUsersToDataBase(Library lib)
        {
            var tempLib = lib.MenList.Concat(lib.WomenList);
            string[] names =  tempLib.Select(c => c.Name).ToArray();
            string[] surnames = tempLib.Select(c => c.Surname).ToArray();
            string[] pesels = tempLib.Select(c => c.Pesel).ToArray();
            char[] gender = tempLib.Select(c => c.Gender).ToArray();
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
    }
}
