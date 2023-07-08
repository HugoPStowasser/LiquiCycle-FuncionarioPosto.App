using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquiCycle_FuncionarioPosto.Dtos
{
    public class LoginDto
    {
        public AdministradorDto Administrador { get; set; }
        public ClienteDto Cliente { get; set; }
        public FuncionarioPostoDto FuncionarioPosto { get; set; }
        public DonoPostoDto DonoPosto { get; set; }
    }
}
