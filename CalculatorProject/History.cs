using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class History
{     
    List<SavePrevAnsList> allPrevAnswers = new List<SavePrevAnsList>();

    public class SavePrevAnsList
    {
        public decimal decimalPreviousAnswer { get; set; }
        public double doublePreviousAnswer { get; set; }
    }
    public void SavePrevAns(decimal answerDecimal, double answerDouble)
    {
        SavePrevAnsList savePrevAnswer = new SavePrevAnsList();
        savePrevAnswer.decimalPreviousAnswer = answerDecimal;
        savePrevAnswer.doublePreviousAnswer = answerDouble;
        allPrevAnswers.Add(savePrevAnswer);
    }
    public List<SavePrevAnsList> GetValue()
    {
        return allPrevAnswers;
    }
}

