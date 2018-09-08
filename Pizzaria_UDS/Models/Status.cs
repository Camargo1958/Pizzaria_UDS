/*
 * WEB API: PIZZARIA UDS
 * 
 * Status.cs
 * 
 * Author: Aldrovando camargo Neves
 * 
 * Data: 09/09/2018
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzariaUDS.Models
{
    /// <summary>
    /// Classe usada para mensagens de status de operações sobre os pedidos
    /// </summary>
    public class Status
    {
        public string Operacao { get; set; }   // INCLUSÃO, ADIÇÃO DE EXTRA, etc.
        public int Numero_Pedido { get; set; }
        public string Status_Op { get; set; }
    }
}