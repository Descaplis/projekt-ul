using ProjektUl.Classes;

class Program
{
    static void Main(string[] args)
    {
        int beesCount, workersCount, guardsCount, dronesCount;
        Random random = new Random();

        Console.WriteLine("Wpisz, ile chcesz mieć pszczół (min. 100):");
        beesCount = Convert.ToInt32(Console.ReadLine());
        while (beesCount < 10000)
        {
            Console.WriteLine("Musisz mieć minimum 10 000 pszczół");
            Console.WriteLine("Wpisz jeszcze raz:");
            beesCount = Convert.ToInt32(Console.ReadLine());
        }

        // Liczenie, ile pszczół z każdego rodzaju, miodu i zapasu młodych pszczół
        workersCount = Convert.ToInt32(Math.Ceiling(beesCount * 0.85));
        int leftBees = beesCount - workersCount;
        dronesCount = Convert.ToInt32(Math.Ceiling(leftBees * 0.4));
        guardsCount = leftBees - dronesCount;

        Hive hive = new Hive(DateTime.Now);
        hive.HoneyStored = beesCount * 10;

        int youngBeesCount = (int)(Math.Ceiling(beesCount * 0.14));
        for (int i = 0; i < youngBeesCount; i++)
        {
            hive.YoungBees.Add(new YoungBee(random.Next(1, 6)));
        }

        Queen queen = new Queen(random.Next(50, 280), (27 * beesCount) / 1000); // to policzenie ile młodych pszczół rodzi królowa mniej więcej odzwierciedla rzeczywistość
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
    }
}