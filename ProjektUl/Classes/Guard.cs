using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Guard : Bee, IDefender
    {
        public int DefenseStrength { get; set; }

        public Guard(int age) : base(age, dailyHoneyConsumption: 7)
        {
            DefenseStrength = new Random().Next(13, 19);
        }

        public override void DoDailyWork(Hive hive)
        {
            hive.defenseStrength += DefenseStrength;
        }

        public override string GetRole() => "Guard";
        public override int DaysToLive() => 35;
    }
}
