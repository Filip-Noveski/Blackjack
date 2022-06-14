using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;


namespace Blackjack
{
    public partial class Table : Form
    {
        // Variables:
        

        Random rand = new Random();     // Random number generator
        
        String Path = "";   // Contains the main path to the directory (after loading of Form)

        double Chips = 0;            // Value of chips player has
        float Bet;                  // Placed bet

        Boolean PlayerMove = false; // Whether or not a game is active

        String ReservePathFacedown = "base/material/cards/backcard.bmp";    // Path to facedown card

        Boolean[] Splitable = new Boolean[4] { false, false, false, false };    // Splitable hands (Player)

        Int16 ActiveHand;                   // Player's active hand

        Boolean PossibleDouble = true;      // Whether player can double-down

        int NextHand = 0;                   // Next hand for the player after standing if they have split (0 - none)
        
        Int16 TotalCards = 312;         // Total number of cards
        Int16 RunningCount = 0;         // Running count

        Int64 HandsPlayed;              // The amount of hands the player has played
        Int32 blackjackCount;           // The amount of blackjacks/naturals the player has gotten
        Int16 mistakes;                 // Number of mistakes the player has made
        Int64 StartingAmount;           // The amount of money the player has started with
        Int32 insurance3u;              // The amount of aces the dealer has had up (count >3)
        Int32 insurance3uc;             // The amount of blackjacks the dealer has had with an ace up (count >3)
        Int32 insurance3d;              // The amount of aces the dealer has had up (count <3)
        Int32 insurance3dc;             // The amount of blackjacks the dealer has had with an ace up (count <3)
        double MoneyChange;             // The change in money from the player's starting amount to now

        char CorrectMoveBS;             // The correct move according to basic strategy
            // The correct move according to deviations, if one exists
        Deviation CorrectMoveDev = new Deviation();
        // - Surrender
        // ! Split
        // H Hit
        // S Stand
        // D Double
        // \0 Doesn't exist

        Boolean CountConfirmed = false; // Whether the player has submitted the running count after the game ended
        


        // Cards:

        Card[] Diamond = new Card[13];  // Ace[0]; Jack[10]; Queen[11]; King[12]; Number Cards[Number - 1]
        Int16 DiamondAmount = 78;       // Amount of Diamond cards in shoe (6 Deck Shoe)

        Card[] Heart = new Card[13];  // Ace[0]; Jack[10]; Queen[11]; King[12]; Number Cards[Number - 1]
        Int16 HeartAmount = 78;       // Amount of Heart cards in shoe (6 Deck Shoe)

        Card[] Clover = new Card[13];  // Ace[0]; Jack[10]; Queen[11]; King[12]; Number Cards[Number - 1]
        Int16 CloverAmount = 78;       // Amount of Clover cards in shoe (6 Deck Shoe)

        Card[] Spade = new Card[13];  // Ace[0]; Jack[10]; Queen[11]; King[12]; Number Cards[Number - 1]
        Int16 SpadeAmount = 78;       // Amount of Spade cards in shoe (6 Deck Shoe)

        Card Facedown;                // Dealer's FaceDown Card


        // Hands:

        Hand[] PlayerHand = new Hand[4];    // Player Hands
        Hand DealerHand;                    // Dealer Hand


        // Files:

        public Excel.Application xlApp = new Excel.Application();
        


        // Controls:
        


        public Table()
        {
            InitializeComponent();
        }
        
        private double GetTrueCount()
        {
            int remCards = 0;
            for (int i = 0; i <= 12; i++)
            {
                remCards += Diamond[i].Amount;
                remCards += Clover[i].Amount;
                remCards += Heart[i].Amount;
                remCards += Spade[i].Amount;
            }

            return (RunningCount * 1.0) / (remCards / 52.0);
        }
        
        /// <summary>
        /// Sets-up the cards of type "Diamond".
        /// Use when starting the game.
        /// Currently setup for a 6-deck shoe.
        /// </summary>
        private void CardSetupDiamond()
        {
            for (int i = 1; i <= 13; i++)
            {
                if (i == 1)
                {
                    Diamond[0].Name = "Ace";
                    Diamond[0].Value = 1;
                    Diamond[0].Path = "base/material/cards/diamond/AD.bmp";
                    Diamond[0].Amount = 6;
                }
                else if (i == 11)
                {
                    Diamond[10].Name = "Jack";
                    Diamond[10].Value = 10;
                    Diamond[10].Path = "base/material/cards/diamond/JD.bmp";
                    Diamond[10].Amount = 6;
                }
                else if (i == 12)
                {
                    Diamond[11].Name = "Queen";
                    Diamond[11].Value = 10;
                    Diamond[11].Path = "base/material/cards/diamond/QD.bmp";
                    Diamond[11].Amount = 6;
                }
                else if (i == 13)
                {
                    Diamond[12].Name = "King";
                    Diamond[12].Value = 10;
                    Diamond[12].Path = "base/material/cards/diamond/KD.bmp";
                    Diamond[12].Amount = 6;
                }
                else
                {
                    Diamond[i - 1].Name = Convert.ToString(i);
                    Diamond[i - 1].Value = Convert.ToInt16(i);
                    Diamond[i - 1].Path = "base/material/cards/diamond/" + Convert.ToString(i) + "D.bmp";
                    Diamond[i - 1].Amount = 6;
                }
            }
        }

        /// <summary>
        /// Sets-up the cards of type "Heart".
        /// Use when starting the game.
        /// Currently setup for a 6-deck shoe.
        /// </summary>
        private void CardSetupHeart()
        {
            for (int i = 1; i <= 13; i++)
          
  {
                if (i == 1)
                {
                    Heart[0].Name = "Ace";
                    Heart[0].Value = 1;
                    Heart[0].Path = "base/material/cards/heart/AH.bmp";
                    Heart[0].Amount = 6;
                }
                else if (i == 11)
                {
                    Heart[10].Name = "Jack";
                    Heart[10].Value = 10;
                    Heart[10].Path = "base/material/cards/heart/JH.bmp";
                    Heart[10].Amount = 6;
                }
                else if (i == 12)
                {
                    Heart[11].Name = "Queen";
                    Heart[11].Value = 10;
                    Heart[11].Path = "base/material/cards/heart/QH.bmp";
                    Heart[11].Amount = 6;
                }
                else if (i == 13)
                {
                    Heart[12].Name = "King";
                    Heart[12].Value = 10;
                    Heart[12].Path = "base/material/cards/heart/KH.bmp";
                    Heart[12].Amount = 6;
                }
                else
                {
                    Heart[i - 1].Name = Convert.ToString(i);
                    Heart[i - 1].Value = Convert.ToInt16(i);
                    Heart[i - 1].Path = "base/material/cards/heart/" + Convert.ToString(i) + "H.bmp";
                    Heart[i - 1].Amount = 6;
                }
            }
        }
        
        /// <summary>
        /// Sets-up the cards of type "Spade".
        /// Use when starting the game.
        /// Currently setup for a 6-deck shoe.
        /// </summary>
        private void CardSetupSpade()
        {
            for (int i = 1; i <= 13; i++)
            {
                if (i == 1)
                {
                    Spade[0].Name = "Ace";
                    Spade[0].Value = 1;
                    Spade[0].Path = "base/material/cards/spade/AS.bmp";
                    Spade[0].Amount = 6;
                }
                else if (i == 11)
                {
                    Spade[10].Name = "Jack";
                    Spade[10].Value = 10;
                    Spade[10].Path = "base/material/cards/spade/JS.bmp";
                    Spade[10].Amount = 6;
                }
                else if (i == 12)
                {
                    Spade[11].Name = "Queen";
                    Spade[11].Value = 10;
                    Spade[11].Path = "base/material/cards/spade/QS.bmp";
                    Spade[11].Amount = 6;
                }
                else if (i == 13)
                {
                    Spade[12].Name = "King";
                    Spade[12].Value = 10;
                    Spade[12].Path = "base/material/cards/spade/KS.bmp";
                    Spade[12].Amount = 6;
                }
                else
                {
                    Spade[i - 1].Name = Convert.ToString(i);
                    Spade[i - 1].Value = Convert.ToInt16(i);
                    Spade[i - 1].Path = "base/material/cards/spade/" + Convert.ToString(i) + "S.bmp";
                    Spade[i - 1].Amount = 6;
                }
            }
        }
        
        /// <summary>
        /// Sets-up the cards of type "Clover".
        /// Use when starting the game.
        /// Currently setup for a 6-deck shoe.
        /// </summary>
        private void CardSetupClover()
        {
            for (int i = 1; i <= 13; i++)
            {
                if (i == 1)
                {
                    Clover[0].Name = "Ace";
                    Clover[0].Value = 1;
                    Clover[0].Path = "base/material/cards/clover/AC.bmp";
                    Clover[0].Amount = 6;
                }
                else if (i == 11)
                {
                    Clover[10].Name = "Jack";
                    Clover[10].Value = 10;
                    Clover[10].Path = "base/material/cards/clover/JC.bmp";
                    Clover[10].Amount = 6;
                }
                else if (i == 12)
                {
                    Clover[11].Name = "Queen";
                    Clover[11].Value = 10;
                    Clover[11].Path = "base/material/cards/clover/QC.bmp";
                    Clover[11].Amount = 6;
                }
                else if (i == 13)
                {
                    Clover[12].Name = "King";
                    Clover[12].Value = 10;
                    Clover[12].Path = "base/material/cards/clover/KC.bmp";
                    Clover[12].Amount = 6;
                }
                else
                {
                    Clover[i - 1].Name = Convert.ToString(i);
                    Clover[i - 1].Value = Convert.ToInt16(i);
                    Clover[i - 1].Path = "base/material/cards/clover/" + Convert.ToString(i) + "C.bmp";
                    Clover[i - 1].Amount = 6;
                }
            }
        }
        
        /// <summary>
        /// Sets-Up the Location of All Buttons.
        /// Use when Launcing the app.
        /// </summary>
        /// <param name="height">Height of Screen</param>
        /// <param name="width">Width of Screen</param>
        private void Button_Load(int height, int width)
        {
            CmdMenu.Location = new Point(width - 40, 7);                // CloseApp

            CmdReset.Location = new Point(width - 150, height - 57);    //Insurance
            CmdSurrend.Location = new Point(width - 150, height - 102); //Surrender
            CmdStand.Location = new Point(width - 150, height - 147);   //Stand

            CmdSplit.Location = new Point(width - 295, height - 57);    //Split
            CmdDouble.Location = new Point(width - 295, height - 102);  //Double Down
            CmdHit.Location = new Point(width - 295, height - 147);     //Hit

            CmdPlaceBet.Location = new Point(width - 150, height - 212);

            cmdSubmitCount.Location = new Point(width / 2 + 5, (int)(2 / 3.0 * height));
            cmdSubmitCount.Visible = false;
            cmdSubmitCount.Text = "Don't know";

            inCountVal.Location = new Point(width / 2 - 5 - inCountVal.Width, (int)(2 / 3.0 * height));
            inCountVal.Visible = false;
        }
        
        /// <summary>
        /// Sets-Up the Location (and text) of All Labels.
        /// Use when Launcing the app.
        /// </summary>
        /// <param name="height">Height of Screen</param>
        /// <param name="width">Width of Screen</param>
        private void Label_Load(int height, int width)
        {
            TxtChips.Location = new Point(width - 250 - CmdShuffle.Width, 15);
            TxtChips.Text = "$ " + Convert.ToString(Chips);

            TxtLastEarning.Location = new Point(width - 250 - CmdShuffle.Width, 35);
            TxtLastEarning.Text = "$ " + Convert.ToString(0);

            txtBJpay.Location = new Point(width / 2 - txtBJpay.Width / 2, height / 3);
            txtDealerRules.Location = new Point(width / 2 - txtDealerRules.Width / 2, height / 3 + 50);
            txtInsurPay.Location = new Point(width / 2 - txtInsurPay.Width / 2, height / 3 + 100);

            inBet.Location = new Point(width - 295, height - 212);

            txtDealerVal.Location = new Point(width / 2 + 150, 251);
            txtHand1Val.Location = new Point(width / 2 - picPlayerH1C1.Width / 2, height - 35);
            txtHand2Val.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width, height - 35);
            txtHand3Val.Location = new Point(width / 4, height - 35);
            txtHand4Val.Location = new Point(width / 20, height - 35);

            txtDealerVal.Visible = false;
            txtHand1Val.Visible = false;
            txtHand2Val.Visible = false;
            txtHand3Val.Visible = false;
            txtHand4Val.Visible = false;
            
            txtCount.Location = new Point(width / 2 - txtCount.Width / 2, (int)(2 / 3.0 * height) - 35);
            txtCount.Visible = false;
        }

