using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektUl.Classes
{
    internal interface ICaretaker
    {
        int CaretakingCapacity { get; }
        bool IsCaretaking { get; set; }
        void CareForYoung(int youngBeesCount);
    }
}
