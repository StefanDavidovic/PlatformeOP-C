using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.DoktoriProzori;
using SF_19_2019_POP2020.Windows.DomZdravljaProzori;
using SF_19_2019_POP2020.Windows.Pretrage;
using SF_19_2019_POP2020.Windows.TerapijaProzori;
using SF_19_2019_POP2020.Windows.TerminiWindow;
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

namespace SF_19_2019_POP2020.Windows.LekariWindowProfil
{
    /// <summary>
    /// Interaction logic for LekarGlavna.xaml
    /// </summary>
    public partial class LekarGlavna : Window
    {
        ICollectionView view;
        Lekar lekar;
        ICollectionView view2;
        ObservableCollection<Lekar> pac2;
        public LekarGlavna(string jmbg)
        {
            InitializeComponent();
            pac2 = nadjiLekara(jmbg);
            lekar = nadjiLekara2(jmbg);

            view = CollectionViewSource.GetDefaultView(pac2);

            dgLekar.ItemsSource = view;
            dgLekar.IsSynchronizedWithCurrentItem = true;

            dgLekar.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            viewT();
          
        }
        private void btnDoktori_Click(object sender, RoutedEventArgs e)
        {

            LekariPick odzm = new LekariPick(LekariPick.Stanje.GLEDANJE);
            odzm.Show();
        }
        public void viewT()
        {

            view2 = CollectionViewSource.GetDefaultView(nadjiLekarauTerapiji(lekar.ID));
            dgTerapije.ItemsSource = view2;
            dgTerapije.IsSynchronizedWithCurrentItem = true;
               

            dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        public ObservableCollection<Lekar> nadjiLekara(string jmbg)
        {
            ObservableCollection<Lekar> pac = new ObservableCollection<Lekar>();

            foreach (Lekar lekar in Util.Instance.Lekari)
            {
                if (lekar.JMBG.Equals(jmbg))
                {

                    pac.Add(lekar);
                }

            }
            return pac;

        }



        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("LekarID"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void DGA_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        public ObservableCollection<Terapija> nadjiLekarauTerapiji(int id)
        {
            ObservableCollection<Terapija> pac = new ObservableCollection<Terapija>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Terapije  where lekar_id =" + id + "and aktivan = 1";

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

    

        public Lekar nadjiLekara2(string jmbg)
        {
            foreach (Lekar lekar in Util.Instance.Lekari)
            {
                if (lekar.JMBG.Equals(jmbg))
                {

                    return lekar;
                }

            }
            return null;

        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Lekar selektovaniKorisnik = view.CurrentItem as Lekar; //preuzimanje selektovane adrese

            if (selektovaniKorisnik != null)
            {
                Lekar old = (Lekar)selektovaniKorisnik.Clone();
                DoktorAddEdit few = new DoktorAddEdit(selektovaniKorisnik,
                    DoktorAddEdit.Stanje.IZMENA);
                if (few.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {


                    int index = pac2.IndexOf(
                        selektovaniKorisnik);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    pac2[index] = old;
                }
            }
        }

        private void btnInstitucija_Click(object sender, RoutedEventArgs e)
        {

            DomZdravljaViaAdresa odzm = new DomZdravljaViaAdresa();
            odzm.Show();
        }

        private void btnDz_Click(object sender, RoutedEventArgs e)
        {

            TerminUdz odzm = new TerminUdz(lekar);
            odzm.Show();
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {

            Terapija novaTerapija = new Terapija();
            LekarZTerapija few = new LekarZTerapija(novaTerapija,lekar);
            //     Util.Instance.CitanjeEntiteta();
            few.ShowDialog();
            viewT();


        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            view.Refresh();

            view2.Refresh();

        }

        private void btnTermin_Click(object sender, RoutedEventArgs e)
        {
            LekarTermini lt = new LekarTermini(lekar);
            lt.Show();

        }

        private void btnTerminLekar_Click(object sender, RoutedEventArgs e)
        {
            TerminiKodLekaraPick t = new TerminiKodLekaraPick();
            t.Show();

        }
    }
}
