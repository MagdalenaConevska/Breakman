namespace Breakman
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Breakman : Form
    {
        public Hero Hero { get; set; }

        public Breakman()
        {
            InitializeComponent();

            Hero = new Hero();
        }

        private void Breakman_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();

            Hero.Paint(g);
        }

        private void Breakman_KeyDown(object sender, KeyEventArgs e)
        {
            Graphics g = CreateGraphics();

            if (e.KeyCode == Keys.Left)
            {
                Hero.Move(Width, Direction.Left);
            }
            else if (e.KeyCode == Keys.Right)
            {
                Hero.Move(Width, Direction.Right);
            }

            Hero.Paint(g);
        }
    }
}
