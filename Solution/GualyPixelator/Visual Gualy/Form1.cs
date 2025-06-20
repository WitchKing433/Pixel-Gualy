using GualyCore;
using Interpreter;
using System;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Gualy
{
    public partial class Form1 : Form
    {
        string outputPath = Path.GetDirectoryName(Application.ExecutablePath);
        string fileName = "NewPixelGualy";
        int canvasSize;
        Interpreter.Interpreter interpreterDebugger;

        public Form1()
        {
            InitializeComponent();
        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                return;
            fileName = saveFileDialog1.FileName;
            SaveAs();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ClearProject();
                outputPath = Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                string[] linesOfCode = File.ReadAllLines(openFileDialog1.FileName);
                dataGridView1.SuspendLayout();
                dataGridView1.Rows.Clear();
                for (int iline = 0; iline < linesOfCode.Length; iline++)
                {
                    dataGridView1.Rows.Add(linesOfCode[iline]);
                }
                dataGridView1.ResumeLayout();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.RowPostPaint -= DataGridView1_RowPostPaint;
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;


            canvasSize = 200;

            saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "Pixel Gualy Files (*.pw)|*.pw",
                DefaultExt = "pw",
                AddExtension = true,
                Title = "Guardar archivo Gualy"
            };
        }
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = (DataGridView)sender;
            string rowNumber = (e.RowIndex + 1).ToString();

            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                grid.RowHeadersWidth,
                e.RowBounds.Height);

            Size textSize = TextRenderer.MeasureText(rowNumber, grid.RowHeadersDefaultCellStyle.Font);
            if (grid.RowHeadersWidth < textSize.Width + 20)
                grid.RowHeadersWidth = textSize.Width + 20;

            TextRenderer.DrawText(
                e.Graphics,
                rowNumber,
                grid.RowHeadersDefaultCellStyle.Font,
                headerBounds,
                grid.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter
            );
        }

        private void ejecutarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = string.Join("\n",
                dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(fila => !fila.IsNewRow)
                    .Select(fila => fila.Cells[0].Value?.ToString() ?? ""));

            Interpreter.Interpreter interpreter = new();
            string outputFullPathFileName = interpreter.Compile(code, outputPath, fileName);
            if (outputFullPathFileName == "")
                outputFullPathFileName = interpreter.PaintAll(canvasSize, outputPath, fileName);
            CheckOutput(outputFullPathFileName);
        }

        private void CheckOutput(string outputFullPathFileName)
        {
            string extensionFile = Path.GetExtension(outputFullPathFileName);
            if (extensionFile == ".png")
            {
                DisplayImage(outputFullPathFileName);
            }
            else
            {
                textBoxErrors.Visible = true;
                pictureBox1.Visible = false;
                string[] errors = File.ReadAllLines(outputFullPathFileName);
                textBoxErrors.Text = string.Join(Environment.NewLine, errors);
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    for (int i = dataGridView1.SelectedRows.Count - 1; i > -0; i--)
                    {
                        if (!dataGridView1.SelectedRows[i].IsNewRow)
                        {
                            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                        }
                    }
                }
                else if (dataGridView1.CurrentCell != null)
                {
                    DataGridViewRow currentDataGridViewRow = dataGridView1.CurrentCell.OwningRow;
                    if (currentDataGridViewRow != null && !currentDataGridViewRow.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(currentDataGridViewRow);
                    }
                }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                if (dataGridView1.CurrentRow?.Index + 1 == dataGridView1.Rows.Count)
                {
                    return;
                }
                int insertIndex = dataGridView1.CurrentRow?.Index + 1 ?? dataGridView1.Rows.Count;
                dataGridView1.Rows.Insert(insertIndex);
                dataGridView1.CurrentCell = dataGridView1.Rows[insertIndex].Cells[0];
                dataGridView1.BeginEdit(true);
            }
            else if (e.KeyCode == Keys.F5)
            {
                ejecutarTodoToolStripMenuItem_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                iniciarDepuraciónToolStripMenuItem_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                siguienteToolStripMenuItem_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                detenerToolStripMenuItem_Click(sender, e);
            }
        }

        void NewGualy()
        {
            outputPath = Path.GetFullPath(Application.ExecutablePath);
            fileName = "";
            dataGridView1.Rows.Clear();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            if (string.IsNullOrEmpty(fileName))
            {
                if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                    return;
                fileName= saveFileDialog1.FileName;
            }

            SaveAs();
        }

        void SaveAs()
        {
            var stringBuilder = new StringBuilder();
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                if (dataGridViewRow.IsNewRow)
                    continue;
                var valueCell = dataGridViewRow.Cells[0].Value;
                string instruction = valueCell?.ToString() ?? string.Empty;
                if (instruction != null)
                {
                    stringBuilder.AppendLine(instruction);
                }
            }

            File.WriteAllText(fileName, stringBuilder.ToString());
        }

        private void lienzoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPreferencias formPreferencias = new FormPreferencias(canvasSize);
            if (formPreferencias.ShowDialog(this) == DialogResult.OK)
            {
                canvasSize = (int)formPreferencias.CanvasSize;
            }
        }

        private void guardarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (saveImageDialog.ShowDialog(this) == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveImageDialog.FileName);
                }
            }
        }

        private void reporteDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearProject();
        }

        void ClearProject()
        {
            dataGridView1.Rows.Clear();
            fileName = "";
            outputPath = Path.GetDirectoryName(Application.ExecutablePath);
            pictureBox1.Image = null;
            textBoxErrors.Text = string.Empty;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guardarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            if (string.IsNullOrEmpty(fileName)) {
                if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                    return;
                fileName = saveFileDialog1.FileName;
            }
            SaveAs();
        }

        private void iniciarDepuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = string.Join("\n",
                dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(fila => !fila.IsNewRow)
                    .Select(fila => fila.Cells[0].Value?.ToString() ?? ""));

            interpreterDebugger = new();
            string outputFullPathFileName = interpreterDebugger.Compile(code, outputPath, fileName);
            if (outputFullPathFileName == "")
            {
                outputFullPathFileName = interpreterDebugger.PaintBegin(canvasSize, outputPath, fileName);
                if (outputFullPathFileName == "")
                {
                    if (interpreterDebugger.Index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows[interpreterDebugger.Index].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[interpreterDebugger.Index].Cells[0];
                        dataGridView1.FirstDisplayedScrollingRowIndex = interpreterDebugger.Index;
                        //outputFullPathFileName = interpreterDebugger.PaintCurrent(outputPath, fileName);
                    }
                    return;
                }
            }
            CheckOutput(outputFullPathFileName);
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (interpreterDebugger == null)
                return;
            if (interpreterDebugger.Index < dataGridView1.Rows.Count)
            {
                string outputFullPathFileName = interpreterDebugger.PaintCurrent(outputPath, fileName);
                CheckOutput(outputFullPathFileName);
                dataGridView1.Rows[interpreterDebugger.Index].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[interpreterDebugger.Index].Cells[0];
                dataGridView1.FirstDisplayedScrollingRowIndex = interpreterDebugger.Index;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void detenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[interpreterDebugger.Index].Selected = false;
            interpreterDebugger = null;
        }

        private void cargarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog(this) != DialogResult.OK)
                return;

            string rutaOriginal = openImageDialog.FileName;
            string newCode = Interpreter.Interpreter.ImageToCode(rutaOriginal);
            using var sfd = new SaveFileDialog()
            {
                Filter = "Archivos de texto|*.pw",
                Title = "Guardar Código Generado"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;
            File.WriteAllText(sfd.FileName, newCode, Encoding.UTF8);
            DisplayImage(rutaOriginal);
        }

        private OpenFileDialog openImageDialog = new OpenFileDialog()
        {
            Filter = "Imágenes|*.png;*.jpg;*.jpeg;*.bmp",
            Title = "Seleccione una imagen"
        };

        private void DisplayImage(string filePath)
        {
            pictureBox1.Image?.Dispose();

            using var original = new Bitmap(filePath);

            int boxW = pictureBox1.Width;
            int boxH = pictureBox1.Height;
            float scale = Math.Min((float)boxW / original.Width,
                                   (float)boxH / original.Height);

            int newW = Math.Max(1, (int)(original.Width * scale));
            int newH = Math.Max(1, (int)(original.Height * scale));

            var finalBmp = new Bitmap(newW, newH, original.PixelFormat);
            using (var g = Graphics.FromImage(finalBmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;
                g.Clear(this.BackColor);
                g.DrawImage(original, 0, 0, newW, newH);
            }

            pictureBox1.Image = finalBmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Visible = true;
            textBoxErrors.Visible = false;
        }
    }
}
