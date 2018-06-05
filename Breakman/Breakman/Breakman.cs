namespace Breakman
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Breakman : Form
    {
        private const int BrickWidth = 100;
        private const int BrickHeight = 25;
        private const int BetweenBrickDistance = 3;
        private int NumberOfRows = 3;

        public Hero Hero { get; set; }
        public Ball Ball { get; set; }
        public List<BrickBase> Bricks { get; set; }

        private Timer Timer { get; set; }

        public Breakman()
        {
            InitializeComponent();

            Hero = new Hero();

            Ball = new Ball();

            Bricks = new List<BrickBase>();

            InitBricks();

            Timer = new Timer();
            Timer.Tick += Timer_Tick;
            Timer.Enabled = true;
            Timer.Interval = 100;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            Ball.ClearBall(g);
            Ball.Move(Width, Height, Hero, Bricks, g, Timer);
            Ball.Paint(g);
        }

        private void InitBricks()
        {
            int numberOfBricksInOneRow = Width / BrickWidth;

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < numberOfBricksInOneRow; j++)
                {
                    ////add bricks in different colors
                    RedBrick newBrick = new RedBrick(j * (BrickWidth + BetweenBrickDistance),
                                                     i * (BrickHeight + BetweenBrickDistance));
                    Bricks.Add(newBrick);
                }
            }
        }

        private void Breakman_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();

            g.Clear(Color.White);

            Hero.Paint(g);

            Ball.Paint(g);

            foreach (BrickBase brick in Bricks)
            {
                brick.Paint(g);
            }
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
