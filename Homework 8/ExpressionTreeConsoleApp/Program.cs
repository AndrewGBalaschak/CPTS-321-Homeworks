// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text;
using SpreadsheetEngine;

int selection = 0;
ExpressionTree tree = new ExpressionTree("A1+1");

while (selection != 4)
{
    StringBuilder menu = new StringBuilder();
    menu.AppendLine("Menu (current expression = \"" + tree.Expression + "\")");
    menu.AppendLine("\t1 = Enter a new expression");
    menu.AppendLine("\t2 = Set a variable value");
    menu.AppendLine("\t3 = Evaluate Tree");
    menu.AppendLine("\t4 = Quit");

    Console.WriteLine(menu);

    do
    {
        selection = int.Parse(Console.ReadLine());
    }
    while (selection < 1 || selection > 4);

    switch (selection)
    {
        case 1:
            Console.Write("Enter new expression: ");
            string expression = Console.ReadLine();
            tree = new ExpressionTree(expression);
            break;
        case 2:
            Console.Write("Enter variable name: ");
            string varName = Console.ReadLine();
            string varValue = string.Empty;
            double num;
            do
            {
                Console.Write("Enter variable value: ");
                varValue = Console.ReadLine();
            }
            while (!double.TryParse(varValue, out num));

            tree.SetVariable(varName, num);
            break;
        case 3:
            Console.WriteLine("Evaluated expression: " + tree.Evaluate());
            break;
        case 4:
            Console.WriteLine("Done.");
            break;
    }
}