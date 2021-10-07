using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Services;
using SF_19_2019_POP2020.Windows.AdresaEditUpdate;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SF_19_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for AdreseWindow.xaml
    /// </summary>
    public partial class AdreseWindow : Window
    {
        ICollectionView view;
        AdresaService adresa1;
        public AdreseWindow()
        {
            InitializeComponent();
            viewA();

        }

        public void viewA()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Adrese);
            view.Filter = PrikazFiltera;
            dgAdresa.ItemsSource = view;
            dgAdresa.IsSynchronizedWithCurrentItem = true;
            dgAdresa.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Potvrda",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
               Adresa selektovanaAdresa = view.CurrentItem as Adresa;

                if(Provera(selektovanaAdresa) == false)
                {
                    if (Provera2(selektovanaAdresa) == false)
                    {
                        Util.Instance.DeleteAdresa(selektovanaAdresa.SifraAdrese);
                        view.Refresh();
                    }
                }

            }
        }

        private bool Provera(Adresa ad)
        {
            foreach (Lekar lekari in Util.Instance.Lekari)
            {
                if (lekari.AdresaID == ad.SifraAdrese && lekari.Aktivan == true)
                {
                    MessageBox.Show("Ne mozete obrisati adresu koji ima instancu", "GRESKA");
                    return true;
                }
            }
            return false;
        }

        private bool Provera2(Adresa ad)
        {
            foreach (Pacijent pacijenti in Util.Instance.Pacijenti)
            {
                if (pacijenti.AdresaID == ad.SifraAdrese && pacijenti.Aktivan == true)
                {
                    MessageBox.Show("Ne mozete obrisati adresu koji ima instancu", "GRESKA");
                    return true;
                }
            }
            return false;
        }

        private bool PrikazFiltera(object obj)
        {
            return ((Adresa)obj).Aktivan;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            Adresa novaAdresa = new Adresa();
            novaAdresa.Aktivan = true;
            AdresaEditAddDelete few = new AdresaEditAddDelete(novaAdresa);
           // Util.Instance.CitanjeEntiteta();
            few.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Adresa selektovanaAdresa = view.CurrentItem as Adresa; //preuzimanje selektovane adrese

            if (selektovanaAdresa != null)//ako je neki fakultet selektovan
            {
                //napravimo kopiju trenutnih vrednosti u objektu,  da bi ih mogli preuzeti ako korisnik ponisti napravljenje izmene
                Adresa old = (Adresa)selektovanaAdresa.Clone();
                AdresaEditAddDelete few = new AdresaEditAddDelete(selektovanaAdresa,
                    AdresaEditAddDelete.Stanje.IZMENA);
                viewA();

                if (few.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {

                    //pronadjemo indeks selektovanog
                    int index = Util.Instance.Adrese.IndexOf(
                        selektovanaAdresa);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Util.Instance.Adrese[index] = old;
                }
            }
            viewA();
        }



    }
}
