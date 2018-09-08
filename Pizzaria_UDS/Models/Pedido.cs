/*
 * WEB API: PIZZARIA UDS
 * 
 * Pedidos.cs
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
    /// A classe Pedido contem os dados do pedido recebido e o resultado dos calculos de valor e tempo de preparo realizados pela classe Pizza
    /// </summary>
    public class Pedido
    {
        public int id;
        public int IdCliente;
        public string nomeCliente;
        public Pizza pizzaPedido;
        public string tamanho;
        public string sabor;
        public string personal;
        public string val_personal;
        public double valorTotal;
        public int tempoPreparo;

        /// <summary>
        /// Classe de pedido
        /// </summary>
        /// <param name="idCliente">Id do cliente do pedido</param>
        /// <param name="nomeCliente">Nome do cliente do pedido</param>
        /// <param name="tamanho">tamanho da pizza do pedido</param>
        /// <param name="sabor">Sabor da pizza do pedido</param>
        public Pedido(int idCliente, string nomeCliente, string tamanho, string sabor)
        {
            this.IdCliente = idCliente;
            this.nomeCliente = nomeCliente;
            this.pizzaPedido = new Pizza(tamanho, sabor);
            
            this.personal = "-,-,-";
            this.val_personal = "0,00;0,00;0,00";
            this.tamanho = getTamanho();
            this.sabor = getSabor();
            this.valorTotal = getValorTotal();
            this.tempoPreparo = getTempoPreparo();
        }

        /// <summary>
        /// Obtém o Id do cliente
        /// </summary>
        /// <returns></returns>
        public string getIdCliente()
        {
            return this.IdCliente.ToString();
        }

        /// <summary>
        /// Obtém o tamanho da pizza do pedido
        /// </summary>
        /// <returns>Tamanho da pizza do pedido</returns>
        public string getTamanho()
        {
            return this.pizzaPedido.getTamanho();
        }

        /// <summary>
        /// Obtém o sabor da pizza do pedido
        /// </summary>
        /// <returns>Sabor da pizza do pedido</returns>
        public string getSabor()
        {
            return this.pizzaPedido.getSabor();
        }

        /// <summary>
        /// Obtém a lista de extras ou personalizações da pizza do pedido
        /// </summary>
        /// <returns>Lista de extras ou personalizações separadas por virgulas</returns>
        public string getPersonal()
        {
            return this.pizzaPedido.getPersonal();
        }

        /// <summary>
        /// Obtém a lista de preços de cada extra ou personalização da pizza do pedido
        /// </summary>
        /// <returns>lista de preços de cada extra ou personalização separada por vírgula</returns>
        public string getValPersonal()
        {
            return this.pizzaPedido.getValPersonal();
        }

        /// <summary>
        /// Obtém o preço total da pizza do pedido
        /// </summary>
        /// <returns>Preço total da pizza do pedido</returns>
        public double getValorTotal()
        {
            return this.pizzaPedido.getPreco();
        }

        /// <summary>
        /// Obtém o tempo de preparo da pizza do pedido
        /// </summary>
        /// <returns>Tempo de preparo da pizza do pedido</returns>
        public int getTempoPreparo()
        {
            return this.pizzaPedido.getTempoPreparo();
        }

        /// <summary>
        ///  Adiciona um extra ou personalização à pizza do pedido
        /// </summary>
        /// <param name="extra"></param>
        public void addPersonalizacao(string extra)
        {
            this.pizzaPedido.addPersonal(extra);
            this.personal = this.getPersonal();
        }
    }
}