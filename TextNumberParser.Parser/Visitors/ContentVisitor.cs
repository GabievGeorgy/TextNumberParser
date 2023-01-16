using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TextNumberParser.Parser.Visitors
{
    internal class ContentVisitor : TextNumbersBaseVisitor<string>
    {
        private readonly NumberVisitor _numberVisitor = new();
        private StringBuilder _result = new();

        public override string VisitContent([NotNull] TextNumbersParser.ContentContext context)
        {
            base.VisitContent(context);

            return _result.ToString();
        }

        public override string VisitNotNumber([NotNull] TextNumbersParser.NotNumberContext context)
        {
            _result.Append(context.GetText());

            return base.VisitNotNumber(context);
        }

        public override string VisitNumber([NotNull] TextNumbersParser.NumberContext context)
        {
            var number = _numberVisitor.VisitNumber(context);
            _result.Append(number);

            if (context.GetText().EndsWith(' '))
            {
                _result.Append(' ');
            }

            return base.VisitNumber(context);
        }
    }
}
