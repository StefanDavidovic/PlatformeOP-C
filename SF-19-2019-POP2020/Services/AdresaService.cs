using SF_19_2019_POP2020.Models;
using SF_19_2019_POP2020.MyExceptions;
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
    public class AdresaService:IService
    {
        public void deleteAdresa(int broj)
        {
            Adresa k = Util.Instance.Adrese.ToList().Find(adresa => adresa.SifraAdrese == (broj));

            k.Aktivan = false;

            updateAdresa(k);
        }

        public void readAdrese()
        {
            Util.Instance.Adrese = new ObservableCollection<Adresa>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select * from Adrese";

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Util.Instance.Adrese.Add(new Adresa
                    {
                        SifraAdrese = reader.GetInt32(0),
                        Broj = reader.GetString(1),
                        Ulica = reader.GetString(2),
                        Grad = reader.GetString(3),
                        Drzava = reader.GetString(4),
                        Aktivan = reader.GetBoolean(5)
                    });


                }



                reader.Close();
            }

        }



        public int saveAdresa(Object obj)
        {
            Random rnd = new Random();
            Adresa adresa = obj as Adresa;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Adrese (id,Broj, Ulica, Grad, Drzava,active)
                                        output inserted.id VALUES (@id,@Broj, @Ulica, @Grad, @Drzava,@active)";
                command.Parameters.Add(new SqlParameter("id", adresa.SifraAdrese = rnd.Next(1, 1000)));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("active", adresa.Aktivan));
                return (int)command.ExecuteScalar();
            }
          
            //return -1;
        }

        public void updateAdresa(object obj)
        {
            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Adrese 
                                        SET Active = @Active 
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("Active", adresa.Aktivan=false));
                command.Parameters.Add(new SqlParameter("id", adresa.SifraAdrese));

                command.ExecuteNonQuery();
            }
        }


        public void updateAdresa1(object obj)
        {
            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Adrese 
                                        SET broj = @broj,
                                            ulica = @ulica,
                                            drzava = @drzava,
                                            grad = @grad
                                        where id = @id";

                command.Parameters.Add(new SqlParameter("broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("id", adresa.SifraAdrese));
                command.Parameters.Add(new SqlParameter("ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("grad", adresa.Grad));




                command.ExecuteScalar();
            }
        }
    }
}
