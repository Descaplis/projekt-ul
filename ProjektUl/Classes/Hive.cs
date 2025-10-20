using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    // Główna klasa modelująca ul i stan symulacji.
    // To tutaj powinny być orchestracje: każdy dzień -> wywołanie DoDailyWork dla każdej pszczoły,
    // agregacja zebranego nektaru, konwersja nektaru na miód, obsługa losowych zdarzeń (atak, pogoda),
    // opieka nad młodymi, logowanie zdarzeń.
    internal class Hive
    {
        public List<Bee> Bees { get; } = new List<Bee>();
        public int HoneyStored { get; set; } = 0; // jednostki miodu
        public int NectarCollected { get; set; } = 0; // tymczasowe zbiory przed konwersją
        public int YoungBees { get; set; } = 0; // liczba larw/młodych oczekujących na dorastanie
        public int Day { get; set; } = 1;
        public bool IsUnderAttack { get; set; } = false;
        public DateTime SimulationStartDate { get; set; }
        public List<LogEntry> logs;


        public Hive(DateTime simulationStartDate)
        {
            SimulationStartDate = simulationStartDate;
        }

        public void AddBee(Bee bee)
        {
            Bees.Add(bee);
        }

        public void RemoveBee(Bee bee)
        {
            Bees.Remove(bee);
        }

        // Główna metoda przechodząca symulację o 1 dzień:
        // - aktualizuje Day
        // - wywołuje DoDailyWork u każdej pszczoły
        // - obsługuje konwersję nektaru na miód
        // - sprawdza losowe zdarzenia (np. atak) i wywołuje DefendAgainstAttack
        // - wykonuje opiekę nad młodymi (CareForYoungBees)
        // - loguje podsumowanie dnia
        public void PassDay()
        {
            Day++;
            foreach (var bee in Bees)
            {
                bee.Age++;
                bee.DoDailyWork(this);
            }
            // check random events
            // log summary of queen, collecting nectar, caring for young bees and staying on defence
            ConvertNectarToHoney();
        }

        public void ConvertNectarToHoney()
        {
            HoneyStored += NectarCollected;
            NectarCollected = 0;
            throw new NotImplementedException();
        }

        // Wywoływana w przypadku ataku; powinna:
        // - obliczyć sumaryczną siłę obrony (OfType<IDefender>())
        // - porównać z attackStrength
        // - zadecydować o stratach (śmierć pszczół, spadek miodu itp.)
        public void DefendAgainstAttack(int attackStrength)
        {
            throw new NotImplementedException();
        }

        // Powinna zebrać nektar od wszystkich pszczół implementujących INectarCollector
        // i zsumować go do NectarCollected (niekonwertowanego).
        public void CollectNectarFromWorkers()
        {
            throw new NotImplementedException();
        }

        // Powinna rozdzielić opiekę nad YoungBees pomiędzy ICaretaker i odpowiednio zaktualizować licznik młodych
        // (np. część larw dorasta do pełnoprawnych pszczół).
        public void CareForYoungBees()
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentSimulationTime()
        {
            return SimulationStartDate.AddDays(Day - 1); // day - 1 because it starts with day 1 not with day 0
        }

        public void LogAndWrite(LogEntry log)
        {
            logs.Add(log);
            Console.WriteLine(log.Description);
        }
    }
}
