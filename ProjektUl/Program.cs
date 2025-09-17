class Program
{
    static void Main(string[] args)
    {
        int beesCount;
        int workersCount;
        int guardsCount;
        int dronesCount;
        int honey;
        int youngBees;

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

        honey = beesCount * 10;
        youngBees = Convert.ToInt32(Math.Ceiling(beesCount * 0.15));


        DateTime SimulationStartDay = DateTime.Now;
    }
}