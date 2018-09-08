/*
 * WEB API: PIZZARIA UDS
 * 
 * PedidosController.cs
 * 
 * Author: Aldrovando camargo Neves
 * 
 * Data: 09/09/2018
 *
 */

using PizzariaUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

namespace PizzariaUDS.Controllers
{
    /// <summary>
    /// ValuesController disponibiliza os endpoints para inclusão, exclusão, deleção e listagem de pedidos
    /// </summary>
    public class PedidosController : ApiController
    {
        private dbPizzariaUDS db = new dbPizzariaUDS();

        /// <summary>
        /// Insere um novo pedido
        /// </summary>
        /// <param name="values">Array de parametros do pedido (tamanho da pizza, sabor, etc.)</param>
        /// <returns>Resultado da operação com status de CONFIRMADO se pedido foi aceito</returns>
        [System.Web.Http.Route("api/pedidos/novo")]
        public Status Post([FromBody]string[] values)
        {
            string cliente = values[0];
            string tamanho = values[1];
            string sabor = values[2];
            int id_cliente = Convert.ToInt32(values[3]);

            // Instancia objeto da classe Pedido onde o tempo de preparo e preço serãp calculados pela classe Pizza

            string status_oper = "AGUARDANDO";
            Pedido pedido = new Pedido(id_cliente, cliente, tamanho, sabor);

            Rpedidos rpedido = new Rpedidos
                {
                    id_cliente = pedido.getIdCliente(),
                    nomecliente = pedido.nomeCliente,
                    tamanho_pizza = pedido.tamanho,
                    sabor_pizza = pedido.sabor,
                    extras_pizza = pedido.personal,
                    val_extras = pedido.val_personal,
                    valor_total = pedido.getValorTotal().ToString("0.00"),
                    tempo_prep = pedido.getTempoPreparo().ToString() + " min",
                    status = "CONFIRMADO"
                };

            if ((pedido.pizzaPedido.tamanhos_validos.Contains(tamanho) ? true : false) && 
                    (pedido.pizzaPedido.sabores_validos.Contains(sabor) ? true : false))
            {
                db.Rpedidos.Add(rpedido);
                db.SaveChanges();
                status_oper = "CONFIRMADO";
            }
            else
            {
                status_oper = "RECUSADO";
            }

            Status status = new Status
            {
                Operacao = "INCLUSÃO",
                Numero_Pedido = rpedido.Id,
                Status_Op = status_oper
            };

            return status;
        }

        /// <summary>
        /// Obtem a lista completa de pedidos já feitos 
        /// </summary>
        /// <returns>Uma lista de pedidos</returns>
        [System.Web.Http.Route("api/pedidos/listagem")]
        public IEnumerable<Rpedidos> Get()
        {
            //return new string[] { "value1", "value2" };
            return db.Rpedidos.ToList();
        }

        /// <summary>
        /// Obtem os detalhes de um pedido identificado por {id}. Se pedido não existe retorn "null".
        /// </summary>
        /// <param name="id">Identificação do pedido</param>
        /// <returns>Detalhes do pedido iu null se pedido não existe</returns>
        [System.Web.Http.Route("api/pedidos/{id}/detalhes")]
        public Rpedidos Get(int id)
        {

            Rpedidos rpedidos = db.Rpedidos.Find(id);
            return rpedidos;
        }

        /// <summary>
        /// Adiciona uma personalização na pizza do pedido
        /// </summary>
        /// <param name="person_extra">id do pedido + personalização extra a ser adicionada na pizza do pedido</param>
        /// <returns>Status com resultado da operação</returns>
        [System.Web.Http.Route("api/pedidos/addextra")]
        public Status AddExtra([FromBody]string[] person_extra) //POST
        {
            int id = Convert.ToInt32( person_extra[0]);  // identificação do pedido no banco de dados
            string extra = person_extra[1];  // personalização extra a ser adicionada na pizza do pedido

            Rpedidos pedidoDB = db.Rpedidos.Find(id);

            Boolean extra_adicionado = false;
            Boolean extra_repetido = false;
            string texto_status = "";

            if (pedidoDB != null)
            {
                Pedido pedido = new Pedido(Convert.ToInt32(pedidoDB.id_cliente), pedidoDB.nomecliente, pedidoDB.tamanho_pizza.TrimEnd(), pedidoDB.sabor_pizza.TrimEnd());
                pedido.id = pedidoDB.Id;

                if (pedidoDB.extras_pizza != "")
                {
                    string[] personalizacoesDB = pedidoDB.extras_pizza.Split(',');
                    int index = 0;
                    foreach (string personalizacao in personalizacoesDB)
                    {
                        if (personalizacao == "-" && index < 3 && !extra_repetido) // apenas 3 personalizações
                        {
                            Boolean ad_result = pedido.pizzaPedido.addPersonal(extra);
                            extra_adicionado = ad_result;
                            index = 3;
                        }
                        else if (personalizacao != "-" && index < 3) // apenas 3 personalizações
                        {
                            pedido.pizzaPedido.addPersonal(personalizacao);
                            if (extra == personalizacao) extra_repetido = true;
                            index = index + 1;
                        }
                    }

                }

                if (extra_adicionado && !extra_repetido)
                {
                    pedidoDB.extras_pizza = pedido.getPersonal();
                    pedidoDB.val_extras = pedido.getValPersonal();
                    pedidoDB.valor_total = pedido.getValorTotal().ToString("0.00");
                    pedidoDB.tempo_prep = pedido.getTempoPreparo().ToString() + " min";

                    db.Entry(pedidoDB).State = EntityState.Modified;
                    db.SaveChanges();
                    texto_status = "CONFIRMADO";
                }
                else
                {
                    texto_status = "EXTRA REPETIDO, INVÁLIDO OU LIMITE ATINGIDO - NÃO ADICIONADO";
                }

                Status status = new Status
                {
                    Operacao = "ADIÇÃO DE EXTRA",
                    Numero_Pedido = pedido.id,
                    Status_Op = texto_status
                };
                return status;
            }
            else
            {
                Status status = new Status
                {
                    Operacao = "ADIÇÃO DE EXTRA",
                    Numero_Pedido = id,
                    Status_Op = "ERRO: PEDIDO INEXISTENTE"
                };
                return status;
            }
 
        }


        /// <summary>
        /// Cancela pedido 
        /// </summary>
        /// <param name="id">Número do pedido</param>
        [System.Web.Http.Route("api/pedidos/cancelapedido")]
        public Status CancelaPedido([FromBody] int id) // POST
        {

            Rpedidos pedidoDB = db.Rpedidos.Find(id);
            string texto_status = "CANÇELADO";

            if(pedidoDB != null)
            {
                db.Rpedidos.Remove(pedidoDB);
                db.SaveChanges();
            }
            else
            {
                texto_status = "ERRO: PEDIO INEXISTENTE";
            }
            Status status = new Status
            {
                Operacao = "CANÇELAMENTO DE PEDIDO",
                Numero_Pedido = id,
                Status_Op = texto_status
            };

            return status; 
        }

    }
}