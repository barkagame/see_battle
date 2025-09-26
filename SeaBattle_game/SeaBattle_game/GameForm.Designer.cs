namespace SeaBattle_game
{
    partial class GameForm
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
            this.label_turn = new System.Windows.Forms.Label();
            this.label_score = new System.Windows.Forms.Label();
            this.textBox_countR = new System.Windows.Forms.TextBox();
            this.textBox_wait = new System.Windows.Forms.TextBox();
            this.textBox_p1name = new System.Windows.Forms.TextBox();
            this.textBox_p2name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_switchUser = new System.Windows.Forms.Button();
            this.label_stTurn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_turn
            // 
            this.label_turn.AutoSize = true;
            this.label_turn.Location = new System.Drawing.Point(25, 23);
            this.label_turn.Name = "label_turn";
            this.label_turn.Size = new System.Drawing.Size(10, 13);
            this.label_turn.TabIndex = 0;
            this.label_turn.Text = " ";
            // 
            // label_score
            // 
            this.label_score.AutoSize = true;
            this.label_score.Location = new System.Drawing.Point(25, 403);
            this.label_score.Name = "label_score";
            this.label_score.Size = new System.Drawing.Size(10, 13);
            this.label_score.TabIndex = 1;
            this.label_score.Text = " ";
            // 
            // textBox_countR
            // 
            this.textBox_countR.Location = new System.Drawing.Point(46, 413);
            this.textBox_countR.Name = "textBox_countR";
            this.textBox_countR.Size = new System.Drawing.Size(100, 20);
            this.textBox_countR.TabIndex = 2;
            this.textBox_countR.Text = "20";
            // 
            // textBox_wait
            // 
            this.textBox_wait.Location = new System.Drawing.Point(164, 413);
            this.textBox_wait.Name = "textBox_wait";
            this.textBox_wait.Size = new System.Drawing.Size(100, 20);
            this.textBox_wait.TabIndex = 3;
            this.textBox_wait.Text = "3";
            // 
            // textBox_p1name
            // 
            this.textBox_p1name.Location = new System.Drawing.Point(49, 23);
            this.textBox_p1name.Name = "textBox_p1name";
            this.textBox_p1name.Size = new System.Drawing.Size(100, 20);
            this.textBox_p1name.TabIndex = 4;
            this.textBox_p1name.Text = "Random";
            // 
            // textBox_p2name
            // 
            this.textBox_p2name.Location = new System.Drawing.Point(445, 23);
            this.textBox_p2name.Name = "textBox_p2name";
            this.textBox_p2name.Size = new System.Drawing.Size(100, 20);
            this.textBox_p2name.TabIndex = 5;
            this.textBox_p2name.Text = "Smart";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Кол-во раундов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Время на ход";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Имя игрока №1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(442, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Имя игрока №2";
            // 
            // button_switchUser
            // 
            this.button_switchUser.Location = new System.Drawing.Point(557, 410);
            this.button_switchUser.Name = "button_switchUser";
            this.button_switchUser.Size = new System.Drawing.Size(160, 23);
            this.button_switchUser.TabIndex = 10;
            this.button_switchUser.Text = "Передать расстановку";
            this.button_switchUser.UseVisualStyleBackColor = true;
            this.button_switchUser.Click += new System.EventHandler(this.button_switchUser_Click);
            // 
            // label_stTurn
            // 
            this.label_stTurn.AutoSize = true;
            this.label_stTurn.Location = new System.Drawing.Point(723, 416);
            this.label_stTurn.Name = "label_stTurn";
            this.label_stTurn.Size = new System.Drawing.Size(94, 13);
            this.label_stTurn.TabIndex = 11;
            this.label_stTurn.Text = "Ставит игрок №1";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 521);
            this.Controls.Add(this.label_stTurn);
            this.Controls.Add(this.button_switchUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_p2name);
            this.Controls.Add(this.textBox_p1name);
            this.Controls.Add(this.textBox_wait);
            this.Controls.Add(this.textBox_countR);
            this.Controls.Add(this.label_score);
            this.Controls.Add(this.label_turn);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_turned;
        private System.Windows.Forms.Label label_scorer;
        private System.Windows.Forms.Label label_turn;
        private System.Windows.Forms.Label label_score;
        private System.Windows.Forms.TextBox textBox_countR;
        private System.Windows.Forms.TextBox textBox_wait;
        private System.Windows.Forms.TextBox textBox_p1name;
        private System.Windows.Forms.TextBox textBox_p2name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_switchUser;
        private System.Windows.Forms.Label label_stTurn;
    }
}