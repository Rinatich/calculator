using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Items
{
    public interface ICalculatorTreeItem
    {
        Decimal Calculate();
    }
}
