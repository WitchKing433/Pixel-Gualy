namespace Visual_Gualy
{
    partial class FormPreferencias
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
            label1 = new Label();
            numericUpDownCanvasSize = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            numericUpDownImageScale = new NumericUpDown();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCanvasSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageScale).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(121, 35);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 1;
            label1.Text = "Tamaño del lienzo:";
            // 
            // numericUpDownCanvasSize
            // 
            numericUpDownCanvasSize.Location = new Point(260, 33);
            numericUpDownCanvasSize.Maximum = new decimal(new int[] { 2000000000, 0, 0, 0 });
            numericUpDownCanvasSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCanvasSize.Name = "numericUpDownCanvasSize";
            numericUpDownCanvasSize.Size = new Size(77, 27);
            numericUpDownCanvasSize.TabIndex = 2;
            numericUpDownCanvasSize.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new Point(72, 138);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "Aceptar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(294, 138);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 4;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // numericUpDownImageScale
            // 
            numericUpDownImageScale.Location = new Point(260, 81);
            numericUpDownImageScale.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownImageScale.Name = "numericUpDownImageScale";
            numericUpDownImageScale.Size = new Size(77, 27);
            numericUpDownImageScale.TabIndex = 5;
            numericUpDownImageScale.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(106, 83);
            label2.Name = "label2";
            label2.Size = new Size(148, 20);
            label2.TabIndex = 6;
            label2.Text = "Tamaño de escalado:";
            // 
            // FormPreferencias
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(475, 208);
            Controls.Add(label2);
            Controls.Add(numericUpDownImageScale);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numericUpDownCanvasSize);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormPreferencias";
            Text = "Preferencias";
            ((System.ComponentModel.ISupportInitialize)numericUpDownCanvasSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageScale).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown numericUpDownCanvasSize;
        private Button button1;
        private Button button2;
        private NumericUpDown numericUpDownImageScale;
        private Label label2;
    }
}