using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Items
{
    public class MultiplyTreeItem : BinaryTreeItem
    {
        public MultiplyTreeItem(ICalculatorTreeItem left, ICalculatorTreeItem right) : base(left, right) { }

        public override Decimal Calculate() => LeftItem.Calculate() * RightItem.Calculate();
    }
}
