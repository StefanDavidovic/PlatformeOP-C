using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SF_19_2019_POP2020.Windows.Pretrage
{
    /// <summary>
    /// Interaction logic for PrikazPacijentaAdresa.xaml
    /// </summary>
    public partial class PrikazPacijentaAdresa : Window
    {
        ICollectionView view;
        public PrikazPacijentaAdresa(int id)
        {
            InitializeComponent();
            ObservableCollection<Pacijent> pac2 = readAdresa(id);

            view = CollectionViewSource.GetDefaultView(pac2);
            dgDomZdravlja.ItemsSource = view;
            dgDomZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("ID") || e.PropertyName.Equals("JMBG") || e.PropertyName.Equals("Lozinka") || e.PropertyName.Equals("AdresaID"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        public ObservableCollection<Pacijent> readAdresa(int sifraAdrese)
        {
            ObservableCollection<Pacijent> pac = new ObservableCollection<Pacijent>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select id,ime,prezime,email,jmbg from Pacijenti d where adresa_id =" + sifraAdrese + "and aktivan = 1";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    pac.Add(new Pacijent
                    {
                        ID = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Email = reader.GetString(3),
                        JMBG = reader.GetString(4)
                    });


                }



                reader.Close();

                return pac;
            }
        }
    }
}