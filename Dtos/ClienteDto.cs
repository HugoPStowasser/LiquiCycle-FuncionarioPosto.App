﻿using System;
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
}
