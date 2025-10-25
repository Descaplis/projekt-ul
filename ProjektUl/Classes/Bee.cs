using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektUl.Classes
{
    internal abstract class Bee
    {
        public int Age { get; set; }
        public int DailyHoneyConsumption { get; protected set; }
        public bool IsAlive { get; set; } = true;


        protected Bee(int age, int dailyHoneyConsumption)
        {
            Age = age;
            DailyHoneyConsumption = dailyHoneyConsumption;
        }

        // Każda pszczoła wykonuje swoje zadania w ciągu dnia
        public abstract void DoDailyWork(Hive hive);

        // Rola/typ pszczoły (np. "Queen", "Worker")
        public abstract string GetRole();
        public abstract int DaysToLive();
    }
}

