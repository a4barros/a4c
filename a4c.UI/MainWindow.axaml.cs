using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace a4c.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }
    private void Evaluate()
    {
        try
        {
            var l = Lexer.ProcessString(InputBox.Text ?? "");
            var expr = new Parser(l);
            InputBox.Text = Convert.ToString(expr.Parse().Evaluate());
        }
        catch (Exception ex)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Syntax error", ex.Message);

            box.ShowAsync();
        }
    }
    public void Evaluate(object source, RoutedEventArgs args)
    {
        Evaluate();
    }

    private void Window_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        var operators = new[] { "+", "-", "*", "/", "^", ".", "(", ")" };

        if (e.KeySymbol is null)
        {
            return; 
        }
        if (operators.Contains(e.KeySymbol))
        {
            if (InputBox.Text is not null && InputBox.Text == "0")
            {
                InputBox.Text = e.KeySymbol;
            }
            else
            {
                InputBox.Text += e.KeySymbol;
            }
        }
        if (e.KeySymbol.All(Char.IsAsciiLetterOrDigit))
        {
            if (InputBox.Text is not null && InputBox.Text == "0")
            {
                InputBox.Text = e.KeySymbol;
            }
            else
            {
                InputBox.Text += e.KeySymbol;
            }
        }
        if (e.PhysicalKey == Avalonia.Input.PhysicalKey.Backspace)
        {
            if (InputBox.Text is not null && InputBox.Text.Length > 1)
            {
                InputBox.Text = InputBox.Text[..^1];
            }
            else
            {
                InputBox.Text = "0";
            }
        }
        if (e.PhysicalKey == Avalonia.Input.PhysicalKey.Enter || e.PhysicalKey == Avalonia.Input.PhysicalKey.NumPadEnter)
        {
            Evaluate();
        }
    }
}