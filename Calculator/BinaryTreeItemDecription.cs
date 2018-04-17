using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BinaryTreeItemDecription : IBinaryTreeItemDecription
    {
        private readonly Func<Decimal, Decimal, Decimal> _itemCreator;

        public Int32 Priority { get; }

        public BinaryTreeItemDecription(Int32 priority, Func<Decimal, Decimal, Decimal> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            Priority = priority;
            _itemCreator = creator;
        }

        public Decimal Calculate(Decimal left, Decimal right)
        {
            return _itemCreator(left, right);
        }
    }
}
