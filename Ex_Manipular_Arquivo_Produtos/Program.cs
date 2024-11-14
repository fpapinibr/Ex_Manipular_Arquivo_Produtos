using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Manipular_Arquivo_Produtos
{
    class Program
    {
        private const string ARQUIVO_CAMINHO = @"C:\Temp\";
        private const string ARQUIVO_NOME = @"produtos.txt";
        private const string ARQUIVO = ARQUIVO_CAMINHO+ARQUIVO_NOME;

        private static List<Produto> listaProdutos = new List<Produto>();

        static void Main(string[] args)
        {
            //Vai verificar se existe o arquivo e carregar os produtos
            //se já tiver algum cadastrado
            CarregarProdutos();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-x-x- MENU -x-x-");
                Console.WriteLine("1 - Cadastrar produto");
                Console.WriteLine("2 - Localizar produto pelo código de barras");
                Console.WriteLine("3 - Sair do programa");
                Console.Write("\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            CadastrarProduto();
                            break;
                        case 2:
                            LocalizarProduto();
                            break;
                        case 3:
                            SalvarProdutos();
                            return;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void CarregarProdutos()
        {
            if (File.Exists(ARQUIVO))
            {
                try
                {
                    string[] linhas = File.ReadAllLines(ARQUIVO);
                    foreach (string linha in linhas)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 3 && decimal.TryParse(dados[2], out decimal preco))
                        {
                            listaProdutos.Add(new Produto
                            {
                                CodigoBarras = dados[0],
                                Nome = dados[1],
                                Preco = preco
                            });
                        }
                    }
                    Console.WriteLine($"Foram carregados {listaProdutos.Count} produtos do arquivo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar produtos: {ex.Message}");
                }
                Console.ReadKey();
            }
        }

        private static void CadastrarProduto()
        {
            Console.Clear();
            Console.WriteLine("-x-x- CADASTRO DE PRODUTO -x-x-\n");

            try
            {
                Produto produto = new Produto();

                Console.Write("Digite o código de barras (máx 13 dígitos): ");
                produto.CodigoBarras = Console.ReadLine();

                Console.Write("Digite o nome do produto (máx 50 caracteres): ");
                produto.Nome = Console.ReadLine();

                Console.Write("Digite o preço do produto: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal preco))
                {
                    produto.Preco = preco;
                    listaProdutos.Add(produto);
                    Console.WriteLine("\nProduto cadastrado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nPreço inválido!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro no cadastro: {ex.Message}");
            }
        }

        private static void LocalizarProduto()
        {
            Console.Clear();
            Console.WriteLine("-x-x- LOCALIZAR PRODUTO -x-x-\n");

            Console.Write("Digite o código de barras: ");
            string codigoBarras = Console.ReadLine() ?? "";

            //LINQ
            var produtosEncontrados = listaProdutos
                .Where(p => p.CodigoBarras == codigoBarras)
                .ToList();

            //LINQ resume o seguinte comando:
            //List<Produto> produtosEncontrados = new List<Produto>();
            //foreach(Produto p in listaProdutos)
            //{
            //    if(p.CodigoBarras == codigoBarras)
            //    {
            //        produtosEncontrados.Add(p);
            //    }
            //}

            if (produtosEncontrados.Any())
            {
                Console.WriteLine("\nProdutos encontrados:");
                foreach (var produto in produtosEncontrados)
                {
                    Console.WriteLine($"Nome: {produto.Nome}");
                    Console.WriteLine($"Código de Barras: {produto.CodigoBarras}");
                    Console.WriteLine($"Preço: R$ {produto.Preco:F2}");
                    Console.WriteLine(new string('-', 30));
                }
            }
            else
            {
                Console.WriteLine("\nNenhum produto encontrado com este código de barras.");
            }
        }

        private static void SalvarProdutos()
        {
            try
            {
                string diretorio = ARQUIVO_CAMINHO;
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                File.WriteAllLines(ARQUIVO, listaProdutos.Select(p => p.ToString()));
                Console.WriteLine("\nProdutos salvos com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao salvar produtos: {ex.Message}");
            }
        }
    }
}
