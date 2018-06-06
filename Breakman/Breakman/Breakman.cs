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
        public KillingObject KillingObject { get; set; }

        private Timer BallTimer { get; set; }
        private Timer FallingObjectsTimer { get; set; }

        public Breakman()
        {
            InitializeComponent();

            Hero = new Hero();

            Ball = new Ball(Hero);

            Bricks = new List<BrickBase>();

            InitBricks();

            BallTimer = new Timer();
            BallTimer.Tick += BallTimer_Tick;
            BallTimer.Enabled = true;
            BallTimer.Interval = 100;
            BallTimer.Start();

            FallingObjectsTimer = new Timer();
            FallingObjectsTimer.Tick += FallingObjectTimer_Tick;
            FallingObjectsTimer.Enabled = true;
            FallingObjectsTimer.Interval = 100;
            FallingObjectsTimer.Start();
        }

        private void FallingObjectTimer_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            HandleKillingObjectMovement(g);
        }

        private void HandleKillingObjectMovement(Graphics g)
        {
            if (KillingObject != null)
            {
                KillingObject.Clear();

                KillingObject.Move();

                if (Hero.Y <= KillingObject.Y + KillingObject.Height && KillingObject.X + KillingObject.Width / 2 >= Hero.X && KillingObject.X <= Hero.X + Hero.Width)
                {
                    KillingObject.Y -= (int)KillingObject.Velocity;

                    KillingObject.Paint();

                    GameOver();
                }
                else
                {
                    KillingObject.Paint();

                    if (KillingObject.Y + KillingObject.Height >= g.ClipBounds.Height)
                    {
                        KillingObject.Clear();
                    }
                }
            }
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            Ball.ClearBall(g);

            List<BrickBase> removedBricks = Ball.Move(Width, Height, Hero, Bricks, g, BallTimer);

            ReleaseFallingObjects(g, removedBricks);

            Ball.Paint(g);
        }

        private void ReleaseFallingObjects(Graphics g, List<BrickBase> removedBricks)
        {
            foreach (BrickBase brick in removedBricks)
            {
                if (brick is RedBrick)
                {
                    KillingObject = new KillingObject(brick.X, brick.Y + brick.Height, g);
                }
            }
        }

        private void InitBricks()
        {
            int numberOfBricksInOneRow = Width / BrickWidth;

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < numberOfBricksInOneRow; j++)
                {
                    ////add bricks in different colors
                    NormalBrick newBrick = new NormalBrick(j * (BrickWidth + BetweenBrickDistance),
                                                     i * (BrickHeight + BetweenBrickDistance));
                    Bricks.Add(newBrick);
                }
            }

            Bricks.RemoveAt(2);
            Bricks.Add(new RedBrick(2 * (BrickWidth + BetweenBrickDistance),
                                                     0 * (BrickHeight + BetweenBrickDistance)));
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

        private void GameOver()
        {
            BallTimer.Stop();
            BallTimer.Dispose();
            FallingObjectsTimer.Stop();
            FallingObjectsTimer.Dispose();
            DialogResult gameOver = MessageBox.Show("Game over", "", MessageBoxButtons.OKCancel);
        }
    }
}
