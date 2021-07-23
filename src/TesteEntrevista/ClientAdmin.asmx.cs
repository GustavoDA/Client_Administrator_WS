using System.Web.Services;
using DAL.Entidades;
using BLL.Validacoes;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.ServiceModel;

namespace ClientAdmin
{
    /// <summary>
    /// Descrição resumida de ClientAdmin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    [System.Web.Script.Services.ScriptService]
    public class ClientAdmin : WebService
    {
        private ClienteService _service = new ClienteService();

        [WebMethod]
        public RetornoWS Insert_Client(string nome, string cpf, int tipoCliente, string sexo, int situacaoCliente)
        {
            RetornoWS retornoInsert = _service.Insert_Client(nome, cpf, tipoCliente, sexo, situacaoCliente);

            return retornoInsert;
        }

        [WebMethod]
        public RetornoWS Update_Client(string nome, string cpf, int tipoCliente, string sexo, int situacaoCliente)
        {
            RetornoWS retornoUpdate = _service.Update_Client(nome, cpf, tipoCliente, sexo, situacaoCliente);

            return retornoUpdate;
        }

        [WebMethod]
        public RetornoWS Delete_Client(string cpf)
        {
            RetornoWS retornoDelete = _service.Delete_Client(cpf);
            
            return retornoDelete;
        }

        [WebMethod]
        public List<Cliente> Get_All_Clientes()
        {
            List<Cliente> clientes = _service.Get_All_Clients();

            return clientes;
        }
    }
}
