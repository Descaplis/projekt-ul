using ProjektUl.Classes;

class Program
{
    static void Main(string[] args)
    {
        int beesCount, workersCount, guardsCount, dronesCount;

        Console.WriteLine("Wpisz, ile chcesz mieć pszczół (min. 25):");
        beesCount = Convert.ToInt32(Console.ReadLine());
        while (beesCount < 25)
        {
            Console.WriteLine("Musisz mieć minimum 25 pszczół");
            Console.WriteLine("Wpisz jeszcze raz:");
            beesCount = Convert.ToInt32(Console.ReadLine());
        }

        // Liczenie, ile pszczół z każdego rodzaju, miodu i zapasu młodych pszczół
        workersCount = Convert.ToInt32(Math.Ceiling(beesCount * 0.8));
        int leftBees = beesCount - workersCount;
        dronesCount = Convert.ToInt32(Math.Ceiling(leftBees * 0.6));
        guardsCount = leftBees - dronesCount;

        Hive hive = new Hive(DateTime.Now);
        hive.HoneyStored = beesCount * 10;
        hive.YoungBees = Convert.ToInt32(Math.Ceiling(beesCount * 0.15));
        Random random = new Random();
        Queen queen = new Queen("queen", random.Next(50, 280));
        hive.AddBee(queen);
        for (int i = 0; i < guardsCount; i++) {
            Guard bee = new Guard($"guard{i}", random.Next(8, 31));
            hive.AddBee(bee);
        }
        for (int i = 0; i < workersCount; i++) {
            Worker bee = new Worker($"worker{i}", random.Next(8, 40));
            hive.AddBee(bee);
        }
        for (int i = 0; i < dronesCount; i++) {
            Drone bee = new Drone($"drone{i}", random.Next(8, 51));
            hive.AddBee(bee);
        }

        Console.WriteLine("Symulacja rozpoczęta");
    }
}