using TextNumberParser.Parser;

namespace TextNumberParser.ParserTests
{
    [TestClass]
    public class AntlrTextNumberParserTests
    {
        private readonly ITextNumberParser _textNumberParser;

        public AntlrTextNumberParserTests()
        {
            _textNumberParser = new AntlrTextNumberParser();
        }

        [DataRow(
            "He paid twenty millions for three such cars.", 
            "He paid 20000000 for 3 such cars.", 
            DisplayName = "Two numbers in a line")]
        [DataRow(
            "Forty-one billion four hundred ninety-two million six hundred thirty-eight thousand five hundred twenty-six", 
            "41492638526",
            DisplayName = "Billion")]
        [DataRow(
            "Sixty nine point nine eight one",
            "69.981",
            DisplayName = "Fractional number")]
        [DataRow(
            "Forty-one notebooks",
            "41 notebooks",
            DisplayName = "Hyphen numbers")]
        [DataRow(
            "TeN tHoUsAnDs of birds were circling over the rocks",
            "10000 of birds were circling over the rocks",
            DisplayName = "Upper and lowercase in the same number")]
        [DataRow(
            "Point point nought point point nought nought five",
            "Point point nought point 0.005",
            DisplayName = "Repeating token around fractional")]
        [DataRow(
            "Three whitespaces:   one hundred   .",
            "3 whitespaces:   100   .",
            DisplayName = "Whitespaces don't disappear")]
        [DataRow(
            "Radiotelephone",
            "Radiotelephone",
            DisplayName = "Number in word")]
        [TestMethod]
        public void ParseTest(string input, string expectedOutput)
        {
            var output = _textNumberParser.Parse(input);
            Assert.AreEqual(expectedOutput, output);
        }
    }
}