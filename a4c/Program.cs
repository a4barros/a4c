using a4c;

while (true)
{
    Console.Write(">> ");
    var input = Console.ReadLine();
    try
    {
        var l = Lexer.ProcessString(input ?? "");
        var expr = new Parser(l);
        Console.WriteLine(expr.Parse().Evaluate());
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
