
using System.IO.Compression;

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
        string Path2;
        string temppath = @"temp";
        ZipArchive archive;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(".zip"))
            {
                Path2 = textBox1.Text;
                string destFile = Path.Combine(temppath,"test.zip");
                File.Copy(Path2, destFile);
                ZipChecker(destFile);
            }
        }
        
        public void ZipChecker(string path)
        {
            try
            {
                archive = ZipFile.OpenRead(path);

            }catch (Exception ex)
            {
                return;
            }

            foreach (var entry in archive.Entries)
            {
                MessageBox.Show(entry.FullName);
                if (entry.FullName.Contains(".zip"))
                {
                    string fullPath = Path.Combine(temppath, entry.FullName);
                    if (String.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else
                    {
                        entry.ExtractToFile(fullPath);
                    }
                }
                else
                {
                    var stream = entry.Open();
                    var reader = new StreamReader(stream);  
                    MessageBox.Show(reader.ReadToEnd());
                    //функцию проверки файла
                }
            }
            archive.Dispose();
            File.Delete(path);
            if (Directory.EnumerateFiles(temppath, "*.*", SearchOption.AllDirectories).Any())
            {
                string[] fileNames = Directory.GetFiles(temppath, "*.*", SearchOption.AllDirectories);
                foreach (var fileName in fileNames)
                {
                    MessageBox.Show(fileName);
                    ZipChecker(Path.GetFullPath(fileName)); 
                }
            }
        }
    }
}