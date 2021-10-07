using SF_19_2019_POP2020.Models;
using SF19_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_19_2019_POP2020.Services
{
    class LekariServis : IUserService
    {
        public void deleteUser(string username)
        {
            Lekar k = Util.Instance.Lekari.ToList().Find(Lekar => Lekar.JMBG.Equals(username));
            k.Aktivan = false;
            //   if (k == null)
            // throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            updateUser(k);
        }

        public void readUsers()
        {
            Util.Instance.Lekari = new ObservableCollection<Lekar>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Doktori";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Util.Instance.Lekari.Add(new Lekar
                    {
                        ID = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        Email = reader.GetString(4),
                        JMBG = reader.GetString(5),
                        Pol = (EPol)Enum.Parse(typeof(EPol), reader.GetString(6)),
                        Aktivan = reader.GetBoolean(7),
                        AdresaID = reader.GetInt32(8),
                        DomZdravljaID = reader.GetInt32(9)





                    });


                }
                reader.Close();
            }
        }
        public int saveUser(object obj)
        {
            Lekar lekar = obj as Lekar;
            Random random = new Random();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into Doktori (id,ime,prezime,lozinka,email,jmbg,pol,aktivan,adresa_id,domZdravlja_id)
                                        output inserted.id VALUES (@id,@ime,@prezime,@lozinka,@email,@jmbg,@pol,@aktivan,@adresa_id,@domZdravlja_id)";
                command.Parameters.Add(new SqlParameter("id", lekar.ID = random.Next(1, 1000)));
                command.Parameters.Add(new SqlParameter("ime", lekar.Ime));
                command.Parameters.Add(new SqlParameter("prezime", lekar.Prezime));
                command.Parameters.Add(new SqlParameter("lozinka", lekar.Lozinka));
                command.Parameters.Add(new SqlParameter("email", lekar.Email));
                command.Parameters.Add(new SqlParameter("jmbg", lekar.JMBG));
                command.Parameters.Add(new SqlParameter("pol", EPol.M));
                command.Parameters.Add(new SqlParameter("aktivan", lekar.Aktivan));
                command.Parameters.Add(new SqlParameter("adresa_id", lekar.AdresaID));
                command.Parameters.Add(new SqlParameter("domZdravlja_id", lekar.DomZdravljaID));

                return (int)command.ExecuteScalar();
            }
        }

        public void updateUser(object obj)
        {
            Lekar lekar = obj as Lekar;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Doktori
                                        SET Aktivan = @Aktivan
                                        where jmbg = @jmbg";

                command.Parameters.Add(new SqlParameter("Aktivan", lekar.Aktivan = false));
                command.Parameters.Add(new SqlParameter("jmbg", lekar.JMBG));

                command.ExecuteNonQuery();
            }
        }
        public void updateUser1(object obj)
        {
            Lekar lekar = obj as Lekar;
          
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update Doktori
                                        SET ime = @ime,
                                             prezime = @prezime,
                                              lozinka = @lozinka,
                                               email = @email,
                                               pol = @pol,
                                               aktivan = @aktivan,
                                            adresa_id = @adresa_id,
                                            domZdravlja_id = @domZdravlja_id
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("id", lekar.ID));
                command.Parameters.Add(new SqlParameter("ime", lekar.Ime));
                command.Parameters.Add(new SqlParameter("prezime", lekar.Prezime));
                command.Parameters.Add(new SqlParameter("lozinka", lekar.Lozinka));
                command.Parameters.Add(new SqlParameter("email", lekar.Email));
                command.Parameters.Add(new SqlParameter("pol", lekar.Pol));
                command.Parameters.Add(new SqlParameter("aktivan", lekar.Aktivan));
                command.Parameters.Add(new SqlParameter("adresa_id", lekar.AdresaID));
                command.Parameters.Add(new SqlParameter("domZdravlja_id", lekar.DomZdravljaID));


                command.ExecuteScalar();
            }
        }
    }
}