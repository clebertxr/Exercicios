class Desafio3
{
    public static void Executar()
    {
        Console.Write("Informe o valor da conta: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        Console.Write("Informe a data de vencimento (dd/mm/aaaa): ");
        DateTime vencimento = DateTime.Parse(Console.ReadLine());

        if (DateTime.Today <= vencimento)
        {
            Console.WriteLine("\nA conta não está vencida. Não há juros a pagar.");
            return;
        }

        int diasAtraso = (DateTime.Today - vencimento).Days;
        decimal taxaDiaria = 0.025m;
        decimal juros = valor * taxaDiaria * diasAtraso;
        decimal valorAtualizado = valor + juros;

        Console.WriteLine($"\nDias de atraso: {diasAtraso}");
        Console.WriteLine($"Juros total: R$ {juros:F2}");
        Console.WriteLine($"Valor atualizado: R$ {valorAtualizado:F2}");
    }
}