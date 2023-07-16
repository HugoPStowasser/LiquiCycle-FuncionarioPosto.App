using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquiCycle_FuncionarioPosto.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public UsuarioDto Usuario { get; set; }
    }
    public class TransacaoClienteDto
    {
        public int Id { get; set; }
        public PostoDto Posto { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime dataAgendada { get; set; }
        public double Valor { get; set; }
        public String CodigoTransacao { get; set; }
        public List<TransacaoItemDto> TransacaoItem { get; set; }
    }
}
