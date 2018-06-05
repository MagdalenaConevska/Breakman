namespace Breakman
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class Ball
    {
        public int R { get; set; }

        private double Angle { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public float Velocity { get; set; }

        private float velocityX;
        private float velocityY;

        public Ball(int x = 400, int y = 460)
        {
            X = x;
            Y = y;
            R = 20;
            Angle = Math.PI / 4;
            Velocity = 20;
            velocityX = (float)Math.Cos(Angle) * Velocity;
            velocityY = (float)Math.Sin(Angle) * Velocity;
        }

        public void Move(int formWidth, int formHeight, Hero hero,
                         List<BrickBase> bricks, Graphics g, Timer timer)
        {
            float nextX = X + velocityX;
            float nextY = Y + velocityY;

            if (CheckSideBorders(nextX, formWidth))
            {
                velocityX = -velocityX;
            }
            if (CheckUpBorder(nextY) || IsCatchedByTheHero(nextX, nextY, hero))
            {
                velocityY = -velocityY;
            }
            if (CheckDownBorder(nextY, formHeight) && !IsCatchedByTheHero(nextX, nextY, hero))
            {
                timer.Stop();
                timer.Dispose();
                DialogResult gameOver = MessageBox.Show("Game over", "", MessageBoxButtons.OKCancel);
            }

            RemoveTouchedBricks(g, bricks, nextX, nextY);

            X += (int)velocityX;
            Y += (int)velocityY;
        }

        private void RemoveTouchedBricks(Graphics g, List<BrickBase> bricks, float nextX, float nextY)
        {
            List<BrickBase> touchedBricks = GetTouchedBricksByTheBall(bricks, nextX, nextY);

            if (touchedBricks.Any())
            {
                foreach (BrickBase brick in touchedBricks)
                {
                    brick.ClearBrick(g);

                    bricks.Remove(brick);
                }

                velocityY = -velocityY;
            }
        }

        private List<BrickBase> GetTouchedBricksByTheBall(List<BrickBase> bricks, float nextX, float nextY)
        {
            return bricks.Where(f => (f.Y + f.Height) >= nextY && nextX >= f.X && nextX <= f.X + f.Width).ToList();
        }

        private bool CheckDownBorder(float nextY, int formHeight)
        {
            return nextY + R >= formHeight;
        }

        private bool CheckUpBorder(float nextY)
        {
            return nextY <= 0;
        }

        private bool CheckSideBorders(float nextX, int formWidth)
        {
            return nextX - R <= 0 || nextX + R >= formWidth;
        }

        private bool IsCatchedByTheHero(float nextX, float nextY, Hero hero)
        {
            return nextY + 2 * (R - 5) >= hero.Y && nextX >= hero.X && nextX <= hero.X + hero.Width;
        }

        public void Paint(Graphics g)
        {
            Brush brush = new SolidBrush(Color.CornflowerBlue);

            g.FillEllipse(brush, X, Y, 2 * R, 2 * R);

            brush.Dispose();
        }

        public void ClearBall(Graphics g)
        {
            Brush brush = new SolidBrush(Color.White);

            g.FillEllipse(brush, X, Y, R * 2, R * 2);

            brush.Dispose();
        }
    }
}
