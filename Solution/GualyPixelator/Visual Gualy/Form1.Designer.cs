namespace Visual_Gualy
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem = new ToolStripMenuItem();
            cargarToolStripMenuItem = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            guardarToolStripMenuItem1 = new ToolStripMenuItem();
            guardarImagenToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            preferenciasToolStripMenuItem = new ToolStripMenuItem();
            lienzoToolStripMenuItem = new ToolStripMenuItem();
            ejecuciónToolStripMenuItem = new ToolStripMenuItem();
            ejecutarTodoToolStripMenuItem = new ToolStripMenuItem();
            depurarToolStripMenuItem = new ToolStripMenuItem();
            iniciarDepuraciónToolStripMenuItem = new ToolStripMenuItem();
            siguienteToolStripMenuItem = new ToolStripMenuItem();
            detenerToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            Linea = new DataGridViewTextBoxColumn();
            Codigo = new DataGridViewTextBoxColumn();
            textBoxErrors = new TextBox();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            saveImageDialog = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, preferenciasToolStripMenuItem, ejecuciónToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(968, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem, cargarToolStripMenuItem, guardarToolStripMenuItem, guardarToolStripMenuItem1, guardarImagenToolStripMenuItem, salirToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(73, 24);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            nuevoToolStripMenuItem.Size = new Size(208, 26);
            nuevoToolStripMenuItem.Text = "Nuevo";
            nuevoToolStripMenuItem.Click += nuevoToolStripMenuItem_Click;
            // 
            // cargarToolStripMenuItem
            // 
            cargarToolStripMenuItem.Name = "cargarToolStripMenuItem";
            cargarToolStripMenuItem.Size = new Size(208, 26);
            cargarToolStripMenuItem.Text = "Cargar...";
            cargarToolStripMenuItem.Click += cargarToolStripMenuItem_Click;
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(208, 26);
            guardarToolStripMenuItem.Text = "Guardar";
            guardarToolStripMenuItem.Click += guardarToolStripMenuItem_Click_1;
            // 
            // guardarToolStripMenuItem1
            // 
            guardarToolStripMenuItem1.Name = "guardarToolStripMenuItem1";
            guardarToolStripMenuItem1.Size = new Size(208, 26);
            guardarToolStripMenuItem1.Text = "Guardar como...";
            guardarToolStripMenuItem1.Click += guardarToolStripMenuItem1_Click;
            // 
            // guardarImagenToolStripMenuItem
            // 
            guardarImagenToolStripMenuItem.Name = "guardarImagenToolStripMenuItem";
            guardarImagenToolStripMenuItem.Size = new Size(208, 26);
            guardarImagenToolStripMenuItem.Text = "Guardar Imagen...";
            guardarImagenToolStripMenuItem.Click += guardarImagenToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(208, 26);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // preferenciasToolStripMenuItem
            // 
            preferenciasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lienzoToolStripMenuItem });
            preferenciasToolStripMenuItem.Name = "preferenciasToolStripMenuItem";
            preferenciasToolStripMenuItem.Size = new Size(103, 24);
            preferenciasToolStripMenuItem.Text = "Preferencias";
            // 
            // lienzoToolStripMenuItem
            // 
            lienzoToolStripMenuItem.Name = "lienzoToolStripMenuItem";
            lienzoToolStripMenuItem.Size = new Size(144, 26);
            lienzoToolStripMenuItem.Text = "Lienzo...";
            lienzoToolStripMenuItem.Click += lienzoToolStripMenuItem_Click;
            // 
            // ejecuciónToolStripMenuItem
            // 
            ejecuciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ejecutarTodoToolStripMenuItem, depurarToolStripMenuItem });
            ejecuciónToolStripMenuItem.Name = "ejecuciónToolStripMenuItem";
            ejecuciónToolStripMenuItem.Size = new Size(77, 24);
            ejecuciónToolStripMenuItem.Text = "Depurar";
            // 
            // ejecutarTodoToolStripMenuItem
            // 
            ejecutarTodoToolStripMenuItem.Name = "ejecutarTodoToolStripMenuItem";
            ejecutarTodoToolStripMenuItem.Size = new Size(181, 26);
            ejecutarTodoToolStripMenuItem.Text = "Ejecutar todo";
            ejecutarTodoToolStripMenuItem.Click += ejecutarTodoToolStripMenuItem_Click;
            // 
            // depurarToolStripMenuItem
            // 
            depurarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { iniciarDepuraciónToolStripMenuItem, siguienteToolStripMenuItem, detenerToolStripMenuItem });
            depurarToolStripMenuItem.Name = "depurarToolStripMenuItem";
            depurarToolStripMenuItem.Size = new Size(181, 26);
            depurarToolStripMenuItem.Text = "Depurar";
            // 
            // iniciarDepuraciónToolStripMenuItem
            // 
            iniciarDepuraciónToolStripMenuItem.Name = "iniciarDepuraciónToolStripMenuItem";
            iniciarDepuraciónToolStripMenuItem.ShortcutKeyDisplayString = "F6";
            iniciarDepuraciónToolStripMenuItem.Size = new Size(178, 26);
            iniciarDepuraciónToolStripMenuItem.Text = "Iniciar";
            iniciarDepuraciónToolStripMenuItem.Click += iniciarDepuraciónToolStripMenuItem_Click;
            // 
            // siguienteToolStripMenuItem
            // 
            siguienteToolStripMenuItem.Name = "siguienteToolStripMenuItem";
            siguienteToolStripMenuItem.ShortcutKeyDisplayString = "F7";
            siguienteToolStripMenuItem.Size = new Size(178, 26);
            siguienteToolStripMenuItem.Text = "Siguiente";
            siguienteToolStripMenuItem.Click += siguienteToolStripMenuItem_Click;
            // 
            // detenerToolStripMenuItem
            // 
            detenerToolStripMenuItem.Name = "detenerToolStripMenuItem";
            detenerToolStripMenuItem.ShortcutKeyDisplayString = "F8";
            detenerToolStripMenuItem.Size = new Size(178, 26);
            detenerToolStripMenuItem.Text = "Detener";
            detenerToolStripMenuItem.Click += detenerToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 28);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(textBoxErrors);
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Size = new Size(968, 538);
            splitContainer1.SplitterDistance = 321;
            splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Linea, Codigo });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(321, 538);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // Linea
            // 
            Linea.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Linea.HeaderText = "Linea";
            Linea.MinimumWidth = 6;
            Linea.Name = "Linea";
            Linea.ReadOnly = true;
            Linea.Resizable = DataGridViewTriState.False;
            Linea.SortMode = DataGridViewColumnSortMode.NotSortable;
            Linea.Width = 50;
            // 
            // Codigo
            // 
            Codigo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Codigo.HeaderText = "Code";
            Codigo.MinimumWidth = 6;
            Codigo.Name = "Codigo";
            Codigo.Resizable = DataGridViewTriState.False;
            Codigo.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // textBoxErrors
            // 
            textBoxErrors.Dock = DockStyle.Fill;
            textBoxErrors.Location = new Point(0, 0);
            textBoxErrors.Multiline = true;
            textBoxErrors.Name = "textBoxErrors";
            textBoxErrors.ReadOnly = true;
            textBoxErrors.Size = new Size(643, 538);
            textBoxErrors.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(643, 538);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "*.pw";
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Archivos  PixelWall-E(*.pw)|*.pw";
            openFileDialog1.Title = "Abrir archivo PixelWall-E";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "*.pw";
            saveFileDialog1.Filter = "Archivos PixelWall-E (*.pw)|*.pw";
            saveFileDialog1.Title = "Salvar archivo PixelWall-E";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // saveImageDialog
            // 
            saveImageDialog.DefaultExt = "*.png";
            saveImageDialog.Filter = "Archivo de imagen (*.png)|*.png";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 566);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Visual Gualy";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem nuevoToolStripMenuItem;
        private ToolStripMenuItem cargarToolStripMenuItem;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripMenuItem guardarToolStripMenuItem1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem preferenciasToolStripMenuItem;
        private ToolStripMenuItem lienzoToolStripMenuItem;
        private ToolStripMenuItem guardarImagenToolStripMenuItem;
        private ToolStripMenuItem ejecuciónToolStripMenuItem;
        private ToolStripMenuItem ejecutarTodoToolStripMenuItem;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private TextBox textBoxErrors;
        private SaveFileDialog saveFileDialog1;
        private ContextMenuStrip contextMenuStrip1;
        private SaveFileDialog saveImageDialog;
        private DataGridViewTextBoxColumn Linea;
        private DataGridViewTextBoxColumn Codigo;
        private ToolStripMenuItem depurarToolStripMenuItem;
        private ToolStripMenuItem iniciarDepuraciónToolStripMenuItem;
        private ToolStripMenuItem siguienteToolStripMenuItem;
        private ToolStripMenuItem detenerToolStripMenuItem;
    }
}
