namespace BreakmanStartup
{
    using System;
    using System.Windows.Forms;
    using Breakman;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public partial class BreakmanStartup : Form
    {
        private Breakman BreakmanForm { get; set; }

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
                MessageBox.Show("No saved game present!");
            }
        }

        private void btnRecordScores_Click(object sender, EventArgs e)
        {
            int savedPoints;

            if (File.Exists("points.bin"))
            {
                using (FileStream stream = File.OpenRead("points.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    savedPoints = (int)bf.Deserialize(stream);

                    MessageBox.Show($"Current best score is: {savedPoints}");
                }
            }
            else
            {
                MessageBox.Show("No record score made yet.");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Breakman is a game where you have to break the bricks above the hero, with his ball. \n" +
                                          "This is a simple edition of the game and it consists only of two levels. \n" + 
                                          "Move -> and <- keys on the keyboard in order to move your hero. \n"+
                                          "If you don't catch your ball when falling down, you lose the game. \n" +
                                          "Be careful with the falling sword, it kills you. \n" + 
                                          "Catch the speeding clock because it speeds you up. \n" + 
                                          "Press S key in order to save the game in the current state, so you can continue playing it later. \n"+
                                          "ENJOY!"), "About Breakman");
        }

        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btnNewGame_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
