using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Models
{
    public class Korisnik: ICloneable, INotifyPropertyChanged
    {
        private string korisnickoIme;
        private string ime;
        private string prezime;
        private string lozinka;
        private string email;
        private string jmbg;
        private Adresa adresa;
        private EPol pol;
        private ETipKorisnika tipKorisnika;
        private bool aktivan;


        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }






        public string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }
            set
            {
                korisnickoIme = value;
                OnPropertyChanged("KorIme");
            }
        }


        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }


        public string JMBG
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }



        public Adresa Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa= value;
                OnPropertyChanged("Adresa");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public EPol Pol
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
                OnPropertyChanged("Pol");
            }
        }

        public ETipKorisnika TipKorisnika
        {
            get
            {
                return tipKorisnika;
            }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("Tip Korisnika");
            }
        }
        public Boolean Aktivan
        {
            get
            {
                return aktivan;
            }
            set
            {
                aktivan = value;
                OnPropertyChanged("Aktivan");
            }
        }


        public override string ToString()
        {
            return KorisnickoIme;
        }

        public string KorisnikZaUpisUFajl()
        {
            return KorisnickoIme + ";" + Ime + ";" + Prezime + ";" + JMBG + ";" +
                Email + ";" + Lozinka + ";" + Pol + ";" + TipKorisnika + ";" + Aktivan;
        }

        public object Clone()
        {
            Korisnik kopija = new Korisnik();

            kopija.ID = ID;
            kopija.KorisnickoIme = KorisnickoIme;
            kopija.Adresa = Adresa;
            kopija.Aktivan = Aktivan;
            kopija.Email = Email;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Pol = Pol;
            kopija.Lozinka = Lozinka;
            kopija.JMBG = JMBG;
         

            return kopija;
        }


        public static ObservableCollection<Korisnik> GetAll()
        {
            var korisnici = new ObservableCollection<Korisnik>();
            using (var conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandText = "SELECT * FROM users WHERE active = 'true';";
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds, "users"); // Izvrsavanje upita

                foreach (DataRow row in ds.Tables["users"].Rows)
                {
                    var k = new Korisnik();
                    k.ID = int.Parse(row["Id"].ToString());
                    k.ime = row["firstname"].ToString();
                    k.Prezime = row["lastname"].ToString();
                    k.Email = row["Email"].ToString();
                    k.korisnickoIme = row["username"].ToString();
                    k.lozinka = row["password"].ToString();
                    k.TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), (row["typeOfUser"].ToString()));
                    k.pol = (EPol)Enum.Parse(typeof(EPol), (row["pol"].ToString()));
                    k.jmbg = row["jmbg"].ToString();
                    k.aktivan = bool.Parse(row["active"].ToString());

                    korisnici.Add(k);
                }
            }
            return korisnici;
        }

        public static Korisnik Create(Korisnik k)
        {
            try
            {
                using (var conn = new SqlConnection(Util.CONNECTION_STRING))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = "INSERT INTO users (username,firstname,typeOfUser,email,Password,active,lastname,jmbg) VALUES (@username,@firstname,@typeOfUser,@email,@Password,@active,@lastname,@jmbg,@active;";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@firstname", k.Ime);
                    cmd.Parameters.AddWithValue("@lastname", k.Prezime);
                    cmd.Parameters.AddWithValue("@Email", k.Email);
                    cmd.Parameters.AddWithValue("@username", k.korisnickoIme);
                    cmd.Parameters.AddWithValue("@Password", k.lozinka);
                    cmd.Parameters.AddWithValue("@typeOfUser", k.TipKorisnika.ToString());
                    cmd.Parameters.AddWithValue("@jmbg", k.jmbg);
                    cmd.Parameters.AddWithValue("@active", k.Aktivan);

                    k.ID = int.Parse(cmd.ExecuteScalar().ToString());
                    //cmd.ExecuteNonQuery();
                }

                Datoteke.Instance.Korisnici.Add(k);
                return k;
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
