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
        public bool IsCaretaking { get; set; } = true;

        public Queen(string name, int age, int reproductiveRate = 20) : base(name, age, dailyHoneyConsumption: 50)
        {
            ReproductiveRatePerDay = reproductiveRate;
        }

        public override void DoDailyWork(Hive hive)
        {
            throw new NotImplementedException();
        }

        public override string GetRole() => "Queen";

        public void CareForYoung(int youngBeesCount)
        {
            throw new NotImplementedException();
        }
    }
}
