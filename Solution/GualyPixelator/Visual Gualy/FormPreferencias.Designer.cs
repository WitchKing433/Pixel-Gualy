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
            ((System.ComponentModel.ISupportInitialize)numericUpDownCanvasSize).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(121, 57);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 1;
            label1.Text = "Tamaño del lienzo:";
            // 
            // numericUpDownCanvasSize
            // 
            numericUpDownCanvasSize.Location = new Point(260, 55);
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
            // FormPreferencias
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(475, 208);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numericUpDownCanvasSize);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FormPreferencias";
            Text = "Preferencias";
            ((System.ComponentModel.ISupportInitialize)numericUpDownCanvasSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown numericUpDownCanvasSize;
        private Button button1;
        private Button button2;
    }
}