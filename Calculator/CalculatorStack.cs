using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorStack
    {
        private readonly Stack<Decimal> _itemStack = new Stack<Decimal>();
        private readonly Stack<IBinaryTreeItemDecription> _operatorStack = new Stack<IBinaryTreeItemDecription>();

        private void Calculate(Func<IBinaryTreeItemDecription, Boolean> predicate)
        {
            while (_operatorStack.Count > 0 && predicate(_operatorStack.Peek()))
            {
                var __operator = _operatorStack.Pop();

                if (_itemStack.Count < 2)
                    throw new Exception("Произошел невозможный результат");

                var __right = _itemStack.Pop();
                var __left = _itemStack.Pop();
                _itemStack.Push(__operator.Calculate(__left, __right));
            }
        }

        public Boolean IsUnaryPossible() => _itemStack.Count == 0;

        public Decimal PushFinalItemAndGetResult(Decimal lastItem)
        {_itemStack.Push(lastItem);
            Calculate(i => true);

            if (_operatorStack.Count > 0 || _itemStack.Count > 1)
                throw new Exception("Произошла невозможная ошибка");

            return _itemStack.Pop();
        }

        public void PushItemAndOperator(Decimal item, IBinaryTreeItemDecription desc)
        {
            _itemStack.Push(item);
            Calculate(i => i.Priority >= desc.Priority);
            _operatorStack.Push(desc);
        }
    }
}
