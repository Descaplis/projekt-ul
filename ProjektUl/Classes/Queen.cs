using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Queen : Bee, ICaretaker
    {
        public int ReproductiveRatePerDay { get; set; }
        public int CaretakingCapacity => 9;
        public bool IsCaretaking { get; set; } = false;

        public Queen(int age, int reproductiveRate) : base(age, dailyHoneyConsumption: 50)
        {
            // reproductive rate is kinda like real life
            ReproductiveRatePerDay = reproductiveRate;
        }

        public override void DoDailyWork(Hive hive)
        {
            // make new young bees
            int cradle = (int)Math.Floor(ReproductiveRatePerDay * 0.103);
            hive.newBeesCount = new Random().Next(ReproductiveRatePerDay - cradle, ReproductiveRatePerDay + cradle);
            for (int i = 0; i < hive.newBeesCount; i++)
            {
                hive.YoungBees.Add(new YoungBee(1));
            }

            if (hive.YoungBees.Count > 0)
            {
                IsCaretaking = true;
                for (int i = 0; i < CareForYoung(hive.YoungBees.Count); i++)
                {
                    // przenosimy młodą pszczołę do listy młodych pod opieką
                    hive.YoungBeesLookedAfter.Add(hive.YoungBees[0]);
                    hive.YoungBees[0].isCared = true;
                    hive.YoungBees[0].daysNotBeingCared = 0;
                    hive.YoungBees.Remove(hive.YoungBees[0]);
                }
            }
            else
            {
                IsCaretaking = false;
            }
        }

        public override string GetRole() => "Queen";
        public override int DaysToLive() => 365;

        public int CareForYoung(int youngBeesCount)
        {
            return youngBeesCount > CaretakingCapacity ? CaretakingCapacity : youngBeesCount
        }
    }
}
