namespace Calculator_Rohde_Nick
{
    partial class Calculator
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.display = new System.Windows.Forms.TextBox();
            this.seven = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.openP = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.one = new System.Windows.Forms.Button();
            this.zero = new System.Windows.Forms.Button();
            this.times = new System.Windows.Forms.Button();
            this.enter = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.closeP = new System.Windows.Forms.Button();
            this.exponentiate = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.period = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(26, 15);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(292, 20);
            this.display.TabIndex = 0;
            this.display.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // seven
            // 
            this.seven.Location = new System.Drawing.Point(25, 123);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(40, 40);
            this.seven.TabIndex = 1;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.digit7Press);
            // 
            // eight
            // 
            this.eight.Location = new System.Drawing.Point(90, 123);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(40, 40);
            this.eight.TabIndex = 2;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.digit8Press);
            this.eight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // nine
            // 
            this.nine.Location = new System.Drawing.Point(152, 123);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(40, 40);
            this.nine.TabIndex = 3;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.digit9Press);
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(216, 155);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(40, 40);
            this.plus.TabIndex = 4;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.opAddPress);
            // 
            // divide
            // 
            this.divide.Location = new System.Drawing.Point(216, 201);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(40, 40);
            this.divide.TabIndex = 5;
            this.divide.Text = "/";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.opDivPress);
            // 
            // four
            // 
            this.four.Location = new System.Drawing.Point(25, 181);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(40, 40);
            this.four.TabIndex = 6;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.digit4Press);
            // 
            // five
            // 
            this.five.Location = new System.Drawing.Point(90, 181);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(40, 40);
            this.five.TabIndex = 7;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.digit5Press);
            // 
            // six
            // 
            this.six.Location = new System.Drawing.Point(152, 181);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(40, 40);
            this.six.TabIndex = 8;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.digit6Press);
            // 
            // openP
            // 
            this.openP.Location = new System.Drawing.Point(216, 247);
            this.openP.Name = "openP";
            this.openP.Size = new System.Drawing.Size(40, 40);
            this.openP.TabIndex = 9;
            this.openP.Text = "(";
            this.openP.UseVisualStyleBackColor = true;
            this.openP.Click += new System.EventHandler(this.openParenPress);
            // 
            // three
            // 
            this.three.Location = new System.Drawing.Point(152, 238);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(40, 40);
            this.three.TabIndex = 10;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.digit3Press);
            // 
            // two
            // 
            this.two.Location = new System.Drawing.Point(90, 238);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(40, 40);
            this.two.TabIndex = 11;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.digit2Press);
            // 
            // one
            // 
            this.one.AccessibleName = "One Button";
            this.one.Location = new System.Drawing.Point(25, 238);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(40, 40);
            this.one.TabIndex = 12;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.digit1Press);
            // 
            // zero
            // 
            this.zero.Location = new System.Drawing.Point(25, 299);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(40, 40);
            this.zero.TabIndex = 13;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.digit0Press);
            // 
            // times
            // 
            this.times.Location = new System.Drawing.Point(273, 201);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(40, 40);
            this.times.TabIndex = 14;
            this.times.Text = "*";
            this.times.UseVisualStyleBackColor = true;
            this.times.Click += new System.EventHandler(this.opMulPress);
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(176, 299);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(80, 40);
            this.enter.TabIndex = 15;
            this.enter.Text = "Enter";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.verifyInput);
            this.enter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // minus
            // 
            this.minus.Location = new System.Drawing.Point(273, 155);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(40, 40);
            this.minus.TabIndex = 16;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.opSubPress);
            // 
            // closeP
            // 
            this.closeP.Location = new System.Drawing.Point(273, 247);
            this.closeP.Name = "closeP";
            this.closeP.Size = new System.Drawing.Size(40, 40);
            this.closeP.TabIndex = 17;
            this.closeP.Text = ")";
            this.closeP.UseVisualStyleBackColor = true;
            this.closeP.Click += new System.EventHandler(this.closeParenPress);
            // 
            // exponentiate
            // 
            this.exponentiate.Location = new System.Drawing.Point(245, 109);
            this.exponentiate.Name = "exponentiate";
            this.exponentiate.Size = new System.Drawing.Size(40, 40);
            this.exponentiate.TabIndex = 18;
            this.exponentiate.Text = "^";
            this.exponentiate.UseVisualStyleBackColor = true;
            this.exponentiate.Click += new System.EventHandler(this.opExpPress);
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(29, 43);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(53, 13);
            this.error.TabIndex = 19;
            this.error.Text = "error label";
            this.error.Visible = false;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.resultLabel.Location = new System.Drawing.Point(42, 63);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(43, 17);
            this.resultLabel.TabIndex = 20;
            this.resultLabel.Text = "result";
            this.resultLabel.Visible = false;
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(325, 4);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(80, 40);
            this.clear.TabIndex = 21;
            this.clear.Text = "Clear (Esc)";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clearPress);
            // 
            // period
            // 
            this.period.Location = new System.Drawing.Point(90, 299);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(40, 40);
            this.period.TabIndex = 22;
            this.period.Text = ".";
            this.period.UseVisualStyleBackColor = true;
            this.period.Click += new System.EventHandler(this.periodPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Operators:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Special Buttons:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(324, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 40);
            this.button2.TabIndex = 26;
            this.button2.Text = "Insert Result";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.insertAnswer);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(324, 181);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 40);
            this.button3.TabIndex = 27;
            this.button3.Text = "Insert Pi";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.insertPi);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(324, 238);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 40);
            this.button4.TabIndex = 28;
            this.button4.Text = "Insert e";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.insertE);
            // 
            // Calculator
            // 
            this.AcceptButton = this.enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 366);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.period);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.error);
            this.Controls.Add(this.exponentiate);
            this.Controls.Add(this.closeP);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.times);
            this.Controls.Add(this.zero);
            this.Controls.Add(this.one);
            this.Controls.Add(this.two);
            this.Controls.Add(this.three);
            this.Controls.Add(this.openP);
            this.Controls.Add(this.six);
            this.Controls.Add(this.five);
            this.Controls.Add(this.four);
            this.Controls.Add(this.divide);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.nine);
            this.Controls.Add(this.eight);
            this.Controls.Add(this.seven);
            this.Controls.Add(this.display);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Calculator";
            this.Text = "Calculator";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox display;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button divide;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button openP;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.Button times;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button closeP;
        private System.Windows.Forms.Button exponentiate;
        private System.Windows.Forms.Label error;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button period;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

