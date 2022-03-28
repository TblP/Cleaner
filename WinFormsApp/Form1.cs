
namespace WinFormsApp
{
    public partial class Antimalware : Form
    {
        public Antimalware()
        {
            InitializeComponent();
            CenterToScreen(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var opd = new OpenFileDialog();
            opd.ValidateNames = false;
            opd.CheckFileExists = false;
            opd.CheckPathExists = true;
            opd.FileName = "Folder Selection.";
            if (opd.ShowDialog() == DialogResult.OK)
            {
                if (opd.FileName.Contains("."))
                {
                    textBox1.Text = opd.FileName;
                }
                else
                {
                    textBox1.Text = Path.GetDirectoryName(opd.FileName);
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ResultForm result = new ResultForm();
            result.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}