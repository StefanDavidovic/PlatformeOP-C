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
    class TerapijaService : ITerapijaService
    {
        public void deleteTerapija(int broj)
        {
            Terapija k = Util.Instance.Terapije.ToList().Find(Terapija => Terapija.Sifra.Equals(broj));
            k.Aktivan = false;
            //   if (k == null)
            // throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            updateTerapija(k);
        }

        public void readTerapija()
        {
            Util.Instance.Terapije = new ObservableCollection<Terapija>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Terapije";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Util.Instance.Terapije.Add(new Terapija
                    {
                        Sifra = reader.GetInt32(0),
                        Opis = reader.GetString(1),
                        LekarID = reader.GetInt32(2),
                        PacijentID = reader.GetInt32(3),
                        Aktivan = reader.GetBoolean(4)
                    });


                }
                reader.Close();
            }
        }

        public int saveTerapija(object obj)
        {
            Terapija terapija = obj as Terapija;
            Random random = new Random();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Terapije (id,opis,lekar_id,pacijent_id,aktivan)
                                        output inserted.id VALUES (@id,@opis,@lekar_id,@pacijent_id,@aktivan)";
                command.Parameters.Add(new SqlParameter("id", terapija.Sifra = random.Next(1,1000)));
                command.Parameters.Add(new SqlParameter("opis", terapija.Opis));
                command.Parameters.Add(new SqlParameter("lekar_id", terapija.LekarID));
                command.Parameters.Add(new SqlParameter("pacijent_id", terapija.PacijentID));
                command.Parameters.Add(new SqlParameter("aktivan", terapija.Aktivan));

                return (int)command.ExecuteScalar();
            }
            //return -1;
        }

        public void updateTerapija(object obj)
        {
            Terapija t = obj as Terapija;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Terapije
                                        SET Aktivan = @Aktivan
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("Aktivan", t.Aktivan = false));
                command.Parameters.Add(new SqlParameter("id", t.Sifra));

                command.ExecuteNonQuery();
            }
        }

        public void updateTerapija1(object obj)
        {
            Terapija terapija = obj as Terapija;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update Terapije
                                        SET opis = @opis,
                                            lekar_id = @lekar_id,
                                            pacijent_id = @pacijent_id
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("opis", terapija.Opis));
                command.Parameters.Add(new SqlParameter("lekar_id", terapija.LekarID));
                command.Parameters.Add(new SqlParameter("pacijent_id", terapija.PacijentID));
                command.Parameters.Add(new SqlParameter("id", terapija.Sifra));


                command.ExecuteScalar();
            }
        }
    }
}