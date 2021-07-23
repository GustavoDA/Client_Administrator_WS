using System.Collections.Generic;

namespace DAL.Entidades
{
    public class RetornoWS
    {
        public bool sucesso { get; }
        public List<ErroValidacao> erros { get; }

        public RetornoWS(bool sucesso, List<ErroValidacao> erros)
        {
            this.sucesso = sucesso;
            this.erros = erros;
        }

        public RetornoWS()
        {

        }
    }
}