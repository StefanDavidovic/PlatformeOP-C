using SF15_2019_POP2020.Models;
using SF15_2019_POP2020.Windows;
using System;
using System.Collections.Generic;
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

namespace SF15_2019_POP2020
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            Util.Instance.CitanjeEntiteta("lekari.txt");

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            AllDoctors window = new AllDoctors();

            this.Hide();
            window.Show();
        }
    }
}
