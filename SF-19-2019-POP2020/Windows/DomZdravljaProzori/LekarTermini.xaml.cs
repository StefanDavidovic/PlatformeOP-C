using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.LekariWindowProfil;
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

namespace SF_19_2019_POP2020.Windows.DomZdravljaProzori
{
    /// <summary>
    /// Interaction logic for LekarTermini.xaml
    /// </summary>
    public partial class LekarTermini : Window
    {
        ICollectionView view;

        Lekar lekar;
        public LekarTermini(Lekar lekar)
        {
            this.lekar = lekar ;
            InitializeComponent();
            viewT();



        }

        public void viewT()
        {

            view = CollectionViewSource.GetDefaultView(nadjiLekarauTerminuZ(lekar.ID));

            dgTerminiZakazani.ItemsSource = view;
            dgTerminiZakazani.IsSynchronizedWithCurrentItem = true;

            dgTerminiZakazani.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        public ObservableCollection<Termin> nadjiLekarauTerminuZ(int id)
        {
            ObservableCollection<Termin> pac = new ObservableCollection<Termin>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Termini  where lekar_id =" + id + "and active = 1";

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



        private void btnT_Click(object sender, RoutedEventArgs e)
        {

            Termin novT = new Termin();
            LekariZakazivanje few = new LekariZakazivanje(novT,lekar);
            //     Util.Instance.CitanjeEntiteta();
            few.ShowDialog();
            viewT();


        }


        private void btnTerDel_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Da li ste sigurni?", "Potvrda",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {


                Termin selektovaniTermin = view.CurrentItem as Termin;

                if (selektovaniTermin.Status == EStatusTermina.SLOBODAN) { 

                    Util.Instance.DeleteTermin(selektovaniTermin.Sifra);
                    view.Refresh();
                    viewT();
                }
                else
                {
                    MessageBox.Show("Ne mozete obrisati zakazan termin", "Greska");
                }

            }


        }
    }
}
