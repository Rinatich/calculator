using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Items;

namespace Calculator
{
    public interface IBinaryTreeItemDecription
    {
        Int32 Priority { get; }

        ICalculatorTreeItem CreateItem(ICalculatorTreeItem left, ICalculatorTreeItem right);
    }
}
