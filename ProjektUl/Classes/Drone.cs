using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Drone : Bee, ICaretaker
    {
        public int CaretakingCapacity => 5;
        public bool IsCaretaking { get; set; } = false;

        public Drone(string name, int age) : base(name, age, dailyHoneyConsumption: 8)
        {
        }

        public override void DoDailyWork(Hive hive)
        {
            throw new NotImplementedException();
        }

        public override string GetRole() => "Drone";

        public void CareForYoung(int youngBeesCount)
        {
            throw new NotImplementedException();
        }
    }
}
