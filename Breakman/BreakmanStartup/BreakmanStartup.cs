namespace BreakmanStartup
{
    using System;
    using System.Windows.Forms;
    using Breakman;

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
            BreakmanForm = new Breakman();

            BreakmanForm.Show();

            Hide();            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
