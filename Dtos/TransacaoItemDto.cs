using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquiCycle_FuncionarioPosto.Dtos
{
    public class TransacaoItemDto
    {
        public int QtdAgendada { get; set; }
        public double Valor { get; set; }
        public LiquidoDto Liquido { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
