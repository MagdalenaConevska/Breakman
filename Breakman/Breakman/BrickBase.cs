namespace Breakman
{
    using System.Drawing;

    public class BrickBase
    {
        private const int BrickWidth = 100;
        private const int BrickHeight = 25;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Color Color { get; set; }

        public BrickBase(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
            Width = BrickWidth;
            Height = BrickHeight;
        }

        public void Paint(Graphics g)
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);

            Brush brush = new SolidBrush(Color);

            g.FillRectangle(brush, rectangle);

            brush.Dispose();
        }

        public void ClearBrick(Graphics g)
        {
            Rectangle rectagle = new Rectangle(X, Y, Width, Height);

            Brush brush = new SolidBrush(Color.White);

            g.FillRectangle(brush, rectagle);
        }
    }
}
