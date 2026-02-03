using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace a4c.UI;

public partial class MainWindow : Window
{
    Display display;
    public MainWindow()
    {
        InitializeComponent();
        display = Display.GetInstance(InputBox);
    }

    public void Evaluate(object source, RoutedEventArgs args)
    {
        display.Evaluate();
    }

    private void Window_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        var operators = new[] { "+", "-", "*", "/", "^", ".", "(", ")" };

        if (e.KeySymbol is null)
        {
            return;
        }
        else if (operators.Contains(e.KeySymbol))
        {
            display.AppendText(e.KeySymbol);
        }
        else if (e.KeySymbol.All(Char.IsAsciiLetterOrDigit))
        {
            display.AppendText(e.KeySymbol);
        }
        else if (e.PhysicalKey == Avalonia.Input.PhysicalKey.Backspace)
        {
            display.Backspace();
        }
        else if (e.PhysicalKey == Avalonia.Input.PhysicalKey.Enter || e.PhysicalKey == Avalonia.Input.PhysicalKey.NumPadEnter)
        {
            display.Evaluate();
        }
    }
    private void Button0_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("0");
    }
    private void Button1_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("1");
    }
    private void Button2_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("2");
    }
    private void Button3_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("3");
    }
    private void Button4_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("4");
    }
    private void Button5_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("5");
    }
    private void Button6_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("6");
    }
    private void Button7_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("7");
    }
    private void Button8_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("8");
    }
    private void Button9_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("9");
    }
    private void ButtonPlus_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("+");
    }
    private void ButtonMinus_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("-");
    }
    private void ButtonMulti_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("*");
    }
    private void ButtonDiv_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("/");
    }
    private void ButtonPower_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("^");
    }
    private void ButtonSin_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("sin(");
    }
    private void ButtonCos_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("cos(");
    }
    private void ButtonTan_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("tan(");
    }
    private void ButtonDecimal_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText(".");
    }
    private void ButtonOpenParenthesis_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("(");
    }
    private void ButtonCloseParenthesis_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText(")");
    }
    private void ButtonClear_Click(object? sender, RoutedEventArgs e)
    {
        display.Clear();
    }
    private void ButtonBackspace_Click(object? sender, RoutedEventArgs e)
    {
        display.Backspace();
    }

    private void ButtonPi_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("PI");
    }

    private void ButtonE_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("e");
    }

    private void ButtonExp_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("exp(");
    }

    private void ButtonASin_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("asin(");
    }

    private void ButtonACos_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("acos(");
    }

    private void ButtonATan_Click(object? sender, RoutedEventArgs e)
    {
        display.AppendText("atan(");
    }
}