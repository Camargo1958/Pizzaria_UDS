//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzariaUDS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rpedidos
    {
        public int Id { get; set; }
        public string id_cliente { get; set; }
        public string nomecliente { get; set; }
        public string tamanho_pizza { get; set; }
        public string sabor_pizza { get; set; }
        public string extras_pizza { get; set; }
        public string valor_total { get; set; }
        public string tempo_prep { get; set; }
        public string status { get; set; }
        public string val_extras { get; set; }
    }
}
