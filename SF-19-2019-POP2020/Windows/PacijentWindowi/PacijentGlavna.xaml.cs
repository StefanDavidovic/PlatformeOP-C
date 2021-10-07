using SF_19_2019_POP2020.Windows.DoktoriProzori;
using SF_19_2019_POP2020.Windows.NEPRIJAVLJENIWindow;
using SF_19_2019_POP2020.Windows.PacijentiProzori;
using SF_19_2019_POP2020.Windows.Pretrage;
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

namespace SF_19_2019_POP2020.Windows.PacijentWindowi
{
    /// <summary>
    /// Interaction logic for PacijentGlavna.xaml
    /// </summary>
    public partial class PacijentGlavna : Window
    {
        ICollectionView view;
        ICollectionView view2;
        ICollectionView view3;
        Pacijent pacijent;
        ObservableCollection<Pacijent> pac2;

        public PacijentGlavna(string jmbg)
        {
            InitializeComponent();
            pac2 = nadjiPacijenta(jmbg);

            pacijent = nadjiPacijenta2(jmbg);

            ObservableCollection<Terapija> ter2 = nadjiPacijentauTerapiji(pacijent.ID);


            //   Pacijent korisnik = nadjiPacijenta(jmbg);


            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(pac2);

            dgPacijenti.ItemsSource = view;
            dgPacijenti.IsSynchronizedWithCurrentItem = true;

            dgPacijenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);



            view2 = CollectionViewSource.GetDefaultView(ter2);

            dgTerapije.ItemsSource = view2;
            dgTerapije.IsSynchronizedWithCurrentItem = true;

            dgTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            InitializeComponent();
        }


        public ObservableCollection<Pacijent> nadjiPacijenta(string jmbg)
        {
            ObservableCollection<Pacijent> pac = new ObservableCollection<Pacijent>();

            foreach (Pacijent pacijent in Util.Instance.Pacijenti)
            {
                if (pacijent.JMBG.Equals(jmbg))
                {

                    pac.Add(pacijent);
                }
               
            }
            return pac;

        }


        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("PacijentID"))
                e.Column.Visibility = Visibility.Collapsed;
        }



        public Pacijent nadjiPacijenta2(string jmbg)
        {
            foreach (Pacijent pacijent in Util.Instance.Pacijenti)
            {
                if (pacijent.JMBG.Equals(jmbg))
                {

                    return pacijent;
                }

            }
            return null;

        }

        public ObservableCollection<Terapija> nadjiPacijentauTerapiji(int id)
        {
            ObservableCollection<Terapija> pac = new ObservableCollection<Terapija>();

            foreach (Terapija terapija in Util.Instance.Terapije)
            {
                if (terapija.PacijentID == id)
                {

                    pac.Add(terapija);
                }

            }
            return pac;

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


/*                    int index = Util.Instance.Pacijenti.IndexOf(
                        selektovaniKorisnik);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    Util.Instance.Pacijenti[index] = old;*/


                    int index2 = pac2.IndexOf(
                selektovaniKorisnik);
                    //vratimo vrednosti njegovih atributa na stare vrednosti, jer je izmena ponistena
                    pac2[index2] = old;


                }
            }
        }


        private void btnInstitucija_Click(object sender, RoutedEventArgs e)
        {

            DomZdravljaViaAdresa odzm = new DomZdravljaViaAdresa();
            odzm.Show();
        }

        private void btnLekar_Click(object sender, RoutedEventArgs e)
        {

            LekaraViaDomZdravlja odzm = new LekaraViaDomZdravlja();
            odzm.Show();
        }


        private void btnDoktori_Click(object sender, RoutedEventArgs e)
        {

            LekariPick odzm = new LekariPick(LekariPick.Stanje.GLEDANJE);
            odzm.Show();
        }




        private void btnTermin_Click(object sender, RoutedEventArgs e)
        {

            TerminiZaPacijenta odzm = new TerminiZaPacijenta(pacijent);
            odzm.Show();
        }


    }




}
