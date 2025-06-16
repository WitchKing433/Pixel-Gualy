using GualyCore;
using Interpreter;
using System;
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
            if (dataGridView1.Rows.Count > 0 && saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                SaveAs();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog1.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                outputPath = Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = Path.GetFileName(openFileDialog1.FileName);
                string[] linesOfCode = File.ReadAllLines(openFileDialog1.FileName);
                dataGridView1.SuspendLayout();
                dataGridView1.Rows.Clear();
                for (int iline = 0; iline < linesOfCode.Length; iline++)
                {
                    dataGridView1.Rows.Add((iline + 1).ToString(), linesOfCode[iline]);
                }
                dataGridView1.ResumeLayout();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.Columns["linea"].ReadOnly = true;
            dataGridView1.Columns["linea"].Width = 60;
            dataGridView1.Columns["linea"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.RowsAdded += (s, e) => UpdateLineNumbers();
            dataGridView1.RowsRemoved += (s, e) => UpdateLineNumbers();


            canvasSize = Math.Min(pictureBox1.Width, pictureBox1.Height);

            saveFileDialog1 = new SaveFileDialog();
        }

        void UpdateLineNumbers()
        {
            for (int iline = 0; iline < dataGridView1.Rows.Count; iline++)
            {
                if (dataGridView1.Rows[iline].IsNewRow)
                    continue;
                dataGridView1.Rows[iline].Cells["Linea"].Value = (iline + 1).ToString();
            }
        }

        private void ejecutarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = string.Join("\n",
                dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(fila => !fila.IsNewRow)
                    .Select(fila => fila.Cells[1].Value?.ToString() ?? ""));

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
                pictureBox1.Visible = true;
                textBoxErrors.Visible = false;
                pictureBox1.ImageLocation = outputFullPathFileName;
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
                        UpdateLineNumbers();
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
                dataGridView1.CurrentCell = dataGridView1.Rows[insertIndex].Cells[1];
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
            fileName = "Annonimous.pw";
            dataGridView1.Rows.Clear();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && saveFileDialog1.FileName != string.Empty && saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                SaveAs();
            }
        }

        void SaveAs()
        {
            var stringBuilder = new StringBuilder();
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                if (dataGridViewRow.IsNewRow)
                    continue;
                var valueCell = dataGridViewRow.Cells[1].Value;
                string instruction = valueCell?.ToString() ?? string.Empty;
                if (instruction != null)
                {
                    stringBuilder.AppendLine(instruction);
                }
            }

            File.WriteAllText(saveFileDialog1.FileName, stringBuilder.ToString());
            fileName = Path.GetFileName(saveFileDialog1.FileName);
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
            dataGridView1.Rows.Clear();
            fileName = "NewPixelGualy";
            outputPath = Path.GetDirectoryName(Application.ExecutablePath);
            pictureBox1.Image = null;
            textBoxErrors.Text = string.Empty;
            saveFileDialog1.FileName = string.Empty;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guardarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 &&
                (fileName != "NewPixelGualy" ||
                (fileName == "NewPixelGualy" && saveFileDialog1.ShowDialog(this) == DialogResult.OK)))
            {
                SaveAs();
            }
        }

        private void iniciarDepuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = string.Join("\n",
                dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(fila => !fila.IsNewRow)
                    .Select(fila => fila.Cells[1].Value?.ToString() ?? ""));

            interpreterDebugger = new();
            string outputFullPathFileName = interpreterDebugger.Compile(code, outputPath, fileName);
            if (outputFullPathFileName == "")
            {
                outputFullPathFileName = interpreterDebugger.PaintBegin(canvasSize, outputPath, fileName);
                if (outputFullPathFileName == "") {
                    if (interpreterDebugger.Index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows[interpreterDebugger.Index].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[interpreterDebugger.Index].Cells[1];
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
                dataGridView1.CurrentCell = dataGridView1.Rows[interpreterDebugger.Index].Cells[1];
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
    }
}
