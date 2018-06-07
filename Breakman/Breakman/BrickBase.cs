namespace Breakman
{
    using Properties;
    using System.Drawing;

    public class BrickBase
    {
        private const int BrickWidth = 100;
        private const int BrickHeight = 25;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Image BrickBackground { get; set; }

        public BrickBase(int x, int y, Image brickBackground)
        {
            X = x;
            Y = y;
            BrickBackground = brickBackground;
            Width = BrickWidth;
            Height = BrickHeight;
        }

        public virtual void Paint(Graphics g)
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);

            Image redBrick = BrickBackground;

            g.DrawImage(redBrick, rectangle);
        }

        public void ClearBrick(Graphics g)
        {
            Rectangle rectagle = new Rectangle(X, Y, Width, Height);

            Brush brush = new SolidBrush(Color.White);

            g.FillRectangle(brush, rectagle);
        }
    }
}
