using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Items;

namespace Calculator
{
    public class CalculatorStack
    {
        private readonly Stack<ICalculatorTreeItem> _itemStack = new Stack<ICalculatorTreeItem>();
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
                _itemStack.Push(__operator.CreateItem(__left, __right));
            }
        }

        public Boolean IsUnaryPossible() => _itemStack.Count == 0;

        public ICalculatorTreeItem PushFinalItemAndGetResult(ICalculatorTreeItem lastItem)
        {
            if (lastItem == null)
                throw new ArgumentNullException(nameof(lastItem));

            _itemStack.Push(lastItem);
            Calculate(i => true);

            if (_operatorStack.Count > 0 || _itemStack.Count > 1)
                throw new Exception("Произошла невозможная ошибка");

            return _itemStack.Pop();
        }

        public void PushItemAndOperator(ICalculatorTreeItem item, IBinaryTreeItemDecription desc)
        {
            _itemStack.Push(item);
            Calculate(i => i.Priority >= desc.Priority);
            _operatorStack.Push(desc);
        }
    }
}
