using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Blackjack
{
    public partial class MainMenu : Form
    {


        //   Variables:


        String Path = "";   // Contains the main path to the directory (after loading of Form)


        bool basic_strat;   // Notify about basic strat mistakes

        bool playing_dev;   // Notify about playing deviation mistakes
        bool dev_bs;        // Notify about about deviation mistakes when basic strat is correct
        double play_dev_tol; // The true count tolerance within which the player accepts mistakes
        
        bool bet_dev;       // Notify about betting deviation mistakes
        bool bet_bankroll;  // Notify overbetting with insufficient bankroll
        double bet_dev_tol;  // The true count tolerance within which the player accepts mistakes

        int decks;          // Amount of decks to be used
        double penetration; // Penetration before shuffling
        int bj_pay;         // The pay of a blackjack/natural
        bool hit_s17;       // Whether the player hits on a soft 17

        int bet_unit;       // The betting unit value the player is using
        int bet_spread;     // The bet spread the player is using (max betting units kept, 1-12 -> 12)


        // Files:

        public Excel.Application xlApp = new Excel.Application();



        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets up the position of each button on the form.
        /// Use when launching the app.
        /// </summary>
        /// <param name="width">Width of the screen.</param>
        /// <param name="height">Height of the screen.</param>

        public void Button_Load(int width, int height)
        {
            cmdStart.Location = new Point(width / 2 - cmdStart.Width / 2, height / 2 - 1 * (cmdStart.Height + 25));
            cmdShowChart.Location = new Point(width / 2 - cmdStart.Width / 2, height / 2);
            cmdSettings.Location = new Point(width / 2 - cmdSettings.Width / 2, height / 2 + 1 * (cmdStart.Height + 25));
            cmdExit.Location = new Point(width / 2 - cmdExit.Width / 2, height / 2 + 2 * (cmdStart.Height + 25));
        }
        
        /// <summary>
        /// Sets-up the position of each group box on the form.
        /// Use when launching the app.
        /// </summary>
        /// <param name="width">Width of the screen.</param>
        /// <param name="height">Height of the screen.</param>
        public void GroupBox_Load(int width, int height)
        {
            grboxSettings.Location = new Point(width / 2 - grboxSettings.Width / 2, height / 2 - grboxSettings.Height / 2);
            grboxCharts.Location = new Point(width / 2 - grboxCharts.Width / 2, height / 2 - grboxCharts.Height / 2);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            String TempPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            // < ^^^ Gets the main path of the directory ^^^ >

            for (int i = 0; i <= TempPath.Length - 10; i++)     // <Gets the path to app's folder>
                Path += TempPath[i];                            // </>

            int width = Screen.PrimaryScreen.Bounds.Width;      // <Full Screen setup>
            int height = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point(0, 0);
            this.Size = new Size(width, height);                // </Full Screen setup>
            
            Button_Load(width, height);                         // <Sets-up the positions of the buttons>
            GroupBox_Load(width, height);                       // <Sets-up the positions of the group boxes>
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();           // <Closes the application>
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            Table form = new Table();           // <Table form>
            this.Hide();
            form.Show();
        }

        /// <summary>
        /// Gets the required value from the given text file.
        /// </summary>
        /// <param name="InputText">Line read from the text file.</param>
        private void GetVal(String InputText)
        {
            String Info = "", Val = "";         // <Info - what has been read; Value - its value>
            int i;

            if (InputText == "")
                return;
            
            if (InputText[0] == '#')            // <Break if comment received>
                return;

            for (i = 0; i <= InputText.Length - 1; i++)
            {
                if ((InputText[i] >= 'a' && InputText[i] <= 'z') || (InputText[i] >= 'A' && InputText[i] <= 'Z') || InputText[i] == '_')
                {
                    Info += InputText[i];
                }
                else if (InputText[i] == ':')
                    goto GetValue;
            }

        GetValue:
            for (; i <= InputText.Length - 1; i++)
            {
                if ((InputText[i] >= '0' && InputText[i] <= '9') || InputText[i] == '.')
                {
                    Val += InputText[i];
                }
                else if (InputText[i] == '#')
                    break;
            }

            if (Val != "")
            {
                switch (Info)
                {
                    case "basic_strat": basic_strat = (Val == "1"); break;

                    case "playing_dev": playing_dev = (Val == "1"); break;
                    case "dev_bs_notify": dev_bs = (Val == "1"); break;
                    case "play_dev_tol": play_dev_tol = Convert.ToDouble(Val); break;

                    case "betting_dev": bet_dev = (Val == "1"); break;
                    case "bankroll_limit": bet_bankroll = (Val == "1"); break;
                    case "bet_dev_tol": bet_dev_tol = Convert.ToDouble(Val); break;

                    case "decks": decks = Convert.ToInt32(Val); break;
                    case "penetration": penetration = Convert.ToDouble(Val); break;
                    case "bj_pay": bj_pay = Convert.ToInt16(Val); break;
                    case "hit_s17": hit_s17 = (Val == "1"); break;

                    case "bet_unit": bet_unit = Convert.ToInt16(Val); break;
                    case "bet_spread": bet_spread = Convert.ToInt16(Val); break;
                }
            }
        }

        private void SetupSettings()
        {
            checkBasicStratNotify.Checked = basic_strat;

            checkDevPlayNotify.Checked = playing_dev;
            checkDev_BS_Notify.Checked = dev_bs;
            inTolerancePlay.Text = play_dev_tol.ToString();

            checkBetNotify.Checked = bet_dev;
            checkOverbetNotify.Checked = bet_bankroll;
            inToleranceBet.Text = bet_dev_tol.ToString();

            inDecksUsed.Text = Convert.ToString(decks);
            inPenetration.Text = Convert.ToString(penetration);
            switch (bj_pay)
            {
                case 0: radioBJ3to2.Checked = true; break;
                case 1: radioBJ6to5.Checked = true; break;
                case 2: radioBJ1to1.Checked = true; break;
            }
            checkDHitS17.Checked = hit_s17;

            inBettingUnits.Text = bet_unit.ToString();
            inBetSpread.Text = "1-" + bet_spread.ToString();

            double Chips = Convert.ToDouble(System.IO.File.ReadAllText(Path + "base/profile/chips.txt"));
            inChipsChange.Text = Convert.ToString(Chips);
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            grboxSettings.Visible = true;

            String ReadText;

            System.IO.StreamReader file = new System.IO.StreamReader(Path + "base/profile/settings.txt");

            while ((ReadText = file.ReadLine()) != null)
            {
                GetVal(ReadText);
            }

            file.Close();

            SetupSettings();
        }

        /// <summary>
        /// Writes the newly added values to the settings.txt file.
        /// </summary>
        private void WriteValSettings()
        {
            List<String> Lines = new List<string>();
            string comment;
            /*
                    case "bet_unit": bet_unit = Convert.ToInt16(Val); break;
                    case "bet_spread": bet_spread = Convert.ToInt16(Val); break;*/

            comment = " # Notify the user if they make a basic strat mistake";  // <Basic strat mistakes>
            Lines.Add("basic_strat: " + (basic_strat ? "1" : "0") + comment);

            comment = " # Notify the user if they make a deviation mistake";    // <Deviation Mistakes>
            Lines.Add("playing_dev: " + (playing_dev ? "1" : "0") + comment);
            comment = " # Notify the user if they make a deviation mistake, while basic strat is correct";
            Lines.Add("dev_bs_notify: " + (dev_bs ? "1" : "0") + comment);
            comment = " # Tru count tolerance within which deviation mistakes are allowed";
            Lines.Add("play_dev_tol: " + play_dev_tol.ToString() + comment);    // </Deviation mistakes>

            comment = " # Notify the user if they make a betting mistake";    // <Betting Mistakes>
            Lines.Add("betting_dev: " + (bet_dev ? "1" : "0") + comment);
            comment = " # Notify the user if they overbet with insufficient bankroll";
            Lines.Add("bankroll_limit: " + (bet_bankroll ? "1" : "0") + comment);
            comment = " # Tru count tolerance within which deviation mistakes are allowed";
            Lines.Add("bet_dev_tol: " + bet_dev_tol.ToString() + comment);    // </Betting mistakes>

            comment = " # Amount of decks to be used";                        // <Decks Used>
            Lines.Add("decks: " + Convert.ToString(decks) + comment);
            comment = " # Amount of decks to be played before shuffling";            // <Penetration>
            Lines.Add("penetration: " + Convert.ToString(penetration) + comment);
            comment = " # Blackjack/Natural Payout";                                // <Blackjack Pay>
            Lines.Add("bj_pay: " + Convert.ToString(bj_pay) + comment);
            comment = " # Does the dealer hit on a soft 17";                     // <Dealer hit S17>
            Lines.Add("hit_s17: " + (hit_s17 ? "1" : "0") + comment);

            comment = " # Player's betting unit";                               // <Player's betting unit>
            Lines.Add("bet_unit: " + Convert.ToString(bet_unit) + comment);
            comment = " # Player's *to* bet-spread";                            // <Player's bet spread>
            Lines.Add("bet_spread: " + Convert.ToString(bet_spread) + comment);

            System.IO.File.WriteAllText(Path + "base/profile/settings.txt", "");    // <Clear text file>
            System.IO.File.WriteAllLines(Path + "base/profile/settings.txt", Lines);// <Update Text File>
        }

        private void cmdCloseSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(inDecksUsed.Text) >= 1 && Convert.ToInt16(inDecksUsed.Text) <= 8)
                    decks = Convert.ToInt32(inDecksUsed.Text);
                else
                {   // Gets the amount of decks used (User input)
                    MessageBox.Show("The amount of decks can be between 1 and 8 and must be an integer!", "Incorrect input");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("The amount of decks can be between 1 and 8 and must be an integer!", "Incorrect input");
                return;
            }

            try
            {
                if (Convert.ToDouble(inPenetration.Text) >= 0.5 && Convert.ToDouble(inPenetration.Text) <= decks - 0.5)
                    penetration = Convert.ToDouble(inPenetration.Text);
                else
                {   // Gets the penetration (User input)
                    MessageBox.Show("The penetration must be greater than 0.5 and less than the amount of decks minus half a deck (Decks used - 0.5)!", "Incorrect input");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("There has been an error in converting the penetration value! Please enter a valid number!", "Incorrect input");
                return;
            }

            // User's chips amount
            double Chips = Convert.ToDouble(System.IO.File.ReadAllText(Path + "base/profile/chips.txt"));
            

            try
            {
                if (Convert.ToDouble(inChipsChange.Text) >= 0)
                {   // Write the chips input by the user
                    System.IO.File.WriteAllText(Path + "base/profile/chips.txt", inChipsChange.Text);
                }
                else
                {
                    MessageBox.Show("The amount of chips you have must be a positive value!", "Incorrect inpput");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("The amount of chips must be number greater than 0!", "Incorrect input");
                return;
            }

            
            grboxSettings.Visible = false;

            WriteValSettings();
        }

        private void cmdShowChart_Click(object sender, EventArgs e)
        {
            comboChartSelect.SelectedIndex = 1;
            grboxCharts.Visible = true;
        }
        
        /// <summary>
        /// Generates the chart for the Charts box.
        /// </summary>
        /// <param name="index">Chart Index (0-Hard; 1-Soft; 2-Surrender; 3-Split)</param>
        private void GenerateChart(int index)
        {
            //tableHard.Visible = false;

            Excel.Workbook xlBook;
            if (index == 0 || index == 1)
                goto MovesBook;
            else
                goto InitMoveBook;

        MovesBook:
            xlBook = xlApp.Workbooks.Open(Path + "base/data/Moves.xlsx");
            goto Continue;

        InitMoveBook:
            xlBook = xlApp.Workbooks.Open(Path + "base/data/InitMove.xlsx");
        Continue:

            Excel._Worksheet xlSheet = xlBook.Worksheets[index % 2 + 1];

            Excel.Range xlRange = xlSheet.UsedRange;

            tableHard.SuspendLayout();
            for (int i = 0; i <= 21; i++)
            {
                if (i != 0)
                    tableHard.Controls.Add(new Label { Text = Convert.ToString(i) }, 0, i);

                for (int j = 1; j <= 10; j++)
                {
                    if (i == 0)
                    {
                        string printVal;
                        if (j == 1)
                            printVal = "A";
                        else
                            printVal = Convert.ToString(j);
                        tableHard.Controls.Add(new Label { Text = printVal }, j, 0);
                        continue;
                    }

                    String xlCell;
                    if (xlRange.Cells[i, j].Value2 != null)
                    {
                        xlCell = xlRange.Cells[i, j].Value2.ToString();
                        Color colourText = new Color();
                        switch (xlCell)
                        {
                            case "H": colourText = Color.Teal; break;
                            case "S": colourText = Color.Red; break;
                            case "D": colourText = Color.Green; break;
                            case "DS": colourText = Color.OliveDrab; break;
                            case "Y": colourText = Color.LimeGreen; break;
                            case "N": colourText = Color.Maroon; break;
                            case "Sur": colourText = Color.SeaGreen; break;
                            default: colourText = Color.Black; break;
                        }
                        try
                        {
                            Convert.ToInt16(xlCell);
                        }
                        catch
                        {
                            tableHard.Controls.Add(new Label { Text = xlCell, ForeColor = colourText }, j, i);
                        }
                    }
                }
                //tableHard.Visible = true;
            }
            tableHard.ResumeLayout();
            xlBook.Close();
        }

        private void tableHard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdCloseCharts_Click(object sender, EventArgs e)
        {
            grboxCharts.Visible = false;
        }

        private void comboChartSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableHard.Controls.Clear();
            GenerateChart(comboChartSelect.SelectedIndex);
        }

        private void cmdResetEVD_Click(object sender, EventArgs e)
        {
            DialogResult Res = MessageBox.Show("Do you want to revert your chips to the starting amount?",
                "Reset EV Data", MessageBoxButtons.YesNoCancel);
            if (Res == DialogResult.Cancel)
                return;
            else if (Res == DialogResult.Yes)
            {       // Return the player to the starting bankroll
                double chips = Convert.ToDouble(System.IO.File.ReadAllText(Path + "base/profile/chips.txt"));
                double chipsChange = 0;
                string[] lines = System.IO.File.ReadAllLines(Path + "base/profile/ev_data.txt");

                for (int i = 0; i <= lines.Length - 1; i++)
                {       // Find the "change in money" line
                    string type = "";
                    int j = 0;
                    while (lines[i][j] != ':' && lines[i][j] != '#')
                        type += lines[i][j++];
                    if (type == "money_change")
                    {       // Get the value of the money_change line
                        string arg = "";
                        while (lines[i][j] != '#')
                        {
                            if (lines[i][j] >= '0' && lines[i][j] <= '9' || lines[i][j] == '-' || lines[i][j] == '.')
                                arg += lines[i][j];
                            j++;
                        }
                        chipsChange = Convert.ToDouble(arg);
                        break;
                    }
                }

                chips -= chipsChange;   // Write the data
                string[] line = new string[] { chips.ToString() };
                System.IO.File.WriteAllLines(Path + "base/profile/chips.txt", line);
                inChipsChange.Text = line[0];
            }


            string[] newLines = System.IO.File.ReadAllLines(Path + "base/profile/ev_data.txt");

            for (int i = 0; i <= newLines.Length - 1; i++)
            {       // Reset the ev_data file
                int j = 0;

                while (newLines[i][j] != ':' && newLines[i][j] != '#')
                    j++;

                if (j == 0) continue;

                while (!((newLines[i][j] >= '0' && newLines[i][j] <= '9') || newLines[i][j] == '-' || newLines[i][j] == '.'))
                    j++;    // Find the index the values (numbers) start at

                List<char> str = new List<char>();

                for (int k = 0; k <= newLines[i].Length - 1; k++)
                    str.Add(newLines[i][k]);    // Create a char[] to shift the characters

                while (str[j + 2] != '#')
                {       // Shift the string to the left, delete all digits except one
                    for (int k = j; k <= str.Count - 1; k++)
                    {
                        try
                        {
                            str[k] = str[k + 1];
                        }
                        catch
                        {
                            str[k] = '\0';
                        }
                    }
                }

                str[j] = '0';   // Replace the reamining digit with 0

                newLines[i] = "";
                for (int k = 0; k <= str.Count - 1 && str[k] != '\0'; k++)
                {
                    newLines[i] += str[k];
                }
            }
                // Write the data
            System.IO.File.WriteAllLines(Path + "base/profile/ev_data.txt", newLines);
        }

        private void inBetSpread_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (inBetSpread.Text[0] != '1' || inBetSpread.Text[1] != '-')
                    inBetSpread.Text = "1-";
            }
            catch
            {
                inBetSpread.Text = "1-";
            }

            if (inBetSpread.Text.Length == 2)
                return;

            string spread = "";
            for (int i = 2; i <= inBetSpread.Text.Length - 1; i++)
            {
                spread += inBetSpread.Text[i];
            }
            bet_spread = Convert.ToInt32(spread);
        }

        private void checkDevPlayNotify_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDevPlayNotify.Checked)
            {
                checkDev_BS_Notify.Enabled = true;
                inTolerancePlay.Enabled = true;
                txtTolerancePlay.Enabled = true;
            }
            else
            {
                checkDev_BS_Notify.Checked = false;

                checkDev_BS_Notify.Enabled = false;
                inTolerancePlay.Enabled = false;
                txtTolerancePlay.Enabled = false;
            }
            playing_dev = checkDevPlayNotify.Checked;
        }

        private void checkBetNotify_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBetNotify.Checked)
            {
                inToleranceBet.Enabled = true;
                txtToleranceBet.Enabled = true;
            }
            else
            {
                inToleranceBet.Enabled = false;
                txtToleranceBet.Enabled = false;
            }
            bet_dev = checkBetNotify.Checked;
        }

        private void checkBasicStratNotify_CheckedChanged(object sender, EventArgs e)
        {
            basic_strat = checkBasicStratNotify.Checked;
        }

        private void checkDev_BS_Notify_CheckedChanged(object sender, EventArgs e)
        {
            dev_bs = checkDev_BS_Notify.Checked;
        }

        private void inTolerancePlay_TextChanged(object sender, EventArgs e)
        {
            play_dev_tol = Convert.ToDouble(inToleranceBet.Text);
        }

        private void checkOverbetNotify_CheckedChanged(object sender, EventArgs e)
        {
            bet_bankroll = checkOverbetNotify.Checked;
        }

        private void inToleranceBet_TextChanged(object sender, EventArgs e)
        {
            bet_dev_tol = Convert.ToDouble(inToleranceBet.Text);
        }

        private void radioBJ3to2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBJ3to2.Checked)
                bj_pay = 0;
        }

        private void radioBJ6to5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBJ6to5.Checked)
                bj_pay = 1;
        }

        private void radioBJ1to1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBJ1to1.Checked)
                bj_pay = 2;
        }

        private void checkDHitS17_CheckedChanged(object sender, EventArgs e)
        {
            hit_s17 = checkDHitS17.Checked;
        }

        private void inBettingUnits_TextChanged(object sender, EventArgs e)
        {
            bet_unit = Convert.ToInt32(inBettingUnits.Text);
        }
    }
}
