/*
 * WEB API: PIZZARIA UDS
 * 
 * Pizza.cs
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
    ///  A classe Pizza realiza os cálculos de tempo de preparo e valor conforme o tamanho, sabor ou personalização (opicional)
    /// </summary>
    public class Pizza
    {
        private string tamanho;
        private string sabor;
        private string[] personalizacao; // personalizações [3]
        private string[] val_personal;   // valores das personalizações [3]
        private int tempo_preparo;
        private double preco;
        // extras possíveis
        public List<string> extras_validos = new List<string> { "extra bacon", "sem cebola", "borda recheada" };
        public List<string> tamanhos_validos = new List<string> { "pequena", "media", "média", "grande" };
        public List<string> sabores_validos = new List<string> { "portuguesa", "calabresa", "marguerita" };

        public Pizza(string tamanho_p, string sabor_p)
        {
            // valida tamanho do pedido se invalido use tamanho = invalido 
            string tamanho = (tamanhos_validos.Contains(tamanho_p)? tamanho_p: "INVALIDO");
            this.setTamanho(tamanho);
            // valida sabor do pedido se invalido use sabor = invalido 
            string sabor = (sabores_validos.Contains(sabor_p) ? sabor_p : "INVALIDO");
            this.setSabor(sabor);
            this.personalizacao = new string[3] { "-", "-", "-" }; // inicializa sem personalizações
            this.val_personal = new string[3] { "0,00", "0,00", "0,00" }; 

            if (tamanho == "pequena") {
                this.setTempoPreparo(15);
                this.setPreco(20.00);
            }
            if (tamanho == "média" || tamanho == "media") {
                this.setTempoPreparo(20);
                this.setPreco(30.00);
            }
            if (tamanho == "grande") {
                this.setTempoPreparo(25);
                this.setPreco(40.00);
            }
            if (sabor == "portuguesa") { tempo_preparo = tempo_preparo + 5; }
        }

        /// <summary>
        /// Adiciona personalização (extra) na pizza com alteração seletiva de preço e tempo de preparo
        /// </summary>
        /// <param name="extra"></param>
        public Boolean addPersonal(string extra)
        {
            if (validaExtra(extra))
            {
                double preco = getPreco();
                double ad_preco = 0.00;
                int tempoprep = getTempoPreparo();

                if (extra == "extra bacon")
                {
                    ad_preco = 3.00;
                    setPreco(preco + ad_preco);
                }
                else if (extra == "borda recheada")
                {
                    ad_preco = 5.00;
                    setPreco(preco + ad_preco);
                    setTempoPreparo(tempoprep + 5);
                }

                int index = 0;
                foreach (string personalizacao in this.personalizacao)
                {
                    if (personalizacao == "-" && index < 3) // apenas 3 personalizações
                    {
                        //extra = extra.Replace('_', ' ');
                        this.personalizacao[index] = extra;
                        this.val_personal[index] = ad_preco.ToString("0.00");
                        index = 3;
                    }
                    else if (index < 3) // apenas 3 personalizações
                    {
                        index = index + 1;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Obetem a lista de personalizações da pizza
        /// </summary>
        /// <returns>Lista de personalizações da pizza, separada  por virgulas</returns>
        public string getPersonal()
        {
            return ((this.personalizacao.Length > 0) ? string.Join(",", this.personalizacao) : "");
        }

        /// <summary>
        /// Obtem a lista dos preços do extras ou poersonalizações da pizza
        /// </summary>
        /// <returns>lista dos preços do extras ou poersonalizações da pizza, separadas por ponto e vírgula</returns>
        public string getValPersonal()
        {
            return ((this.val_personal.Length > 0) ? string.Join(";", this.val_personal) : "0.00");
        }

        /// <summary>
        /// Ajusta o tamanho da pizza
        /// </summary>
        /// <param name="tamanho"></param>
        private void setTamanho(string tamanho)
        {
            this.tamanho = tamanho;
        }

        /// <summary>
        /// Obtém o tamanho da pizza
        /// </summary>
        /// <returns>Tamanho da pizza</returns>
        public string getTamanho()
        {
            return this.tamanho;
        }

        /// <summary>
        /// Ajusta o sabor da pizza
        /// </summary>
        /// <param name="sabor"></param>
        private void setSabor(string sabor)
        {
            this.sabor = sabor;
        }

        /// <summary>
        /// Obtém o sabor da pizza
        /// </summary>
        /// <returns>Sabor da pizza</returns>
        public string getSabor()
        {
            return this.sabor;
        } 

        /// <summary>
        /// Ajusta o preço total da pizza
        /// </summary>
        /// <param name="newPreco"></param>
        private void setPreco(double newPreco)
        {
            this.preco = newPreco;
        }

        /// <summary>
        /// Obtém o preço total da pizza
        /// </summary>
        /// <returns></returns>
        public double getPreco()
        {
            return this.preco;
        }

        /// <summary>
        /// Ajusta o tempo de preparo da pizza
        /// </summary>
        /// <param name="tempo"></param>
        private void setTempoPreparo(int tempo)
        {
            this.tempo_preparo = tempo;
        }

        /// <summary>
        /// Obtém o tempo de preparo da pizza
        /// </summary>
        /// <returns>Tempo de preparo da pizza</returns>
        public int getTempoPreparo()
        {
            return this.tempo_preparo;
        }

        /// <summary>
        /// Valida se extra a ser usado é válido
        /// </summary>
        /// <param name="extra"></param>
        /// <returns>true se é válido</returns>
        public Boolean validaExtra(string extra)
        {
            if(extras_validos.Contains(extra))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}