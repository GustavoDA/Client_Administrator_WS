using DAL.Enum;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DAL.Entidades
{
    public class Cliente
    {
        public string nome { get; set; }
        public string cpf { get; set; }
        public int tipoCliente { get; set; }
        public string sexo { get; set; }
        public int situacaoCliente { get; set; }

        public Cliente(string nome, string cpf, int tipoCliente, string sexo, int situacaoCliente)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.tipoCliente = tipoCliente;
            this.sexo = sexo;
            this.situacaoCliente = situacaoCliente;
        }

        public Cliente()
        {

        }

        public List<ErroValidacao> Validar_Cliente()
        {
            List<ErroValidacao> response = new List<ErroValidacao>();

            if (!Valida_Vazio_Branco_Default())
            {
                response.Add(new ErroValidacao { Message = "Favor Preencher todos os campos" });
                return response;
            }

            //if (!Valida_Nome())
            //    response.Add(new ErroValidacao { Message = "Nome em formato inválido" });

            if (!Valida_CPF())
                response.Add(new ErroValidacao { Message = "CPF inválido" });

            if (!Valida_TipoCliente())
                response.Add(new ErroValidacao { Message = "Tipo de Cliente Inválido" });

            if (!Valida_Sexo())
                response.Add(new ErroValidacao { Message = "Digite um Sexo válido" });

            if (!Valida_SituacaoCliente())
                response.Add(new ErroValidacao { Message = "Digite uma situação válida" });

            return response;
        }

        private bool Valida_Vazio_Branco_Default()
        {
            if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(cpf) || tipoCliente > 0 || !string.IsNullOrEmpty(sexo) || situacaoCliente > 0)
                return true;

            return false;
        }

        //private bool Valida_Nome()
        //{
        //    Regex rgx = new Regex(@"\[A - Z]\[a - z] \s \[A - Z]\[a - z]");
        //    return rgx.IsMatch(this.nome);
        //}

        private bool Valida_CPF()
        {
            string cpf = this.cpf;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool Valida_TipoCliente()
        {
            //if (Tipo_Cliente.fisico.Equals(this.tipoCliente) || Tipo_Cliente.juridico.Equals(this.tipoCliente))
            if (this.tipoCliente.Equals(1) || this.tipoCliente.Equals(2))
                return true;

            return false;
        }

        private bool Valida_Sexo()
        {
            if (this.sexo.Equals("M") || this.sexo.Equals("F"))
                return true;

            return false;
        }

        private bool Valida_SituacaoCliente()
        {
            //if (Situacao_Cliente.Ativo.Equals(this.situacaoCliente) || Situacao_Cliente.Inativo.Equals(this.situacaoCliente))
            if (this.situacaoCliente.Equals(1) || this.situacaoCliente.Equals(2))
                return true;

            return false;
        }
    }
}
