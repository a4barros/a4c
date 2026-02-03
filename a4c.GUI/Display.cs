using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Vulkan;
using MsBox.Avalonia;

namespace a4c.UI
{
    internal class Display
    {
        private static TextBox? DisplayBox;
        private static Display? instance;
        private Display(TextBox displayTextBox)
        {
            DisplayBox = displayTextBox;
        }
        public static Display GetInstance(TextBox display)
        {
            if (instance == null)
            {
                instance = new Display(display);
            }
            return instance;
        }
        public void AppendText(string text)
        {
            if (DisplayBox?.Text is not null && DisplayBox.Text == "0")
            {
                DisplayBox.Text = text;
            }
            else
            {
                DisplayBox?.Text += text;
            }
        }
        public void Clear()
        {
            DisplayBox?.Text = "0";
        }
        public void Backspace()
        {
            if (DisplayBox?.Text is not null && DisplayBox.Text.Length > 1)
            {
                DisplayBox.Text = DisplayBox.Text[..^1];
            }
            else
            {
                DisplayBox?.Text = "0";
            }
        }
        public void Evaluate()
        {
            try
            {
                var l = Lexer.ProcessString(DisplayBox?.Text ?? "");
                var expr = new Parser(l);
                DisplayBox?.Text = Convert.ToString(expr.Parse().Evaluate());
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Syntax error", ex.Message);

                box.ShowAsync();
            }
        }
    }
}
