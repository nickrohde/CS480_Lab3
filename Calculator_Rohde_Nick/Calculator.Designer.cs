/* * * * * * * * * * * * * * * * * *
 * Programmer: Nick Rohde          *
 * Project   : Lab 3 - Calculator  *
 * Class     : CS 480_003          *
 * Instructor: Szilard Vajda       *
 * Date      : 2nd November 2017   *
 * * * * * * * * * * * * * * * * * */


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
            this.clear = new System.Windows.Forms.Button();
            this.period = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Enabled = false;
            this.display.Font = new System.Drawing.Font("Lucida Bright", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.display.Location = new System.Drawing.Point(12, 12);
            this.display.Name = "display";
            this.display.ReadOnly = true;
            this.display.Size = new System.Drawing.Size(392, 36);
            this.display.TabIndex = 0;
            this.display.Enter += new System.EventHandler(this.focusEnterKey);
            this.display.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // seven
            // 
            this.seven.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seven.Location = new System.Drawing.Point(12, 114);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(110, 58);
            this.seven.TabIndex = 1;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.digit7Press);
            this.seven.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // eight
            // 
            this.eight.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eight.Location = new System.Drawing.Point(128, 114);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(110, 58);
            this.eight.TabIndex = 2;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.digit8Press);
            this.eight.Enter += new System.EventHandler(this.focusEnterKey);
            this.eight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // nine
            // 
            this.nine.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nine.Location = new System.Drawing.Point(244, 114);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(110, 58);
            this.nine.TabIndex = 3;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.digit9Press);
            this.nine.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // plus
            // 
            this.plus.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plus.Location = new System.Drawing.Point(371, 114);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(120, 40);
            this.plus.TabIndex = 4;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.opAddPress);
            this.plus.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // divide
            // 
            this.divide.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divide.Location = new System.Drawing.Point(371, 240);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(120, 40);
            this.divide.TabIndex = 5;
            this.divide.Text = "/";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.opDivPress);
            this.divide.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // four
            // 
            this.four.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.four.Location = new System.Drawing.Point(12, 178);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(110, 58);
            this.four.TabIndex = 6;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.digit4Press);
            this.four.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // five
            // 
            this.five.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.five.Location = new System.Drawing.Point(128, 178);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(110, 58);
            this.five.TabIndex = 7;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.digit5Press);
            this.five.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // six
            // 
            this.six.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.six.Location = new System.Drawing.Point(244, 178);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(110, 58);
            this.six.TabIndex = 8;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.digit6Press);
            this.six.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // openP
            // 
            this.openP.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openP.Location = new System.Drawing.Point(371, 324);
            this.openP.Name = "openP";
            this.openP.Size = new System.Drawing.Size(60, 40);
            this.openP.TabIndex = 9;
            this.openP.Text = "(";
            this.openP.UseVisualStyleBackColor = true;
            this.openP.Click += new System.EventHandler(this.openParenPress);
            this.openP.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // three
            // 
            this.three.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.three.Location = new System.Drawing.Point(244, 242);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(110, 58);
            this.three.TabIndex = 10;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.digit3Press);
            this.three.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // two
            // 
            this.two.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.two.Location = new System.Drawing.Point(128, 242);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(110, 58);
            this.two.TabIndex = 11;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.digit2Press);
            this.two.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // one
            // 
            this.one.AccessibleName = "One Button";
            this.one.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.one.Location = new System.Drawing.Point(12, 242);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(110, 58);
            this.one.TabIndex = 12;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.digit1Press);
            this.one.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // zero
            // 
            this.zero.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zero.Location = new System.Drawing.Point(12, 306);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(226, 58);
            this.zero.TabIndex = 13;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.digit0Press);
            this.zero.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // times
            // 
            this.times.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.times.Location = new System.Drawing.Point(371, 198);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(120, 40);
            this.times.TabIndex = 14;
            this.times.Text = "*";
            this.times.UseVisualStyleBackColor = true;
            this.times.Click += new System.EventHandler(this.opMulPress);
            this.times.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // enter
            // 
            this.enter.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enter.Location = new System.Drawing.Point(410, 57);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(80, 38);
            this.enter.TabIndex = 15;
            this.enter.Text = "Enter";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.verifyInput);
            this.enter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyboardPress);
            // 
            // minus
            // 
            this.minus.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minus.Location = new System.Drawing.Point(371, 156);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(120, 40);
            this.minus.TabIndex = 16;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.opSubPress);
            this.minus.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // closeP
            // 
            this.closeP.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeP.Location = new System.Drawing.Point(431, 324);
            this.closeP.Name = "closeP";
            this.closeP.Size = new System.Drawing.Size(60, 40);
            this.closeP.TabIndex = 17;
            this.closeP.Text = ")";
            this.closeP.UseVisualStyleBackColor = true;
            this.closeP.Click += new System.EventHandler(this.closeParenPress);
            this.closeP.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // exponentiate
            // 
            this.exponentiate.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exponentiate.Location = new System.Drawing.Point(371, 282);
            this.exponentiate.Name = "exponentiate";
            this.exponentiate.Size = new System.Drawing.Size(120, 40);
            this.exponentiate.TabIndex = 18;
            this.exponentiate.Text = "^";
            this.exponentiate.UseVisualStyleBackColor = true;
            this.exponentiate.Click += new System.EventHandler(this.opExpPress);
            this.exponentiate.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // clear
            // 
            this.clear.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Bold);
            this.clear.Location = new System.Drawing.Point(410, 10);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(40, 38);
            this.clear.TabIndex = 30;
            this.clear.Text = "C";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clearPress);
            this.clear.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // period
            // 
            this.period.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.period.Location = new System.Drawing.Point(244, 306);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(110, 58);
            this.period.TabIndex = 22;
            this.period.Text = ".";
            this.period.UseVisualStyleBackColor = true;
            this.period.Click += new System.EventHandler(this.periodPress);
            this.period.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // resultLabel
            // 
            this.resultLabel.Enabled = false;
            this.resultLabel.Font = new System.Drawing.Font("Lucida Bright", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(12, 58);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.ReadOnly = true;
            this.resultLabel.Size = new System.Drawing.Size(392, 36);
            this.resultLabel.TabIndex = 29;
            this.resultLabel.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(450, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 38);
            this.button1.TabIndex = 30;
            this.button1.Text = "CE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.backspacePress);
            this.button1.Enter += new System.EventHandler(this.focusEnterKey);
            // 
            // Calculator
            // 
            this.AcceptButton = this.enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 377);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.period);
            this.Controls.Add(this.clear);
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
            this.Enter += new System.EventHandler(this.focusEnterKey);
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
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button period;
        private System.Windows.Forms.TextBox resultLabel;
        private System.Windows.Forms.Button button1;
    }
}

