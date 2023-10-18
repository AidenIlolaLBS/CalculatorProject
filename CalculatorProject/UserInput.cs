using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UserInput
{
    public string userInput;
    public UserInput(string userInput)
    {
        this.userInput = userInput.ToLower();
    }

    string acceptedChar = "0123456789-+*/^ans.,";
    string acceptedStart = "0123456789%-";
    string acceptedEnd = "0123456789%";
    public bool CheckInput()
    {
        Console.Clear();
        
        if (userInput.Length < 3)
        {
            Console.WriteLine("You need to write out something, try again by pressing any key");
            Console.ReadKey();
            return false;
        }
        
        foreach (char chars in userInput)
        {
            if (acceptedChar.Contains(chars))
            {
                continue;
            }
            Console.WriteLine("Your input can only contain these charachters: + - * / ^ Ans . , 0123456789, try again by pressing any key");
            Console.ReadKey();
            return false;
        }
        
        if (CheckExtraOperations())
        {
            if (userInput.Contains("a") || userInput.Contains("n") || userInput.Contains("s"))
            {
                Console.WriteLine("You have to many letters you can only have Ans, try again by pressing any key");
                Console.ReadKey();
                return false;
            }
        }
        else
        {
            return false;
        }
        
        if (!acceptedStart.Contains(userInput[0]))
        {
            Console.WriteLine("You can only begin with a positive or negative number or Ans, try again by pressing any key");
            Console.ReadKey();
            return false;
        }
        
        if (!acceptedEnd.Contains(userInput[userInput.Length-1]))
        {
            Console.WriteLine("You can only end with a number or Ans, try again by pressing any key");
            Console.ReadKey();
            return false;
        }
       
        List<int> commaIndexes = new();
        for (int i = 0; i < userInput.Length; i++)
        {
            if (userInput[i] == ',')
            {
                commaIndexes.Add(i);
            }
        }
        
        if (commaIndexes.ToArray().Length > 0)
        {
            for (int i = 0; i < commaIndexes.ToArray().Length; i++)
            {
                if (!userInput[commaIndexes[i] - 1].ToString().Any(char.IsDigit) || !userInput[commaIndexes[i] + 1].ToString().Any(char.IsDigit) && userInput[commaIndexes[i] + 1].ToString().Contains(","))
                {
                    Console.WriteLine("You need to have a number before and after a comma, try again by pressing any key");
                    Console.ReadKey();
                    return false;
                }            
            }
        }
        return true;
    }

    public List<string> SplitInput()
    {
        List<string> splitInput = new List<string>();

        
        foreach (char item in userInput)
        {
            splitInput.Add(item.ToString());
        }
        
        for (int i = userInput.Length-1; i > 0; i--)
        {
            bool twoNums = splitInput[i].ToString().Any(char.IsDigit) && splitInput[i - 1].ToString().Any(char.IsDigit);
            bool scientificNum = splitInput[i].ToString().Any(char.IsDigit) && splitInput[i-1].ToString().Any(char.IsLetter);
            bool positiveNegativeNums = splitInput[i].Contains("-");
            bool negativeNum = splitInput[i].ToString().Any(char.IsDigit) && splitInput[i - 1].Contains('-');
            bool decimalNum = splitInput[i].ToString().Any(char.IsDigit) && splitInput[i - 1].Contains(',');
            bool twoComas = splitInput[i].ToString().Any(char.IsDigit) && splitInput[i].ToString().Contains(",") && splitInput[i - 1].Contains(',');

            if (twoComas)
            {
                Console.WriteLine("You cannot have two comas in a singel number, try again by pressing any key");
                Console.ReadKey();
                return null;
            }
            if (twoNums && !positiveNegativeNums || negativeNum || decimalNum || scientificNum)
            {
               
                splitInput[i-1] += splitInput[i].ToString();
                splitInput.RemoveAt(i);
            }
            else if (splitInput[i].ToString().Any(char.IsDigit) && splitInput[i - 1].Contains(','))
            {
                splitInput[i - 1] += splitInput[i].ToString();
                splitInput.RemoveAt(i);
            }
        }
        return splitInput;
    }

    private bool CheckExtraOperations()
    {
        string acceptedOperations = "+-/*^";

        for (int i = 0; i < userInput.Length; i++)
        {
            userInput = userInput.Replace("--", "+");
            userInput = userInput.Replace("+-", "-");
            userInput = userInput.Replace("++", "+");
            userInput = userInput.Replace("**", "*");
            userInput = userInput.Replace("//", "/");
            userInput = userInput.Replace("^^", "^");
            userInput = userInput.Replace("..", ".");
        }
        userInput = userInput.Replace(".", ",");
        userInput = userInput.Replace("ans", "%");

        //Checks if there are two operators after eachother
        for (int j = 0; j < acceptedOperations.Length; j++)
        {
            for (int k = 0; k < acceptedOperations.Length; k++)
            {
                if ($"{acceptedOperations[j].ToString() + acceptedOperations[k].ToString()}" != "/-" && $"{acceptedOperations[j].ToString() + acceptedOperations[k].ToString()}" != "*-")
                {
                    if (userInput.Contains($"{acceptedOperations[j].ToString() + acceptedOperations[k].ToString()}"))
                    {
                        Console.WriteLine("You can not have multiple operations after eachother except for + - and operations before parentheses, try again by pressing any key");
                        Console.ReadKey();
                        return false;
                    }
                }               
            }
        }
        return true;
    }
}

