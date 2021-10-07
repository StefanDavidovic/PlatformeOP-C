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
    /// Interaction logic for TerminViaLekarPrikaz.xaml
    /// </summary>
    public partial class TerminViaLekarPrikaz : Window
    {
        ICollectionView view;
        public TerminViaLekarPrikaz(int sifraLekara)
        {
            InitializeComponent();
            ObservableCollection<Termin> pac2 = readTer(sifraLekara);

            view = CollectionViewSource.GetDefaultView(pac2);
            dgLekari.ItemsSource = view;
            dgLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        public ObservableCollection<Termin> readTer(int sifraLekara)
        {
            ObservableCollection<Termin> pac = new ObservableCollection<Termin>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Termini d where lekar_id =" + sifraLekara + " and datum >= CAST(CURRENT_TIMESTAMP AS DATE) and active = 1";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    pac.Add(new Termin
                    {
                        Sifra = reader.GetInt32(0),
                        LekarID = reader.GetInt32(1),
                        Datum = reader.GetDateTime(2),
                        Status = (EStatusTermina)Enum.Parse(typeof(EStatusTermina), reader.GetString(3)),
                        PacijentID = reader.GetInt32(4),
                        Aktivan = reader.GetBoolean(5)
                    });


                }



                reader.Close();

                return pac;
            }
        }
    }

}