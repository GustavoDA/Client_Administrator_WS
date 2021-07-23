using DAL.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositorio
{
    public class ClienteRepositorio
    {
        public RetornoWS Insert_Client(Cliente clienteIncluido)
        {
            List<ErroValidacao> erros = new List<ErroValidacao>();
            RetornoWS response = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INS_CLIENTE", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = clienteIncluido.nome;
                        cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = clienteIncluido.cpf;
                        cmd.Parameters.Add("@Tipo_Cliente", SqlDbType.Int).Value = clienteIncluido.tipoCliente;
                        cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = clienteIncluido.sexo;
                        cmd.Parameters.Add("@Situacao_Cliente", SqlDbType.Int).Value = clienteIncluido.situacaoCliente;

                        conn.Open();

                        if (cmd.ExecuteNonQuery() > 0)
                            response = new RetornoWS(true, erros);
                        else
                        {
                            erros.Add(new ErroValidacao { Message = "Ocorreu um erro inesperado ao gravar o cliente" });
                            response = new RetornoWS(false, erros);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                erros.Add(new ErroValidacao { Message = e.Message });
                response = new RetornoWS(false, erros);
            }

            return response;
        }

        public RetornoWS Update_Client(Cliente clienteAtualizado)
        {
            List<ErroValidacao> erros = new List<ErroValidacao>();

            RetornoWS response = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ALT_CLIENTE", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = clienteAtualizado.nome;
                        cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = clienteAtualizado.cpf;
                        cmd.Parameters.Add("@Tipo_Cliente", SqlDbType.Int).Value = clienteAtualizado.tipoCliente;
                        cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = clienteAtualizado.sexo;
                        cmd.Parameters.Add("@Situacao_Cliente", SqlDbType.Int).Value = clienteAtualizado.situacaoCliente;

                        conn.Open();

                        if (cmd.ExecuteNonQuery() > 0)
                            response = new RetornoWS(true, erros);
                        else
                        {
                            erros.Add(new ErroValidacao { Message = "Ocorreu um erro inesperado ao gravar o cliente" });
                            response = new RetornoWS(false, erros);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                List<ErroValidacao> exception = new List<ErroValidacao>();
                exception.Add(new ErroValidacao { Message = e.Message });
                response = new RetornoWS(false, exception);
            }

            return response;
        }

        public RetornoWS Delete_Client(string cpf)
        {
            RetornoWS response = null;

            List<ErroValidacao> erros = new List<ErroValidacao>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DEL_CLIENTE", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = cpf;
                        conn.Open();

                        if (cmd.ExecuteNonQuery() > 0)
                            response = new RetornoWS(true, erros);
                        else
                        {
                            erros.Add(new ErroValidacao { Message = "Ocorreu um erro inesperado ao gravar o cliente" });
                            response = new RetornoWS(false, erros);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                erros.Add(new ErroValidacao { Message = e.Message });
                response = new RetornoWS(false, erros);
            }


            return response;
        }

        public List<Cliente> Get_All_Clientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CON_CLIENTE", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    clientes.Add(new Cliente(dr[0] != DBNull.Value ? dr[0].ToString() : "", dr[1] != DBNull.Value ? dr[1].ToString() : "", dr[2] != DBNull.Value ? Convert.ToInt32(dr[2].ToString()) : 0, dr[3] != DBNull.Value ? dr[3].ToString() : "", dr[4] != DBNull.Value ? Convert.ToInt32(dr[4].ToString()) : 0));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return clientes;
        }

        public bool Existe_CPF_Cadastrado(string cpf)
        {
            bool response = true;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CON_EXISTENCIA_CLIENTE", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@CPF", SqlDbType.VarChar).Value = cpf;
                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToInt32(dr[0].ToString()) == 0)
                                    response = false;
                            }
                        }

                    }
                }
            }

            return response;
        }
    }
}