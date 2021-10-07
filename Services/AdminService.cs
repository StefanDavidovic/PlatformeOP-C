using SF15_2019_POP2020.Services;
using SF15_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace SF15_2019_POP2020.Services
{
    public class AdminService : IUserService

    {
        public void deleteUser(string username)
        {

        }

        public void readUsers(string filename)
        {
            Util.Instance.Lekari = new ObservableCollection<Korisnik>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] lekarIzFajla = line.Split(';');

                    Korisnik korisnik = Util.Instance.Korisnici.ToList().Find(kori => kori.KorisnickoIme.Equals(lekarIzFajla[1]));
                    //Korisnik korisnik = NadjiKorisnika(lekarIzFajla[1]);
                    Lekar lekar = new Lekar
                    {
                        DomZdravlja = lekarIzFajla[0],
                        Korisnicko = korisnik
                    };
                    lekar.Aktivan = korisnik.Aktivan;
                    lekar.TipKorisnika = korisnik.TipKorisnika;
                    lekar.Ime = korisnik.Ime;
                    lekar.Prezime = korisnik.Prezime;
                    lekar.KorisnickoIme = korisnik.KorisnickoIme;
                    lekar.Lozinka = korisnik.Lozinka;
                    lekar.Pol = korisnik.Pol;
                    lekar.Email = korisnik.Email;
                    lekar.JMBG = korisnik.JMBG;

                    Util.Instance.Lekari.Add(lekar);
                }
            }
        }

        public void saveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Lekar lekar in Util.Instance.Lekari)
                {
                    file.WriteLine(lekar.LekarUpisUFajl());
                }
            }
        }


    }
}
