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
    class PacijentService : IPacijentService
    {
        public void deletePacijent(int id)
        {
            Pacijent k = Util.Instance.Pacijenti.ToList().Find(Pacijent => Pacijent.ID.Equals(id));
            k.Aktivan = false;
            //   if (k == null)
            // throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            updatePacijent(k);
        }

        public void readPacijent()
        {
            Util.Instance.Pacijenti = new ObservableCollection<Pacijent>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Pacijenti";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Pacijenti.Add(new Pacijent
                    {
                        ID = reader.GetInt32(0),
                        Ime = reader.GetString(1),
                        Prezime = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        Email = reader.GetString(4),
                        JMBG = reader.GetString(5),
                        Pol = (EPol)Enum.Parse(typeof(EPol), reader.GetString(6)),
                        Aktivan = reader.GetBoolean(7),
                        AdresaID = reader.GetInt32(8)



                    });


                }
                reader.Close();



            }
        }

        public int savePacijent(object obj)
        {
            Pacijent pacijent = obj as Pacijent;
            Random random = new Random();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into Pacijenti (id,ime,prezime,lozinka,email,jmbg,pol,aktivan,adresa_id)
                                        output inserted.id VALUES (@id,@ime,@prezime,@lozinka,@email,@jmbg,@pol,@aktivan,@adresa_id)";
                command.Parameters.Add(new SqlParameter("id", pacijent.ID = random.Next(1,1000)));
                command.Parameters.Add(new SqlParameter("ime", pacijent.Ime));
                command.Parameters.Add(new SqlParameter("prezime", pacijent.Prezime));
                command.Parameters.Add(new SqlParameter("lozinka", pacijent.Lozinka));
                command.Parameters.Add(new SqlParameter("email", pacijent.Email));
                command.Parameters.Add(new SqlParameter("jmbg", pacijent.JMBG));
                command.Parameters.Add(new SqlParameter("pol", pacijent.Pol));
                command.Parameters.Add(new SqlParameter("aktivan", pacijent.Aktivan));
                command.Parameters.Add(new SqlParameter("adresa_id", pacijent.AdresaID));
                return (int)command.ExecuteScalar();









            }
        }

        public void updatePacijent(object obj)
        {
            Pacijent pacijent = obj as Pacijent;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Pacijenti
                                        SET Aktivan = @Aktivan
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("Aktivan", pacijent.Aktivan = false));
                command.Parameters.Add(new SqlParameter("id", pacijent.ID));

                command.ExecuteNonQuery();
            }
        }
        public void updatePacijent1(object obj)
        {
            Pacijent pacijent = obj as Pacijent;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update Pacijenti
                                        SET ime = @ime,
                                             prezime = @prezime,
                                              lozinka = @lozinka,
                                               email = @email,
                                               pol = @pol,
                                               aktivan = @aktivan,
                                            adresa_id = @adresa_id
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("id", pacijent.ID));
                command.Parameters.Add(new SqlParameter("ime", pacijent.Ime));
                command.Parameters.Add(new SqlParameter("prezime", pacijent.Prezime));
                command.Parameters.Add(new SqlParameter("lozinka", pacijent.Lozinka));
                command.Parameters.Add(new SqlParameter("email", pacijent.Email));
                command.Parameters.Add(new SqlParameter("pol", pacijent.Pol));
                command.Parameters.Add(new SqlParameter("aktivan", pacijent.Aktivan));
                command.Parameters.Add(new SqlParameter("adresa_id", pacijent.AdresaID));

                command.ExecuteScalar();
            }
        }
    }
}
