using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.PacijentiProzori;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PacijentiWindow.xaml
    /// </summary>
    public partial class PacijentiWindow : Window
    {
        ICollectionView view;
        ICollectionView view2;


        public PacijentiWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Util.Instance.Pacijenti);
            view.Filter = CustomFilter;

            dgPacijenti.ItemsSource = view;
            dgPacijenti.IsSynchronizedWithCurrentItem = true;

            dgPacijenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
           // view2 = CollectionViewSource.GetDefaultView(Util.Instance.Pacijenti);
           // view2.Filter = PrikazFiltera;
/*            dgPacijentiZasebno.ItemsSource = view2;
            dgPacijentiZasebno.IsSynchronizedWithCurrentItem = true;
            dgPacijentiZasebno.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);*/
        }
        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
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



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Potvrda",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Pacijent selektovaniKorisnik = view.CurrentItem as Pacijent;
                if(terminProvera(selektovaniKorisnik) == false)
                {
                    if (terapijaProvera(selektovaniKorisnik) == false)
                    {
                        Util.Instance.DeletePacijent(selektovaniKorisnik.ID);
                        view.Refresh();
                    }
                }

            }
           
        }





        private bool terminProvera(Pacijent pac)
        {
            foreach (Termin termini in Util.Instance.Termini)
            {
                if (termini.PacijentID == pac.ID && termini.Datum > DateTime.Now)
                {
                    MessageBox.Show("Ne mozete obrisati pacijenta koji ima zakazan termin", "GRESKA");
                    return true;
                }
            }
            return false;
        }

        private bool terapijaProvera(Pacijent pacijent)
        {
            foreach (Terapija terapije in Util.Instance.Terapije)
            {
                if (terapije.PacijentID == pacijent.ID && terapije.Aktivan == true)
                {
                    MessageBox.Show("Ne mozete obrisati pacijenta koji ima poseduje terapije na svoje ime", "GRESKA");
                    return true;
                }
            }
            return false;
        }





        private bool PrikazFiltera(object obj)
        {
            return ((Pacijent)obj).Aktivan;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            PacijentiPick pp = new PacijentiPick(PacijentiPick.Stanje.PREUZIMANJE);
            pp.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Pacijent novPacijent = new Pacijent();
            PacijentAddEdit few = new PacijentAddEdit(novPacijent);
        //    Util.Instance.CitanjeEntiteta();
            few.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Pacijent selektovaniKorisnik = view.CurrentItem as Pacijent; //preuzimanje selektovane adrese

            if (selektovaniKorisnik != null)
            {
                Pacijent old = (Pacijent)selektovaniKorisnik.Clone();
                PacijentAddEdit few = new PacijentAddEdit(selektovaniKorisnik,
                    PacijentAddEdit.Stanje.IZMENA);
                if (few.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {

   
                    int index = Util.Instance.Pacijenti.IndexOf(
                        selektovaniKorisnik);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Util.Instance.Pacijenti[index] = old;
                }
            }
        }







    }
}
