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
        public int CaretakingCapacity => 10;
        public bool IsCaretaking { get; set; } = false;

        public Queen(string name, int age, int reproductiveRate = 20) : base(name, age, dailyHoneyConsumption: 50)
        {
            ReproductiveRatePerDay = reproductiveRate;
        }

        public override void DoDailyWork(Hive hive)
        {
            hive.YoungBees += new Random().Next(20, 26) * ReproductiveRatePerDay;
        }

        public override string GetRole() => "Queen";
        public override int DaysToLive() => 365;

        public int CareForYoung(int youngBeesCount)
        {
            throw new NotImplementedException();
        }
    }
}
