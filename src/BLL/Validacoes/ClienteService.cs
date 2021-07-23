using DAL.Entidades;
using DAL.Repositorio;
using System.Collections.Generic;

namespace BLL.Validacoes
{
    public class ClienteService
    {
        ClienteRepositorio _repo = new ClienteRepositorio();

        public RetornoWS Insert_Client(string nome, string cpf, int tipoCliente, string sexo, int situacaoCliente)
        {
            RetornoWS response = null;

            Cliente clienteIncluido = new Cliente(nome, cpf, tipoCliente, sexo, situacaoCliente);

            List<ErroValidacao> erros = clienteIncluido.Validar_Cliente();

            if (erros.Count > 0)
                return response = new RetornoWS(false, erros);

            if (_repo.Existe_CPF_Cadastrado(clienteIncluido.cpf))
            {
                erros.Add(new ErroValidacao { Message = "CPF Já existente na base" });
                return response = new RetornoWS(false, erros);
            }

            response = _repo.Insert_Client(clienteIncluido);

            return response;
        }

        public RetornoWS Update_Client(string nome, string cpf, int tipoCliente, string sexo, int situacaoCliente)
        {
            RetornoWS response = null;

            Cliente clienteAtualizado = new Cliente(nome, cpf, tipoCliente, sexo, situacaoCliente);

            List<ErroValidacao> erros = clienteAtualizado.Validar_Cliente();

            if (erros.Count > 0)
                return response = new RetornoWS(false, erros);

            if (_repo.Existe_CPF_Cadastrado(clienteAtualizado.cpf))
            {
                response = _repo.Update_Client(clienteAtualizado);
            }
            else
            {
                erros.Add(new ErroValidacao { Message = "CPF não encontrado" });
                response = new RetornoWS(false, erros);
            }

            return response;
        }

        public RetornoWS Delete_Client(string cpf)
        {
            RetornoWS response = null;

            List<ErroValidacao> erros = new List<ErroValidacao>();

            if (_repo.Existe_CPF_Cadastrado(cpf))
            {
                response = _repo.Delete_Client(cpf);
            }
            else
            {
                erros.Add(new ErroValidacao { Message = "CPF não encontrado" });
                response = new RetornoWS(false, erros);
            }

            return response;
        }

        public List<Cliente> Get_All_Clients()
        {
            return _repo.Get_All_Clientes();
        }
    }
}
