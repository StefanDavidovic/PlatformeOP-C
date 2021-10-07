using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SF19_2019_POP2020.Models
{
    public sealed class Util
    {
        public static string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly Util instance = new Util();
        IUserService _userService;
        IUserService _doctorService;
        IPacijentService _pacijentService;
        IService _adresaService;
        IDomZdravljaServis _domZdravljaServis;
        IUserService _lekariServis;
        ITerapijaService _terapijaService;
        ITerminService _terminService;
        Random _random;

        private Util()
        {
            _adresaService = new AdresaService();
            _domZdravljaServis = new DomZdravljaService();
            _pacijentService = new PacijentService();
            _lekariServis = new LekariServis();
            _terapijaService = new TerapijaService();
            _terminService = new TerminiService();
            _random = new Random();
        }
        static Util()
        {

        }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<Lekar> Lekari { get; set; }

        public ObservableCollection<Pacijent> Pacijenti { get; set; }


        public ObservableCollection<Adresa> Adrese { get; set; }
        public ObservableCollection<DomZdravlja> DomoviZdravlja { get; set; }

        public ObservableCollection<Terapija> Terapije { get; set; }

        public ObservableCollection<Termin> Termini { get; set; }
        public Random Random { get;  set; }

        public void Initialize()
        {
            Korisnici = new ObservableCollection<Korisnik>();
            Adrese = new ObservableCollection<Adresa>();
            Lekari = new ObservableCollection<Lekar>();
            DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            Pacijenti = new ObservableCollection<Pacijent>();
            Lekari = new ObservableCollection<Lekar>();
            Terapije = new ObservableCollection<Terapija>();
            Termini = new ObservableCollection<Termin>();
            Random = new Random();

        }



        public int SacuvajEntitet(Object obj)
        {
            if (obj is Korisnik)
            {
                return _userService.saveUser(obj);
            }
            else if(obj is Adresa)
            {
                return _adresaService.saveAdresa(obj);
            }
            else if(obj is DomZdravlja)
            {
                return _domZdravljaServis.saveDomZdravlja(obj);
            }
            else if(obj is Pacijent)
            {
                return _pacijentService.savePacijent(obj);
            }
            else if (obj is Lekar)
            {
                return _lekariServis.saveUser(obj);
            }
            else if(obj is Terapija)
            {
                return _terapijaService.saveTerapija(obj);
            }
            else if(obj is Termin)
            {
                return _terminService.saveTermin(obj);
            }

            return -1;
        }

        public void CitanjeEntiteta()
        {



                _adresaService.readAdrese();
            _domZdravljaServis.readDomZdravlja();
            _pacijentService.readPacijent();
            _lekariServis.readUsers();
            _terapijaService.readTerapija();
            _terminService.readTermin();
        }

        public void DeleteUser(string username)
        {
            _userService.deleteUser(username);
        }

        public void DeleteDomZdravlja(int id)
        {
            _domZdravljaServis.deleteDomZdravlja(id);

        }

        public void DeleteAdresa(int id)
        {
            _adresaService.deleteAdresa(id);
        }
        public void DeletePacijent(int id)
        {
            _pacijentService.deletePacijent(id);
        }
        public void DeleteDoktor(string username)
        {
            _lekariServis.deleteUser(username);
        }
        
        public void DeleteTerapija(int id)
        {
            _terapijaService.deleteTerapija(id);
        }

        public void DeleteTermin(int id)
        {
            _terminService.deleteTermin(id);
        }

        public void UpdateUser(Korisnik korisnik)
        {
            _userService.updateUser1(korisnik);
        }

        public void updateAdresa(Adresa adresa)
        {
            _adresaService.updateAdresa1(adresa);
        }

        public void updateDomZdravlja(DomZdravlja domZdravlja)
        {
            _domZdravljaServis.updateDomZdravlja1(domZdravlja);
        }

        public void updatePacijent(Pacijent pacijent)
        {
            _pacijentService.updatePacijent1(pacijent);
        }

        public void updateDoktor(Lekar lekar)
        {
            _lekariServis.updateUser1(lekar);
        }

        public void updateTerapija(Terapija terapija)
        {
            _terapijaService.updateTerapija1(terapija);
        }

        public void updateTermin(Termin termin)
        {
            _terminService.updateTermin1(termin);
        }

        public bool proveriAdresu(int id)
        {
            foreach(Adresa adresa in Adrese)
            {
                if (adresa.SifraAdrese.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool proveriDZ(int id)
        {
            foreach (DomZdravlja dz in DomoviZdravlja)
            {
                if (dz.Sifra.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool proveriLekara(int id)
        {
            foreach (Lekar l in Lekari)
            {
                if (l.ID.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }


        public bool proveriPacijenta(int id)
        {
            foreach (Pacijent p in Pacijenti)
            {
                if (p.ID.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }
    }

}

