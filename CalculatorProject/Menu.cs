using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Menu
{
    public void StartMenu()
    {
        //Showing some useful information before showing the menu
        Text(-1);
        Console.ReadKey();
        MenuSelection();
    }


    bool isDecimalPrecision = false;
    History history = new History();
    private void MenuSelection()
    {
        int menuChoice = 0;
        
        while (menuChoice != 5)
        {
            Text(0);
            //Catches error if you input letters instead of numbers
            try
            {
                menuChoice = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Your input can only be between 1 and 5, try again by pressing any key");
                Console.ReadKey();
                continue;
            }
            switch (menuChoice)
            {
                case 1:
                    bool exit = false;
                    while (!exit)
                    {                                          
                        string previousAnswerString;
                        if (history.GetValue().ToArray().Length > 0)
                        {
                            History.SavePrevAnsList preiousvAnswer = history.GetValue()[history.GetValue().ToArray().Length - 1];
                            if (isDecimalPrecision)
                            {
                                previousAnswerString = preiousvAnswer.decimalPreviousAnswer.ToString();
                            }
                            else
                            {
                                previousAnswerString = preiousvAnswer.doublePreviousAnswer.ToString();
                            }                            
                        }
                        else
                        {
                            previousAnswerString = "0";
                        }

                        Console.Clear();
                        Console.WriteLine("Write out your calculation");
                        UserInput userInput = new(Console.ReadLine());
                        switch (userInput.userInput)
                        {
                            case "!help":
                                Text(4);
                                Console.ReadKey();
                                break;
                            case "!exit":
                                Environment.Exit(0);
                                break;
                            case "!back":
                                exit = true;
                                break;
                            default:
                                if (userInput.CheckInput())
                                {
                                    List<string> splitedInput = userInput.SplitInput();
                                    if (splitedInput.ToArray().Length != 0)
                                    {
                                        Operations operations = new(splitedInput, isDecimalPrecision, previousAnswerString);
                                        if (isDecimalPrecision)
                                        {
                                            history.SavePrevAns(decimal.Parse(operations.Start()), 0);
                                        }
                                        else
                                        {
                                            history.SavePrevAns(0, double.Parse(operations.Start()));
                                        }
                                        Text(1);
                                        Console.ReadKey();
                                    }                               
                                }
                                break;
                        }                      
                    }
                    break;
                case 2:
                    Text(2);
                    Console.ReadKey();
                    break;
                case 3:                   
                    bool exitSettings = false;
                    while (!exitSettings)
                    {
                        Text(3);
                        string changePrecision = Console.ReadLine().ToLower();
                        switch (changePrecision)
                        {
                            case "yes":
                                if (isDecimalPrecision)
                                {
                                    isDecimalPrecision = false;
                                }
                                else
                                {
                                    isDecimalPrecision = true;
                                }                             
                                break;
                            case "back":
                                exitSettings = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Your input can only be Yes or back, try again by pressing any key");
                                Console.ReadKey();
                                break;
                        }
                    }                   
                    break;
                case 4:
                    Text(4);
                    Console.ReadKey();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Your input can only be between 1 and 5, try again by pressing any key");
                    Console.ReadKey();
                    break;
            }
        }
        Environment.Exit(0);
    }

    public bool IsDecimalPrecision()
    {
        if (!isDecimalPrecision)
        {
            return false;
        }
        else return true;
    }

    public void Text(int input)
    {
        Console.Clear();
        switch (input)
        {
            case -1:
                Console.WriteLine("Welcome to your calculator?\n" +
                                  "Write out what you want to calculate without spaces\n" +
                                  "ex. 2+2-54/6*2+3 : correct input\n" +
                                  "ex. 2+2-54/*2+3 : invalid input\n" +
                                  "You cant have two operations after eachother except except for + and -\n" +
                                  "You can always write !help to get more information\n" +
                                  "or write !exit to close the program\n\n");
                Console.WriteLine("Press any key to continue");
                break;
            case 0:
                Console.WriteLine("Welcome to your calculator!\n" +
                                  "1. Calculator\n" +
                                  "2. Calculation history\n" +
                                  "3. Settings\n" +
                                  "4. Information\n" +
                                  "5. Exit\n\n");
                Console.Write("Choose one option by writting the number, your answer: ");
                break;
            case 1:
                History.SavePrevAnsList answer = history.GetValue()[history.GetValue().ToArray().Length - 1];
                if (isDecimalPrecision)
                {
                    
                    Console.WriteLine($"Your answer is: {answer.decimalPreviousAnswer}");
                }
                else
                {
                    Console.WriteLine($"Your answer is: {answer.doublePreviousAnswer}");
                }
                
                Console.WriteLine("Press any key to continue, you can write !back to go back to the menu and !exit to close the program");
                break;
            case 2:
                Console.WriteLine("Your latest answers: ");
                foreach (History.SavePrevAnsList item in history.GetValue())
                {
                    if (item.doublePreviousAnswer == 0)
                    {
                        Console.WriteLine(item.decimalPreviousAnswer);
                    }
                    else
                    {
                        Console.WriteLine(item.doublePreviousAnswer);
                    }
                }
                Console.WriteLine("Press any key to go bakc to the menu");              
                break;
            case 3:
                if (isDecimalPrecision)
                {
                    Console.WriteLine("Settings\n" +
                                  "Answer precision: 28 decimals\n" +
                                  "Do you want to change it to 15 decimal places, this means also that your number can be much larger");
                    Console.Write("Yes or Back, your answer: ");
                }
                else
                {
                    Console.WriteLine("Settings\n" +
                                  "Answer precision: 15 decimals\n" +
                                  "Do you want to change it to 28 decimal places, this means also that your number will need to be smaller\n" +
                                  "This will not effect exponents as they will be approximated to 15 decimal places");
                    Console.Write("Yes or Back, your answer: ");
                }                
                break;
            case 4:
                Console.WriteLine("Information how you use the calculator\n" +
                                  "You will be able to write out what you want to calculate\n" +
                                  "In your input you can only use these symbols: + - * / ^ Ans . , 0123456789 \n" +
                                  "You will not be able to use operations after eachother except for + -\n" +
                                  "Ans will use the previous answer but if there are none it will use 0 as its value\n" +
                                  "If you have the 28 decimal option activeted, the answer from exponents will be approximated to 15 decimals\n" +
                                  "Write !Help to get this information in the calculator, write !Back to go back to the menu and write !Exit to leave the program\n\n" +
                                  "Press any button to go back to the menu");
                break;
            default:
                break;
        }
    }
}

