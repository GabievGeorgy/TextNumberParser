using Antlr4.Runtime.Misc;

namespace TextNumberParser.Parser.Visitors
{
    internal class NumberVisitor : TextNumbersBaseVisitor<decimal>
    {
        private static readonly Dictionary<string, int> digitValues = new()
        {
            {"nought", 0},
            {"zero", 0},
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
        };

        private static readonly Dictionary<string, int> tensValues = new()
        {
            {"ten", 10},
            {"eleven", 11},
            {"twelve", 12},
            {"thirteen", 13},
            {"fourteen", 14},
            {"fifteen", 15},
            {"sixteen", 16},
            {"seventeen", 17},
            {"eighteen", 18},
            {"nineteen", 19},
            {"twenty", 20},
            {"thirty", 30},
            {"forty", 40},
            {"fifty", 50},
            {"sixty", 60},
            {"seventy", 70},
            {"eighty", 80},
            {"ninety", 90},
        };

        private const string PointToken = "point";

        private decimal _currentValue;
        private decimal _tempValue;

        public override decimal VisitDigit([NotNull] TextNumbersParser.DigitContext context)
        {
            var token = context.GetText().Trim().ToLower();
            _tempValue += digitValues[token];

            return base.VisitDigit(context);
        }

        public override decimal VisitTens([NotNull] TextNumbersParser.TensContext context)
        {
            var token = context.GetText().Trim().ToLower();
            _tempValue += tensValues[token];

            return base.VisitTens(context);
        }

        public override decimal VisitTensHyphenDigit([NotNull] TextNumbersParser.TensHyphenDigitContext context)
        {
            var token = context.GetText().Trim().ToLower().Split('-');
            var ten = token[0];
            var digit = token[1];

            _tempValue += tensValues[ten];
            _tempValue += digitValues[digit];

            return base.VisitTensHyphenDigit(context);
        }

        public override decimal VisitHundreds([NotNull] TextNumbersParser.HundredsContext context)
        {
            MultiplyOrAddTempValue(100);

            return base.VisitHundreds(context);
        }

        public override decimal VisitThousands([NotNull] TextNumbersParser.ThousandsContext context)
        {
            HandleVisitAbove1000(1_000);

            return base.VisitThousands(context);
        }

        public override decimal VisitMillions([NotNull] TextNumbersParser.MillionsContext context)
        {
            HandleVisitAbove1000(1_000_000);

            return base.VisitMillions(context);
        }

        public override decimal VisitBillions([NotNull] TextNumbersParser.BillionsContext context)
        {
            HandleVisitAbove1000(1_000_000_000);

            return base.VisitBillions(context);
        }

        public override decimal VisitFractional([NotNull] TextNumbersParser.FractionalContext context)
        {
            var tokens = context.GetText().Trim().ToLower().Split(' ');
            var pointTokenIndex = Array.LastIndexOf(tokens, PointToken);
            var numberTokens = tokens[(pointTokenIndex + 1)..];

            var currentMultiplier = 0.1m;

            foreach (var token in numberTokens)
            {
                _tempValue += currentMultiplier * digitValues[token];
                currentMultiplier *= 0.1m;
            }

            return base.VisitFractional(context);
        }

        public override decimal VisitNumber([NotNull] TextNumbersParser.NumberContext context)
        {
            base.VisitNumber(context);
            var result = _currentValue + _tempValue;
            ResetState();

            return result;
        }

        private void HandleVisitAbove1000(int value)
        {
            MultiplyOrAddTempValue(value);

            _currentValue += _tempValue;
            _tempValue = 0;
        }

        private void MultiplyOrAddTempValue(int value)
        {
            if (_tempValue == 0)
            {
                _tempValue += value;
            }
            else
            {
                _tempValue *= value;
            }
        }

        private void ResetState()
        {
            _tempValue = 0;
            _currentValue = 0;
        }
    }
}
