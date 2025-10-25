using ProjektUl.Classes;

class Program
{
    static public void EndSimulation()
    {
        Environment.Exit(0);
    }

    static void Main(string[] args)
    {
        int beesCount = 0, workersCount, guardsCount, dronesCount, daysGoal = 0;
        Random random = new Random();

        Console.WriteLine("Wpisz, ile chcesz mieć pszczół (min. 10000):");
        try
        {
            beesCount = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.FormatException) {
            Console.WriteLine("Napisz liczbę większą od 10000");
        }

        while (beesCount < 10000)
        {
            Console.WriteLine("Musisz mieć minimum 10 000 pszczół");
            Console.WriteLine("Wpisz jeszcze raz:");
            try
            {
                beesCount = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Napisz liczbę większą lub równą 10000");
            }
        }

        Console.WriteLine("Wpisz, jaki jest twój cel przetrwania (ilość dni) (min.10):");
        try
        {
            daysGoal = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Napisz liczbę większą lub równą 10");
        }

        while (daysGoal < 10)
        {
            Console.WriteLine("Wpisz minimum 10 dni");
            Console.WriteLine("Wpisz jeszcze raz:");
            try
            {
                daysGoal = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Napisz liczbę większą lub równą 10");
            }
        }

        // Liczenie, ile pszczół z każdego rodzaju, miodu i zapasu młodych pszczół
        workersCount = Convert.ToInt32(Math.Ceiling(beesCount * 0.85));
        int leftBees = beesCount - workersCount;
        dronesCount = Convert.ToInt32(Math.Ceiling(leftBees * 0.4));
        guardsCount = leftBees - dronesCount;

        Hive hive = new Hive(DateTime.Now);
        hive.HoneyStored = beesCount * 10;

        int youngBeesCount = (int)(Math.Ceiling(beesCount * 0.15));
        for (int i = 0; i < youngBeesCount; i++)
        {
            YoungBee youngBee = new YoungBee(random.Next(1, 6));
            hive.YoungBees.Add(youngBee);
        }

        Queen queen = new Queen(random.Next(50, 280), (28 * beesCount) / 1000); // to policzenie ile młodych pszczół rodzi królowa mniej więcej odzwierciedla rzeczywistość
        hive.AddBee(queen);
        for (int i = 0; i < guardsCount; i++) {
            Guard bee = new Guard(random.Next(8, 31));
            hive.AddBee(bee);
        }
        for (int i = 0; i < workersCount; i++) {
            Worker bee = new Worker(random.Next(8, 40));
            hive.AddBee(bee);
        }
        for (int i = 0; i < dronesCount; i++) {
            Drone bee = new Drone(random.Next(8, 51));
            hive.AddBee(bee);
        }

        Console.WriteLine("Symulacja rozpoczęta");
        while (daysGoal > hive.Day)
        {
            hive.PassDay();
            Console.WriteLine("Wciśnij ENTER, aby przejść dalej");
            Console.ReadLine();
        }
        Console.WriteLine("---------------------");
        Console.WriteLine($"Wygrałeś. Twój ul przeżył {daysGoal} dni");
        Console.WriteLine("---------------------");
    }
}