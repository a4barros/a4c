using a4c;

var t = Lexer.ProcessString("1+23 * 4/5");
Console.WriteLine(t.Consume());
