using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Manipular_Arquivo_Produtos
{
    public class Produto
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        //public Produto(string codigoBarras, string nome, decimal preco)
        //{
        //    CodigoBarras = codigoBarras;
        //    Nome = nome;
        //    Preco = preco;
        //}

        public override string ToString()
        {
            return $"{CodigoBarras};{Nome};{Preco:F2}";
        }

    }
}
