﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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
        public int HoneyStored { get; set; } = 0;
        public int NectarCollected { get; set; } = 0; // tymczasowe zbiory przed konwersją
        public List<YoungBee> YoungBees;
        public List<YoungBee> YoungBeesLookedAfter; // młode, które są teraz pod opieką
        public int newBeesCount; // ile młodych urodzila królowa
        public int Day { get; set; } = 1;
        public bool IsUnderAttack { get; set; } = false;
        public int defenseStrength;
        public DateTime SimulationStartDate { get; set; }
        public List<LogEntry> logs;
        private Random random = new Random();


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
            NectarCollected = 0;
            defenseStrength = 0;

            // rozwój młodych pszczół
            foreach (YoungBee youngeBee in YoungBees)
            {
                youngeBee.age++;
                if (youngeBee.age == 8)
                {
                    // młoda pszczoła staje się dorosła - 85% robotnica, 6% truteń, 9% strażnica
                    Bee newBee;
                    if (random.NextDouble() <= 0.85)
                    {
                        newBee = new Worker(8);
                    }
                    {
                        if (random.NextDouble() <= 0.4)
                        {
                            newBee = new Drone(8);
                        } else
                        {
                            newBee = new Guard(8);
                        }
                    }
                    AddBee(newBee);
                    YoungBees.Remove(youngeBee);
                }
            }
            
            foreach (Bee bee in Bees)
            {
                bee.Age++;
                if (bee.Age >= bee.DaysToLive()) // sprawdzamy, czy pszczoła umarła ze starości
                {
                    bee.IsAlive = false;
                    RemoveBee(bee);
                }
                else
                {
                    HoneyStored -= bee.DailyHoneyConsumption;
                    bee.DoDailyWork(this);
                }
            }

            // Po wykonaniu prac, przenosimy młode pszczoły pod opieką do listy wszystkich i aktualizujemy te na liście youngBees, aby dni bez opieki były liczone
            foreach (YoungBee youngBee in YoungBees) {
                youngBee.daysNotBeingCared++;
                HoneyStored -= 2;
            }
            YoungBees.RemoveAll(youngBee => youngBee.daysNotBeingCared == 2);
            YoungBeesLookedAfter.AddRange(YoungBees); // teraz zaopiekowane młode to wszystkie młode pszczoły
            YoungBees.Clear();
            YoungBees.AddRange(YoungBeesLookedAfter);
            YoungBeesLookedAfter.Clear();

            // Random events
            if (random.NextDouble() <= 0.08)
            {
                double eventType = random.NextDouble();
                if (eventType >= 0.5)
                {
                    DefendAgainstAttack();
                } else if (eventType >= 0.9)
                {
                    // Deszcz
                    NectarCollected = 0;
                    LogAndWrite(new LogEntry(
                        GetCurrentSimulationTime(),
                        Day,
                        "Dzisiaj pada deszcz. Robotnice nie mogą zbierać nektaru"
                        ));
                } else
                {
                    // dodatkowe źródło nektaru
                    int bonusNectar = random.Next(300, 500);
                    LogAndWrite(new LogEntry(
                        GetCurrentSimulationTime(),
                        Day,
                        $"Znaleziono skupisko nektaru! Bonusowe {bonusNectar} plastrów nektaru."
                        ));
                }
            }
            // system wygranej i przegranej

            // log summary of queen, collecting nectar, caring for young bees and staying on defense
            ConvertNectarToHoney();

            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"Królowa urodziła {newBeesCount} młodych pszczół"
            ));
            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"Robotnice zebrały {NectarCollected} jednostek nektaru i przerobiły je na miód"
            ));
            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"{CountRole("Drone")} trutni zajęło się młodymi pszczołami"
            ));
            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"{CountRole("Guard")} strażnic stoi na warcie. Siła obrony: {defenseStrength}"
                ));
        }

        public void ConvertNectarToHoney()
        {
            HoneyStored += NectarCollected;
            NectarCollected = 0;
            throw new NotImplementedException();
        }

        public void DefendAgainstAttack()
        {
            // losowa siła ataku bazuje na ilości pszczół w ulu
            int guardsCount = Bees.Where(bee => bee.GetRole() == "Guard").Count();
            // attackRate is the multipliter of defenseCount, from 0.8 to 1.35
            double attackRate = random.NextDouble() * 0.55 + 0.8; // random * (max - min) + min
            int attackStrength = random.Next((int)Math.Ceiling(defenseStrength * 0.8), (int)Math.Ceiling(defenseStrength * 1.35));
            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"Atak szerszeni!"
                ));
            LogAndWrite(new LogEntry(
                GetCurrentSimulationTime(),
                Day,
                $"Siła ataku szerszeni: {attackStrength}. Siła obrony: {defenseStrength}."
                ));
            if (attackRate >= 1)
            {
                LogAndWrite(new LogEntry(
                    GetCurrentSimulationTime(),
                    Day,
                    $"Szerszenie są silniejsze. Wygrały. Przegrałeś."
                   ));
            } else
            {
                double difference = 1 - attackRate; // difference between defense strength and attack strength in percents (1% - 20%)
                int guardsLoss = (int)Math.Ceiling(CountRole("Guard") * difference * 0.11);
                int workersLoss = (int)Math.Ceiling(CountRole("Worker") * difference * 0.12);
                LogAndWrite(new LogEntry(
                    GetCurrentSimulationTime(),
                    Day,
                    $"Pszczoły obroniły swój ul. Straty: {guardsLoss} strażnic i {workersLoss} robotnic"
                    ));
            }
        }

        public DateTime GetCurrentSimulationTime()
        {
            return SimulationStartDate.AddDays(Day - 1); // day - 1 ponieważ zaczynamy dniem 1 a nie dniem 0
        }
        // function to count how many bees from a role
        public int CountRole(string role)
        {
            return Bees.Where(bee => bee.GetRole() == role).Count();
        }

        public void LogAndWrite(LogEntry log)
        {
            logs.Add(log);
            Console.WriteLine($"[{log.SimulationTime}] Dzień {log.Day}. {log.Description}");
        }
    }
}
