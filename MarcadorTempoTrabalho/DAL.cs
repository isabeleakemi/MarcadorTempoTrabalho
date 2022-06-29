using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcadorTempoTrabalho
{
    public class DAL
    {
        static string serverName = "127.0.0.1";                                          //localhost
        static string port = "5432";                                                            //porta default
        static string userName = "postgres";                                               //nome do administrador
        static string password = "1234";                                             //senha do administrador
        static string databaseName = "marcador_tempo";                                       //nome do banco de dados
        NpgsqlConnection pgsqlConnection = null;
        string connString = null;

        public DAL()
        {
            connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                serverName, port, userName, password, databaseName);
        }

        public void Salvar(int idMarcador, string descricao)
        {

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdInserir = "INSERT INTO marcador (id_marcador, descricao) VALUES (@id_marcador, @descricao)";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);
                        pgsqlcommand.Parameters.AddWithValue("descricao", descricao);

                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //pgsqlConnection.Close();
            }
        }

        public void Excluir(int idMarcador)
        {

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdExcluir = "delete from marcador where id_marcador = @id_marcador";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdExcluir, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);

                        pgsqlcommand.ExecuteNonQuery();
                    }

                    string cmdExcluirTempos = "delete from tempo where id_marcador = @id_marcador";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdExcluirTempos, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);

                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SalvarTempo(int idMarcador, DateTime horaInicio)
        {

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdInserir = "INSERT INTO tempo (id_marcador, hora_inicio) VALUES (@id_marcador, @hora_inicio)";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);
                        pgsqlcommand.Parameters.AddWithValue("hora_inicio", horaInicio);

                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarTempo(int idMarcador, DateTime horaFinal)
        {

            try
            {
                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string cmdAtualizar = "update tempo set hora_final = @hora_final where id_marcador = @id_marcador and hora_final is null";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualizar, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);
                        pgsqlcommand.Parameters.AddWithValue("hora_final", horaFinal);

                        pgsqlcommand.ExecuteNonQuery();
                    }

                    string cmdAtualizarTempoTotal = "update tempo set tempo_total = (hora_final - hora_inicio) where id_marcador = @id_marcador and tempo_total is null";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualizarTempoTotal, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);

                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObterUltimoId()
        {

            try
            {
                var id = 0;

                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string query = "select id_marcador from marcador order by id_marcador desc limit 1";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, pgsqlConnection))
                    {
                        using (NpgsqlDataReader reader = pgsqlcommand.ExecuteReader())
                        {
                            if (reader.Read())
                                id = int.Parse(reader["id_marcador"].ToString());
                        }
                    }

                    return (id + 1).ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObterMarcadores()
        {
            try
            {
                var dt = new DataTable();

                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string query = "select * from marcador order by id_marcador";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, pgsqlConnection))
                    {
                        adapter.Fill(dt);
                    }

                    return dt;
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObterTempoTotal(int idMarcador)
        {

            try
            {
                var tempoTotal = "";

                using (NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connString))
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();

                    string query = "select sum(tempo_total) as total from tempo where id_marcador = @id_marcador";

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, pgsqlConnection))
                    {
                        pgsqlcommand.Parameters.AddWithValue("id_marcador", idMarcador);

                        using (NpgsqlDataReader reader = pgsqlcommand.ExecuteReader())
                        {
                            if (reader.Read())
                                tempoTotal = reader["total"].ToString();
                        }
                    }

                    return tempoTotal;
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
