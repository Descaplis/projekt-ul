using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Guard : Bee, IDefender
    {
        public int DefenseStrength => 15;
        public bool IsOnGuard { get; set; } = true;

        public Guard(string name, int age) : base(name, age, dailyHoneyConsumption: 7)
        {
        }

        public override void DoDailyWork(Hive hive)
        {
            throw new NotImplementedException();
        }

        public override string GetRole() => "Guard";

        public void DefendHive(int attackStrength)
        {
            throw new NotImplementedException();
        }
    }
}
