using System.Text.Json;

class Desafio2
{
    public const string json = @"{
        ""estoque"": [
          { ""codigoProduto"": 101, ""descricaoProduto"": ""Caneta Azul"", ""estoque"": 150 },
          { ""codigoProduto"": 102, ""descricaoProduto"": ""Caderno Universitário"", ""estoque"": 75 },
          { ""codigoProduto"": 103, ""descricaoProduto"": ""Borracha Branca"", ""estoque"": 200 },
          { ""codigoProduto"": 104, ""descricaoProduto"": ""Lápis Preto HB"", ""estoque"": 320 },
          { ""codigoProduto"": 105, ""descricaoProduto"": ""Marcador de Texto Amarelo"", ""estoque"": 90 }
        ]
    }";

    public class Produto
    {
        public int Codigo { get; set; }
        public int Estoque { get; set; }
        public required string Descricao { get; set; }
    }

    public class Movimentacao
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }

        /// <summary>
        /// Entrada ou Saída
        /// </summary>
        public required string Tipo { get; set; }
        public required string Descricao { get; set; }
    }

    public static void Executar()
    {
        List<Produto> estoque = [];

        var jDoc = JsonDocument.Parse(json);
        var estoqueJson = jDoc.RootElement.GetProperty("estoque");

        foreach (var item in estoqueJson.EnumerateArray())
            estoque.Add(new Produto
            {
                Estoque = item.GetProperty("estoque").GetInt32(),
                Codigo = item.GetProperty("codigoProduto").GetInt32(),
                Descricao = item.GetProperty("descricaoProduto").GetString()!
            });

        int contadorMov = 1;

        Console.WriteLine("Sistema de Movimentação de Estoque:\n");

        while (true)
        {
            Console.WriteLine("\n\nProdutos disponíveis no estoque:");

            foreach (var p in estoque)
                Console.WriteLine($"{p.Codigo} - {p.Descricao} (Estoque: {p.Estoque})");

            Console.Write("\n\nInforme o código do produto ou digite 0 para sair: \n");

            int cod = int.TryParse(Console.ReadLine(), out int temp) ? temp : 0;

            if (cod == 0)
                break;

            var produto = estoque.Find(p => p.Codigo == cod);

            Console.Clear();

            if (produto == null)
            {
                Console.WriteLine("\nProduto não encontrado!");
                continue;
            }
            else
            {
                Console.WriteLine($"{produto.Codigo} - {produto.Descricao} (Estoque: {produto.Estoque})");
            }

            Console.Write("\nMovimentação (1 = Entrada, 2 = Saída): ");
            int tipoMov = int.Parse(Console.ReadLine());

            string tipo = tipoMov == 1 ? "entrada" : "saida";

            Console.Write("Quantidade: ");
            int qtd = int.Parse(Console.ReadLine());

            Console.Write("Descrição da movimentação: ");
            string desc = Console.ReadLine();

            Movimentacao mov = new()
            {
                Id = contadorMov++,
                Descricao = desc,
                Quantidade = qtd,
                Tipo = tipo
            };

            if (mov.Tipo == "entrada")
                produto.Estoque += mov.Quantidade;
            else
            {
                if (mov.Quantidade > produto.Estoque)
                {
                    Console.WriteLine("Não disponível!");
                    continue;
                }

                produto.Estoque -= mov.Quantidade;
            }

            Console.WriteLine($"\nRegistrado!");
            Console.WriteLine($"ID: {mov.Id} - Tipo: {mov.Tipo.ToUpper()} - Produto: {produto.Descricao} - Quantidade: {mov.Quantidade} - Descrição: {mov.Descricao} - Estoque atual: {produto.Estoque}");
        }
    }
}