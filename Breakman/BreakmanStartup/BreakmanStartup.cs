namespace BreakmanStartup
{
    using System;
    using System.Windows.Forms;
    using Breakman;
    using System.IO;

    public partial class BreakmanStartup : Form
    {
        public Breakman BreakmanForm { get; set; }

        private Timer CheckBreakmanForm { get; set; }

        public BreakmanStartup()
        {
            InitializeComponent();

            CheckBreakmanForm = new Timer();
            CheckBreakmanForm.Enabled = true;
            CheckBreakmanForm.Interval = 1000;
            CheckBreakmanForm.Tick += CheckBreakmanForm_Tick;
        }

        private void CheckBreakmanForm_Tick(object sender, EventArgs e)
        {
            if (BreakmanForm != null && BreakmanForm.IsClosed)
            {
                Close();
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            BreakmanForm = new Breakman(false);

            BreakmanForm.Show();

            Hide();            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnContinueSavedGame_Click(object sender, EventArgs e)
        {
            if (File.Exists("bricks.bin"))
            {
                BreakmanForm = new Breakman(true);

                BreakmanForm.Show();

                Hide();
            }
            else
            {
                MessageBox.Show("No saved game present!", "No saved game present!", MessageBoxButtons.OK);
            }
        }
    }
}
