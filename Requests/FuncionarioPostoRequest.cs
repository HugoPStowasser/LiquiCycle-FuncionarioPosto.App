using LiquiCycle_FuncionarioPosto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquiCycle_FuncionarioPosto.Requests
{
    public class FuncionarioPostoRequest
    {
    }
    public class FuncionarioPostoStatusRequest
    {
        public int FuncionarioPostoId { get; set; }
        public StatusEnum Status { get; set; }
    }
    public class TransacaoFuncionarioPostoRequest
    {
        public int Id { get; set; }
        public int FuncionarioPostoId { get; set; }
        public String CodigoTransacao { get; set; }
        public List<TransacaoFuncionarioPostoItensRequest> transacaoFuncionarioPostoItensRequests { get; set; }
    }
    public class TransacaoFuncionarioPostoItensRequest
    {
        public int QtdConfirmada { get; set; }
        public int IdLiquido { get; set; }
    }
    public class TransacaoUsinaRequest
    {
        public int IdFuncionarioPosto { get; set; }
        public int IdUsina { get; set; }
        public List<TransacaoUsinaItensRequest> TransacaoUsinaItensRequest { get; set; }
    }
    public class TransacaoUsinaItensRequest
    {
        public int Qtd { get; set; }
        public int IdLiquido { get; set; }
    }
}
