namespace Breakman
{
    using System.Drawing;

    public class Hero
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Velocity { get; set; }

        public Hero(int x = 350, int y = 500, int width = 150, int height = 30, int velocity = 10)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Velocity = velocity;
        }

        public void Paint(Graphics g)
        {
            Rectangle heroPath = new Rectangle(0, Y, (int)g.ClipBounds.Width, (int)g.ClipBounds.Height);

            Brush brushClear = new SolidBrush(Color.White);

            g.FillRectangle(brushClear, heroPath);

            Rectangle rectangle = new Rectangle(X, Y, Width, Height);

            Brush brush = new SolidBrush(Color.Blue);

            g.FillRectangle(brush, rectangle);

            brush.Dispose();
            brushClear.Dispose();
        }

        public void Move(int formWidth, Direction direction)
        {
            int nextX = X + Velocity;

            if (direction == Direction.Left)
            {
                nextX = X - Velocity;
            }

            if (nextX + Width >= formWidth - Velocity || nextX <= 0)
            {
                return;
            }

            X = nextX;
        }
    }
}
