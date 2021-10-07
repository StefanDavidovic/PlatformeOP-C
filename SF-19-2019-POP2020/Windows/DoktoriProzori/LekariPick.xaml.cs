using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.Pretrage;
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

namespace SF_19_2019_POP2020.Windows.DoktoriProzori
{
    /// <summary>
    /// Interaction logic for LekariPick.xaml
    /// </summary>
    public partial class LekariPick : Window
    {
        public enum Stanje { ADMINISTRACIJA, PREUZIMANJE,GLEDANJE };
        Stanje stanje;
        ICollectionView view;

        public Lekar SelektovaniLekar = null;

        public LekariPick(Stanje stanje = Stanje.ADMINISTRACIJA)
        {
            InitializeComponent();
            this.stanje = stanje;

            if (stanje == Stanje.PREUZIMANJE)
            {
                btnAdd.Visibility = System.Windows.Visibility.Collapsed;
                btnDelete.Visibility = System.Windows.Visibility.Collapsed;
                btnUpdate.Visibility = System.Windows.Visibility.Collapsed;
            }

            else if(stanje == Stanje.GLEDANJE)
            {
                btnAdd.Visibility = System.Windows.Visibility.Collapsed;
                btnDelete.Visibility = System.Windows.Visibility.Collapsed;
                btnUpdate.Visibility = System.Windows.Visibility.Collapsed;
                btnPick.Visibility = System.Windows.Visibility.Hidden;

            }

            view = CollectionViewSource.GetDefaultView(Util.Instance.Lekari);
            view.Filter = CustomFilter;
            dgLekari.ItemsSource = view;

            dgLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("ID") || e.PropertyName.Equals("JMBG") || e.PropertyName.Equals("Lozinka"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private bool CustomFilter(object obj)
        {
            Lekar korisnik = obj as Lekar;
            // Korisnik korisnik1 = (Korisnik)obj;

            if (korisnik.Aktivan)
            {
                if (TxtPretraga.Text != "")
                {
                    if (korisnik.Ime.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Ime.Contains(TxtPretraga.Text);
                    }
                    if (korisnik.Prezime.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Prezime.Contains(TxtPretraga.Text);
                    }
                    if (korisnik.Email.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Email.Contains(TxtPretraga.Text);
                    }
                    if (korisnik.AdresaID.ToString().Contains(TxtPretraga.Text))
                    {
                        return korisnik.AdresaID.ToString().Contains(TxtPretraga.Text);
                    }
                }
                else
                    return true;

            }
            return false;
        }

        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            LekarViaAdresa lva = new LekarViaAdresa();
            lva.Show();
            
        }


        private bool PrikazFiltera(object obj)
        {
            return ((Lekar)obj).Aktivan;
        }
        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            SelektovaniLekar = dgLekari.SelectedItem as Lekar;
            this.DialogResult = true;
            this.Close();
        }
    }

}
