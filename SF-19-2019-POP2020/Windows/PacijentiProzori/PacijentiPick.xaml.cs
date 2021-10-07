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

namespace SF_19_2019_POP2020.Windows.PacijentiProzori
{
    /// <summary>
    /// Interaction logic for PacijentiPick.xaml
    /// </summary>
    public partial class PacijentiPick : Window
    {
        public enum Stanje { ADMINISTRACIJA, PREUZIMANJE };
        Stanje stanje;
        ICollectionView view;


        public Pacijent SelektovaniPacijent = null;

        public PacijentiPick(Stanje stanje = Stanje.ADMINISTRACIJA)
        {
            InitializeComponent();
            this.stanje = stanje;

            if (stanje == Stanje.PREUZIMANJE)
            {
                btnAdd.Visibility = System.Windows.Visibility.Collapsed;
                btnDelete.Visibility = System.Windows.Visibility.Collapsed;
                btnUpdate.Visibility = System.Windows.Visibility.Collapsed;
               
            }
            else
            {
                btnPick.Visibility = System.Windows.Visibility.Hidden;
                btnAdresa.Visibility = System.Windows.Visibility.Collapsed;
            }
            view = CollectionViewSource.GetDefaultView(Util.Instance.Pacijenti);
            view.Filter = CustomFilter;
            dgPacijenti.ItemsSource = view;
       //     dgPacijenti.IsSynchronizedWithCurrentItem = true;

            dgPacijenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private bool CustomFilter(object obj)
        {
            Pacijent korisnik = obj as Pacijent;
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

        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("ID") || e.PropertyName.Equals("JMBG") || e.PropertyName.Equals("Lozinka"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            SelektovaniPacijent = dgPacijenti.SelectedItem as Pacijent;
            this.DialogResult = true;
            this.Close();
        }
        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            PacijentPoAdresi lva = new PacijentPoAdresi();
            lva.Show();

        }
    }
}
