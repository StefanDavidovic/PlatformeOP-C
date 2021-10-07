using SF_19_2019_POP2020.Windows;
using SF_19_2019_POP2020.Windows.LekariWindowProfil;
using SF_19_2019_POP2020.Windows.NEPRIJAVLJENIWindow;
using SF_19_2019_POP2020.Windows.PacijentWindowi;
using SF19_2019_POP2020;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SF_19_2019_POP2020
{
    public partial class Login : Window
    {
        public Login()
        {
            Util.Instance.CitanjeEntiteta();
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(Util.CONNECTION_STRING);
            try
            {


                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM pacijenti WHERE jmbg=@jmbg AND lozinka=@lozinka and aktivan=1";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@jmbg", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@lozinka", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    PacijentGlavna dashboard = new PacijentGlavna(txtUsername.Text);
                    dashboard.Show();
                    this.Close();
                }
                if (count != 1)
                {
                    String query1 = "SELECT COUNT(1) FROM doktori WHERE jmbg=@jmbg AND lozinka=@lozinka and aktivan=1";
                    SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
                    sqlCmd1.CommandType = CommandType.Text;
                    sqlCmd1.Parameters.AddWithValue("@jmbg", txtUsername.Text);
                    sqlCmd1.Parameters.AddWithValue("@lozinka", txtPassword.Password);
                    int count1 = Convert.ToInt32(sqlCmd1.ExecuteScalar());
                    if (count1 == 1)
                    {
                        LekarGlavna dashboard = new LekarGlavna(txtUsername.Text);
                        dashboard.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect.");
                    }

                }
            
                if(txtUsername.Text.Equals("admin") && txtPassword.Password.Equals("admin"))
                {
                    HomeWindow dashboard = new HomeWindow();
                    dashboard.Show();
                    this.Close();
                }
       


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Pacijent novKorisnik = new Pacijent();
            RegistracijaPacijent dashboard = new RegistracijaPacijent();
            dashboard.Show();
            this.Close();

        }

        private void btnNeprijavljeni_Click(object sender, RoutedEventArgs e)
        {
            Neregistrovani dashboard = new Neregistrovani();
            dashboard.Show();
            this.Close();

        }
    }
}