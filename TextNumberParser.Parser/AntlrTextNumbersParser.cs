using Antlr4.Runtime;
using TextNumberParser.Parser.Visitors;

namespace TextNumberParser.Parser
{
    public class AntlrTextNumberParser : ITextNumberParser
    {
        public string Parse(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new TextNumbersLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new TextNumbersParser(tokenStream);
            var tree = parser.content();
            var visitor = new ContentVisitor();
            
            return visitor.Visit(tree);
        }
    }
}
