namespace Breakman
{
    #region UsingStatements

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    #endregion

    public partial class Breakman : Form
    {
        #region HelperProps

        private const int BrickWidth = 100;
        private const int BrickHeight = 25;
        private const int BetweenBrickDistance = 3;
        private const int BallAndFallingObjectsTimerInterval = 100;

        private int NumberOfRows { get; set; }
        private int Level { get; set; }
        public bool IsClosed { get; set; } = false;

        #endregion

        #region FormProps

        public Hero Hero { get; set; }
        public Ball Ball { get; set; }
        public List<BrickBase> Bricks { get; set; }
        public KillingObject KillingObject { get; set; }
        public SpeedingObject SpeedingObject { get; set; }

        private Timer BallTimer { get; set; }
        private Timer FallingObjectsTimer { get; set; }

        #endregion

        #region FormCtors

        public Breakman()
        {
            InitializeComponent();

            StartGame(1);
        }

        #endregion

        #region GlobalGameHelperMethods

        private void StartGame(int level)
        {
            Level = level;

            NumberOfRows = 2 * Level;

            Hero = new Hero();

            Ball = new Ball(Hero);

            Bricks = new List<BrickBase>();

            InitBricks();

            BallTimer = new Timer();
            BallTimer.Tick += BallTimer_Tick;
            BallTimer.Enabled = true;
            BallTimer.Interval = BallAndFallingObjectsTimerInterval;
            BallTimer.Start();

            FallingObjectsTimer = new Timer();
            FallingObjectsTimer.Tick += FallingObjectTimer_Tick;
            FallingObjectsTimer.Enabled = true;
            FallingObjectsTimer.Interval = BallAndFallingObjectsTimerInterval;
            FallingObjectsTimer.Start();
        }

        private void GameOver()
        {
            StopTimers();
            DialogResult gameOver = MessageBox.Show("Sorry, you lost!", "Game Over!", MessageBoxButtons.OK);
            Close();
        }

        private void StopTimers()
        {
            BallTimer.Stop();
            BallTimer.Dispose();
            FallingObjectsTimer.Stop();
            FallingObjectsTimer.Dispose();
        }

        #endregion

        #region Timers

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            Ball.ClearBall(g);

            try
            {
                List<BrickBase> removedBricks = Ball.Move(Width, Height, Hero, Bricks, g, BallTimer);

                ReleaseFallingObjects(g, removedBricks);

                Ball.Paint(g);
            }
            catch
            {
                GameOver();
            }
        }

        private void FallingObjectTimer_Tick(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            HandleKillingObjectMovement(g);

            HandleSpeedingObjectMovement(g);
        }

        #endregion

        #region LogicHelperMethods

        private void InitBricks()
        {
            int numberOfBricksInOneRow = Width / BrickWidth;

            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < numberOfBricksInOneRow; j++)
                {
                    NormalBrick newBrick = new NormalBrick(j * (BrickWidth + BetweenBrickDistance),
                                                           i * (BrickHeight + BetweenBrickDistance));
                    Bricks.Add(newBrick);
                }
            }

            Bricks.RemoveAt(2);
            Bricks.Add(new RedBrick(2 * (BrickWidth + BetweenBrickDistance),
                                                     0 * (BrickHeight + BetweenBrickDistance)));

            Bricks.RemoveAt(numberOfBricksInOneRow * NumberOfRows - 2);
            Bricks.Add(new GreenBrick((numberOfBricksInOneRow - 1) * (BrickWidth + BetweenBrickDistance),
                                               (NumberOfRows - 1) * (BrickHeight + BetweenBrickDistance)));
        }

        private void ReleaseFallingObjects(Graphics g, List<BrickBase> removedBricks)
        {
            foreach (BrickBase brick in removedBricks)
            {
                if (brick is RedBrick)
                {
                    KillingObject = new KillingObject(brick.X, brick.Y + brick.Height, g);
                }
                else if (brick is GreenBrick)
                {
                    SpeedingObject = new SpeedingObject(brick.X, brick.Y + brick.Height, g);
                }
            }
        }

        private void HandleSpeedingObjectMovement(Graphics g)
        {
            if (SpeedingObject.IsEmpty())
            {
                return;
            }

            SpeedingObject.Clear();

            SpeedingObject.Move();

            if (IsHeroHittenByFallingObject(SpeedingObject))
            {
                SpeedingObject.Y -= (int)SpeedingObject.Velocity;

                SpeedingObject.Paint();

                SpeedUpHero();

                SpeedingObject.Clear();

                SpeedingObject.Invalidate();
            }
            else
            {
                SpeedingObject.Paint();

                if (HasFallingObjectDisappeared(SpeedingObject))
                {
                    SpeedingObject.Clear();

                    SpeedingObject.Invalidate();
                }
            }
        }

        private void HandleKillingObjectMovement(Graphics g)
        {
            if (KillingObject.IsEmpty())
            {
                return;
            }

            KillingObject.Clear();

            KillingObject.Move();

            if (IsHeroHittenByFallingObject(KillingObject))
            {
                KillingObject.Y -= (int)KillingObject.Velocity;

                KillingObject.Paint();

                GameOver();
            }
            else
            {
                KillingObject.Paint();

                if (HasFallingObjectDisappeared(KillingObject))
                {
                    KillingObject.Clear();

                    KillingObject.Invalidate();
                }
            }
        }

        private bool IsHeroHittenByFallingObject(FallingObject fallingObject)
        {
            return Hero.Y <= fallingObject.Y + fallingObject.Height && fallingObject.X + fallingObject.Width / 2 >= Hero.X && fallingObject.X <= Hero.X + Hero.Width;
        }

        private bool HasFallingObjectDisappeared(FallingObject fallingObject)
        {
            return fallingObject.Y + fallingObject.Height >= Height;
        }

        private void SpeedUpHero()
        {
            Hero.Velocity += 10;
        }

        #endregion

        #region FormEvents

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

        private void Breakman_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosed = true;

            StopTimers();
        }

        #endregion
    }
}
