using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Blackjack
{
    public class Count
    {

    }

    public class Cards
    {

    }

    public class Strategy
    {
        private static bool GetDeviationMove()
        {
            Excel.Workbook xlBook = xlApp.Workbooks.Open(Path + "base/data/Deviations.xlsx");

            Excel._Worksheet xlSheetHard = xlBook.Worksheets[1];
            Excel.Range xlRangeHard = xlSheetHard.UsedRange;

            Excel._Worksheet xlSheetSplit = xlBook.Worksheets[2];
            Excel.Range xlRangeSplit = xlSheetSplit.UsedRange;

            int PV = PlayerHand[ActiveHand].Value;
            int DV = DealerHand.Value;

            string xlCell = "/";

            if (PossibleDouble && Splitable[ActiveHand])
            {       // Check for splitting
                try
                {
                    xlCell = xlRangeSplit.Cells[PV / 2, DV].Value2.ToString();
                    goto DevFound;
                }
                catch { }
            }

        CheckHard:
            if (!PlayerHand[ActiveHand].Soft)
            {       // Check for a hard hand
                try
                {
                    xlCell = xlRangeHard.Cells[PV, DV].Value2.ToString();
                }
                catch
                {
                    MessageBox.Show("Null Reference: [" + PV.ToString() + ", " + DV.ToString() + "]", "NR DEV");
                    xlBook.Close();
                    CorrectMoveDev.Move = '\0';
                    return false;
                }
            }

            if (xlCell == "/")
            {
                xlBook.Close();
                CorrectMoveDev.Move = '\0';
                return false;
            }

        DevFound:

            char move1 = xlCell[0];
            char move2 = xlCell[3];
            CorrectMoveDev.Count = (xlCell[1] == '+' ? (1) : (-1)) * (xlCell[2] - 0x30);
            char finalMove;

            int cardsRemaining = 0;

            for (int i = 0; i <= 12; i++)
            {
                cardsRemaining += Diamond[i].Amount;
                cardsRemaining += Clover[i].Amount;
                cardsRemaining += Spade[i].Amount;
                cardsRemaining += Heart[i].Amount;
            }

            if (GetTrueCount() >= CorrectMoveDev.Count)
            {
                finalMove = move1;
                CorrectMoveDev.Above = true;
            }
            else
            {
                finalMove = move2;
                CorrectMoveDev.Above = false;
            }


            switch (finalMove)
            {
                case 'H':
                case 'S':
                    CorrectMoveDev.Move = finalMove;

                    break;

                case 'D':
                    CorrectMoveDev.Move = PossibleDouble ? 'D' : 'H';
                    break;

                case 'Y':
                    CorrectMoveDev.Move = '!';
                    break;

                case 'N':
                    xlCell = "/";
                    goto CheckHard;
            }

            xlBook.Close();
            return true;
        }
    }
}
