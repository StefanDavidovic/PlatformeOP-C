using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.TerminiWindow;
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

namespace SF_19_2019_POP2020.Windows.DoktoriProzori
{
    /// <summary>
    /// Interaction logic for DoktoriWindow.xaml
    /// </summary>
    public partial class DoktoriWindow : Window
    {
        ICollectionView view;



        public DoktoriWindow()
        {
            InitializeComponent();


            viewT();




            /*            dgPacijentiZasebno.ItemsSource = view2;
                        dgPacijentiZasebno.IsSynchronizedWithCurrentItem = true;
                        dgPacijentiZasebno.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);*/
        }


        public void viewT()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Lekari);
            view.Filter = CustomFilter;



            dgLekari.ItemsSource = view;
            dgLekari.IsSynchronizedWithCurrentItem = true;


            dgLekari.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
                        if (MessageBox.Show("Da li ste sigurni?", "Potvrda",
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Lekar selektovaniKorisnik = view.CurrentItem as Lekar;

                            if (terminProvera(selektovaniKorisnik) == false)
                            {
                                if (terapijaProvera(selektovaniKorisnik) == false)
                                {
                                    Util.Instance.DeleteDoktor(selektovaniKorisnik.JMBG);
                                    view.Refresh();
                                }
                            }
                        }
        }

        private bool terminProvera(Lekar lekar)
        {
            foreach(Termin termini in Util.Instance.Termini)
            {
                if(termini.LekarID == lekar.ID && termini.Datum > DateTime.Now)
                {
                    MessageBox.Show("Ne mozete obrisati lekara koji ima zakazan termin", "GRESKA");
                    return true;
                }
            }
            return false;
        }


        private bool terapijaProvera(Lekar lekar)
        {
            foreach (Terapija terapije in Util.Instance.Terapije)
            {
                if (terapije.LekarID == lekar.ID && terapije.Aktivan == true)
                {
                    MessageBox.Show("Ne mozete obrisati lekara koji ima poseduje terapije na svoje ime", "GRESKA");
                    return true;
                }
            }
            return false;
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTermin_Click(object sender, RoutedEventArgs e)
        {
            TerminiKodLekaraPick tklp = new TerminiKodLekaraPick();
            tklp.Show();
        }


        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            LekariPick lp = new LekariPick(LekariPick.Stanje.PREUZIMANJE);
            lp.Show();
        }

        private bool PrikazFiltera(object obj)
        {
            return ((Lekar)obj).Aktivan;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Lekar novLekar = new Lekar();
            DoktorAddEdit few = new DoktorAddEdit(novLekar);
       //     Util.Instance.CitanjeEntiteta();
            few.ShowDialog();
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


                    int index = Util.Instance.Lekari.IndexOf(
                        selektovaniKorisnik);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Util.Instance.Lekari[index] = old;
                }
            }
            viewT();
        }
 


    }
}

/*    }

}
*/