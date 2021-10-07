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
    class DomZdravljaService : IDomZdravljaServis
    {
        public void deleteDomZdravlja(int broj)
        {
            DomZdravlja k = Util.Instance.DomoviZdravlja.ToList().Find(DomZdravlja => DomZdravlja.Sifra.Equals(broj));
            k.Aktivan = false;
            //   if (k == null)
            // throw new UserNotFoundException($"Ne postoji korisnik sa korisnickim imenom {username}");
            updateDomZdravlja(k);
        }

        public void readDomZdravlja()
        {
            Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from domZdravlja";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Util.Instance.DomoviZdravlja.Add(new DomZdravlja
                    {
                        Sifra = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        AdresaID = reader.GetInt32(2),
                        Aktivan = reader.GetBoolean(3)
                    });


                }



                reader.Close();
            }
        }

        public int saveDomZdravlja(object obj)
        {
            DomZdravlja domZdravlja = obj as DomZdravlja;
            Random rnd = new Random();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.DomZdravlja (id,Naziv,adresa_id,active)
                                        output inserted.id VALUES (@id,@Naziv,@adresa_id,@active)";
                command.Parameters.Add(new SqlParameter("id", domZdravlja.Sifra = rnd.Next(1,1000)));
                command.Parameters.Add(new SqlParameter("Naziv", domZdravlja.Naziv));
                command.Parameters.Add(new SqlParameter("adresa_id", domZdravlja.AdresaID));
                command.Parameters.Add(new SqlParameter("active", domZdravlja.Aktivan));

                return (int)command.ExecuteScalar();
            }
            //return -1;
        }

        public void updateDomZdravlja(object obj)
        {
            DomZdravlja domZdravlja = obj as DomZdravlja;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.DomZdravlja
                                        SET Active = @Active
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("Active", domZdravlja.Aktivan=false));
                command.Parameters.Add(new SqlParameter("id", domZdravlja.Sifra));

                command.ExecuteNonQuery();
            }
        }

        public void updateDomZdravlja1(object obj)
        {

            DomZdravlja domZdravlja = obj as DomZdravlja;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update DomZdravlja 
                                        SET naziv = @naziv,
                                            adresa_id = @adresa_id
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("naziv", domZdravlja.Naziv));
                command.Parameters.Add(new SqlParameter("adresa_id", domZdravlja.AdresaID));
                command.Parameters.Add(new SqlParameter("id", domZdravlja.Sifra));


                command.ExecuteScalar();
            }

        }
    }
}
