
namespace WinFormsApp
{
    public partial class ResultForm : Form
    {
        String ResInfoPath = @"Result/Log.txt";
        public ResultForm()
        {
            InitializeComponent();
            if (!File.Exists(ResInfoPath))
            {
                FileStream fs = File.Create(ResInfoPath);
            }
            
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Result",-2);
            ReadResult();
        }

        public async void ReadResult()
        {
            using (StreamReader reader = new StreamReader(ResInfoPath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    ListViewItem item;
                    item = new ListViewItem(line);
                    listView1.Items.Add(item);
                }
                
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Antimalware MainForm = new Antimalware();
            MainForm.Show();
        }
    }
}
