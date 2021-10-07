using SF_19_2019_POP2020.Models;
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
    /// Interaction logic for PrikazLekaraDz.xaml
    /// </summary>
    public partial class PrikazLekaraDz : Window
    {
        ICollectionView view;
        public PrikazLekaraDz(int sifraDz)
        {
            InitializeComponent();
            InitializeComponent();
            ObservableCollection<Lekar> pac2 = readDomZdravlja(sifraDz);

            view = CollectionViewSource.GetDefaultView(pac2);
            dgDomZdravlja.ItemsSource = view;
            dgDomZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
        private void DGL_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("ID") || e.PropertyName.Equals("JMBG") || e.PropertyName.Equals("Lozinka") || e.PropertyName.Equals("AdresaID") || e.PropertyName.Equals("DomZdravljaID"))
                e.Column.Visibility = Visibility.Collapsed;
        }
        public ObservableCollection<Lekar> readDomZdravlja(int sifraAdrese)
        {
            ObservableCollection<Lekar> pac = new ObservableCollection<Lekar>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select id,ime,prezime,email,jmbg from Doktori d where domZdravlja_id =" + sifraAdrese +"and aktivan = 1";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    pac.Add(new Lekar
                    {
                        ID = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Email = reader.GetString(3),
                        JMBG = reader.GetString(4)
                    });


                }



                reader.Close();

                return pac;
            }
        }
    }
}
