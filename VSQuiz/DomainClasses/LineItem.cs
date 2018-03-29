using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSQuiz.DomainClasses
{
    class LineItem
    {
        public int LineNo { get; set; }
        public Item Item { get; set; }
        public DateTime OrderDate { get; set; }
        public int Charge { get; set; }
    }
}
