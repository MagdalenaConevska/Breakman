namespace Breakman
{
    #region UsingStatements

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
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
        public int Points { get; set; } = 0;

        private Timer BallTimer { get; set; }
        private Timer FallingObjectsTimer { get; set; }

        #endregion

        #region FormCtors

        public Breakman(bool openSavedGame)
        {
            InitializeComponent();

            if (openSavedGame)
            {
                OpenSavedGame();
            }
            else
            {
                StartGame(1);
            }
        }

        public Breakman()
        {
            InitializeComponent();
        }

        #endregion

        #region GlobalGameHelperMethods

        private void StartGame(int level)
        {
            Level = level;

            NumberOfRows = 2 * Level + 1;

            Hero = new Hero();

            Ball = new Ball(Hero);

            Bricks = new List<BrickBase>();

            InitBricks();

            Invalidate();

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

        public void OpenSavedGame()
        {
            List<BrickBase> savedBricks = null;
            Hero savedHero = null;
            Ball savedBall = null;

            using (FileStream str = File.OpenRead("bricks.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                savedBricks = (List<BrickBase>)bf.Deserialize(str);
            }
            using (FileStream str = File.OpenRead("hero.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                savedHero = (Hero)bf.Deserialize(str);
            }
            using (FileStream str = File.OpenRead("ball.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                savedBall = (Ball)bf.Deserialize(str);
            }
            using (FileStream str = File.OpenRead("points.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Points = (int)bf.Deserialize(str);
            }

            if (File.Exists("killingobject.bin"))
            {
                using (FileStream str = File.OpenRead("killingobject.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    KillingObject = (KillingObject)bf.Deserialize(str);
                    KillingObject.Canvas = CreateGraphics();
                }
            }
            if (File.Exists("speedingobject.bin"))
            {
                using (FileStream str = File.OpenRead("speedingobject.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    SpeedingObject = (SpeedingObject)bf.Deserialize(str);
                    SpeedingObject.Canvas = CreateGraphics();
                }
            }

            Hero = savedHero;

            Ball = savedBall;

            Bricks = new List<BrickBase>();

            savedBricks.ForEach(f => Bricks.Add(f));

            Invalidate();

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

        private void SaveGame()
        {
            if (File.Exists("killingobject.bin"))
            {
                File.Delete("killingobject.bin");
            }

            if (File.Exists("speedingobject.bin"))
            {
                File.Delete("speedingobject.bin");
            }

            using (FileStream stream = File.Create("bricks.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, Bricks);
            }

            using (FileStream stream = File.Create("hero.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, Hero);
            }

            using (FileStream stream = File.Create("ball.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, Ball);
            }

            using (FileStream stream = File.Create("points.bin"))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, Points);
            }

            if (KillingObject != null)
            {
                using (FileStream stream = File.Create("killingobject.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(stream, KillingObject);
                }
            }
            if (SpeedingObject != null)
            {
                using (FileStream stream = File.Create("speedingobject.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(stream, SpeedingObject);
                }
            }
        }

        private void GameOver()
        {
            StopTimers();

            DialogResult gameOver = MessageBox.Show("You lost!", "Game Over!");

            CheckNewRecord();

            IsClosed = true;

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

                CheckLeftBricksNumber();

                ReleaseFallingObjects(g, removedBricks);

                lblPoints.Text = $"{Points}";

                Ball.Paint(g);
            }
            catch
            {
                if (!IsClosed)
                {
                    GameOver();
                }
            }
        }

        private void CheckLeftBricksNumber()
        {
            if (!Bricks.Any())
            {
                if (Level == 1)
                {
                    StopTimers();

                    DialogResult levelFinished = MessageBox.Show("Click OK to continue on next level.", "Level finished!", MessageBoxButtons.OK);

                    KillingObject = null;
                    SpeedingObject = null;

                    StartGame(2);
                }
                else
                {
                    StopTimers();

                    DialogResult gameWin = MessageBox.Show("YOU WIN!", "YOU WIN!", MessageBoxButtons.OK);

                    CheckNewRecord();

                    IsClosed = true;

                    Close();
                }
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

            if (Level == 1)
            {
                Bricks.RemoveAt(3);
                Bricks.Add(new GreenBrick(3 * (BrickWidth + BetweenBrickDistance),
                                                         0 * (BrickHeight + BetweenBrickDistance)));

                Bricks.RemoveAt(numberOfBricksInOneRow * NumberOfRows - 4);
                Bricks.Add(new RedBrick(5 * (BrickWidth + BetweenBrickDistance),
                                                   (NumberOfRows - 1) * (BrickHeight + BetweenBrickDistance)));
            }
            else if (Level == 2)
            {
                Bricks.RemoveAt(2);
                Bricks.Add(new RedBrick(2 * (BrickWidth + BetweenBrickDistance),
                                                         0 * (BrickHeight + BetweenBrickDistance)));

                Bricks.RemoveAt(numberOfBricksInOneRow * NumberOfRows - 3);
                Bricks.Add(new GreenBrick((numberOfBricksInOneRow - 2) * (BrickWidth + BetweenBrickDistance),
                                                   (NumberOfRows - 1) * (BrickHeight + BetweenBrickDistance)));
            }
        }

        private void ReleaseFallingObjects(Graphics g, List<BrickBase> removedBricks)
        {
            foreach (BrickBase brick in removedBricks)
            {
                Points++;

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

                Points += 5;

                SpeedingObject.Clear();

                SpeedingObject = null;
            }
            else
            {
                SpeedingObject.Paint();

                if (HasFallingObjectDisappeared(SpeedingObject))
                {
                    SpeedingObject.Clear();

                    SpeedingObject = null;
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

                Points -= 5;

                KillingObject.Paint();

                if (!IsClosed)
                {
                    GameOver();
                }
            }
            else
            {
                KillingObject.Paint();

                if (HasFallingObjectDisappeared(KillingObject))
                {
                    KillingObject.Clear();

                    KillingObject = null;
                }
            }
        }

        private void CheckNewRecord()
        {
            int savedPoints;

            if (File.Exists("points.bin"))
            {
                using (FileStream stream = File.OpenRead("points.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    savedPoints = (int)bf.Deserialize(stream);
                }

                if (Points > savedPoints)
                {
                    MessageBox.Show("You have made a new record!", "Congratulations!");

                    using (FileStream stream = File.Create("points.bin"))
                    {
                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(stream, Points);
                    }
                }
            }
            else
            {
                using (FileStream stream = File.Create("points.bin"))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(stream, Points);
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
            else if (e.KeyCode == Keys.S)
            {
                SaveGame();

                MessageBox.Show("Game has been saved!");
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
