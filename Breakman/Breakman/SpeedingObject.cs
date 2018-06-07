namespace Breakman
{
    using Properties;
    using System.Drawing;

    public class SpeedingObject
    {
        private const int SpeedingObjectVelocity = 10;
        private const int BrickWidth = 50;
        private const int BrickHeight = 55;

        private Graphics Canvas;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public float Velocity { get; set; }

        public SpeedingObject(int x, int y, Graphics canvas)
        {
            X = x;
            Y = y;
            Width = BrickWidth;
            Height = BrickHeight;
            Velocity = SpeedingObjectVelocity;
            Canvas = canvas;
            Paint();
        }

        public void Move()
        {
            Y += (int)Velocity;
        }

        public void Clear()
        {
            Brush brush = new SolidBrush(Color.White);

            Canvas.FillRectangle(brush, X, Y, Width, Height);

            brush.Dispose();
        }

        public void Paint()
        {
            Image image = Resources.SpeedUp;

            Canvas.DrawImage(image, X, Y, Width, Height);
        }
    }
}
