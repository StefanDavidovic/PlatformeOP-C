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
    class TerminiService : ITerminService
    {
        public void deleteTermin(int broj)
        {
            Termin k = Util.Instance.Termini.ToList().Find(Termin => Termin.Sifra.Equals(broj));
            k.Aktivan = false;
            //   if (k == null)
            // throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            updateTermin(k);
        }

        public void readTermin()
        {
            Util.Instance.Termini= new ObservableCollection<Termin>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Termini where datum >= CAST(CURRENT_TIMESTAMP AS DATE);";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Util.Instance.Termini.Add(new Termin
                    {
                        Sifra = reader.GetInt32(0),
                        LekarID = reader.GetInt32(1),
                        Datum = reader.GetDateTime(2),
                        Status = (EStatusTermina)Enum.Parse(typeof(EStatusTermina), reader.GetString(3)),
                        PacijentID = reader.GetInt32(4),
                        Aktivan = reader.GetBoolean(5)



                    });


                }
                reader.Close();
            }
        }
        public int saveTermin(object obj)
        {
            Termin termin = obj as Termin;
            Random random = new Random();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Termini (id,lekar_id,datum,status,pacijent_id,active)
                                        output inserted.id VALUES (@id,@lekar_id,@datum,@status,@pacijent_id,@active)";
                command.Parameters.Add(new SqlParameter("id", termin.Sifra = random.Next(1,1000)));
                command.Parameters.Add(new SqlParameter("lekar_id", termin.LekarID));
                command.Parameters.Add(new SqlParameter("datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("status", termin.Status));
                command.Parameters.Add(new SqlParameter("pacijent_id", termin.PacijentID));
                command.Parameters.Add(new SqlParameter("active", termin.Aktivan));

                return (int)command.ExecuteScalar();
            }
            //return -1;
        }

        public void updateTermin(object obj)
        {
            Termin t = obj as Termin;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Termini
                                        SET active = @active
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("Active", t.Aktivan = false));
                command.Parameters.Add(new SqlParameter("id", t.Sifra));

                command.ExecuteNonQuery();
            }
        }
        public void updateTermin1(object obj)
        {
            Termin termin = obj as Termin;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update Termini
                                        SET lekar_id = @lekar_id,
                                            datum = @datum,
                                            status = @status,
                                            pacijent_id = @pacijent_id
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("lekar_id", termin.LekarID));
                command.Parameters.Add(new SqlParameter("datum", termin.Datum));
                command.Parameters.Add(new SqlParameter("status", termin.Status));
                command.Parameters.Add(new SqlParameter("pacijent_id", termin.PacijentID));
                command.Parameters.Add(new SqlParameter("id", termin.Sifra));

                command.ExecuteScalar();
            }
        }
    }
}
