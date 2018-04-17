using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Items;

namespace Calculator
{
    public class Calculator
    {
        private static readonly Char _ParenthesisStart = '(';
        private static readonly Char _ParenthesisEnd = ')';
        private static readonly Char _UnaryMinus = '-';

        private static readonly Dictionary<Char, IBinaryTreeItemDecription> _BinaryOperators =
            new Dictionary<Char, IBinaryTreeItemDecription>();

        static Calculator()
        {
            _BinaryOperators.Add('+', new BinaryTreeItemDecription(1, (l, r) => new PlusTreeItem(l, r)));
            _BinaryOperators.Add('-', new BinaryTreeItemDecription(1, (l, r) => new MinusTreeItem(l, r)));
            _BinaryOperators.Add('*', new BinaryTreeItemDecription(2, (l, r) => new MultiplyTreeItem(l, r)));
            _BinaryOperators.Add('/', new BinaryTreeItemDecription(2, (l, r) => new DivideTreeItem(l, r)));
        }

        private IBinaryTreeItemDecription GetBinaryOperatorOrNull(Char c, Boolean isUnaryPossible)
        {
            if (isUnaryPossible && c == _UnaryMinus)
                return null;

            IBinaryTreeItemDecription __description;
            if (!_BinaryOperators.TryGetValue(c, out __description))
                __description = null;

            return __description;
        }

        private Boolean IsParenthesisStart(Char c)
        {
            return c == _ParenthesisStart;
        }

        private Boolean IsParenthesisEnd(Char c)
        {
            return c == _ParenthesisEnd;
        }

        private Boolean IsNotPartOfNumber(Char c, Boolean isUnaryPossible)
        {
            return IsParenthesisStart(c) || IsParenthesisEnd(c) || GetBinaryOperatorOrNull(c, isUnaryPossible) != null;
        }

        private void SkipWhite(String expression, ref Int32 currentIndex)
        {
            while (currentIndex < expression.Length && Char.IsWhiteSpace(expression[currentIndex]))
                ++currentIndex;
        }

        private Decimal ReadNumber(String expression, ref Int32 currentIndex, Boolean isUnaryPossible)
        {
            var __builder = new StringBuilder();
            while (currentIndex < expression.Length)
            {
                var __char = expression[currentIndex];
                if (IsNotPartOfNumber(__char, isUnaryPossible && __builder.Length == 0))
                    break;
                __builder.Append(__char);
                ++currentIndex;
            }

            Decimal __result;
            var __numberStr = __builder.ToString();
            if (
                !Decimal.TryParse(__numberStr,
                    NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign |
                    NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out __result))
                throw new Exception($"Не удалось распознать число из строки {__numberStr} по индексу {currentIndex}");

            return __result;
        }

        private ICalculatorTreeItem BuildTree(String expression, ref Int32 currentIndex)
        {
            var __stack = new CalculatorStack();

            while (currentIndex < expression.Length)
            {
                SkipWhite(expression, ref currentIndex);
                ICalculatorTreeItem __nextItem;
                if (IsParenthesisStart(expression[currentIndex]))
                {
                    ++currentIndex;
                    __nextItem = BuildTree(expression, ref currentIndex);

                    if (currentIndex >= expression.Length || !IsParenthesisEnd(expression[currentIndex]))
                        throw new Exception($"Не найдена закрываюая скобка, ожидалась по индексу {currentIndex}");

                    ++currentIndex;
                }
                else
                {
                    var __number = ReadNumber(expression, ref currentIndex, __stack.IsUnaryPossible());
                    __nextItem = new SimpleTreeItem(__number);
                }

                SkipWhite(expression, ref currentIndex);
                var __operator = currentIndex >= expression.Length
                    ? null
                    : GetBinaryOperatorOrNull(expression[currentIndex], false);

                if (__operator == null)
                {
                    var __result = __stack.PushFinalItemAndGetResult(__nextItem);
                    return __result;
                }

                __stack.PushItemAndOperator(__nextItem, __operator);
                ++currentIndex;
            }

            throw new Exception($"Не удалось распознать выражение по индексу {currentIndex}");
        }

        public Decimal Calculate(String inputData)
        {
            if (String.IsNullOrWhiteSpace(inputData))
                throw new ArgumentNullException(nameof(inputData));

            var __index = 0;
            var __tree = BuildTree(inputData, ref __index);
            if (__index < inputData.Length)
                throw new Exception($"Распознавание выражение было остановлено по индексу {__index}");

            var __result = __tree.Calculate();

            return __result;
        }
    }
}
