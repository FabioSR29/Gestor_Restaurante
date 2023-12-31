﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Model
{
    public class Mesas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        [NotMapped]
        public EstadoDeLaMesa estadoDeLaMesa { get; set; }
        [NotMapped]

        public List<Menu>? PlatillosAsociadosALaMesa { get; set; }

    }
}
