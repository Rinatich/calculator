using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface IBinaryTreeItemDecription
    {
        Int32 Priority { get; }

        Decimal Calculate(Decimal left, Decimal right);
    }
}
