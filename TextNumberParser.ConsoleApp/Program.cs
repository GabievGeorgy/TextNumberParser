using TextNumberParser.Parser;

if (args.Length == 0)
{
    Console.WriteLine("There is no argument provided!");
    return;
}

var parser = new AntlrTextNumberParser();
var output = parser.Parse(args[0]);

Console.WriteLine(output);
