using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Drone : Bee, ICaretaker
    {
        public int CaretakingCapacity => 6;
        public bool IsCaretaking { get; set; } = false;

        public Drone(string name, int age) : base(name, age, dailyHoneyConsumption: 8)
        {
        }

        public override void DoDailyWork(Hive hive)
        {
            if (hive.YoungBeesLookedAfter < hive.YoungBees)
            {
                // If there are less than 10 young bees to look after, take them
                IsCaretaking = true;
                hive.YoungBeesLookedAfter += CareForYoung(hive.YoungBees);
            } else
            {
                IsCaretaking = false;
            }
        }

        public override string GetRole() => "Drone";
        public override int DaysToLive() => 56;

        public int CareForYoung(int youngBeesCount)
        {
            return youngBeesCount > CaretakingCapacity ? CaretakingCapacity : youngBeesCount;
        }
    }
}
