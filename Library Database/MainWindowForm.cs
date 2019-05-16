using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Database
{
    public partial class MainWindowForm : Form
    {
        public static string[] menSurnames;
        public static string[] womenSurnames;
        public static string[] menNames;
        public static string[] WomenNames;
        public static string[] BookNames;
        public static string[] LibraryNames;
        PeselGenerator peselGenerator = new PeselGenerator();
        Random random = new Random();
        List<User> users;

        public MainWindowForm()
        {
            InitializeComponent();
        }
        private void GenerateData(object sender, EventArgs e)
        {
            if (LoadFiles() == true)
            {
                List<Library> libraryList = new List<Library>();

                
                for (int i = 0; i < LibraryNames.Length; i++)
                {
                    libraryList.Add(new Library(LibraryNames[i]));
                    libraryList[i].GeneratePeopleData();
                    libraryList[i].GenerateBookData(BookNames, i, LibraryNames.Length);
                    Database database = new Database();
                    int libraryID = database.AddLibraryToDatabase(libraryList[i]);
                    Console.WriteLine("Added library to database! " + i);
                    int[] booksIDs = database.AddBooksToDataBase(libraryList[i], libraryID).ToArray();
                    Console.WriteLine("Added books to database! " + i);
                    int[] authorsIDS = database.AddAuthorsToBooks(libraryList[i]).ToArray();
                    Console.WriteLine("Added authors to database! " + i);
                  //  database.CreateConnectionsBetweenAuthorsAndBooks(libraryList[i], booksIDs, authorsIDS);
                    Console.WriteLine("Added connections to database! " + i);
                    database.AddUsersToDataBase(libraryList[i]);
                    Console.WriteLine("Added  to database! " + i);
                }
            }
        }
        private bool LoadFiles()
        {
            try
            {
                menSurnames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\100_Men_Surnames.txt");
                womenSurnames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\100_Women_Surnames.txt");
                WomenNames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\100_Women_Names.txt");
                menNames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\100_Men_Names.txt");
                BookNames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\100_Book_Names.txt");
                LibraryNames = System.IO.File.ReadAllLines(@"C:\Users\Maciej\Source\Repos\Library Database\Library Database\Data\2_Library_Names.txt");
                
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine("File: " + ex.FileName + " not found");
                return false;
            }
            return true;
        }

        private void CreateListOfUsers(object sender, EventArgs e)
        {
            users = GenerateListOfUsers();
            NumberOfUsers.Text = "Librarians in Database: " + (users.Count - 1).ToString() + " and 1 Administrator";
        }
        private List<User> GenerateListOfUsers()
        {
            List<User> thisList = new List<User>();
            int numberOfUsers;
            try
            {
                numberOfUsers = int.Parse(TotalUsersTextBox.Text.ToString());
            }
            catch (InvalidCastException)
            {
                numberOfUsers = 1;
            }

            for(int i=0; i<numberOfUsers; i++)
            {
                thisList.Add(new Librarian());
            }
            thisList.Add(new DatabaseAdmin());
            return thisList;
        }

    }
}
