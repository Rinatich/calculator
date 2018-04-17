using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Items;

namespace Calculator
{
    public class BinaryTreeItemDecription : IBinaryTreeItemDecription
    {
        private readonly Func<ICalculatorTreeItem, ICalculatorTreeItem, ICalculatorTreeItem> _itemCreator;

        public Int32 Priority { get; }

        public BinaryTreeItemDecription(Int32 priority, Func<ICalculatorTreeItem, ICalculatorTreeItem, ICalculatorTreeItem> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            Priority = priority;
            _itemCreator = creator;
        }

        public ICalculatorTreeItem CreateItem(ICalculatorTreeItem left, ICalculatorTreeItem right)
        {
            return _itemCreator(left, right);
        }
    }
}
