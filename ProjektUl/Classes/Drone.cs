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

        public Drone(int age) : base(age, dailyHoneyConsumption: 8)
        {
        }

        public override void DoDailyWork(Hive hive)
        {
            if (hive.YoungBees.Count > 0)
            {
                IsCaretaking = true;
                for (int i = 0; i <  CareForYoung(hive.YoungBees.Count); i++)
                {
                    // przenosimy młodą pszczołę do listy młodych pod opieką
                    hive.YoungBeesLookedAfter.Add(hive.YoungBees[0]);
                    hive.YoungBees[0].isCared = true;
                    hive.YoungBees[0].daysNotBeingCared = 0;
                    hive.YoungBees.Remove(hive.YoungBees[0]);
                }
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