        /// <summary>
        /// Sets-Up the Location of All Pictures i.e. PictureBoxes.
        /// Use when Launching the app.
        /// </summary>
        /// <param name="height">Height of Screen</param>
        /// <param name="width">Width of Screen</param>
        private void Picture_Load(int height, int width)
        {
            picDealerCard1.Location = new Point(width / 2 + 150, 50);  // <Sets-up dealer card locations>
            picDealerCard2.Location = new Point(width / 2 - 150, 50);
            picDealerCard3.Location = new Point(width / 2 - 200, 50);
            picDealerCard4.Location = new Point(width / 2 - 250, 50);
            picDealerCard5.Location = new Point(width / 2 - 300, 50);
            picDealerCard6.Location = new Point(width / 2 - 350, 50);
            picDealerCard7.Location = new Point(width / 2 - 400, 50);  // </Sets-up dealer cards locations>
            /*
            picDealerCard7.Image = Image.FromFile(Path + Diamond[0].Path);  // <Dealer Card Images (Test)>
            picDealerCard6.Image = Image.FromFile(Path + Diamond[1].Path);
            picDealerCard5.Image = Image.FromFile(Path + Diamond[2].Path);
            picDealerCard4.Image = Image.FromFile(Path + Diamond[3].Path);
            picDealerCard3.Image = Image.FromFile(Path + Diamond[4].Path);
            picDealerCard2.Image = Image.FromFile(Path + Diamond[5].Path);
            picDealerCard1.Image = Image.FromFile(Path + Diamond[6].Path);  // </Dealer Cards Images (Test)>
            */
            
            picPlayerH1C1.Location = new Point(width / 2 - picPlayerH1C1.Width / 2, height - 50 - picPlayerH1C1.Height);        // <Player Cards Hand1>
            picPlayerH1C2.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 25, height - 85 - picPlayerH1C1.Height);
            picPlayerH1C3.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 50, height - 120 - picPlayerH1C1.Height);
            picPlayerH1C4.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 75, height - 155 - picPlayerH1C1.Height);
            picPlayerH1C5.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 100, height - 190 - picPlayerH1C1.Height);
            picPlayerH1C6.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 125, height - 225 - picPlayerH1C1.Height);
            picPlayerH1C7.Location = new Point(width / 2 - picPlayerH1C1.Width / 2 + 150, height - 260 - picPlayerH1C1.Height); // </Player Cards Hand1>
            /*
            picPlayerH1C1.Image = Image.FromFile(Path + Diamond[6].Path);   // Test
            picPlayerH1C2.Image = Image.FromFile(Path + Diamond[7].Path);
            picPlayerH1C3.Image = Image.FromFile(Path + Diamond[8].Path);
            picPlayerH1C4.Image = Image.FromFile(Path + Diamond[9].Path);
            picPlayerH1C5.Image = Image.FromFile(Path + Diamond[10].Path);
            picPlayerH1C6.Image = Image.FromFile(Path + Diamond[11].Path);
            picPlayerH1C7.Image = Image.FromFile(Path + Diamond[12].Path);  // /Test
            */

            picPlayerH2C1.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width, height - 50 - picPlayerH1C1.Height);        // <Player Cards Hand2>
            picPlayerH2C2.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 25, height - 85 - picPlayerH1C1.Height);
            picPlayerH2C3.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 50, height - 120 - picPlayerH1C1.Height);
            picPlayerH2C4.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 75, height - 155 - picPlayerH1C1.Height);
            picPlayerH2C5.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 100, height - 190 - picPlayerH1C1.Height);
            picPlayerH2C6.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 125, height - 225 - picPlayerH1C1.Height);
            picPlayerH2C7.Location = new Point(width * 3 / 4 - picPlayerH1C1.Width + 150, height - 260 - picPlayerH1C1.Height); // </Player Cards Hand2>
            /*
            picPlayerH2C1.Image = Image.FromFile(Path + Heart[0].Path);   // Test
            picPlayerH2C2.Image = Image.FromFile(Path + Heart[1].Path);
            picPlayerH2C3.Image = Image.FromFile(Path + Heart[2].Path);
            picPlayerH2C4.Image = Image.FromFile(Path + Heart[3].Path);
            picPlayerH2C5.Image = Image.FromFile(Path + Heart[4].Path);
            picPlayerH2C6.Image = Image.FromFile(Path + Heart[5].Path);
            picPlayerH2C7.Image = Image.FromFile(Path + Heart[6].Path);  // /Test
            */

            picPlayerH3C1.Location = new Point(width  / 4, height - 50 - picPlayerH1C1.Height);        // <Player Cards Hand3>
            picPlayerH3C2.Location = new Point(width  / 4 + 25, height - 85 - picPlayerH1C1.Height);
            picPlayerH3C3.Location = new Point(width  / 4 + 50, height - 120 - picPlayerH1C1.Height);
            picPlayerH3C4.Location = new Point(width  / 4 + 75, height - 155 - picPlayerH1C1.Height);
            picPlayerH3C5.Location = new Point(width  / 4 + 100, height - 190 - picPlayerH1C1.Height);
            picPlayerH3C6.Location = new Point(width  / 4 + 125, height - 225 - picPlayerH1C1.Height);
            picPlayerH3C7.Location = new Point(width  / 4 + 150, height - 260 - picPlayerH1C1.Height); // </Player Cards Hand3>
            /*
            picPlayerH3C1.Image = Image.FromFile(Path + Heart[6].Path);   // Test
            picPlayerH3C2.Image = Image.FromFile(Path + Heart[7].Path);
            picPlayerH3C3.Image = Image.FromFile(Path + Heart[8].Path);
            picPlayerH3C4.Image = Image.FromFile(Path + Heart[9].Path);
            picPlayerH3C5.Image = Image.FromFile(Path + Heart[10].Path);
            picPlayerH3C6.Image = Image.FromFile(Path + Heart[11].Path);
            picPlayerH3C7.Image = Image.FromFile(Path + Heart[12].Path);  // /Test
            */

            picPlayerH4C1.Location = new Point(width / 20, height - 50 - picPlayerH1C1.Height);        // <Player Cards Hand4>
            picPlayerH4C2.Location = new Point(width / 20 + 25, height - 85 - picPlayerH1C1.Height);
            picPlayerH4C3.Location = new Point(width / 20 + 50, height - 120 - picPlayerH1C1.Height);
            picPlayerH4C4.Location = new Point(width / 20 + 75, height - 155 - picPlayerH1C1.Height);
            picPlayerH4C5.Location = new Point(width / 20 + 100, height - 190 - picPlayerH1C1.Height);
            picPlayerH4C6.Location = new Point(width / 20 + 125, height - 225 - picPlayerH1C1.Height);
            picPlayerH4C7.Location = new Point(width / 20 + 150, height - 260 - picPlayerH1C1.Height); // </Player Cards Hand4>
            /*
            picPlayerH4C1.Image = Image.FromFile(Path + Diamond[6].Path);   // Test
            picPlayerH4C2.Image = Image.FromFile(Path + Diamond[7].Path);
            picPlayerH4C3.Image = Image.FromFile(Path + Diamond[8].Path);
            picPlayerH4C4.Image = Image.FromFile(Path + Diamond[9].Path);
            picPlayerH4C5.Image = Image.FromFile(Path + Diamond[10].Path);
            picPlayerH4C6.Image = Image.FromFile(Path + Diamond[11].Path);
            picPlayerH4C7.Image = Image.FromFile(Path + Diamond[12].Path);  // /Test */

            picShoeBack.Location = new Point(25, 25);
            picShoeBack.Size = new Size(125, 175);

            picShoeFrt.Location = new Point(30, 30);
            picShoeFrt.Size = new Size(120, 0);

            picShoeDeck1.Location = new Point(25, (int)(25 + 175 * (1 / 6.0)));
            picShoeDeck2.Location = new Point(25, (int)(25 + 175 * (2 / 6.0)));
            picShoeDeck3.Location = new Point(25, (int)(25 + 175 * (3 / 6.0)));
            picShoeDeck4.Location = new Point(25, (int)(25 + 175 * (4 / 6.0)));
            picShoeDeck5.Location = new Point(25, (int)(25 + 175 * (5 / 6.0)));
        }
        
        /// <summary>
        /// Sets-up the locations of the Group Boxes.
        /// Use when Launching the app.
        /// </summary>
        /// <param name="height">Height of Screen</param>
        /// <param name="width">Width of Screen</param>
        private void GroupBox_Load(int height, int width)
        {
            grboxOptionsMenu.Location = new Point(width - 7 - grboxOptionsMenu.Width, 40);
            grboxGameInfo.Location = new Point(width / 2 - grboxGameInfo.Width / 2, height / 2 - 2 * grboxGameInfo.Height / 3);
        }
        
        /// <summary>
        /// Reads the required values from the files.
        /// </summary>
        private void GetVal(String InputText)
        {
            String Input = "", Val = "";
            int i;

            for (i = 0; i <= InputText.Length - 1; i++)
            {
                if ((InputText[i] >= 'a' && InputText[i] <= 'z') || (InputText[i] >= 'A' && InputText[i] <= 'Z') || InputText[i] == '_')
                {
                    Input += InputText[i];
                }
                else if (InputText[i] == '#' || InputText[i] == ':')
                    break;
            }

            for (; i <= InputText.Length - 1; i++)
            {
                if (InputText[i] >= '0' && InputText[i] <= '9' || InputText[i] == '-' || InputText[i] == '.')
                {
                    Val += InputText[i];
                }
                else if (InputText[i] == '#')
                    break;
            }

            if (Val != "")
            {
                switch (Input)
                {
                    case "hands_played": HandsPlayed = Convert.ToInt64(Val); break;
                    case "blackjacks": blackjackCount = Convert.ToInt32(Val); break;
                    case "mistakes": mistakes = Convert.ToInt16(Val); break;
                    case "ace_up_A": insurance3u = Convert.ToInt16(Val); break;
                    case "ace_up_Abj": insurance3uc = Convert.ToInt16(Val); break;
                    case "ace_up_B": insurance3d = Convert.ToInt16(Val); break;
                    case "ace_up_Bbj": insurance3dc = Convert.ToInt16(Val); break;
                    case "starting_amount": StartingAmount = Convert.ToInt64(Val); break;
                    case "money_change": MoneyChange = Convert.ToDouble(Val); break;
                }
            }
        }
        
        private void Table_Load(object sender, EventArgs e)
        {
            KeyPreview = false;

            String TempPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            // < ^^^ Gets the main path of the directory ^^^ >
            int width = Screen.PrimaryScreen.Bounds.Width;      // <Full Screen setup>
            int height = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point(0, 0);
            this.Size = new Size(width, height);                // </Full Screen setup>

            for (int i = 0; i <= TempPath.Length - 10; i++)     // <Gets the path to app's folder>
                Path += TempPath[i];                            // </>

            CardSetupDiamond();                                 // <Sets-up Diamond Cards>
            CardSetupHeart();                                   // <Sets-up Heart Cards>
            CardSetupSpade();                                   // <Sets-up Spade Cards>
            CardSetupClover();                                  // <Sets-up Clover Cards>

            Facedown.Path = ReservePathFacedown;                // <Sets-up the path to the facedown card iamge>

            Button_Load(height, width);                         // <Sets-Up All Buttons>
            Label_Load(height, width);                          // <Sets-Up All Labels>
            Picture_Load(height, width);                        // <Sets-Up All PictureBoxes>
            GroupBox_Load(height, width);                       // <Sets-up All GroupBoxes>

            for (int i = 0; i <= 3; i++)                        // <All hands are invalid>
                PlayerHand[i].Valid = false;                    // <Dealer should not try to beat them>

            Chips = Convert.ToDouble(System.IO.File.ReadAllText(Path + "base/profile/chips.txt"));
            TxtChips.Text = "$ " + Convert.ToString(Chips);

            String ReadText;

            System.IO.StreamReader file = new System.IO.StreamReader(Path + "base/profile/ev_data.txt");

            while ((ReadText = file.ReadLine()) != null)
            {
                GetVal(ReadText);
            }

            file.Close();

            HandsPlayed--;
            EndGame(sender, e);                                 // <Resets items, makes images invisible>
        }
        
        /// <summary>
        /// Gets the appropriate move for the player according to basic strategy.
        /// </summary>
        private void GetBasicStratMove()
        {
            // - Surrender
            // ! Split
            // H Hit
            // S Stand
            // D Double


                // If Dealer or Player has Blackjack or Player has 21.
            if (txtDealerVal.Text == "Natural" || 
                (PlayerHand[ActiveHand].Value == 11 && PlayerHand[ActiveHand].Soft) || 
                PlayerHand[ActiveHand].Value == 21 ||
                DealerHand.Value > 10)
                return;

            GetDeviationMove();     // If the player should make a certain deviation.

            string xlCell = "/";
            int PV = PlayerHand[ActiveHand].Value;
            int DV = DealerHand.Value;
            xlCell = "/";

            Excel.Workbook xlBookMoves = xlApp.Workbooks.Open(Path + "base/data/Moves.xlsx");
            Excel.Workbook xlBookInitMove = xlApp.Workbooks.Open(Path + "base/data/InitMove.xlsx");


            Excel._Worksheet xlMovesSheet = (PlayerHand[ActiveHand].Soft) ? xlBookMoves.Worksheets[2] : xlBookMoves.Worksheets[1];
            Excel.Range xlMovesRange = xlMovesSheet.UsedRange;

            Excel._Worksheet xlInitMoveSplit = xlBookInitMove.Worksheets[1];
            Excel._Worksheet xlInitMoveSur = xlBookInitMove.Worksheets[2];
            Excel.Range xlSplitRange = xlInitMoveSplit.UsedRange;
            Excel.Range xlSurRange = xlInitMoveSur.UsedRange;


            if (PossibleDouble && Splitable[ActiveHand])
            {   // Check for splitting
                try
                {
                    xlCell = xlSplitRange.Cells[PV / 2, DV].Value2.ToString();
                    if (xlCell == "Y")
                        goto Found;
                }
                catch
                {
                    MessageBox.Show("Null reference soft [" + PV.ToString() + ", " + DV.ToString() + "]",
                        "Null Reference");
                }
            }

            try
            {   // Check for Surrender
                if (PossibleDouble)
                {
                    xlCell = xlSurRange.Cells[PV, DV].Value2.ToString();
                    if (xlCell == "Sur")
                        goto Found;
                }
            }
            catch
            {
                MessageBox.Show("Null reference surrender [" + PV.ToString() + ", " + DV.ToString() + "]",
                    "Null Reference");
                return;
            }

            try
            {
                if (xlCell == "N" || xlCell == "/")
                    xlCell = xlMovesRange.Cells[PV, DV].Value2.ToString();
            }
            catch
            {
                MessageBox.Show("Null reference hard [" + PV.ToString() + ", " + DV.ToString() + "]",
                    "Null Reference");
            }

        Found:
            switch (xlCell)
            {
                case "Sur":
                    CorrectMoveBS = '-'; break;
                case "Y":
                    CorrectMoveBS = '!'; break;
                case "DS":
                    if (PossibleDouble)
                        CorrectMoveBS = 'D';
                    else
                        CorrectMoveBS = 'S';
                    break;
                case "/":
                    MessageBox.Show("No Value for correct move [" + PV.ToString() + ", " + DV.ToString() + "]", "Error");
                    break;
                case "D":
                    if (PossibleDouble)
                        CorrectMoveBS = 'D';
                    else
                        CorrectMoveBS = 'H';
                    break;

                default:
                    CorrectMoveBS = xlCell[0]; break;
            }

            xlBookInitMove.Close();
            xlBookMoves.Close();
        }
        
        /// <summary>
        /// Writes the required data in the ev_data.txt file.
        /// </summary>
        private void WriteValEV_data()
        {
            List<String> Lines = new List<String>();

            string comment = "# Calculates the EV of the player (Money / Hand); Reset values when the amount of chips is changed via Settings";
            Lines.Add(comment);

            comment = " # The amount of hands the player has played";
            Lines.Add("hands_played: " + Convert.ToString(HandsPlayed) + comment);

            comment = " # The amount of blackjacks/naturals the player has gotten";
            Lines.Add("blackjacks: " + Convert.ToString(blackjackCount) + comment);
            
            comment = " # The number of mistakes the player has made";
            Lines.Add("mistakes: " + Convert.ToString(mistakes) + comment);

            comment = " # The amount of times the  dealer has had an ace up above a true 3";
            Lines.Add("ace_up_A: " + Convert.ToString(insurance3u) + comment);

            comment = " # The amount of times the  dealer has had an ace up and blackjack above a true 3";
            Lines.Add("ace_up_Abj: " + Convert.ToString(insurance3uc) + comment);

            comment = " # The amount of times the  dealer has had an ace up above a below 3";
            Lines.Add("ace_up_B: " + Convert.ToString(insurance3d) + comment);

            comment = " # The amount of times the  dealer has had an ace up and blackjack above a below 3";
            Lines.Add("ace_up_Bbj: " + Convert.ToString(insurance3dc) + comment);

            comment = " # The change in money over the player's 'career'";
            Lines.Add("money_change: " + Convert.ToString(MoneyChange) + comment);

            System.IO.File.WriteAllText(Path + "base/profile/ev_data.txt", "");
            System.IO.File.WriteAllLines(Path + "base/profile/ev_data.txt", Lines);
        }
        
        private void CloseApp_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(Path + "base/profile/chips.txt", Convert.ToString(Chips));
            WriteValEV_data();
            Application.ExitThread();                           // Closes the App
        }

        /// <summary>
        /// Pays-out the required sum to the player.
        /// </summary>
        private void Payout()
        {
            double prevChips = Chips;

            if (DealerHand.Soft && txtDealerVal.Text != "Bust")
                DealerHand.Value += 10;

            for (int i = 0; i <= 3; i++)
            {
                if (PlayerHand[i].Soft)
                    PlayerHand[i].Value += 10;
            }

            if (txtDealerVal.Text != "Bust")
            {
                if (txtHand1Val.Visible && txtHand1Val.Text != "Bust" && PlayerHand[0].Valid)
                {
                    if (DealerHand.Value < PlayerHand[0].Value && PlayerHand[0].Double)
                        Chips += 4 * Bet;
                    else if ((DealerHand.Value < PlayerHand[0].Value && !(PlayerHand[0].Double)) || (DealerHand.Value == PlayerHand[0].Value && PlayerHand[0].Double))
                        Chips += 2 * Bet;
                    else if (DealerHand.Value == PlayerHand[0].Value && !(PlayerHand[0].Double))
                        Chips += Bet;
                }

                if (txtHand2Val.Visible && txtHand2Val.Text != "Bust" && PlayerHand[1].Valid)
                {
                    if (DealerHand.Value < PlayerHand[1].Value && PlayerHand[1].Double)
                        Chips += 4 * Bet;
                    else if ((DealerHand.Value < PlayerHand[1].Value && !(PlayerHand[1].Double)) || (DealerHand.Value == PlayerHand[1].Value && PlayerHand[1].Double))
                        Chips += 2 * Bet;
                    else if (DealerHand.Value == PlayerHand[1].Value && !(PlayerHand[1].Double))
                        Chips += Bet;
                }

                if (txtHand3Val.Visible && txtHand3Val.Text != "Bust" && PlayerHand[2].Valid)
                {
                    if (DealerHand.Value < PlayerHand[2].Value && PlayerHand[2].Double)
                        Chips += 4 * Bet;
                    else if ((DealerHand.Value < PlayerHand[2].Value && !(PlayerHand[2].Double)) || (DealerHand.Value == PlayerHand[2].Value && PlayerHand[2].Double))
                        Chips += 2 * Bet;
                    else if (DealerHand.Value == PlayerHand[2].Value && !(PlayerHand[2].Double))
                        Chips += Bet;
                }

                if (txtHand4Val.Visible && txtHand4Val.Text != "Bust" && PlayerHand[3].Valid)
                {
                    if (DealerHand.Value < PlayerHand[3].Value && PlayerHand[3].Double)
                        Chips += 4 * Bet;
                    else if ((DealerHand.Value < PlayerHand[3].Value && !(PlayerHand[3].Double)) || (DealerHand.Value == PlayerHand[3].Value && PlayerHand[3].Double))
                        Chips += 2 * Bet;
                    else if (DealerHand.Value == PlayerHand[3].Value && !(PlayerHand[3].Double))
                        Chips += Bet;
                }
            }
            else if (txtDealerVal.Text == "Bust")
            {
                if (txtHand1Val.Visible && txtHand1Val.Text != "Bust" && PlayerHand[0].Valid)
                {
                    if (PlayerHand[0].Double)
                        Chips += 4 * Bet;
                    else if (txtHand1Val.Visible)
                        Chips += 2 * Bet;
                }

                if (txtHand2Val.Visible && txtHand2Val.Text != "Bust" && PlayerHand[1].Valid)
                {
                    if (PlayerHand[1].Double)
                        Chips += 4 * Bet;
                    else if (txtHand2Val.Visible)
                        Chips += 2 * Bet;
                }

                if (txtHand3Val.Visible && txtHand3Val.Text != "Bust" && PlayerHand[2].Valid)
                {
                    if (PlayerHand[2].Double)
                        Chips += 4 * Bet;
                    else if (txtHand3Val.Visible)
                        Chips += 2 * Bet;
                }

                if (txtHand4Val.Visible && txtHand4Val.Text != "Bust" && PlayerHand[3].Valid)
                {
                    if (PlayerHand[1].Double)
                        Chips += 4 * Bet;
                    else if (txtHand4Val.Visible)
                        Chips += 2 * Bet;
                }
            }

            HandsPlayed++;
            MoneyChange += Chips - prevChips;

            TxtLastEarning.Text = "$ " + Convert.ToString(Chips - prevChips);
            TxtChips.Text = "$ " + Convert.ToString(Chips);
        }   
        
        /// <summary>
        /// Dealer's play.
        /// </summary>
        private async void DealerPlay()
        {
            int NextCard = 3;

            picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);    // <Reveals facedown card>
            Facedown.Path = ReservePathFacedown;                            // <Returns to facedown card image's base path>

            if (Facedown.Value == 1 || Facedown.Value == 10)                // <Update the running count>
                RunningCount--;
            else if (Facedown.Value >= 2 && Facedown.Value <= 6)
                RunningCount++;                                             // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            DealerHand.Value += Facedown.Value;
            if (Facedown.Value == 1)
                DealerHand.Soft = true;
            Facedown.Value = 0;

            int Index = 8;                                                  // <Temporary, neutral card>
            goto CheckDealerStand;

        DealerHit:
            if (NextCard == 8)
                goto End;

            int Type = rand.Next(1, 5);
            Index = rand.Next(0, 13);
            switch (Type)
            {
                case 1:
                    if (Diamond[Index].Amount == 0)
                        goto DealerHit;
                    else
                    {
                        Diamond[Index].Amount--;
                        switch (NextCard)
                        {
                            case 3:
                                picDealerCard3.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picDealerCard3.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 4:
                                picDealerCard4.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picDealerCard4.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 5:
                                picDealerCard5.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picDealerCard5.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 6:
                                picDealerCard6.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picDealerCard6.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 7:
                                picDealerCard7.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picDealerCard7.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;
                        }

                        NextCard++;
                    }
                    break;

                case 2:
                    if (Heart[Index].Amount == 0)
                        goto DealerHit;
                    else
                    {
                        Heart[Index].Amount--;
                        switch (NextCard)
                        {
                            case 3:
                                picDealerCard3.Image = Image.FromFile(Path + Heart[Index].Path);
                                picDealerCard3.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 4:
                                picDealerCard4.Image = Image.FromFile(Path + Heart[Index].Path);
                                picDealerCard4.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 5:
                                picDealerCard5.Image = Image.FromFile(Path + Heart[Index].Path);
                                picDealerCard5.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 6:
                                picDealerCard6.Image = Image.FromFile(Path + Heart[Index].Path);
                                picDealerCard6.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 7:
                                picDealerCard7.Image = Image.FromFile(Path + Heart[Index].Path);
                                picDealerCard7.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;
                        }

                        NextCard++;
                    }
                    break;

                case 3:
                    if (Spade[Index].Amount == 0)
                        goto DealerHit;
                    else
                    {
                        Spade[Index].Amount--;
                        switch (NextCard)
                        {
                            case 3:
                                picDealerCard3.Image = Image.FromFile(Path + Spade[Index].Path);
                                picDealerCard3.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 4:
                                picDealerCard4.Image = Image.FromFile(Path + Spade[Index].Path);
                                picDealerCard4.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 5:
                                picDealerCard5.Image = Image.FromFile(Path + Spade[Index].Path);
                                picDealerCard5.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 6:
                                picDealerCard6.Image = Image.FromFile(Path + Spade[Index].Path);
                                picDealerCard6.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 7:
                                picDealerCard7.Image = Image.FromFile(Path + Spade[Index].Path);
                                picDealerCard7.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;
                        }

                        NextCard++;
                    }
                    break;

                case 4:
                    if (Clover[Index].Amount == 0)
                        goto DealerHit;
                    else
                    {
                        Clover[Index].Amount--;
                        switch (NextCard)
                        {
                            case 3:
                                picDealerCard3.Image = Image.FromFile(Path + Clover[Index].Path);
                                picDealerCard3.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 4:
                                picDealerCard4.Image = Image.FromFile(Path + Clover[Index].Path);
                                picDealerCard4.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 5:
                                picDealerCard5.Image = Image.FromFile(Path + Clover[Index].Path);
                                picDealerCard5.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 6:
                                picDealerCard6.Image = Image.FromFile(Path + Clover[Index].Path);
                                picDealerCard6.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;

                            case 7:
                                picDealerCard7.Image = Image.FromFile(Path + Clover[Index].Path);
                                picDealerCard7.Visible = true;
                                DealerHand.Value += Diamond[Index].Value;
                                if (DealerHand.Soft && DealerHand.Value > 11)
                                    DealerHand.Soft = false;

                                if (Index == 0 && DealerHand.Value <= 11)
                                    DealerHand.Soft = true;
                                break;
                        }

                        NextCard++;
                    }
                    break;
            }

        CheckDealerStand:

            if (Diamond[Index].Value == 1 || Diamond[Index].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[Index].Value >= 2 && Diamond[Index].Value <= 6)
                RunningCount++;                                                 // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            if (DealerHand.Soft)
                txtDealerVal.Text = "S" + Convert.ToString(DealerHand.Value + 10);
            else
                txtDealerVal.Text = Convert.ToString(DealerHand.Value);

            if (DealerHand.Value > 21)
                txtDealerVal.Text = "Bust";                                              

            await Task.Delay(750);      // <Delays dealer's draw>

            if (!PlayerHand[0].Valid && !PlayerHand[1].Valid && !PlayerHand[2].Valid && !PlayerHand[3].Valid)
                goto End;

            if ((DealerHand.Soft && DealerHand.Value < 7) || (DealerHand.Value < 17 && !DealerHand.Soft))
                goto DealerHit;

        End:
        
            if (DealerHand.Soft)
                txtDealerVal.Text = "S" + Convert.ToString(DealerHand.Value + 10);
            else if (!DealerHand.Soft && DealerHand.Value <= 21)
                txtDealerVal.Text = Convert.ToString(DealerHand.Value);
            else
                txtDealerVal.Text = "Bust";

            Payout();
            CmdReset.Visible = true;
        }

        /// <summary>
        /// Checks whether the player made the correct move according to deviations.
        /// </summary>
        /// <param name="Move">Move made by the player.</param>
        /// <returns>Whether the player deviated correctly.</returns>
        private bool DeviationCheck(char Move)
        {
            if (CorrectMoveBS == '-' && Move == '-')
                return false;
            else if (CorrectMoveBS == '!' && Move == '!')
                return false;

            if (Move != CorrectMoveDev.Move)
            {
                string madeMove = "";
                string correctMove = "";
                string count;

                int PV = PlayerHand[ActiveHand].Value;
                int DV = DealerHand.Value;

                switch (Move)
                {
                    case 'H': madeMove = "Hit."; break;
                    case 'S': madeMove = "Stand."; break;
                    case 'D': madeMove = "Double."; break;
                    case '!': madeMove = "Split."; break;
                    case '-': madeMove = "Surrender."; break;
                }

                switch (CorrectMoveDev.Move)
                {
                    case 'H': correctMove = "Hit"; break;
                    case 'S': correctMove = "Stand"; break;
                    case 'D': correctMove = "Double"; break;
                    case '!': correctMove = "Split"; break;
                    case '-': correctMove = "Surrender"; break;
                }

                if (CorrectMoveDev.Count < 0)
                    count = '-' + Math.Abs(CorrectMoveDev.Count).ToString();
                else if (CorrectMoveDev.Count > 0)
                    count = '+' + Math.Abs(CorrectMoveDev.Count).ToString();
                else
                    count = "0";

                if (CorrectMoveDev.Move == '!')
                {
                    if (CorrectMoveDev.Above)
                        MessageBox.Show("If you have a pair of " + (PV / 2).ToString() +
                            " against a dealer's " + DV.ToString() + " at a true " + count +
                            " or higher, you should split the pair. You chose to " + madeMove, "Deviations");
                    else
                        MessageBox.Show("If you have a pair of " + (PV / 2).ToString() +
                            " against a dealer's " + DV.ToString() + " at a true count less than" + 
                            count +
                            ", you should split the pair. You chose to " + madeMove, "Deviations");
                }
                else if (PlayerHand[ActiveHand].Soft)
                {
                    if (CorrectMoveDev.Above)
                        MessageBox.Show("If you have a S" + (PV + 10).ToString() + " against a dealer's " +
                            DV.ToString() + " at a true " + count + " or higher, " +
                            "you should " + correctMove + ". You chose to " + madeMove);
                    else
                        MessageBox.Show("If you have a S" + (PV + 10).ToString() + " against a dealer's " +
                            DV.ToString() + " at a true count less than " + count +
                            ", you should " + correctMove + ". You chose to " + madeMove);
                }
                else
                {
                    if (CorrectMoveDev.Above)
                        MessageBox.Show("If you have a " + (PV).ToString() + " against a dealer's " +
                            DV.ToString() + " at a true " + count + " or higher, " +
                            "you should " + correctMove + ". You chose to " + madeMove);
                    else
                        MessageBox.Show("If you have a " + (PV).ToString() + " against a dealer's " +
                            DV.ToString() + " at a true count less than " + count +
                            ", you should " + correctMove + ". You chose to " + madeMove);
                }

                mistakes++; // Increment the number of mistakes

                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Checs if the player has made the correct move
        /// according to basic strategy.
        /// </summary>
        /// <param name="Move">Player's move</param>
        private void BasicStratCheck(char Move)
        {
            // Hit: H ;  Double: D ;  Stand: S ;
            // Split: ! ;  Surrender: - ;  Move icons.

            bool DeviatedCorrectly = false;

            if (CorrectMoveDev.Move != '\0')
            {
                DeviatedCorrectly = DeviationCheck(Move);
            }

            if (DeviatedCorrectly)
                return;

            if (Move != CorrectMoveBS && CorrectMoveDev.Move == '\0')
            {
                string madeMove = "";
                string correctMove = "";

                int PV = PlayerHand[ActiveHand].Value;
                int DV = DealerHand.Value;

                switch (Move)
                {
                    case 'H': madeMove = "Hit."; break;
                    case 'S': madeMove = "Stand."; break;
                    case 'D': madeMove = "Double."; break;
                    case '!': madeMove = "Split."; break;
                    case '-': madeMove = "Surrender."; break;
                }

                switch (CorrectMoveBS)
                {
                    case 'H': correctMove = "Hit"; break;
                    case 'S': correctMove = "Stand"; break;
                    case 'D': correctMove = "Double"; break;
                    case '!': correctMove = "Split"; break;
                    case '-': correctMove = "Surrender"; break;
                }

                if (CorrectMoveBS == '!')
                    MessageBox.Show("If you have a pair of " + Convert.ToString(PV / 2) +
                        " against a Dealer's " + Convert.ToString(DV) + " you should split the pair." +
                        " You chose to " + madeMove, "Basic Strategy");
                else if (!PlayerHand[ActiveHand].Soft)
                    MessageBox.Show("If you have a " + PV.ToString() + " against a Dealer's " +
                        DV.ToString() + " you should " + correctMove + ". You chose to " + madeMove,
                        "Basic Strategy");
                else
                    MessageBox.Show("If you have a S" + (PV + 10).ToString() +
                        "against a Dealer's " + DV.ToString() + " you should " + correctMove +
                        ". You chose to " + madeMove, "Basic Strategy");

                mistakes++; // Increment the number of mistakes
            }
        }
        
        /// <summary>
        /// Selects a card for the player after they double-down.
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Index"></param>
        public void GiveCardDouble(int Type, int Index)
        {
            int temp;

            switch (ActiveHand)
            {
                case 0:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH1C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                            DiamondAmount--;
                            break;

                        case 2:
                            picPlayerH1C3.Image = Image.FromFile(Path + Heart[Index].Path);
                            HeartAmount--;
                            break;

                        case 3:
                            picPlayerH1C3.Image = Image.FromFile(Path + Spade[Index].Path);
                            SpadeAmount--;
                            break;

                        case 4:
                            picPlayerH1C3.Image = Image.FromFile(Path + Clover[Index].Path);
                            CloverAmount--;
                            break;
                    }

                    picPlayerH1C3.Visible = true;
                    picPlayerH1C3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    temp = picPlayerH1C3.Width;
                    picPlayerH1C3.Width = picPlayerH1C3.Height;
                    picPlayerH1C3.Height = temp;

                    break;


                case 1:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH2C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                            DiamondAmount--;
                            break;

                        case 2:
                            picPlayerH2C3.Image = Image.FromFile(Path + Heart[Index].Path);
                            HeartAmount--;
                            break;

                        case 3:
                            picPlayerH2C3.Image = Image.FromFile(Path + Spade[Index].Path);
                            SpadeAmount--;
                            break;

                        case 4:
                            picPlayerH2C3.Image = Image.FromFile(Path + Clover[Index].Path);
                            CloverAmount--;
                            break;
                    }

                    picPlayerH2C3.Visible = true;
                    picPlayerH2C3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    temp = picPlayerH2C3.Width;
                    picPlayerH2C3.Width = picPlayerH2C3.Height;
                    picPlayerH2C3.Height = temp;

                    break;


                case 2:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH3C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                            DiamondAmount--;
                            break;

                        case 2:
                            picPlayerH3C3.Image = Image.FromFile(Path + Heart[Index].Path);
                            HeartAmount--;
                            break;

                        case 3:
                            picPlayerH3C3.Image = Image.FromFile(Path + Spade[Index].Path);
                            SpadeAmount--;
                            break;

                        case 4:
                            picPlayerH3C3.Image = Image.FromFile(Path + Clover[Index].Path);
                            CloverAmount--;
                            break;
                    }

                    picPlayerH3C3.Visible = true;
                    picPlayerH3C3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    temp = picPlayerH3C3.Width;
                    picPlayerH3C3.Width = picPlayerH3C3.Height;
                    picPlayerH3C3.Height = temp;

                    break;


                case 3:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH4C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                            DiamondAmount--;
                            break;

                        case 2:
                            picPlayerH4C3.Image = Image.FromFile(Path + Heart[Index].Path);
                            HeartAmount--;
                            break;

                        case 3:
                            picPlayerH4C3.Image = Image.FromFile(Path + Spade[Index].Path);
                            SpadeAmount--;
                            break;

                        case 4:
                            picPlayerH4C3.Image = Image.FromFile(Path + Clover[Index].Path);
                            CloverAmount--;
                            break;
                    }

                    picPlayerH4C3.Visible = true;
                    picPlayerH4C3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    temp = picPlayerH4C3.Width;
                    picPlayerH4C3.Width = picPlayerH4C3.Height;
                    picPlayerH4C3.Height = temp;

                    break;
            }
        }
        
        /// <summary>
        /// Gives the player a card in the appropriate hand.
        /// Use if the player has hit.
        /// </summary>
        /// <param name="Type">Type of card picked.</param>
        /// <param name="Index">Index/Number of card picked.</param>
        /// <param name="CardNum">The index of the card in player's hand.</param>
        private void GiveCardHit(int Type, int Index, int CardNum)
        {
            switch (ActiveHand)
            {
                case 0:
                    switch (CardNum)
                    {
                        case 3:
                            if (Type == 1)
                            {
                                picPlayerH1C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH1C3.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH1C3.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH1C3.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH1C3.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH1C3.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH1C3.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH1C3.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 4:
                            if (Type == 1)
                            {
                                picPlayerH1C4.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH1C4.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH1C4.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH1C4.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH1C4.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH1C4.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH1C4.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH1C4.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 5:
                            if (Type == 1)
                            {
                                picPlayerH1C5.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH1C5.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH1C5.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH1C5.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH1C5.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH1C5.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH1C5.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH1C5.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 6:
                            if (Type == 1)
                            {
                                picPlayerH1C6.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH1C6.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH1C6.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH1C6.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH1C6.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH1C6.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH1C6.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH1C6.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 7:
                            if (Type == 1)
                            {
                                picPlayerH1C7.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH1C7.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH1C7.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH1C7.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH1C7.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH1C7.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH1C7.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH1C7.Visible = true;
                                CloverAmount--;
                            }
                            break;
                    }

                    break;


                case 1:
                    switch (CardNum)
                    {
                        case 3:
                            if (Type == 1)
                            {
                                picPlayerH2C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH2C3.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH2C3.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH2C3.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH2C3.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH2C3.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH2C3.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH2C3.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 4:
                            if (Type == 1)
                            {
                                picPlayerH2C4.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH2C4.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH2C4.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH2C4.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH2C4.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH2C4.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH2C4.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH2C4.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 5:
                            if (Type == 1)
                            {
                                picPlayerH2C5.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH2C5.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH2C5.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH2C5.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH2C5.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH2C5.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH2C5.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH2C5.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 6:
                            if (Type == 1)
                            {
                                picPlayerH2C6.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH2C6.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH2C6.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH2C6.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH2C6.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH2C6.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH2C6.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH2C6.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 7:
                            if (Type == 1)
                            {
                                picPlayerH2C7.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH2C7.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH2C7.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH2C7.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH2C7.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH2C7.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH2C7.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH2C7.Visible = true;
                                CloverAmount--;
                            }
                            break;
                    }

                    break;


                case 2:
                    switch (CardNum)
                    {
                        case 3:
                            if (Type == 1)
                            {
                                picPlayerH3C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH3C3.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH3C3.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH3C3.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH3C3.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH3C3.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH3C3.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH3C3.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 4:
                            if (Type == 1)
                            {
                                picPlayerH3C4.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH3C4.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH3C4.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH3C4.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH3C4.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH3C4.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH3C4.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH3C4.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 5:
                            if (Type == 1)
                            {
                                picPlayerH3C5.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH3C5.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH3C5.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH3C5.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH3C5.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH3C5.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH3C5.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH3C5.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 6:
                            if (Type == 1)
                            {
                                picPlayerH3C6.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH3C6.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH3C6.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH3C6.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH3C6.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH3C6.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH3C6.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH3C6.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 7:
                            if (Type == 1)
                            {
                                picPlayerH3C7.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH3C7.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH3C7.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH3C7.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH3C7.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH3C7.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH3C7.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH3C7.Visible = true;
                                CloverAmount--;
                            }
                            break;
                    }

                    break;
                case 3:
                    switch (CardNum)
                    {
                        case 3:
                            if (Type == 1)
                            {
                                picPlayerH4C3.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH4C3.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH4C3.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH4C3.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH4C3.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH4C3.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH4C3.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH4C3.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 4:
                            if (Type == 1)
                            {
                                picPlayerH4C4.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH4C4.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH4C4.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH4C4.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH4C4.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH4C4.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH4C4.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH4C4.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 5:
                            if (Type == 1)
                            {
                                picPlayerH4C5.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH4C5.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH4C5.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH4C5.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH4C5.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH4C5.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH4C5.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH4C5.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 6:
                            if (Type == 1)
                            {
                                picPlayerH4C6.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH4C6.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH4C6.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH4C6.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH4C6.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH4C6.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH4C6.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH4C6.Visible = true;
                                CloverAmount--;
                            }
                            break;

                        case 7:
                            if (Type == 1)
                            {
                                picPlayerH4C7.Image = Image.FromFile(Path + Diamond[Index].Path);
                                picPlayerH4C7.Visible = true;
                                DiamondAmount--;
                            }
                            else if (Type == 2)
                            {
                                picPlayerH4C7.Image = Image.FromFile(Path + Heart[Index].Path);
                                picPlayerH4C7.Visible = true;
                                HeartAmount--;
                            }
                            else if (Type == 3)
                            {
                                picPlayerH4C7.Image = Image.FromFile(Path + Spade[Index].Path);
                                picPlayerH4C7.Visible = true;
                                SpadeAmount--;
                            }
                            else
                            {
                                picPlayerH4C7.Image = Image.FromFile(Path + Clover[Index].Path);
                                picPlayerH4C7.Visible = true;
                                CloverAmount--;
                            }
                            break; 
                    }

                    break;
            }

        }
        
        /// <summary>
        /// Gets a random card for the player (if the Player hits or doubles)
        /// </summary>
        /// <param name="Move">Player's Move</param>
        private int GetCardPlayer(String Move)
        {
            int Type = rand.Next(1, 5), Index = rand.Next(0, 13), CardInHand = 3;

            switch (ActiveHand)
            {
                case 0:
                    if (!picPlayerH1C3.Visible)
                        CardInHand = 3;
                    else if (!picPlayerH1C4.Visible)
                        CardInHand = 4;
                    else if (!picPlayerH1C5.Visible)
                        CardInHand = 5;
                    else if (!picPlayerH1C6.Visible)
                        CardInHand = 6;
                    else if (!picPlayerH1C7.Visible)
                        CardInHand = 7;
                    else
                        CardInHand = 8;

                    break;

                case 1:
                    if (!picPlayerH2C3.Visible)
                        CardInHand = 3;
                    else if (!picPlayerH2C4.Visible)
                        CardInHand = 4;
                    else if (!picPlayerH2C5.Visible)
                        CardInHand = 5;
                    else if (!picPlayerH2C6.Visible)
                        CardInHand = 6;
                    else if (!picPlayerH2C7.Visible)
                        CardInHand = 7;
                    else
                        CardInHand = 8;
                    break;

                case 2:
                    if (!picPlayerH3C3.Visible)
                        CardInHand = 3;
                    else if (!picPlayerH3C4.Visible)
                        CardInHand = 4;
                    else if (!picPlayerH3C5.Visible)
                        CardInHand = 5;
                    else if (!picPlayerH3C6.Visible)
                        CardInHand = 6;
                    else if (!picPlayerH3C7.Visible)
                        CardInHand = 7;
                    else
                        CardInHand = 8;
                    break;

                case 3:
                    if (!picPlayerH4C3.Visible)
                        CardInHand = 3;
                    else if (!picPlayerH4C4.Visible)
                        CardInHand = 4;
                    else if (!picPlayerH4C5.Visible)
                        CardInHand = 5;
                    else if (!picPlayerH4C6.Visible)
                        CardInHand = 6;
                    else if (!picPlayerH4C7.Visible)
                        CardInHand = 7;
                    else
                        CardInHand = 8;
                    break;
            }

        Recheck:
            switch (Type)
            {
                case 1:
                    if (Diamond[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Diamond[Index].Amount--;
                    break;

                case 2:
                    if (Heart[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Heart[Index].Amount--;
                    break;

                case 3:
                    if (Spade[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Spade[Index].Amount--;
                    break;

                case 4:
                    if (Clover[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Clover[Index].Amount--;
                    break;
            }

            if (Diamond[Index].Value == 1 || Diamond[Index].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[Index].Value >= 2 && Diamond[Index].Value <= 6)
                RunningCount++;                                                 // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            if (Move == "Hit")
                GiveCardHit(Type, Index, CardInHand);
            else if (Move == "Double")
                GiveCardDouble(Type, Index);

            if (Index == 0 && PlayerHand[ActiveHand].Value <= 11)
                PlayerHand[ActiveHand].Soft = true;

            PlayerHand[ActiveHand].Value += Diamond[Index].Value;

            return CardInHand;
        }
        
        /// <summary>
        /// Gets a card post split.
        /// </summary>
        private void GetCardSplit()
        {
            int Type = rand.Next(1, 5), Index = rand.Next(0, 13);

        Recheck:
            switch (Type)
            {
                case 1:
                    if (Diamond[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Diamond[Index].Amount--;
                    break;

                case 2:
                    if (Heart[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Heart[Index].Amount--;
                    break;

                case 3:
                    if (Spade[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Spade[Index].Amount--;
                    break;

                case 4:
                    if (Clover[Index].Amount == 0)
                    {
                        Type = rand.Next(1, 5);
                        Index = rand.Next(0, 13);
                        goto Recheck;
                    }
                    else
                        Clover[Index].Amount--;
                    break;
            }


            if (Diamond[Index].Value == 1 || Diamond[Index].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[Index].Value >= 2 && Diamond[Index].Value <= 6)
                RunningCount++;                                                 // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            PlayerHand[ActiveHand].Value += Diamond[Index].Value;
            if (Diamond[Index].Name == "Ace")
                PlayerHand[ActiveHand].Soft = true;

            PlayerHand[ActiveHand].Card2 = Diamond[Index].Value;
            CheckIfSplitable(ActiveHand);

            switch (ActiveHand)
            {
                case 0:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH1C2.Image = Image.FromFile(Path + Diamond[Index].Path);
                            break;

                        case 2:
                            picPlayerH1C2.Image = Image.FromFile(Path + Heart[Index].Path);
                            break;

                        case 3:
                            picPlayerH1C2.Image = Image.FromFile(Path + Spade[Index].Path);
                            break;

                        case 4:
                            picPlayerH1C2.Image = Image.FromFile(Path + Clover[Index].Path);
                            break;
                    }

                    picPlayerH1C2.Visible = true;
                    if (PlayerHand[ActiveHand].Soft)
                        txtHand1Val.Text = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                    else
                        txtHand1Val.Text = Convert.ToString(PlayerHand[ActiveHand].Value);

                    break;


                case 1:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH2C2.Image = Image.FromFile(Path + Diamond[Index].Path);
                            break;

                        case 2:
                            picPlayerH2C2.Image = Image.FromFile(Path + Heart[Index].Path);
                            break;

                        case 3:
                            picPlayerH2C2.Image = Image.FromFile(Path + Spade[Index].Path);
                            break;

                        case 4:
                            picPlayerH2C2.Image = Image.FromFile(Path + Clover[Index].Path);
                            break;
                    }

                    picPlayerH2C2.Visible = true;
                    if (PlayerHand[ActiveHand].Soft)
                        txtHand2Val.Text = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                    else
                        txtHand2Val.Text = Convert.ToString(PlayerHand[ActiveHand].Value);

                    break;


                case 2:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH3C2.Image = Image.FromFile(Path + Diamond[Index].Path);
                            break;

                        case 2:
                            picPlayerH3C2.Image = Image.FromFile(Path + Heart[Index].Path);
                            break;

                        case 3:
                            picPlayerH3C2.Image = Image.FromFile(Path + Spade[Index].Path);
                            break;

                        case 4:
                            picPlayerH3C2.Image = Image.FromFile(Path + Clover[Index].Path);
                            break;
                    }

                    picPlayerH3C2.Visible = true;
                    if (PlayerHand[ActiveHand].Soft)
                        txtHand3Val.Text = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                    else
                        txtHand3Val.Text = Convert.ToString(PlayerHand[ActiveHand].Value);

                    break;


                case 3:
                    switch (Type)
                    {
                        case 1:
                            picPlayerH4C2.Image = Image.FromFile(Path + Diamond[Index].Path);
                            break;

                        case 2:
                            picPlayerH4C2.Image = Image.FromFile(Path + Heart[Index].Path);
                            break;

                        case 3:
                            picPlayerH4C2.Image = Image.FromFile(Path + Spade[Index].Path);
                            break;

                        case 4:
                            picPlayerH4C2.Image = Image.FromFile(Path + Clover[Index].Path);
                            break;
                    }

                    picPlayerH4C2.Visible = true;
                    if (PlayerHand[ActiveHand].Soft)
                        txtHand4Val.Text = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                    else
                        txtHand4Val.Text = Convert.ToString(PlayerHand[ActiveHand].Value);

                    break;
            }
        }
        
        /// <summary>
        /// Checks whether the player can purchase insurance.
        /// </summary>
        private Boolean CheckInsurance()
        {
            if (DialogResult.Yes == MessageBox.Show("Dealer has an Ace. Would you like to purchase insurance?", "Purchase Insurance", MessageBoxButtons.YesNo))
            {
                Chips -= Bet / 2.0;
                MoneyChange -= Bet / 2.0;
                TxtChips.Text = "$ " + Convert.ToString(Chips);
                if (GetTrueCount() < 3)
                {
                    MessageBox.Show("You should not buy insurance at a true count less than 3!", "Deviations");
                    mistakes++;
                    insurance3d++;
                }
                else
                {
                    insurance3u++;
                }
                return true;
            }
            else
            {
                if (GetTrueCount() >= 3)
                {
                    MessageBox.Show("At a true count of +3 or higher, you should buy insurance!", "Deviations");
                    mistakes++;
                    insurance3u++;
                }
                else
                {
                    insurance3d++;
                }
                return false;
            }
        }
        
        /// <summary>
        /// Checks whether the player's active hand is splitable.
        /// </summary>
        /// <param name="Hand">Index of active hand</param>
        private void CheckIfSplitable(Int16 Hand)
        {
            if (PlayerHand[Hand].Card1 == PlayerHand[Hand].Card2)
                Splitable[Hand] = true;
        }
        
        /// <summary>
        /// Checks whether the Dealer and/or player has a Blackjack (Natural);
        /// and if the player's first hand is splitable.
        /// </summary>
        private void CheckCards(Boolean Insurance)
        {
            Boolean PBJ = false, DBJ = false;

            if (PlayerHand[0].Value == 11 && PlayerHand[0].Soft)
            {
                PBJ = true;
                txtHand1Val.Text = "Natural";
                txtHand1Val.ForeColor = Color.Maroon;
                PlayerMove = false;
            }

            if (DealerHand.Value == 1 && Facedown.Value == 10)
            {   // If the dealer is showing an ace and has a blackjack
                if (GetTrueCount() >= 3)
                    insurance3uc++;
                else
                    insurance3dc++;
            }

            if ((DealerHand.Value == 1 && Facedown.Value == 10) || (DealerHand.Value == 10 && Facedown.Value == 1))
            {
                picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                Facedown.Path = ReservePathFacedown;
                DealerHand.Value = 11;
                DealerHand.Soft = true;
                txtDealerVal.Text = "Natural";
                DBJ = true;
                PlayerMove = false;

                if (!PBJ)
                    txtHand1Val.ForeColor = Color.Black;
            }

            if (PBJ && DBJ)
            {
                Chips += Bet;
                MoneyChange += Bet;
                TxtLastEarning.Text = "$ " + Bet.ToString();
                blackjackCount++;
            }
            else if (PBJ)
            {
                Chips += (Bet * 5) / 2.0;
                MoneyChange += (Bet * 5) / 2.0;
                TxtLastEarning.Text = "$ " + (Bet * 5 / 2.0).ToString();
                picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                Facedown.Path = ReservePathFacedown;
                DealerHand.Value += Facedown.Value;
                if (Facedown.Value == 1)
                    DealerHand.Soft = true;
                if (DealerHand.Soft)
                    txtDealerVal.Text = "S" + Convert.ToString(DealerHand.Value + 10);
                else
                    txtDealerVal.Text = Convert.ToString(DealerHand.Value);
                blackjackCount++;
            }

            if (Insurance && DBJ)
            {
                Chips += 1.5 * Bet;
                MoneyChange += 1.5 * Bet;
                if (TxtLastEarning.Text == "$ 0")
                    TxtLastEarning.Text = "$ " + (1.5 * Bet).ToString();
                else
                    TxtLastEarning.Text = "$ " + (2.5 * Bet).ToString();
            }
            
            if (PBJ || DBJ)
            {
                PlayerMove = false;
                CmdReset.Visible = true;

                if (Facedown.Value == 1 || Facedown.Value == 10)
                    RunningCount--;
                else if (Facedown.Value >= 2 && Facedown.Value <= 6)
                    RunningCount++;

                RC.Text = Convert.ToString(RunningCount);

                HandsPlayed++;
            }

            TxtChips.Text = "$ " + Convert.ToString(Chips);

            CheckIfSplitable(0);
        }
        
        private void CheckRunningCount()
        {
            cmdSubmitCount.Visible = true;
            cmdSubmitCount.Text = "Don't know";

            inCountVal.Visible = true;
            txtCount.Visible = true;
        }
        
        /// <summary>
        /// Resets various items on the table after the end of a game.
        /// Can also be used when launching the app?.
        /// </summary>
        private async void EndGame(object sender, EventArgs e)
        {
            KeyPreview = false;

            picDealerCard1.Visible = false;
            picDealerCard2.Visible = false;
            picDealerCard3.Visible = false;
            picDealerCard4.Visible = false;
            picDealerCard5.Visible = false;
            picDealerCard6.Visible = false;
            picDealerCard7.Visible = false;

            picPlayerH1C1.Visible = false;
            picPlayerH1C2.Visible = false;
            picPlayerH1C3.Visible = false;
            picPlayerH1C4.Visible = false;
            picPlayerH1C5.Visible = false;
            picPlayerH1C6.Visible = false;
            picPlayerH1C7.Visible = false;

            picPlayerH2C1.Visible = false;
            picPlayerH2C2.Visible = false;
            picPlayerH2C3.Visible = false;
            picPlayerH2C4.Visible = false;
            picPlayerH2C5.Visible = false;
            picPlayerH2C6.Visible = false;
            picPlayerH2C7.Visible = false;

            picPlayerH3C1.Visible = false;
            picPlayerH3C2.Visible = false;
            picPlayerH3C3.Visible = false;
            picPlayerH3C4.Visible = false;
            picPlayerH3C5.Visible = false;
            picPlayerH3C6.Visible = false;
            picPlayerH3C7.Visible = false;

            picPlayerH4C1.Visible = false;
            picPlayerH4C2.Visible = false;
            picPlayerH4C3.Visible = false;
            picPlayerH4C4.Visible = false;
            picPlayerH4C5.Visible = false;
            picPlayerH4C6.Visible = false;
            picPlayerH4C7.Visible = false;

            picPlayerH1C3.Size = picPlayerH1C1.Size;
            picPlayerH2C3.Size = picPlayerH1C1.Size;
            picPlayerH3C3.Size = picPlayerH1C1.Size;
            picPlayerH4C3.Size = picPlayerH1C1.Size;

            picPlayerH1C2.Size = picPlayerH1C1.Size;
            picPlayerH2C2.Size = picPlayerH1C1.Size;
            picPlayerH3C2.Size = picPlayerH1C1.Size;
            picPlayerH4C2.Size = picPlayerH1C1.Size;

            for (int i = 0; i <= 3; i++)
            {
                PlayerHand[i].Value = 0;
                PlayerHand[i].Soft = false;
                PlayerHand[i].Card1 = PlayerHand[i].Card2 = 0;
                PlayerHand[i].Valid = false;
                PlayerHand[i].Double = false;
                Splitable[i] = false;
            }

            DealerHand.Value = 0;
            DealerHand.Soft = false;

            txtDealerVal.Visible = false;
            txtHand1Val.Visible = false;
            txtHand2Val.Visible = false;
            txtHand3Val.Visible = false;
            txtHand4Val.Visible = false;

            CmdPlaceBet.Visible = true;
            inBet.Visible = true;
            inBet.Text = "";

            NextHand = 0;

            ActiveHand = 0;
            PossibleDouble = true;
            PlayerMove = false;

            DiamondAmount = 0;
            CloverAmount = 0;
            HeartAmount = 0;
            SpadeAmount = 0;

            for (int i = 0; i <= 12; i++)
            {
                DiamondAmount += Diamond[i].Amount;
                HeartAmount += Heart[i].Amount;
                SpadeAmount += Spade[i].Amount;
                CloverAmount += Clover[i].Amount;
            }

            if (DiamondAmount + HeartAmount + CloverAmount + SpadeAmount <= 26)
            {
                CheckRunningCount();

                while (!CountConfirmed)
                    await Task.Delay(250);
                CountConfirmed = false;

                MessageBox.Show("The shoe will be shuffled.", "Shoe shuffle");
                CmdShuffle_Click(sender, e);
            }

            txtHand1Val.ForeColor = Color.Black;

            DiamondAmount = 0;
            SpadeAmount = 0;
            HeartAmount = 0;
            CloverAmount = 0;

            for (int i  = 0; i <= 12; i++)
            {
                DiamondAmount += Diamond[i].Amount;
                CloverAmount += Clover[i].Amount;
                HeartAmount += Heart[i].Amount;
                SpadeAmount += Spade[i].Amount;
            }

            double sub = (((TotalCards - SpadeAmount - DiamondAmount - CloverAmount - HeartAmount) * 165) / TotalCards);

            picShoeFrt.Height = Convert.ToInt32(sub);
            picShoeFrt.Top = 195 - picShoeFrt.Height;

            DCA.Text = Convert.ToString(DiamondAmount);
            HCA.Text = Convert.ToString(HeartAmount);
            SCA.Text = Convert.ToString(SpadeAmount);
            CCA.Text = Convert.ToString(CloverAmount);

            TCA.Text = Convert.ToString(CloverAmount + SpadeAmount + HeartAmount + DiamondAmount);

            HandsPlayed++;

            CMBS.Text = "";
            CorrectMoveBS = ' ';
            CorrectMoveDev.Count = 0;
            CorrectMoveDev.Move = '\0';
        }
        
        /// <summary>
        /// Sets-up the start of a game (Initial Draw).
        /// </summary>
        private async void StartGame()
        {
            Boolean RandPicked = false;     // Whether a card has been picked
            int k = 8;                      // Index number (random picked)

            await Task.Delay(125);

        GetPlayerCard1:                     // <Gets Player's first card>
            switch (rand.Next(1, 5))        // <Picks between types of cards>
            {
                case 1:                     // <Diamond card>
                    k = rand.Next(0, 13);
                    if (Diamond[k].Amount > 0)
                    {
                        picPlayerH1C1.Image = Image.FromFile(Path + Diamond[k].Path);
                        picPlayerH1C1.Visible = true;
                        PlayerHand[0].Value = Diamond[k].Value;
                        PlayerHand[0].Card1 = Diamond[k].Value;
                        if (k == 0)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Diamond[k].Amount--;
                        DiamondAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Diamond card>

                case 2:                     // <Heart Card>
                    k = rand.Next(0, 13);
                    if (Heart[k].Amount > 0)
                    {
                        picPlayerH1C1.Image = Image.FromFile(Path + Heart[k].Path);
                        picPlayerH1C1.Visible = true;
                        PlayerHand[0].Value = Heart[k].Value;
                        PlayerHand[0].Card1 = Heart[k].Value;
                        if (k == 0)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Heart[k].Amount--;
                        HeartAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Heart Card>

                case 3:                     // <Spade Card>
                    k = rand.Next(0, 13);
                    if (Spade[k].Amount > 0)
                    {
                        picPlayerH1C1.Image = Image.FromFile(Path + Spade[k].Path);
                        picPlayerH1C1.Visible = true;
                        PlayerHand[0].Value = Spade[k].Value;
                        PlayerHand[0].Card1 = Spade[k].Value;
                        if (k == 0)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Spade[k].Amount--;
                        SpadeAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Spade Card>

                case 4:                     // <Clover Card>
                    k = rand.Next(0, 13);
                    if (Clover[k].Amount > 0)
                    {
                        picPlayerH1C1.Image = Image.FromFile(Path + Clover[k].Path);
                        picPlayerH1C1.Visible = true;
                        PlayerHand[0].Value = Clover[k].Value;
                        PlayerHand[0].Card1 = Clover[k].Value;
                        if (k == 0)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Clover[k].Amount--;
                        CloverAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Clover Card>
            }                       

            if (!RandPicked)                // <If a card is not picked, re-pick a card>
                goto GetPlayerCard1;        // </Gets player's first card>

            txtHand1Val.Visible = true;
            if (PlayerHand[0].Soft)
                txtHand1Val.Text = "S" + Convert.ToString(PlayerHand[0].Value + 10);
            else
                txtHand1Val.Text = Convert.ToString(PlayerHand[0].Value);

            RandPicked = false;

            if (Diamond[k].Value == 1 || Diamond[k].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[k].Value >= 2 && Diamond[k].Value <= 6)
                RunningCount++;                                         // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            await Task.Delay(500);


        GetDealerCard1:                 // <Gets Dealer's first card>
            switch (rand.Next(1, 5))        // <Picks between types of cards>
            {
                case 1:                     // <Diamond card>
                    k = rand.Next(0, 13);
                    if (Diamond[k].Amount > 0)
                    {
                        picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                        picDealerCard1.Visible = true;
                        Facedown.Value = Diamond[k].Value;
                        Facedown.Path = Diamond[k].Path;
                        
                        Diamond[k].Amount--;
                        DiamondAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Diamond card>

                case 2:                     // <Heart Card>
                    k = rand.Next(0, 13);
                    if (Heart[k].Amount > 0)
                    {
                        picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                        picDealerCard1.Visible = true;
                        Facedown.Value = Heart[k].Value;
                        Facedown.Path = Heart[k].Path;

                        Heart[k].Amount--;
                        HeartAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Heart Card>

                case 3:                     // <Spade Card>
                    k = rand.Next(0, 13);
                    if (Spade[k].Amount > 0)
                    {
                        picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                        picDealerCard1.Visible = true;
                        Facedown.Value = Spade[k].Value;
                        Facedown.Path = Spade[k].Path;

                        Spade[k].Amount--;
                        SpadeAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Spade Card>

                case 4:                     // <Clover Card>
                    k = rand.Next(0, 13);
                    if (Clover[k].Amount > 0)
                    {
                        picDealerCard1.Image = Image.FromFile(Path + Facedown.Path);
                        picDealerCard1.Visible = true;
                        Facedown.Value = Clover[k].Value;
                        Facedown.Path = Clover[k].Path;

                        Clover[k].Amount--;
                        CloverAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Clover Card>
            }

            if (!RandPicked)
                goto GetDealerCard1;        // </Gets Dealer's first card>

            RandPicked = false;

            await Task.Delay(500);



        GetPlayerCard2:                     // <Gets Player's second card>
            switch (rand.Next(1, 5))        // <Picks between types of cards>
            {
                case 1:                     // <Diamond card>
                    k = rand.Next(0, 13);
                    if (Diamond[k].Amount > 0)
                    {
                        picPlayerH1C2.Image = Image.FromFile(Path + Diamond[k].Path);
                        picPlayerH1C2.Visible = true;
                        PlayerHand[0].Value += Diamond[k].Value;
                        PlayerHand[0].Card2 = Diamond[k].Value;
                        if (k == 0 || PlayerHand[0].Soft)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Diamond[k].Amount--;
                        DiamondAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Diamond card>

                case 2:                     // <Heart Card>
                    k = rand.Next(0, 13);
                    if (Heart[k].Amount > 0)
                    {
                        picPlayerH1C2.Image = Image.FromFile(Path + Heart[k].Path);
                        picPlayerH1C2.Visible = true;
                        PlayerHand[0].Value += Heart[k].Value;
                        PlayerHand[0].Card2 = Heart[k].Value;
                        if (k == 0 || PlayerHand[0].Soft)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Heart[k].Amount--;
                        HeartAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Heart Card>

                case 3:                     // <Spade Card>
                    k = rand.Next(0, 13);
                    if (Spade[k].Amount > 0)
                    {
                        picPlayerH1C2.Image = Image.FromFile(Path + Spade[k].Path);
                        picPlayerH1C2.Visible = true;
                        PlayerHand[0].Value += Spade[k].Value;
                        PlayerHand[0].Card2 = Spade[k].Value;
                        if (k == 0 || PlayerHand[0].Soft)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Spade[k].Amount--;
                        SpadeAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Spade Card>

                case 4:                     // <Clover Card>
                    k = rand.Next(0, 13);
                    if (Clover[k].Amount > 0)
                    {
                        picPlayerH1C2.Image = Image.FromFile(Path + Clover[k].Path);
                        picPlayerH1C2.Visible = true;
                        PlayerHand[0].Value += Clover[k].Value;
                        PlayerHand[0].Card2 = Clover[k].Value;
                        if (k == 0 || PlayerHand[0].Soft)
                            PlayerHand[0].Soft = true;
                        else
                            PlayerHand[0].Soft = false;
                        Clover[k].Amount--;
                        CloverAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Clover Card>
            }

            if (!RandPicked)                // <If a card is not picked, re-pick a card>
                goto GetPlayerCard2;        // </Gets player's first card>
            
            if (PlayerHand[0].Soft)
                txtHand1Val.Text = "S" + Convert.ToString(PlayerHand[0].Value + 10);
            else
                txtHand1Val.Text = Convert.ToString(PlayerHand[0].Value);

            RandPicked = false;
            
            if (Diamond[k].Value == 1 || Diamond[k].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[k].Value >= 2 && Diamond[k].Value <= 6)
                RunningCount++;                                         // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);

            await Task.Delay(500);



        GetDealerCard2:                 // <Gets Dealer's second card>
            switch (rand.Next(1, 5))        // <Picks between types of cards>
            {
                case 1:                     // <Diamond card>
                    k = rand.Next(0, 13);
                    if (Diamond[k].Amount > 0)
                    {
                        picDealerCard2.Image = Image.FromFile(Path + Diamond[k].Path);
                        picDealerCard2.Visible = true;
                        DealerHand.Value = Diamond[k].Value;
                        if (k == 0)
                            DealerHand.Soft = true;
                        else
                            DealerHand.Soft = false;
                        Diamond[k].Amount--;
                        DiamondAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Diamond card>

                case 2:                     // <Heart Card>
                    k = rand.Next(0, 13);
                    if (Heart[k].Amount > 0)
                    {
                        picDealerCard2.Image = Image.FromFile(Path + Heart[k].Path);
                        picDealerCard2.Visible = true;
                        DealerHand.Value = Heart[k].Value;
                        if (k == 0)
                            DealerHand.Soft = true;
                        else
                            DealerHand.Soft = false;
                        Heart[k].Amount--;
                        HeartAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Heart Card>

                case 3:                     // <Spade Card>
                    k = rand.Next(0, 13);
                    if (Spade[k].Amount > 0)
                    {
                        picDealerCard2.Image = Image.FromFile(Path + Spade[k].Path);
                        picDealerCard2.Visible = true;
                        DealerHand.Value = Spade[k].Value;
                        if (k == 0)
                            DealerHand.Soft = true;
                        else
                            DealerHand.Soft = false;
                        Spade[k].Amount--;
                        SpadeAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Spade Card>

                case 4:                     // <Clover Card>
                    k = rand.Next(0, 13);
                    if (Clover[k].Amount > 0)
                    {
                        picDealerCard2.Image = Image.FromFile(Path + Clover[k].Path);
                        picDealerCard2.Visible = true;
                        DealerHand.Value = Clover[k].Value;
                        if (k == 0)
                            DealerHand.Soft = true;
                        else
                            DealerHand.Soft = false;
                        Clover[k].Amount--;
                        CloverAmount--;

                        RandPicked = true;
                    }
                    break;                  // </Clover Card>
            }

            if (!RandPicked)
                goto GetDealerCard2;        // </Gets Dealer's first card>

            txtDealerVal.Visible = true;
            if (DealerHand.Soft)
                txtDealerVal.Text = "S" + Convert.ToString(DealerHand.Value + 10);
            else
                txtDealerVal.Text = Convert.ToString(DealerHand.Value);
                       
            if (Diamond[k].Value == 1 || Diamond[k].Value == 10)        // <Update the running count>
                RunningCount--;
            else if (Diamond[k].Value >= 2 && Diamond[k].Value <= 6)
                RunningCount++;                                         // </Update the running count>

            RC.Text = Convert.ToString(RunningCount);
        }

        private async void CmdPlaceBet_Click(object sender, EventArgs e)
        {
            Boolean ok = true, Insurance = false;                                  // Is entered text a number

            CmdReset.Visible = false;

            TxtLastEarning.Text = "$ 0";

            if (inBet.Text == "")
                ok = false;

            for (int i = 0; i <= inBet.TextLength - 1; i++)     // <Whether the TextBox contains a number>
            {
                if (inBet.Text[i] < '0' || inBet.Text[i] > '9')
                {
                    ok = false;
                    break;
                }
            }                                                   // </Whether the TextBox contains a number>

            if (ok)
            {
                Bet = Convert.ToInt32(inBet.Text);
                MoneyChange -= Bet;
            }
            else
            {
                inBet.Text = "";
                MessageBox.Show("This box can only contain integer numbers, please enter your bet", "Incorrect input!");
                return;
            }

            if (Bet > Chips)
            {
                inBet.Text = "";
                MessageBox.Show("You do not have suffitient funds to place this bet!", "Insufficient funds");
                return;
            }
            Chips -= Bet;
            TxtChips.Text = "$ " + Convert.ToString(Chips);

            StartGame();

            await Task.Delay(1800);

            inBet.Visible = false;                              // <Makes bet input box invisible>
            CmdPlaceBet.Visible = false;                        // <Makes bet input button invisible>

            if (DealerHand.Value == 1)
                Insurance = CheckInsurance();

            CheckCards(Insurance);                              // <Checks for Blackjacks and Splitables>

            ActiveHand = 0;                                     // <Player's active hand ( -1 )

            PlayerHand[0].Valid = true;
            if (txtHand1Val.Text != "Natural" && txtDealerVal.Text != "Natural")
            {
                PlayerMove = true;
                txtHand1Val.ForeColor = Color.Gold;                 // <Active Hand Colour>
            }

            GetBasicStratMove();
            KeyPreview = true;
        }

        private void CmdHit_Click(object sender, EventArgs e)
        {
            String ActHandVal;              // <Value of player's hand (To be returned)>
            int cards;                      // <Amount of cards in player's hand>
            
            if (PlayerMove)
            {
                BasicStratCheck('H');       // <Check whether the player should have hit>

                PossibleDouble = false;                                                 // <Player cannot double-down>
                cards = GetCardPlayer("Hit");                                           // <Gets a card for the player's active hand>

                if (PlayerHand[ActiveHand].Soft && PlayerHand[ActiveHand].Value > 11)   // <Whether the hand is still soft>
                    PlayerHand[ActiveHand].Soft = false;

                if (PlayerHand[ActiveHand].Soft)                                        // <Label text>
                    ActHandVal = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                else if (PlayerHand[ActiveHand].Value > 21)
                {
                    ActHandVal = "Bust";
                    PlayerHand[ActiveHand].Valid = false;
                }
                else
                    ActHandVal = Convert.ToString(PlayerHand[ActiveHand].Value);



                switch (ActiveHand)
                {
                    case 0:
                        txtHand1Val.Text = ActHandVal;
                        break;

                    case 1:
                        txtHand2Val.Text = ActHandVal;
                        break;

                    case 2:
                        txtHand3Val.Text = ActHandVal;
                        break;

                    case 3:
                        txtHand4Val.Text = ActHandVal;
                        break;
                }                                                                       // </Label text>

                if (cards == 7)                                                         // <If player has 7 cards, then stand>
                    CmdStand_Click(sender, e);

                if (PlayerHand[ActiveHand].Value == 21 || PlayerHand[ActiveHand].Value == 11 && PlayerHand[ActiveHand].Soft)
                    CmdStand_Click(sender, e);                                          // <Stand if player has a 21>

                if (ActHandVal == "Bust")                                               // <If player busts, then stand>
                    CmdStand_Click(sender, e);

                if (ActHandVal != "Bust")
                    GetBasicStratMove();
            }
        }

        private void CmdStand_Click(object sender, EventArgs e)
        {
            bool NextMove = false;

            if (PlayerMove)
            {
                if (sender.Equals(CmdStand))        // <If the player chose to stand>
                    BasicStratCheck('S');           // <Check whether the player should have stood>

                switch (ActiveHand)
                {
                    case 0:
                        txtHand1Val.ForeColor = Color.Black;
                        if (NextHand >= 1)
                        {
                            txtHand2Val.ForeColor = Color.Gold;
                            ActiveHand++;
                            NextMove = true;
                            GetCardSplit();
                        }
                        break;

                    case 1:
                        txtHand2Val.ForeColor = Color.Black;
                        if (NextHand >= 2)
                        {
                            txtHand3Val.ForeColor = Color.Gold;
                            ActiveHand++;
                            NextMove = true;
                            GetCardSplit();
                        }
                        break;

                    case 2:
                        txtHand3Val.ForeColor = Color.Black;
                        if (NextHand == 3)
                        {
                            txtHand4Val.ForeColor = Color.Gold;
                            ActiveHand++;
                            NextMove = true;
                            GetCardSplit();
                        }
                        break;

                    case 3:
                        txtHand4Val.ForeColor = Color.Black;
                        break;
                }

                PossibleDouble = true;
                if (PlayerHand[ActiveHand].Value < 21)
                    GetBasicStratMove();
                if (!NextMove)
                {
                    PlayerMove = false;
                    DealerPlay();
                }
            }
        }

        private void CmdDouble_Click(object sender, EventArgs e)
        {
            String ActHandVal;

            if (PlayerMove && PossibleDouble)
            {
                BasicStratCheck('D');           // <Check whether the player should have doubled>

                if (Bet > Chips)
                {
                    MessageBox.Show("You do not have sufficient funds to make this move!", "Insufficient funds");
                    return;
                }

                Chips -= Bet;
                MoneyChange -= Bet;
                TxtChips.Text = "$ " + Convert.ToString(Chips);
                PlayerHand[ActiveHand].Double = true;

                GetCardPlayer("Double");

                if (PlayerHand[ActiveHand].Value > 11 && PlayerHand[ActiveHand].Soft)
                    PlayerHand[ActiveHand].Soft = false;

                if (PlayerHand[ActiveHand].Soft)                                        // <Label text>
                    ActHandVal = "S" + Convert.ToString(PlayerHand[ActiveHand].Value + 10);
                else if (PlayerHand[ActiveHand].Value > 21)
                {
                    ActHandVal = "Bust";
                    PlayerHand[ActiveHand].Valid = false;
                }
                else
                    ActHandVal = Convert.ToString(PlayerHand[ActiveHand].Value);

                switch (ActiveHand)
                {
                    case 0:
                        txtHand1Val.Text = ActHandVal;
                        break;

                    case 1:
                        txtHand2Val.Text = ActHandVal;
                        break;

                    case 2:
                        txtHand3Val.Text = ActHandVal;
                        break;

                    case 3:
                        txtHand4Val.Text = ActHandVal;
                        break;
                }                                                                       // </Label text>

                CmdStand_Click(sender, e);
            }
        }

        private void CmdSurrend_Click(object sender, EventArgs e)
        {
            if (PlayerMove)
            {
                BasicStratCheck('-');   // <Whether the player should surrender>

                Chips += Bet / 2.0;
                MoneyChange += Bet / 2.0;
                TxtChips.Text = "$ " + Convert.ToString(Chips);
                PlayerHand[ActiveHand].Valid = false;

                CmdStand_Click(sender, e);
            }
        }
        
        private void CmdSplit_Click(object sender, EventArgs e)
        {
            if (PlayerMove && Splitable[ActiveHand])
            {
                BasicStratCheck('!');   // <Whether the player should have split>

                if (Bet > Chips)
                {   // <Whether the player can afford to split>
                    MessageBox.Show("You have insufficient funds to make this move!", "Insufficient funds");
                    return;
                }

                Chips -= Bet;
                MoneyChange -= Bet;
                TxtChips.Text = "$ " + Convert.ToString(Chips);

                if (NextHand == 0)
                {
                    picPlayerH2C1.Image = picPlayerH1C2.Image;      // <Transfers card to new hand>
                    picPlayerH2C1.Visible = true;

                    bool SplitAces = false;
                    if (PlayerHand[0].Value == 2 && PlayerHand[0].Soft)
                    {
                        PlayerHand[1].Soft = true;
                        SplitAces = true;
                    }

                    PlayerHand[ActiveHand].Value /= 2;                       // <Adjusts hand values>
                    PlayerHand[1].Value = PlayerHand[ActiveHand].Value;
                    PlayerHand[1].Valid = true;                              // <Dealer should try to beat this hand>
                    PlayerHand[1].Card1 = PlayerHand[0].Card1;


                    GetCardSplit();
                    NextHand = 1;

                    if (SplitAces)
                    {
                        ActiveHand++;
                        GetCardSplit();

                        picPlayerH1C2.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        int res = picPlayerH1C2.Width;
                        picPlayerH1C2.Width = picPlayerH1C2.Height;
                        picPlayerH1C2.Height = res;

                        picPlayerH2C2.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        res = picPlayerH2C2.Width;
                        picPlayerH2C2.Width = picPlayerH2C2.Height;
                        picPlayerH2C2.Height = res;

                        PlayerMove = false;

                        txtHand1Val.ForeColor = Color.Black;

                        txtHand2Val.Visible = true;
                        PlayerHand[1].Valid = true;
                        DealerPlay();
                        return;
                    }

                    if (PlayerHand[0].Value == 2 * PlayerHand[1].Value)
                        Splitable[0] = true;
                    else
                        Splitable[0] = false;

                    txtHand2Val.Visible = true;
                    txtHand2Val.Text = Convert.ToString(PlayerHand[1].Value);
                }
                else if (NextHand == 1)
                {
                    if (ActiveHand == 0)                                    // <Transfers card to new hand>
                        picPlayerH3C1.Image = picPlayerH1C2.Image;   
                    else
                        picPlayerH3C1.Image = picPlayerH2C2.Image;
                    picPlayerH3C1.Visible = true;

                    PlayerHand[ActiveHand].Value /= 2;                       // <Adjusts hand values>
                    PlayerHand[2].Value = PlayerHand[ActiveHand].Value;
                    PlayerHand[2].Valid = true;
                    PlayerHand[2].Card1 = PlayerHand[0].Card1;                             // <Dealer should try to beat this hand>

                    GetCardSplit();
                    NextHand = 2;

                    txtHand3Val.Visible = true;
                    txtHand3Val.Text = Convert.ToString(PlayerHand[2].Value);

                    if (PlayerHand[ActiveHand].Value == 2 * PlayerHand[2].Value)
                        Splitable[ActiveHand] = true;
                    else
                        Splitable[ActiveHand] = false;
                }
                else if (NextHand == 2)
                {
                    if (ActiveHand == 0)                                    // <Transfers card to new hand>
                        picPlayerH4C1.Image = picPlayerH1C2.Image;
                    else if (ActiveHand == 1)
                        picPlayerH4C1.Image = picPlayerH2C2.Image;
                    else
                        picPlayerH4C1.Image = picPlayerH3C2.Image;
                    picPlayerH4C1.Visible = true;

                    PlayerHand[ActiveHand].Value /= 2;                       // <Adjusts hand values>
                    PlayerHand[3].Value = PlayerHand[ActiveHand].Value;
                    PlayerHand[3].Valid = true;
                    PlayerHand[3].Card1 = PlayerHand[0].Card1;                           // <Dealer should try to beat this hand>

                    GetCardSplit();
                    NextHand = 3;

                    txtHand4Val.Visible = true;
                    txtHand4Val.Text = Convert.ToString(PlayerHand[3].Value);

                    if (PlayerHand[ActiveHand].Value == 2 * PlayerHand[3].Value)
                        Splitable[ActiveHand] = true;
                    else
                        Splitable[ActiveHand] = false;
                }

                GetBasicStratMove();
            }
        }

        private void CmdShuffle_Click(object sender, EventArgs e)
        {
            CardSetupDiamond();
            CardSetupHeart();
            CardSetupSpade();
            CardSetupClover();

            RunningCount = 0;

            CmdReset_Click(sender, e);
            
            CmdMenu_Click(sender, e);
        }

        private void CmdReset_Click(object sender, EventArgs e)
        {
            HandsPlayed--;
            EndGame(sender, e);
        }

        private void Table_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1: CmdHit_Click(sender, e); break;               // <Hit>
                case Keys.D2: CmdDouble_Click(sender, e); break;            // <Double-Down>
                case Keys.D3: CmdSplit_Click(sender, e); break;             // <Split>
                case Keys.D4: CmdStand_Click(sender, e); break;             // <Stand>
                case Keys.D5: CmdSurrend_Click(sender, e); break;           // <Surrender>

                case Keys.NumPad1: CmdHit_Click(sender, e); break;          // <Hit>
                case Keys.NumPad2: CmdDouble_Click(sender, e); break;       // <Double-Down>
                case Keys.NumPad3: CmdSplit_Click(sender, e); break;        // <Split>
                case Keys.NumPad4: CmdStand_Click(sender, e); break;        // <Stand>
                case Keys.NumPad5: CmdSurrend_Click(sender, e); break;      // <Surrender>

                case Keys.D0: CmdReset_Click(sender, e); break;             // <Reset>   
                case Keys.NumPad0: CmdReset_Click(sender, e); break;        // <Reset>             
            }

        }

        private void CmdMenu_Click(object sender, EventArgs e)
        {
            if (CmdMenu.Text == "↓")
            {
                grboxOptionsMenu.Visible = true;
                CmdMenu.Text = "↑";
            }
            else if (CmdMenu.Text == "↑")
            {
                grboxOptionsMenu.Visible = false;
                CmdMenu.Text = "↓";
            }
            
        }

        private void CmdMainMenu_Click(object sender, EventArgs e)
        {
            CmdMenu_Click(sender, e);
            System.IO.File.WriteAllText(Path + "base/profile/chips.txt", Convert.ToString(Chips));
            MainMenu form = new MainMenu();
            WriteValEV_data();
            this.Hide();
            form.Show();
        }

        private void CmdGameInfo_Click(object sender, EventArgs e)
        {
            // < Amount of cards per attribute >
            
            setupCards(0);
            setupPlayerInfo(0);
            
            // </ Amount of cards per attribute >
            // ----
            // < Count and Shoe info >

            RC.Text = Convert.ToString(RunningCount);

            double RemDecks = (HeartAmount + SpadeAmount + CloverAmount + DiamondAmount) / 52.0; // <Amount of cards remaining / 52 cards per deck>

            double TrueCount = GetTrueCount();  // <True count (Running count * % of total cards remaining * 6 decks)>

            TC.Text = Convert.ToString(Convert.ToInt16(TrueCount * 4) / 4.0);

            DR.Text = Convert.ToString(1.0 * Convert.ToInt16(RemDecks * 4) / 4.0);
            //DR.Text = Convert.ToString(RemDecks);


            // </ Count and Shoe info >
            // ----
            // < Correct Move (Hand info) >

            CmdMenu_Click(sender, e);

            string correctMove = "";
            switch (CorrectMoveBS)
            {
                case 'H': correctMove = "Hit"; break;
                case 'S': correctMove = "Stand"; break;
                case 'D': correctMove = "Double"; break;
                case '!': correctMove = "Split"; break;
                case '-': correctMove = "Surrender"; break;
            }

            CMBS.Text = correctMove;

            switch (CorrectMoveDev.Move)
            {
                case 'H': correctMove = "Hit"; break;
                case 'S': correctMove = "Stand"; break;
                case 'D': correctMove = "Double"; break;
                case '!': correctMove = "Split"; break;
                case '-': correctMove = "Surrender"; break;
                case '\0': correctMove = "Basic Strat"; break;
            }

            CMD.Text = correctMove;

            int PV = PlayerHand[ActiveHand].Value;
            int bustCount = 0, safeCount = 0;


            if (PlayerMove)
            {
                for (int i = 0; i <= 12; i++)
                {
                    if (PV == 0)
                        break;
                    if (Diamond[i].Value + PV > 21)
                    {
                        bustCount += Diamond[i].Amount + Heart[i].Amount + Spade[i].Amount + Clover[i].Amount;
                    }
                    else
                    {
                        safeCount += Diamond[i].Amount + Heart[i].Amount + Spade[i].Amount + Clover[i].Amount;
                    }
                }

                if (PV != 0)
                {
                    string bustRisk = (bustCount * 10000 / (bustCount + safeCount) / 100.0).ToString() + '%';
                    txtBR.Text = "Bust Risk:";
                    BR.Text = bustRisk;
                }
                else
                {
                    txtBR.Text = "Bust Risk:";
                    BR.Text = "";
                }
            }
            else
            {
                int tens = 0, aces = 0, rest = 0;

                for (int i = 0; i <= 12; i++)
                {
                    int tempsum = Diamond[i].Amount + Heart[i].Amount;
                    tempsum += Spade[i].Amount + Clover[i].Amount;

                    if (i == 0)
                        aces = tempsum;
                    else if (i >= 9 && i <= 12)
                        tens += tempsum;
                    else
                        rest += tempsum;
                }
                
                double bjChance = 0;
                double temp = 0;
                double totalCards = aces + tens + rest;
                // P: 10;   D: A;   P: A;
                temp = (tens /totalCards) * (aces / totalCards) * ((aces - 1) / totalCards);
                bjChance += temp;
                // P: 10;   D: !A;  P: A;
                temp = (tens / totalCards) * ((totalCards - aces) / totalCards) * (aces / totalCards);
                bjChance += temp;
                // P: A;   D: 10;   P: 10;
                temp = (aces / totalCards) * (tens / totalCards) * ((tens - 1) / totalCards);
                bjChance += temp;
                // P: A;   D: !10;   P: 10;
                temp = (aces / totalCards) * ((totalCards - tens) / totalCards) * (tens / totalCards);
                bjChance += temp;

                bjChance = Math.Round(bjChance * 10000) / 100;

                txtBR.Text = "Blackjack chance:";
                BR.Text = bjChance.ToString() + "%";
            }
            
            // </ Correct Move (Hand info) >
            // ----
            // < Player info >

            if (HandsPlayed != 0)
                MpH.Text = Convert.ToString(Convert.ToInt32(MoneyChange * 100 / HandsPlayed) / 100.0);
            else
                MpH.Text = "N/A";
            TE.Text = Convert.ToString(MoneyChange);
            HP.Text = Convert.ToString(HandsPlayed);
            
            // </ Player info >
            // ----
            // < Time >

            int hrs = DateTime.Now.Hour, min = DateTime.Now.Minute;
            string time = "";

            if (hrs < 10)
                time = "0";
            time += Convert.ToString(hrs) + ":";

            if (min < 10)
                time += "0";
            time += Convert.ToString(min);

            txtTime.Text = time;

            // </ Time >

            grboxGameInfo.Visible = true;
        }

        private void txtCollapseInfo_Click(object sender, EventArgs e)
        {
            grboxGameInfo.Visible = false;
        }

        private void grboxOptionsMenu_Enter(object sender, EventArgs e)
        {

        }

        private async void cmdWongOut_Click(object sender, EventArgs e)
        {
            string retVal = Interaction.InputBox("How many hands do you want to Wong-Out for?", "Wonging out");
            int rounds;

        ConvertValue:
            try
            {
                if (retVal == "")
                    return;
                rounds = Convert.ToInt32(retVal);
            }
            catch
            {
                retVal = Interaction.InputBox("How many hands do you want to Wong-Out for? Please enter an integer value!", "Wonging out");
                goto ConvertValue;
            }

            for (int i = 1; i <= rounds; i++)
            {
                inBet.Text = "0";

                CmdPlaceBet_Click(sender, e);
                await Task.Delay(1800);

                bool loop = true;
                int k = 0;
                while (loop)
                {
                    switch (CorrectMoveDev.Move)
                    {
                        case 'H':
                            k = 0;
                            CmdHit_Click(sender, e); break;   // Hit

                        case 'D':
                            k = 0;
                            if (PossibleDouble)
                                CmdDouble_Click(sender, e);         // Double if possible
                            else
                                CmdHit_Click(sender, e);            // Else Hit
                            break;

                        case 'S':
                            CmdStand_Click(sender, e);              // Stand
                            loop = false;
                            break; 

                        case '!':
                            k = 0;
                            CmdSplit_Click(sender, e); break; // Split

                        case '\0':
                            k = 0;
                            switch (CorrectMoveBS)
                            {
                                case 'H': CmdHit_Click(sender, e); break;       // Hit

                                case 'D':
                                    if (PossibleDouble)
                                        CmdDouble_Click(sender, e);             // Double if possible
                                    else
                                        CmdHit_Click(sender, e);                // Else Hit
                                    break;

                                case 'S':
                                    CmdStand_Click(sender, e);                  // Stand
                                    loop = false;
                                    break;

                                case '!': CmdSplit_Click(sender, e); break;     // Split

                                case '-': CmdSurrend_Click(sender, e); break;   // Surrender
                            }
                            break;
                    }
                    if (PlayerHand[ActiveHand].Value == 21 || (PlayerHand[ActiveHand].Value == 11 && PlayerHand[ActiveHand].Soft))
                        loop = false;

                    if (++k == 7)
                    {
                        DealerPlay();
                        loop = false;
                    }

                    await Task.Delay(350);
                    //MessageBox.Show("Loop: " + loop.ToString(), "");
                }

                HandsPlayed--;
                await Task.Delay(1500);     // Delay for 1.5sec
                CmdReset_Click(sender, e);  // Reset the table
            }
        }

        private void cmdMoreInfo_MouseEnter(object sender, EventArgs e)
        {
            Label Sender = (Label)sender;
            Sender.ForeColor = Color.Gold;
            Sender.Font = new Font(Sender.Font, FontStyle.Bold);
        }

        private void cmdMoreInfo_MouseLeave(object sender, EventArgs e)
        {
            Label Sender = (Label)sender;
            Sender.ForeColor = Color.Black;
            Sender.Font = new Font(Sender.Font, FontStyle.Regular);
        }

        private void setupCards(int toPage)
        {
            switch (toPage)
            {
                case 0:

                    txtTCA.Text = "Total Cards Remaining:";
                    int remCards = CloverAmount + HeartAmount + DiamondAmount + SpadeAmount;
                    TCA.Text = remCards.ToString();

                    txtDCA.Text = "Diamond Cards Remaining:";
                    DCA.Text = DiamondAmount.ToString();
                    txtHCA.Text = "Heart Cards Remaining:";
                    HCA.Text = HeartAmount.ToString();
                    txtCCA.Text = "Clover Cards Remaining:";
                    CCA.Text = CloverAmount.ToString();
                    txtSCA.Text = "Spade Cards Remaining:";
                    SCA.Text = SpadeAmount.ToString();

                    txtDCA.ForeColor = Color.Maroon;
                    txtHCA.ForeColor = Color.Maroon;

                    break;

                case 1:

                    txtTCA.Text = "2's (Twos) Remaining:";
                    TCA.Text = (Diamond[1].Amount + Heart[1].Amount + Clover[1].Amount + Spade[1].Amount).ToString();
                    txtDCA.Text = "3's (Threes) Remaining:";
                    DCA.Text = (Diamond[2].Amount + Heart[2].Amount + Clover[2].Amount + Spade[2].Amount).ToString();
                    txtHCA.Text = "4's (Fours) Remaining:";
                    HCA.Text = (Diamond[3].Amount + Heart[3].Amount + Clover[3].Amount + Spade[3].Amount).ToString();
                    txtCCA.Text = "5's (Fives) Remaining:";
                    CCA.Text = (Diamond[4].Amount + Heart[4].Amount + Clover[4].Amount + Spade[4].Amount).ToString();
                    txtSCA.Text = "6's (Sixes) Remaining:";
                    SCA.Text = (Diamond[5].Amount + Heart[5].Amount + Clover[5].Amount + Spade[5].Amount).ToString();

                    txtDCA.ForeColor = Color.Black;
                    txtHCA.ForeColor = Color.Black;

                    break;

                case 2:

                    txtTCA.Text = "7's (Sevens) Remaining:";
                    TCA.Text = (Diamond[6].Amount + Heart[6].Amount + Clover[6].Amount + Spade[6].Amount).ToString();
                    txtDCA.Text = "8's (Eights) Remaining:";
                    DCA.Text = (Diamond[7].Amount + Heart[7].Amount + Clover[7].Amount + Spade[7].Amount).ToString();
                    txtHCA.Text = "9's (Nines) Remaining:";
                    HCA.Text = (Diamond[8].Amount + Heart[8].Amount + Clover[8].Amount + Spade[8].Amount).ToString();

                    txtCCA.Text = "10 (Ten) Value Remaining:";
                    int Tens = 0;
                    for (int i = 9; i <= 12; i++)
                    {
                        Tens += Diamond[i].Amount + Heart[i].Amount + Clover[i].Amount + Spade[i].Amount;
                    }
                    CCA.Text = Tens.ToString();
                    txtSCA.Text = "Aces Remaining:";
                    SCA.Text = (Diamond[0].Amount + Heart[0].Amount + Clover[0].Amount + Spade[0].Amount).ToString();



                    txtDCA.ForeColor = Color.Black;
                    txtHCA.ForeColor = Color.Black;

                    break;

                case 3:

                    txtTCA.Text = "10's (Tens) Remaining:";
                    TCA.Text = (Diamond[9].Amount + Heart[9].Amount + Clover[9].Amount + Spade[9].Amount).ToString();
                    txtDCA.Text = "Jacks Remaining:";
                    DCA.Text = (Diamond[10].Amount + Heart[10].Amount + Clover[10].Amount + Spade[10].Amount).ToString();
                    txtHCA.Text = "Queens Remaining:";
                    HCA.Text = (Diamond[11].Amount + Heart[11].Amount + Clover[11].Amount + Spade[11].Amount).ToString();
                    txtCCA.Text = "Kings Remaining:";
                    CCA.Text = (Diamond[12].Amount + Heart[12].Amount + Clover[12].Amount + Spade[12].Amount).ToString();
                    txtSCA.Text = "Aces Remaining:";
                    SCA.Text = (Diamond[0].Amount + Heart[0].Amount + Clover[0].Amount + Spade[0].Amount).ToString();

                    txtDCA.ForeColor = Color.Black;
                    txtHCA.ForeColor = Color.Black;

                    break;
            }
        }

        private void cmdCardsNav_Click(object sender, EventArgs e)
        {
            Label Sender = (Label)sender;
            char direction = Sender.Name[8];

            int page;
            switch (txtTCA.Text[0])
            {
                case 'T': page = 4; break;
                case '2': page = 1; break;
                case '7': page = 2; break;
                case '1': page = 3; break;
                default: page = 4; break;
            }

            setupCards(direction == 'R' ? ++page % 4 : --page % 4);
        }

        private void inCountVal_TextChanged(object sender, EventArgs e)
        {
            if (inCountVal.Text == "")
                cmdSubmitCount.Text = "Don't know";
            else
                cmdSubmitCount.Text = "Submit";
        }

        private void cmdSubmitCount_Click(object sender, EventArgs e)
        {
            if (cmdSubmitCount.Text == "Don't know")
            {
                MessageBox.Show("The running count is: " + RunningCount.ToString(), "Running Count");
            }
            else
            {
                if (inCountVal.Text == RunningCount.ToString())
                    MessageBox.Show("Correct!", "Running Count");
                else
                    MessageBox.Show("Incorrect! The running count is: " + RunningCount.ToString(), "Running Count");
            }

            CountConfirmed = true;
            inCountVal.Text = "";

            cmdSubmitCount.Visible = false;
            inCountVal.Visible = false;
            txtCount.Visible = false;
        }

        private void setupPlayerInfo(int page)
        {
            switch (page)
            {
                case 0: // Show money per hand & Total Earnings:
                    txtMpH.Text = "Money / Hand: ";
                    MpH.Text = (((int)(100 * MoneyChange / HandsPlayed)) / 100.0).ToString();

                    txtTE.Text = "Total Earnings: ";
                    TE.Text = (MoneyChange - StartingAmount).ToString();
                    break;
                case 1: // Show Blackjacks received and Non-Blackjack hands:
                    txtMpH.Text = "Blackjacks Received: ";
                    MpH.Text = blackjackCount.ToString();

                    txtTE.Text = "Non-Blackjack hands: ";
                    TE.Text = (HandsPlayed - blackjackCount).ToString();
                    break;
                case 2: // Show Number of incorrect moves and mistakes per 1000 hands:
                    txtMpH.Text = "Number of Incorrect moves:";
                    MpH.Text = mistakes.ToString();

                    txtTE.Text = "Mistakes per 1000 hands:";
                    if (HandsPlayed != 0)
                        TE.Text = (((int)(10000000 * mistakes / HandsPlayed)) / 10000.0).ToString();
                    else              //^10.000.000^
                        TE.Text = "N/A";
                    break;
                case 3: // Show How many times the dealer has had a blackjack with an ace-up above and below a true 3
                    txtMpH.Text = "Ace-up & BJ above true 3: ";
                    if (insurance3u != 0)
                        MpH.Text = (((int)(10000 * insurance3uc / insurance3u)) / 100).ToString() + '%';
                    else
                        MpH.Text = "N/A";

                    txtTE.Text = "Ace-up & BJ above below 3:";
                    if (insurance3d != 0)
                        TE.Text = (((int)(10000 * insurance3dc / insurance3d)) / 100).ToString() + '%';
                    else
                        TE.Text = "N/A";
                    break;
                default: // Show money per hand & Total Earnings (default state, case 0):
                    txtMpH.Text = "Money / Hand: ";
                    MpH.Text = (((int)(100 * MoneyChange / HandsPlayed)) / 100.0).ToString();

                    txtTE.Text = "Total Earnings: ";
                    TE.Text = (MoneyChange - StartingAmount).ToString();
                    break;
            }
        }

        private void cmdPlayerRight_Click(object sender, EventArgs e)
        {
            int page = 4;
            if (txtMpH.Text[0] == 'M')
            {   // Displaying Money/Hand, change:
                page = 4;
            }
            else if (txtMpH.Text[0] == 'B')
            {   // Displaying Blackjacks received, change:
                page = 1;
            }
            else if (txtMpH.Text[0] == 'N')
            {   // Displaying Blackjacks received, change:
                page = 2;
            }
            else if (txtMpH.Text[0] == 'A')
            {   // Displaying Insurance data, change:
                page = 3;
            }

            setupPlayerInfo(++page % 4);
        }

        private void cmdPlayerLeft_Click(object sender, EventArgs e)
        {
            int page = 4;
            if (txtMpH.Text[0] == 'M')
            {   // Displaying Money/Hand, change:
                page = 4;
            }
            else if (txtMpH.Text[0] == 'B')
            {   // Displaying Blackjacks received, change:
                page = 1;
            }
            else if (txtMpH.Text[0] == 'N')
            {   // Displaying Blackjacks received, change:
                page = 2;
            }
            else if (txtMpH.Text[0] == 'A')
            {   // Displaying Insurance data, change:
                page = 3;
            }

            setupPlayerInfo(--page % 4);
        }
    }
}
