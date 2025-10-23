using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Worker : Bee, IDefender, INectarCollector
    {
        public double CollectionEfficiency { get; private set; }
        public bool CanCollect { get; private set; } = true;

        public int DefenseStrength => 8;
        public bool IsOnGuard { get; set; } = false;

        public Worker(int age) : base(age, dailyHoneyConsumption: 5)
        {
            CollectionEfficiency = new Random().NextDouble() + 0.5;
        }

        public override void DoDailyWork(Hive hive)
        {
            if (CanCollect)
            {
                hive.NectarCollected += CollectNectar();
            }
        }

        public override string GetRole() => "Worker";
        public override int DaysToLive() => 42;

        public int CollectNectar()
        {
            // 50-150
            return (int)((int) 100 * CollectionEfficiency);
        }

        public void DefendHive(int attackStrength)
        {
            throw new NotImplementedException();
        }
    }
}
