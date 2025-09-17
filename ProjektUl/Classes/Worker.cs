using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal class Worker : Bee, IDefender, INectarCollector
    {
        public int CollectionEfficiency { get; private set; }
        public bool CanCollect { get; private set; } = true;

        public int DefenseStrength => 8;
        public bool IsOnGuard { get; set; } = false;

        public Worker(string name, int age, int collectionEfficiency = 5) : base(name, age, dailyHoneyConsumption: 5)
        {
            CollectionEfficiency = collectionEfficiency;
        }

        public override void DoDailyWork(Hive hive)
        {
            throw new NotImplementedException();
        }

        public override string GetRole() => "Worker";

        public int CollectNectar()
        {
            throw new NotImplementedException();
        }

        public void DefendHive(int attackStrength)
        {
            throw new NotImplementedException();
        }
    }
}
