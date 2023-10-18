using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Calculator
{
    public static void Run()
    {
        Update();
    }

    
    public static void Update()
    {
        Menu menu = new();
        menu.StartMenu();
    }
}
