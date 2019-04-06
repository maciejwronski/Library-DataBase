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
                    libraryList[i].GenerateBookData(BookNames);
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
    }
}
