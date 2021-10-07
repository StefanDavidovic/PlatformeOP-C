using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SF15_2019_POP2020.Models;
using SF15_2019_POP2020.MyExceptions;

namespace SF15_2019_POP2020.Services
{
    public class UserService : IUserService
    {
        public void deleteUser(string username)
        {
            Korisnik k = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.KorisnickoIme.Equals(username));

            if (k == null)
                throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            k.Aktivan = false;
            Util.Instance.SacuvajEntite("korisnici.txt");
        }

        public void readUsers(string filename)
        {
            Util.Instance.Korisnici = new ObservableCollection<Korisnik>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] korisnikIzFajla = line.Split(';');

                    Enum.TryParse(korisnikIzFajla[6], out EPol pol);
                    Enum.TryParse(korisnikIzFajla[7], out ETipKorisnika tip);

                    Boolean.TryParse(korisnikIzFajla[8], out Boolean aktivan);
                    Korisnik korisnik = new Korisnik
                    {
                        KorisnickoIme = korisnikIzFajla[0],
                        Ime = korisnikIzFajla[1],
                        Prezime = korisnikIzFajla[2],
                        JMBG = korisnikIzFajla[3],
                        Email = korisnikIzFajla[4],
                        Lozinka = korisnikIzFajla[5],
                        Pol = pol,
                        TipKorisnika = tip,
                        Aktivan = aktivan
                        //Aktivan = Convert.ToBoolean(korisnikIzFajla[8])

                    };
                    Util.Instance.Korisnici.Add(korisnik);
                }
            }
        }

        public void saveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Korisnik korisnik in Util.Instance.Korisnici)
                {
                    file.WriteLine(korisnik.KorisnikZaUpisUFajl());
                }
            }
        }


    }
}
