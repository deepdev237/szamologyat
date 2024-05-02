/*
Írj egy számológépet, ahol menü pontban lehet kiválasztani, hogy milyen
műveletet végezzen el. Ezeket a műveleteket és az eredményt mentsd el
egy txt fájlba, amit a kiíratás menüponttal lehet megtekinteni.
*/

class Program
{
    static void Main(string[] args)
    {
        var calculator = new Calculator();

        int valasztas = 0;
        while (valasztas != 5)
        {
            Console.Clear();

            Console.WriteLine("Szamologep");
            Console.WriteLine("1. Osszeadas");
            Console.WriteLine("2. Kivonas");
            Console.WriteLine("3. Szorzas");
            Console.WriteLine("4. Osztas");
            Console.WriteLine("5. Kilepes");
            Console.WriteLine("6. Eredmenyek megtekintese");

            Console.WriteLine();

            Console.Write("Valasztas: ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                continue;
            }
            valasztas = Convert.ToInt32(input);
            calculator.PerformOperation(valasztas);
        }
    }
}

class Calculator
{
    private Dictionary<int, Action> operations;

    public Calculator()
    {
        operations = new Dictionary<int, Action>
        {
            { 1, Osszeadas },
            { 2, Kivonas },
            { 3, Szorzas },
            { 4, Osztas },
            { 6, Eredmenyek }
        };
    }

    public void PerformOperation(int operation)
    {
        if (operations.ContainsKey(operation))
        {
            operations[operation]();
        }
        else
        {
            Console.WriteLine("Nincs ilyen menupont!");
        }
    }

    private void Osszeadas()
    {
        var numbers = GetNumbersFromUser();
        var eredmeny = numbers.Sum();
        Console.WriteLine("Az osszeg: " + eredmeny);
        SaveResult("Osszeadas", numbers, eredmeny);
    }

    private void Kivonas()
    {
        var numbers = GetNumbersFromUser();
        if (numbers.Count < 2)
        {
            Console.WriteLine("Legalább két számot kell megadni a kivonáshoz!");
            return;
        }
        var eredmeny = numbers[0] - numbers[1];
        Console.WriteLine("A kulonbseg: " + eredmeny);
        SaveResult("Kivonas", numbers, eredmeny);
    }

    private void Szorzas()
    {
        var numbers = GetNumbersFromUser();
        var eredmeny = numbers.Aggregate((a, b) => a * b);
        Console.WriteLine("A szorzat: " + eredmeny);
        SaveResult("Szorzas", numbers, eredmeny);
    }

    private void Osztas()
    {
        var numbers = GetNumbersFromUser();
        if (numbers.Count < 2)
        {
            Console.WriteLine("Legalább két számot kell megadni az osztáshoz!");
            return;
        }
        if (numbers[1] == 0)
        {
            Console.WriteLine("Nullaval nem lehet osztani!");
            return;
        }
        double eredmeny = numbers[0] / numbers[1];
        Console.WriteLine("Az osztas: " + eredmeny);
        SaveResult("Osztas", numbers, eredmeny);
    }

    private void Eredmenyek()
    {
        string[] eredmenyek = File.ReadAllLines("eredmenyek.txt");
        foreach (string eredmeny in eredmenyek)
        {
            Console.WriteLine(eredmeny);
        }
    }

    private List<double> GetNumbersFromUser()
    {
        var numbers = new List<double>();
        while (true)
        {
            Console.Write("Add meg a következő számot (írj 'exit' a kilépéshez vagy ne adj meg semmit): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "exit" || string.IsNullOrEmpty(input))
            {
                break;
            }
            numbers.Add(Convert.ToDouble(input));
        }
        return numbers;
    }

    private void SaveResult(string operation, List<double> numbers, double result)
    {
        var numbersString = string.Join(" " + GetOperator(operation) + " ", numbers);
        File.AppendAllText("eredmenyek.txt", $"{operation}: {numbersString} = {result}\n");
    }

    private string GetOperator(string operation)
    {
        return operation switch
        {
            "Osszeadas" => "+",
            "Kivonas" => "-",
            "Szorzas" => "*",
            "Osztas" => "/",
            _ => throw new ArgumentException("Invalid operation"),
        };
    }
}