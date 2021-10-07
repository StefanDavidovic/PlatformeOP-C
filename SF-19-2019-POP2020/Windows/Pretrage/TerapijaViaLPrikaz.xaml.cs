using SF_19_2019_POP2020.Models;
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
    /// Interaction logic for TerapijaViaLPrikaz.xaml
    /// </summary>
    public partial class TerapijaViaLPrikaz : Window
    {
        ICollectionView view;
        public TerapijaViaLPrikaz(int sifraLekara)
        {
            InitializeComponent();
            ObservableCollection<Terapija> pac2 = readTer(sifraLekara);

            view = CollectionViewSource.GetDefaultView(pac2);
            dgLekari.ItemsSource = view;
            dgLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        public ObservableCollection<Terapija> readTer(int sifraLekara)
        {
            ObservableCollection<Terapija> pac = new ObservableCollection<Terapija>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Terapije d where lekar_id =" + sifraLekara + " and aktivan = 1";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    pac.Add(new Terapija
                    {
                        Sifra = reader.GetInt32(0),
                        Opis = reader.GetString(1),
                        LekarID = reader.GetInt32(2),
                        PacijentID = reader.GetInt32(3),
                        Aktivan = reader.GetBoolean(4)
                    });


                }



                reader.Close();

                return pac;
            }
        }

    }
}
