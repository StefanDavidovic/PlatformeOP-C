using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Services;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020
{
    //SLUZI SAMO ZA PODATKE DA UCITAVA IZ LISTE
    class Aplikacija
    {
        public ObservableCollection<Korisnik> Korisnici { get; set; }

        public ObservableCollection<Korisnik> KorisniciPacijenti { get; set; }  //Ovo je za sad ovako implementirano, promenicu kasnije
        public ObservableCollection<Pacijent> Pacijenti { get; set; }
        public ObservableCollection<Lekar> Lekari { get; set; }

        public ObservableCollection<DomZdravlja> DomoviZdravlja { get; set; }

        public ObservableCollection<Termin> Termini { get; set; }

        public ObservableCollection<Adresa> Adrese { get; set; }

        public ObservableCollection<Terapija> Terapije { get; set; }

        private static Aplikacija instance = new Aplikacija();
        IService _adresaService;


        public static Aplikacija Instance
        {
            get
            {
                return instance;
            }
        }

        private Aplikacija()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            Termini = new ObservableCollection<Termin>();
            Adrese = new ObservableCollection<Adresa>();
            Terapije = new ObservableCollection<Terapija>();
            Pacijenti = new ObservableCollection<Pacijent>();;
            Lekari = new ObservableCollection<Lekar>();
            KorisniciPacijenti = new ObservableCollection<Korisnik>();
            _adresaService = new AdresaService();
            PopuniPodatke();
            _adresaService.readAdrese();
        }




        private void PopuniPodatke()
        {  


            //ZABETONIRANI PODACI
            Korisnik korisnik1 = new Korisnik
            {
                Aktivan = true,
                Email = "imejl@gmail.com",
                Ime = "Perica",
                JMBG = "2313212",
                KorisnickoIme = "perica123",
                Lozinka = "perica11",
                Pol = EPol.M,
                Prezime = "Peric",
                //SifraAdrese = adresa,
                TipKorisnika = ETipKorisnika.PACIJENT



            };
            KorisniciPacijenti.Add(korisnik1);

            Pacijent pacijent4 = new Pacijent
            {
                //Korisnicko = korisnik1
            };
            Pacijenti.Add(pacijent4);



            Korisnik korisnik2 = new Korisnik
            {
                Aktivan = true,
                Email = "imejl@gmail.com",
                Ime = "jovica",
                JMBG = "2313212",
                KorisnickoIme = "jovica123",
                Lozinka = "perica11",
                Pol = EPol.M,
                Prezime = "Peric",
                //SifraAdrese = "23213",
                TipKorisnika = ETipKorisnika.PACIJENT



            };
            KorisniciPacijenti.Add(korisnik2);

            Korisnik korisnik3 = new Korisnik
            {
                Aktivan = true,
                Email = "ado@gmail.com",
                Ime = "Ado",
                JMBG = "233213",
                KorisnickoIme = "adminat",
                Lozinka = "adminat11",
                Pol = EPol.M,
                Prezime = "Adminic",
                //SifraAdrese = "34223423",
                TipKorisnika = ETipKorisnika.ADMINISTRATOR



            };

            Korisnik korisnik4 = new Korisnik
            {
                Aktivan = true,
                Email = "ado@gmail.com",
                Ime = "Leko",
                JMBG = "2332213",
                KorisnickoIme = "leko",
                Lozinka = "leko123",
                Pol = EPol.M,
                Prezime = "Lekaric",
                //SifraAdrese = "21323",
                TipKorisnika = ETipKorisnika.LEKAR



            };

            Korisnici.Add(korisnik4);




            Pacijent pacijent = new Pacijent
            {
                //Korisnicko = korisnik2,
                //Termini = new ObservableCollection<string>(),
            };

            Pacijenti.Add(pacijent);

            Korisnici.Add(korisnik3);



            Termin termin = new Termin
            {
                Aktivan = true,
                Datum = new DateTime(2020, 5, 1, 8, 30, 52),
           //     Lekar = lekar,
                //Pacijent = pacijent,
              //  Sifra = "232432432",
                Status = EStatusTermina.SLOBODAN


            };
            Termini.Add(termin);




            Terapija terapija = new Terapija
            {
                Aktivan = true,
                Opis = "Sve okej",
          //      Sifra = "332423",
           //     Lekar = lekar,
           //     Pacijent = pacijent
            };

            Terapije.Add(terapija);


/*            DomZdravlja domZdravlja = new DomZdravlja
            {
                Naziv = "DOM ZDRAVLJA SECANJ",
                Sifra = "255366",
               // Adresa = adresa,
                Aktivan = true
            };
            DomoviZdravlja.Add(domZdravlja);*/

        }


        

        private Terapija NadjiTerapiju(string sifra)
        {
            foreach (Terapija terapija in Terapije)
            {
                if (terapija.Sifra.Equals(sifra))
                {
                    return terapija;
                }

            }

            return null;


        }

    }
}
