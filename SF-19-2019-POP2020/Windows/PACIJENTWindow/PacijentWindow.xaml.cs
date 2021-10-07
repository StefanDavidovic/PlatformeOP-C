using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF_19_2019_POP2020.Windows.PACIJENTWindow
{
    /// <summary>
    /// Interaction logic for PacijentWindow.xaml
    /// </summary>
    public partial class PacijentWindow : Window
    {
        ObservableCollection<Pacijent> Pacijenti { get;set; }
        Pacijent korisnik;
        public PacijentWindow(string jmbg)
        {
            InitializeComponent();
            this.korisnik = korisnik;





        }


        public void readPacijent()
        {
            Pacijenti = new ObservableCollection<Pacijent>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Pacijenti where id = @id";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Pacijenti.Add(new Pacijent
                    {
                        ID = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        Email = reader.GetString(4),
                        JMBG = reader.GetString(5),
                        Pol = (EPol)Enum.Parse(typeof(EPol), reader.GetString(6)),
                        Aktivan = reader.GetBoolean(7),
                        AdresaID = reader.GetInt32(8)



                    });


                }
                reader.Close();



            }
        }
    }
}
