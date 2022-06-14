namespace Blackjack
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdShowChart = new System.Windows.Forms.Button();
            this.grboxCharts = new System.Windows.Forms.GroupBox();
            this.comboChartSelect = new System.Windows.Forms.ComboBox();
            this.tableHard = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCloseCharts = new System.Windows.Forms.Button();
            this.txtCharts = new System.Windows.Forms.Label();
            this.grboxSettings = new System.Windows.Forms.GroupBox();
            this.grboxPlayerData = new System.Windows.Forms.GroupBox();
            this.cmdResetEVD = new System.Windows.Forms.Button();
            this.inChipsChange = new System.Windows.Forms.TextBox();
            this.txtChips = new System.Windows.Forms.Label();
            this.grboxBetData = new System.Windows.Forms.GroupBox();
            this.inBettingUnits = new System.Windows.Forms.TextBox();
            this.txtBettingUnits = new System.Windows.Forms.Label();
            this.inBetSpread = new System.Windows.Forms.TextBox();
            this.txtBetSpread = new System.Windows.Forms.Label();
            this.grboxRules = new System.Windows.Forms.GroupBox();
            this.checkDHitS17 = new System.Windows.Forms.CheckBox();
            this.grboxBJPay = new System.Windows.Forms.GroupBox();
            this.radioBJ1to1 = new System.Windows.Forms.RadioButton();
            this.radioBJ6to5 = new System.Windows.Forms.RadioButton();
            this.radioBJ3to2 = new System.Windows.Forms.RadioButton();
            this.inPenetration = new System.Windows.Forms.TextBox();
            this.txtPenetration = new System.Windows.Forms.Label();
            this.inDecksUsed = new System.Windows.Forms.TextBox();
            this.txtDecksUsed = new System.Windows.Forms.Label();
            this.grboxBetDev = new System.Windows.Forms.GroupBox();
            this.inToleranceBet = new System.Windows.Forms.TextBox();
            this.txtToleranceBet = new System.Windows.Forms.Label();
            this.checkOverbetNotify = new System.Windows.Forms.CheckBox();
            this.checkBetNotify = new System.Windows.Forms.CheckBox();
            this.grboxPlayDev = new System.Windows.Forms.GroupBox();
            this.inTolerancePlay = new System.Windows.Forms.TextBox();
            this.txtTolerancePlay = new System.Windows.Forms.Label();
            this.checkDev_BS_Notify = new System.Windows.Forms.CheckBox();
            this.checkDevPlayNotify = new System.Windows.Forms.CheckBox();
            this.grboxBasicStrat = new System.Windows.Forms.GroupBox();
            this.checkBasicStratNotify = new System.Windows.Forms.CheckBox();
            this.cmdCloseSettings = new System.Windows.Forms.Button();
            this.txtSettings = new System.Windows.Forms.Label();
            this.grboxCharts.SuspendLayout();
            this.grboxSettings.SuspendLayout();
            this.grboxPlayerData.SuspendLayout();
            this.grboxBetData.SuspendLayout();
            this.grboxRules.SuspendLayout();
            this.grboxBJPay.SuspendLayout();
            this.grboxBetDev.SuspendLayout();
            this.grboxPlayDev.SuspendLayout();
            this.grboxBasicStrat.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Modern No. 20", 14.25F);
            this.cmdStart.Location = new System.Drawing.Point(12, 12);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(114, 34);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start Game";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.Font = new System.Drawing.Font("Modern No. 20", 14.25F);
            this.cmdExit.Location = new System.Drawing.Point(12, 204);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(114, 34);
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdSettings
            // 
            this.cmdSettings.Font = new System.Drawing.Font("Modern No. 20", 14.25F);
            this.cmdSettings.Location = new System.Drawing.Point(12, 154);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(114, 34);
            this.cmdSettings.TabIndex = 2;
            this.cmdSettings.Text = "Settings";
            this.cmdSettings.UseVisualStyleBackColor = true;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // cmdShowChart
            // 
            this.cmdShowChart.Font = new System.Drawing.Font("Modern No. 20", 14.25F);
            this.cmdShowChart.Location = new System.Drawing.Point(12, 101);
            this.cmdShowChart.Name = "cmdShowChart";
            this.cmdShowChart.Size = new System.Drawing.Size(114, 34);
            this.cmdShowChart.TabIndex = 4;
            this.cmdShowChart.Text = "Charts";
            this.cmdShowChart.UseVisualStyleBackColor = true;
            this.cmdShowChart.Click += new System.EventHandler(this.cmdShowChart_Click);
            // 
            // grboxCharts
            // 
            this.grboxCharts.Controls.Add(this.comboChartSelect);
            this.grboxCharts.Controls.Add(this.tableHard);
            this.grboxCharts.Controls.Add(this.cmdCloseCharts);
            this.grboxCharts.Controls.Add(this.txtCharts);
            this.grboxCharts.Location = new System.Drawing.Point(621, 15);
            this.grboxCharts.Name = "grboxCharts";
            this.grboxCharts.Size = new System.Drawing.Size(648, 579);
            this.grboxCharts.TabIndex = 5;
            this.grboxCharts.TabStop = false;
            this.grboxCharts.Visible = false;
            // 
            // comboChartSelect
            // 
            this.comboChartSelect.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboChartSelect.FormattingEnabled = true;
            this.comboChartSelect.Items.AddRange(new object[] {
            "Basic Strategy Hard",
            "Basic Strategy Soft",
            "Basic Strategy Split",
            "Basic Strategy Surrender"});
            this.comboChartSelect.Location = new System.Drawing.Point(15, 65);
            this.comboChartSelect.Name = "comboChartSelect";
            this.comboChartSelect.Size = new System.Drawing.Size(284, 29);
            this.comboChartSelect.TabIndex = 9;
            this.comboChartSelect.SelectedIndexChanged += new System.EventHandler(this.comboChartSelect_SelectedIndexChanged);
            // 
            // tableHard
            // 
            this.tableHard.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableHard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableHard.ColumnCount = 11;
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.091817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090817F));
            this.tableHard.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableHard.Location = new System.Drawing.Point(15, 100);
            this.tableHard.Name = "tableHard";
            this.tableHard.RowCount = 22;
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545454F));
            this.tableHard.Size = new System.Drawing.Size(618, 464);
            this.tableHard.TabIndex = 8;
            this.tableHard.Paint += new System.Windows.Forms.PaintEventHandler(this.tableHard_Paint);
            // 
            // cmdCloseCharts
            // 
            this.cmdCloseCharts.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold);
            this.cmdCloseCharts.Location = new System.Drawing.Point(576, 16);
            this.cmdCloseCharts.Name = "cmdCloseCharts";
            this.cmdCloseCharts.Size = new System.Drawing.Size(63, 30);
            this.cmdCloseCharts.TabIndex = 7;
            this.cmdCloseCharts.Text = "Close";
            this.cmdCloseCharts.UseVisualStyleBackColor = true;
            this.cmdCloseCharts.Click += new System.EventHandler(this.cmdCloseCharts_Click);
            // 
            // txtCharts
            // 
            this.txtCharts.AutoSize = true;
            this.txtCharts.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharts.Location = new System.Drawing.Point(6, 16);
            this.txtCharts.Name = "txtCharts";
            this.txtCharts.Size = new System.Drawing.Size(97, 29);
            this.txtCharts.TabIndex = 6;
            this.txtCharts.Text = "Charts:";
            // 
            // grboxSettings
            // 
            this.grboxSettings.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grboxSettings.Controls.Add(this.grboxPlayerData);
            this.grboxSettings.Controls.Add(this.grboxBetData);
            this.grboxSettings.Controls.Add(this.grboxRules);
            this.grboxSettings.Controls.Add(this.grboxBetDev);
            this.grboxSettings.Controls.Add(this.grboxPlayDev);
            this.grboxSettings.Controls.Add(this.grboxBasicStrat);
            this.grboxSettings.Controls.Add(this.cmdCloseSettings);
            this.grboxSettings.Controls.Add(this.txtSettings);
            this.grboxSettings.Location = new System.Drawing.Point(36, 31);
            this.grboxSettings.Name = "grboxSettings";
            this.grboxSettings.Size = new System.Drawing.Size(872, 466);
            this.grboxSettings.TabIndex = 6;
            this.grboxSettings.TabStop = false;
            this.grboxSettings.Visible = false;
            // 
            // grboxPlayerData
            // 
            this.grboxPlayerData.Controls.Add(this.cmdResetEVD);
            this.grboxPlayerData.Controls.Add(this.inChipsChange);
            this.grboxPlayerData.Controls.Add(this.txtChips);
            this.grboxPlayerData.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxPlayerData.Location = new System.Drawing.Point(11, 389);
            this.grboxPlayerData.Name = "grboxPlayerData";
            this.grboxPlayerData.Size = new System.Drawing.Size(850, 66);
            this.grboxPlayerData.TabIndex = 19;
            this.grboxPlayerData.TabStop = false;
            this.grboxPlayerData.Text = "Player Data";
            // 
            // cmdResetEVD
            // 
            this.cmdResetEVD.Location = new System.Drawing.Point(456, 27);
            this.cmdResetEVD.Name = "cmdResetEVD";
            this.cmdResetEVD.Size = new System.Drawing.Size(164, 30);
            this.cmdResetEVD.TabIndex = 21;
            this.cmdResetEVD.Text = "Reset EV Data";
            this.cmdResetEVD.UseVisualStyleBackColor = true;
            this.cmdResetEVD.Click += new System.EventHandler(this.cmdResetEVD_Click);
            // 
            // inChipsChange
            // 
            this.inChipsChange.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.inChipsChange.Location = new System.Drawing.Point(269, 27);
            this.inChipsChange.Name = "inChipsChange";
            this.inChipsChange.Size = new System.Drawing.Size(134, 30);
            this.inChipsChange.TabIndex = 20;
            this.inChipsChange.Tag = "";
            // 
            // txtChips
            // 
            this.txtChips.AutoSize = true;
            this.txtChips.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtChips.Location = new System.Drawing.Point(11, 30);
            this.txtChips.Name = "txtChips";
            this.txtChips.Size = new System.Drawing.Size(102, 24);
            this.txtChips.TabIndex = 19;
            this.txtChips.Text = "Bankroll:";
            // 
            // grboxBetData
            // 
            this.grboxBetData.Controls.Add(this.inBettingUnits);
            this.grboxBetData.Controls.Add(this.txtBettingUnits);
            this.grboxBetData.Controls.Add(this.inBetSpread);
            this.grboxBetData.Controls.Add(this.txtBetSpread);
            this.grboxBetData.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxBetData.Location = new System.Drawing.Point(452, 275);
            this.grboxBetData.Name = "grboxBetData";
            this.grboxBetData.Size = new System.Drawing.Size(409, 108);
            this.grboxBetData.TabIndex = 14;
            this.grboxBetData.TabStop = false;
            this.grboxBetData.Text = "Betting Data";
            // 
            // inBettingUnits
            // 
            this.inBettingUnits.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.inBettingUnits.Location = new System.Drawing.Point(269, 27);
            this.inBettingUnits.Name = "inBettingUnits";
            this.inBettingUnits.Size = new System.Drawing.Size(134, 30);
            this.inBettingUnits.TabIndex = 18;
            this.inBettingUnits.Tag = "";
            this.inBettingUnits.TextChanged += new System.EventHandler(this.inBettingUnits_TextChanged);
            // 
            // txtBettingUnits
            // 
            this.txtBettingUnits.AutoSize = true;
            this.txtBettingUnits.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtBettingUnits.Location = new System.Drawing.Point(11, 30);
            this.txtBettingUnits.Name = "txtBettingUnits";
            this.txtBettingUnits.Size = new System.Drawing.Size(197, 24);
            this.txtBettingUnits.TabIndex = 17;
            this.txtBettingUnits.Text = "Betting Unit Value:";
            // 
            // inBetSpread
            // 
            this.inBetSpread.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.inBetSpread.Location = new System.Drawing.Point(269, 63);
            this.inBetSpread.Name = "inBetSpread";
            this.inBetSpread.Size = new System.Drawing.Size(134, 30);
            this.inBetSpread.TabIndex = 13;
            this.inBetSpread.Tag = "";
            this.inBetSpread.TextChanged += new System.EventHandler(this.inBetSpread_TextChanged);
            // 
            // txtBetSpread
            // 
            this.txtBetSpread.AutoSize = true;
            this.txtBetSpread.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtBetSpread.Location = new System.Drawing.Point(11, 66);
            this.txtBetSpread.Name = "txtBetSpread";
            this.txtBetSpread.Size = new System.Drawing.Size(118, 24);
            this.txtBetSpread.TabIndex = 12;
            this.txtBetSpread.Text = "Bet Spread:";
            // 
            // grboxRules
            // 
            this.grboxRules.Controls.Add(this.checkDHitS17);
            this.grboxRules.Controls.Add(this.grboxBJPay);
            this.grboxRules.Controls.Add(this.inPenetration);
            this.grboxRules.Controls.Add(this.txtPenetration);
            this.grboxRules.Controls.Add(this.inDecksUsed);
            this.grboxRules.Controls.Add(this.txtDecksUsed);
            this.grboxRules.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxRules.Location = new System.Drawing.Point(452, 51);
            this.grboxRules.Name = "grboxRules";
            this.grboxRules.Size = new System.Drawing.Size(409, 218);
            this.grboxRules.TabIndex = 13;
            this.grboxRules.TabStop = false;
            this.grboxRules.Text = "Game Rules";
            // 
            // checkDHitS17
            // 
            this.checkDHitS17.AutoSize = true;
            this.checkDHitS17.Location = new System.Drawing.Point(15, 177);
            this.checkDHitS17.Name = "checkDHitS17";
            this.checkDHitS17.Size = new System.Drawing.Size(236, 28);
            this.checkDHitS17.TabIndex = 14;
            this.checkDHitS17.Text = "Dealer hits on Soft 17";
            this.checkDHitS17.UseVisualStyleBackColor = true;
            this.checkDHitS17.CheckedChanged += new System.EventHandler(this.checkDHitS17_CheckedChanged);
            // 
            // grboxBJPay
            // 
            this.grboxBJPay.Controls.Add(this.radioBJ1to1);
            this.grboxBJPay.Controls.Add(this.radioBJ6to5);
            this.grboxBJPay.Controls.Add(this.radioBJ3to2);
            this.grboxBJPay.Location = new System.Drawing.Point(15, 101);
            this.grboxBJPay.Name = "grboxBJPay";
            this.grboxBJPay.Size = new System.Drawing.Size(388, 70);
            this.grboxBJPay.TabIndex = 12;
            this.grboxBJPay.TabStop = false;
            this.grboxBJPay.Text = "Blackjack/Natural Pay";
            // 
            // radioBJ1to1
            // 
            this.radioBJ1to1.AutoSize = true;
            this.radioBJ1to1.Location = new System.Drawing.Point(257, 32);
            this.radioBJ1to1.Name = "radioBJ1to1";
            this.radioBJ1to1.Size = new System.Drawing.Size(81, 28);
            this.radioBJ1to1.TabIndex = 2;
            this.radioBJ1to1.TabStop = true;
            this.radioBJ1to1.Text = "1 to 1";
            this.radioBJ1to1.UseVisualStyleBackColor = true;
            this.radioBJ1to1.CheckedChanged += new System.EventHandler(this.radioBJ1to1_CheckedChanged);
            // 
            // radioBJ6to5
            // 
            this.radioBJ6to5.AutoSize = true;
            this.radioBJ6to5.Location = new System.Drawing.Point(137, 32);
            this.radioBJ6to5.Name = "radioBJ6to5";
            this.radioBJ6to5.Size = new System.Drawing.Size(81, 28);
            this.radioBJ6to5.TabIndex = 1;
            this.radioBJ6to5.TabStop = true;
            this.radioBJ6to5.Text = "6 to 5";
            this.radioBJ6to5.UseVisualStyleBackColor = true;
            this.radioBJ6to5.CheckedChanged += new System.EventHandler(this.radioBJ6to5_CheckedChanged);
            // 
            // radioBJ3to2
            // 
            this.radioBJ3to2.AutoSize = true;
            this.radioBJ3to2.Location = new System.Drawing.Point(17, 32);
            this.radioBJ3to2.Name = "radioBJ3to2";
            this.radioBJ3to2.Size = new System.Drawing.Size(81, 28);
            this.radioBJ3to2.TabIndex = 0;
            this.radioBJ3to2.TabStop = true;
            this.radioBJ3to2.Text = "3 to 2";
            this.radioBJ3to2.UseVisualStyleBackColor = true;
            this.radioBJ3to2.CheckedChanged += new System.EventHandler(this.radioBJ3to2_CheckedChanged);
            // 
            // inPenetration
            // 
            this.inPenetration.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.inPenetration.Location = new System.Drawing.Point(269, 63);
            this.inPenetration.Name = "inPenetration";
            this.inPenetration.Size = new System.Drawing.Size(134, 30);
            this.inPenetration.TabIndex = 11;
            this.inPenetration.Tag = "";
            // 
            // txtPenetration
            // 
            this.txtPenetration.AutoSize = true;
            this.txtPenetration.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtPenetration.Location = new System.Drawing.Point(11, 66);
            this.txtPenetration.Name = "txtPenetration";
            this.txtPenetration.Size = new System.Drawing.Size(224, 24);
            this.txtPenetration.TabIndex = 10;
            this.txtPenetration.Text = "Penetration (in decks):";
            // 
            // inDecksUsed
            // 
            this.inDecksUsed.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.inDecksUsed.Location = new System.Drawing.Point(269, 27);
            this.inDecksUsed.Name = "inDecksUsed";
            this.inDecksUsed.Size = new System.Drawing.Size(134, 30);
            this.inDecksUsed.TabIndex = 9;
            this.inDecksUsed.Tag = "";
            // 
            // txtDecksUsed
            // 
            this.txtDecksUsed.AutoSize = true;
            this.txtDecksUsed.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtDecksUsed.Location = new System.Drawing.Point(11, 30);
            this.txtDecksUsed.Name = "txtDecksUsed";
            this.txtDecksUsed.Size = new System.Drawing.Size(121, 24);
            this.txtDecksUsed.TabIndex = 8;
            this.txtDecksUsed.Text = "Decks Used:";
            // 
            // grboxBetDev
            // 
            this.grboxBetDev.Controls.Add(this.inToleranceBet);
            this.grboxBetDev.Controls.Add(this.txtToleranceBet);
            this.grboxBetDev.Controls.Add(this.checkOverbetNotify);
            this.grboxBetDev.Controls.Add(this.checkBetNotify);
            this.grboxBetDev.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxBetDev.Location = new System.Drawing.Point(11, 256);
            this.grboxBetDev.Name = "grboxBetDev";
            this.grboxBetDev.Size = new System.Drawing.Size(409, 127);
            this.grboxBetDev.TabIndex = 12;
            this.grboxBetDev.TabStop = false;
            this.grboxBetDev.Text = "Betting Deviations";
            // 
            // inToleranceBet
            // 
            this.inToleranceBet.Enabled = false;
            this.inToleranceBet.Location = new System.Drawing.Point(269, 91);
            this.inToleranceBet.Name = "inToleranceBet";
            this.inToleranceBet.Size = new System.Drawing.Size(134, 30);
            this.inToleranceBet.TabIndex = 7;
            this.inToleranceBet.Text = "0.5";
            this.inToleranceBet.TextChanged += new System.EventHandler(this.inToleranceBet_TextChanged);
            // 
            // txtToleranceBet
            // 
            this.txtToleranceBet.AutoSize = true;
            this.txtToleranceBet.Enabled = false;
            this.txtToleranceBet.Location = new System.Drawing.Point(11, 94);
            this.txtToleranceBet.Name = "txtToleranceBet";
            this.txtToleranceBet.Size = new System.Drawing.Size(218, 24);
            this.txtToleranceBet.TabIndex = 6;
            this.txtToleranceBet.Text = "True Count Tolerance:";
            // 
            // checkOverbetNotify
            // 
            this.checkOverbetNotify.AutoSize = true;
            this.checkOverbetNotify.Location = new System.Drawing.Point(15, 63);
            this.checkOverbetNotify.Name = "checkOverbetNotify";
            this.checkOverbetNotify.Size = new System.Drawing.Size(357, 28);
            this.checkOverbetNotify.TabIndex = 5;
            this.checkOverbetNotify.Text = "Notify Overbetting due to Bankroll";
            this.checkOverbetNotify.UseVisualStyleBackColor = true;
            this.checkOverbetNotify.CheckedChanged += new System.EventHandler(this.checkOverbetNotify_CheckedChanged);
            // 
            // checkBetNotify
            // 
            this.checkBetNotify.AutoSize = true;
            this.checkBetNotify.Location = new System.Drawing.Point(15, 29);
            this.checkBetNotify.Name = "checkBetNotify";
            this.checkBetNotify.Size = new System.Drawing.Size(251, 28);
            this.checkBetNotify.TabIndex = 4;
            this.checkBetNotify.Text = "Notify Betting mistakes";
            this.checkBetNotify.UseVisualStyleBackColor = true;
            this.checkBetNotify.CheckedChanged += new System.EventHandler(this.checkBetNotify_CheckedChanged);
            // 
            // grboxPlayDev
            // 
            this.grboxPlayDev.Controls.Add(this.inTolerancePlay);
            this.grboxPlayDev.Controls.Add(this.txtTolerancePlay);
            this.grboxPlayDev.Controls.Add(this.checkDev_BS_Notify);
            this.grboxPlayDev.Controls.Add(this.checkDevPlayNotify);
            this.grboxPlayDev.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxPlayDev.Location = new System.Drawing.Point(11, 123);
            this.grboxPlayDev.Name = "grboxPlayDev";
            this.grboxPlayDev.Size = new System.Drawing.Size(409, 127);
            this.grboxPlayDev.TabIndex = 11;
            this.grboxPlayDev.TabStop = false;
            this.grboxPlayDev.Text = "Playing Deviations";
            // 
            // inTolerancePlay
            // 
            this.inTolerancePlay.Enabled = false;
            this.inTolerancePlay.Location = new System.Drawing.Point(269, 91);
            this.inTolerancePlay.Name = "inTolerancePlay";
            this.inTolerancePlay.Size = new System.Drawing.Size(134, 30);
            this.inTolerancePlay.TabIndex = 3;
            this.inTolerancePlay.Text = "0.5";
            this.inTolerancePlay.TextChanged += new System.EventHandler(this.inTolerancePlay_TextChanged);
            // 
            // txtTolerancePlay
            // 
            this.txtTolerancePlay.AutoSize = true;
            this.txtTolerancePlay.Enabled = false;
            this.txtTolerancePlay.Location = new System.Drawing.Point(11, 94);
            this.txtTolerancePlay.Name = "txtTolerancePlay";
            this.txtTolerancePlay.Size = new System.Drawing.Size(218, 24);
            this.txtTolerancePlay.TabIndex = 2;
            this.txtTolerancePlay.Text = "True Count Tolerance:";
            // 
            // checkDev_BS_Notify
            // 
            this.checkDev_BS_Notify.AutoSize = true;
            this.checkDev_BS_Notify.Enabled = false;
            this.checkDev_BS_Notify.Location = new System.Drawing.Point(15, 63);
            this.checkDev_BS_Notify.Name = "checkDev_BS_Notify";
            this.checkDev_BS_Notify.Size = new System.Drawing.Size(348, 28);
            this.checkDev_BS_Notify.TabIndex = 1;
            this.checkDev_BS_Notify.Text = "Notify when Basic Strat is correct";
            this.checkDev_BS_Notify.UseVisualStyleBackColor = true;
            this.checkDev_BS_Notify.CheckedChanged += new System.EventHandler(this.checkDev_BS_Notify_CheckedChanged);
            // 
            // checkDevPlayNotify
            // 
            this.checkDevPlayNotify.AutoSize = true;
            this.checkDevPlayNotify.Location = new System.Drawing.Point(15, 29);
            this.checkDevPlayNotify.Name = "checkDevPlayNotify";
            this.checkDevPlayNotify.Size = new System.Drawing.Size(273, 28);
            this.checkDevPlayNotify.TabIndex = 0;
            this.checkDevPlayNotify.Text = "Notify Deviation mistakes";
            this.checkDevPlayNotify.UseVisualStyleBackColor = true;
            this.checkDevPlayNotify.CheckedChanged += new System.EventHandler(this.checkDevPlayNotify_CheckedChanged);
            // 
            // grboxBasicStrat
            // 
            this.grboxBasicStrat.Controls.Add(this.checkBasicStratNotify);
            this.grboxBasicStrat.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold);
            this.grboxBasicStrat.Location = new System.Drawing.Point(11, 51);
            this.grboxBasicStrat.Name = "grboxBasicStrat";
            this.grboxBasicStrat.Size = new System.Drawing.Size(409, 66);
            this.grboxBasicStrat.TabIndex = 10;
            this.grboxBasicStrat.TabStop = false;
            this.grboxBasicStrat.Text = "Basic Strategy";
            // 
            // checkBasicStratNotify
            // 
            this.checkBasicStratNotify.AutoSize = true;
            this.checkBasicStratNotify.Location = new System.Drawing.Point(15, 29);
            this.checkBasicStratNotify.Name = "checkBasicStratNotify";
            this.checkBasicStratNotify.Size = new System.Drawing.Size(316, 28);
            this.checkBasicStratNotify.TabIndex = 0;
            this.checkBasicStratNotify.Text = "Notify Basic Strategy mistakes";
            this.checkBasicStratNotify.UseVisualStyleBackColor = true;
            this.checkBasicStratNotify.CheckedChanged += new System.EventHandler(this.checkBasicStratNotify_CheckedChanged);
            // 
            // cmdCloseSettings
            // 
            this.cmdCloseSettings.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold);
            this.cmdCloseSettings.Location = new System.Drawing.Point(798, 16);
            this.cmdCloseSettings.Name = "cmdCloseSettings";
            this.cmdCloseSettings.Size = new System.Drawing.Size(63, 30);
            this.cmdCloseSettings.TabIndex = 5;
            this.cmdCloseSettings.Text = "Close";
            this.cmdCloseSettings.UseVisualStyleBackColor = true;
            this.cmdCloseSettings.Click += new System.EventHandler(this.cmdCloseSettings_Click);
            // 
            // txtSettings
            // 
            this.txtSettings.AutoSize = true;
            this.txtSettings.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSettings.Location = new System.Drawing.Point(6, 16);
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.Size = new System.Drawing.Size(114, 29);
            this.txtSettings.TabIndex = 0;
            this.txtSettings.Text = "Settings:";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1393, 627);
            this.Controls.Add(this.grboxSettings);
            this.Controls.Add(this.grboxCharts);
            this.Controls.Add(this.cmdShowChart);
            this.Controls.Add(this.cmdSettings);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.grboxCharts.ResumeLayout(false);
            this.grboxCharts.PerformLayout();
            this.grboxSettings.ResumeLayout(false);
            this.grboxSettings.PerformLayout();
            this.grboxPlayerData.ResumeLayout(false);
            this.grboxPlayerData.PerformLayout();
            this.grboxBetData.ResumeLayout(false);
            this.grboxBetData.PerformLayout();
            this.grboxRules.ResumeLayout(false);
            this.grboxRules.PerformLayout();
            this.grboxBJPay.ResumeLayout(false);
            this.grboxBJPay.PerformLayout();
            this.grboxBetDev.ResumeLayout(false);
            this.grboxBetDev.PerformLayout();
            this.grboxPlayDev.ResumeLayout(false);
            this.grboxPlayDev.PerformLayout();
            this.grboxBasicStrat.ResumeLayout(false);
            this.grboxBasicStrat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdSettings;
        private System.Windows.Forms.Button cmdShowChart;
        private System.Windows.Forms.GroupBox grboxCharts;
        private System.Windows.Forms.Button cmdCloseCharts;
        private System.Windows.Forms.Label txtCharts;
        private System.Windows.Forms.TableLayoutPanel tableHard;
        private System.Windows.Forms.GroupBox grboxSettings;
        private System.Windows.Forms.Button cmdCloseSettings;
        private System.Windows.Forms.Label txtSettings;
        private System.Windows.Forms.ComboBox comboChartSelect;
        private System.Windows.Forms.GroupBox grboxBetDev;
        private System.Windows.Forms.GroupBox grboxPlayDev;
        private System.Windows.Forms.GroupBox grboxBasicStrat;
        private System.Windows.Forms.GroupBox grboxRules;
        private System.Windows.Forms.TextBox inDecksUsed;
        private System.Windows.Forms.Label txtDecksUsed;
        private System.Windows.Forms.TextBox inToleranceBet;
        private System.Windows.Forms.Label txtToleranceBet;
        private System.Windows.Forms.CheckBox checkOverbetNotify;
        private System.Windows.Forms.CheckBox checkBetNotify;
        private System.Windows.Forms.TextBox inTolerancePlay;
        private System.Windows.Forms.Label txtTolerancePlay;
        private System.Windows.Forms.CheckBox checkDev_BS_Notify;
        private System.Windows.Forms.CheckBox checkDevPlayNotify;
        private System.Windows.Forms.CheckBox checkBasicStratNotify;
        private System.Windows.Forms.GroupBox grboxBetData;
        private System.Windows.Forms.TextBox inBettingUnits;
        private System.Windows.Forms.Label txtBettingUnits;
        private System.Windows.Forms.TextBox inBetSpread;
        private System.Windows.Forms.Label txtBetSpread;
        private System.Windows.Forms.GroupBox grboxBJPay;
        private System.Windows.Forms.RadioButton radioBJ1to1;
        private System.Windows.Forms.RadioButton radioBJ6to5;
        private System.Windows.Forms.RadioButton radioBJ3to2;
        private System.Windows.Forms.TextBox inPenetration;
        private System.Windows.Forms.Label txtPenetration;
        private System.Windows.Forms.GroupBox grboxPlayerData;
        private System.Windows.Forms.TextBox inChipsChange;
        private System.Windows.Forms.Label txtChips;
        private System.Windows.Forms.Button cmdResetEVD;
        private System.Windows.Forms.CheckBox checkDHitS17;
    }
}