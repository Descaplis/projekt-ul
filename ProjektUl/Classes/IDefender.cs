using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal interface IDefender
    {
        int DefenseStrength { get; }
        bool IsOnGuard { get; set; }
        void DefendHive(int attackStrength);
    }
}

