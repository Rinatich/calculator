using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Items
{
    public class SimpleTreeItem : ICalculatorTreeItem
    {
        public Decimal Number { get; }

        public SimpleTreeItem(Decimal number)
        {
            Number = number;
        }

        public Decimal Calculate() => Number;
    }
}
