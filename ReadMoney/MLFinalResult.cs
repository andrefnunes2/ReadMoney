using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMoney
{
    public class MLFinalResult
    {
        public string Ativo { get; set; }
        public int TipoAtivo { get; set; }
        public string Nome { get; set; }
        public int? LT { get; set; }
        public double? CFA { get; set; }
        public double? CAb { get; set; }
        public int? CFA_STP { get; set; }
        public int? CAb_STP { get; set; }
        public double? VFA { get; set; }
        public double? VAb { get; set; }
        public int? VFA_STP { get; set; }
        public int? VAb_STP { get; set; }
        public double? VarS100 { get; set; }
        public double? VarSBal { get; set; }
        public DateTime DtUltimaLeitura { get; set; }
    }

    public class MLTraderName
    {
        public string Nome { get; set; }
        public bool IsAtivo { get; set; }
    }
}
