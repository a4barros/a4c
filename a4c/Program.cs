using a4c;

while (true)
{
    var input = Console.ReadLine();
    var l = Lexer.ProcessString(input ?? "");
    var expr = new Parser(l);
    Console.WriteLine(expr.Parse().Evaluate());
}
