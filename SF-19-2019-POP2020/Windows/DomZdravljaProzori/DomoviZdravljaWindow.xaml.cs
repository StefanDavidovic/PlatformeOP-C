using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Windows.DomZdravljaProzori;
using SF_19_2019_POP2020.Windows.NEPRIJAVLJENIWindow;
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

namespace SF_19_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for DomoviZdravljaWindow.xaml
    /// </summary>
    public partial class DomoviZdravljaWindow : Window
    {
        ICollectionView view;
        public DomoviZdravljaWindow()
        {
            InitializeComponent();

            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);
            view.Filter = CustomFilter;
            dgDomZdravlja.ItemsSource = view;
            dgDomZdravlja.IsSynchronizedWithCurrentItem = true;



            dgDomZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Potvrda",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DomZdravlja selektovaniDomZdravlja = view.CurrentItem as DomZdravlja;

                if (terminProvera(selektovaniDomZdravlja) == false) { 
                    Util.Instance.DeleteDomZdravlja(selektovaniDomZdravlja.Sifra);
                    view.Refresh();
                }
            }
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        private bool CustomFilter(object obj)
        {
            DomZdravlja korisnik = obj as DomZdravlja;
            // Korisnik korisnik1 = (Korisnik)obj;

            if (korisnik.Aktivan)
            {
                if (TxtPretraga.Text != "")
                {
                    if (korisnik.Naziv.Contains(TxtPretraga.Text))
                    {
                        return korisnik.Naziv.Contains(TxtPretraga.Text);
                    }

                }
                else
                    return true;

            }
            return false;
        }

        private bool terminProvera(DomZdravlja dz)
        {
            foreach (Lekar lekari in Util.Instance.Lekari)
            {
                if (lekari.DomZdravljaID == dz.Sifra && lekari.Aktivan == true)
                {
                    MessageBox.Show("Ne mozete obrisati dom zdravlja koji ima instancu", "GRESKA");
                    return true;
                }
            }
            return false;
        }


        private void TxtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }

        private bool PrikazFiltera(object obj)
        {
            return ((DomZdravlja)obj).Aktivan;
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja noviDomZdravlja = new DomZdravlja();
            DomoviZdravljaAddEdit few = new DomoviZdravljaAddEdit(noviDomZdravlja);
  
            few.ShowDialog();
        }

        private void btnDZ_Click(object sender, RoutedEventArgs e)
        {
            DomZdravljaViaAdresa few = new DomZdravljaViaAdresa();
            few.ShowDialog();
        }




        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja selektovaniDomZdravlja = view.CurrentItem as DomZdravlja; //preuzimanje selektovanog 

            if (selektovaniDomZdravlja != null)//ako je elektovan
            {
                DomZdravlja old = (DomZdravlja)selektovaniDomZdravlja.Clone();
                DomoviZdravljaAddEdit few = new DomoviZdravljaAddEdit(selektovaniDomZdravlja,
                    DomoviZdravljaAddEdit.Stanje.IZMENA);
                view.Refresh();
                if (few.ShowDialog() != true) //ako je kliknuo cancel, ponistavaju se izmene nad objektom
                {

                    //pronadjemo indeks selektovanog fakulteta
                    int index = Util.Instance.DomoviZdravlja.IndexOf(
                        selektovaniDomZdravlja);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Util.Instance.DomoviZdravlja[index] = old;
                }
            }
        }

    
}
}
