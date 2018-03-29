using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSQuiz.DomainClasses
{
    class Header
    {
        public string HeaderID { get; set; }
        public Client Client { get; set; }
        public IList<LineItem> Lines { get; set; }
    }
}
