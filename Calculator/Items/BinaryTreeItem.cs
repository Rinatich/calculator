using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Items
{
    public abstract class BinaryTreeItem : ICalculatorTreeItem
    {
        public ICalculatorTreeItem LeftItem { get; }

        public ICalculatorTreeItem RightItem { get; }

        protected BinaryTreeItem(ICalculatorTreeItem left, ICalculatorTreeItem right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            if (right == null)
                throw new ArgumentNullException(nameof(right));

            LeftItem = left;
            RightItem = right;
        }

        public abstract Decimal Calculate();
    }
}
