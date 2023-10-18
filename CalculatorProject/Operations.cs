using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Operations
{
    public List<string> userInput;
    bool isDecimalPrecision;
    string previousAnswer;
    public Operations(List<string> userInput, bool isDecimalPrecision, string previousAnswer)
    {
        this.userInput = userInput;
        this.isDecimalPrecision = isDecimalPrecision;
        this.previousAnswer = previousAnswer;
    }

    public string Start()
    {       
        for (int i = 0; i < userInput.ToArray().Length; i++)
        {
            if (userInput[i] == "%")
            {
                userInput[i] = previousAnswer;
            }
            
        }
        while (userInput.Contains("^"))
        {
            int powerIndex = userInput.FindIndex(item => item == "^");
            Power(userInput[powerIndex-1], userInput[powerIndex+1], powerIndex);
        }
        while (userInput.Contains("*") || userInput.Contains("/"))
        {
            int multiplyIndex = userInput.FindIndex(item => item == "*");
            int divideIndex = userInput.FindIndex(item => item == "/");
            if (multiplyIndex < divideIndex && multiplyIndex > -1 || divideIndex == -1)
            {
                Multiply(userInput[multiplyIndex - 1], userInput[multiplyIndex + 1], multiplyIndex);
            }
            else
            {
                Divide(userInput[divideIndex - 1], userInput[divideIndex + 1], divideIndex);
            }
        }
        while (userInput.Contains("+"))
        {
            int addIndex = userInput.FindIndex(item => item == "+");
            Add(userInput[addIndex - 1], userInput[addIndex + 1], addIndex);
        }
        //The userinput will miss negative / subtracting signs inbeetwen numbers because they will be linked togheter with one of the nums
        while (userInput.ToArray().Length > 1)
        {
            Add(userInput[0], userInput[1]);
        }
        return userInput[0];
    }
    
    //All operation except power of will look if the asnwer will be in decimal or double depending on users choice

    private void Add(string num1, string num2)
    {
        if (isDecimalPrecision)
        {
            userInput[0] = (decimal.Parse(num1) + decimal.Parse(num2)).ToString();
        }
        else
        {
            userInput[0] = (double.Parse(num1) + double.Parse(num2)).ToString();           
        }
        userInput.RemoveAt(1);
    }

    
    private void Add(string num1, string num2, int index)
    {

        if (isDecimalPrecision)
        {
            userInput[index - 1] = (decimal.Parse(num1) + decimal.Parse(num2)).ToString();
        }
        else
        {
            userInput[index - 1] = (double.Parse(num1) + double.Parse(num2)).ToString();
        }
        userInput.RemoveAt(index);
        userInput.RemoveAt(index);
    }
    
    private void Multiply(string num1, string num2, int index)
    {
        if (isDecimalPrecision)
        {
            userInput[index - 1] = (decimal.Parse(num1) * decimal.Parse(num2)).ToString();
        }
        else
        {
            userInput[index - 1] = (double.Parse(num1) * double.Parse(num2)).ToString();
        }
        userInput.RemoveAt(index);
        userInput.RemoveAt(index);
    }
    
    private void Divide(string num1, string num2, int index)
    {
        if (isDecimalPrecision)
        {
            userInput[index - 1] = (decimal.Parse(num1) / decimal.Parse(num2)).ToString();
        }
        else
        {
            userInput[index - 1] = (double.Parse(num1) / double.Parse(num2)).ToString();
        }
        userInput.RemoveAt(index);
        userInput.RemoveAt(index);
    }

    private void Power(string num1, string num2, int index)
    {
        userInput[index - 1] = (Math.Pow(double.Parse(num1), double.Parse(num2))).ToString();
        userInput.RemoveAt(index);
        userInput.RemoveAt(index);
    }
}
