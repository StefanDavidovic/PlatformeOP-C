using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF15_2019_POP2020.Models
{
    [Serializable]
    public class Korisnik
    {
        private string _korisnickoIme;

        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; }
        }

        private string _ime;

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        private string _prezime;

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        private string _lozinka;

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _jmbg;

        public string JMBG
        {
            get { return _jmbg; }
            set { _jmbg = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private EPol _pol;

        public EPol  Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        private ETipKorisnika _tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public Korisnik()
        {
            
        }

        public override string ToString()
        {
            return "Ja sam " + KorisnickoIme + ". Moje email je:" + Email + ". Tip: " + TipKorisnika; // + ". Moja adresa je " + Adresa.ToString();
        }

        public string KorisnikZaUpisUFajl()
        {
            return KorisnickoIme + ";" + Ime + ";" + Prezime + ";" + JMBG + ";" +
                Email + ";" + Lozinka + ";" + Pol + ";" + TipKorisnika+ ";" + Aktivan;
        }
     
    }
}
