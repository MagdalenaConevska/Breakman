namespace Breakman
{
    using System;
    using System.Drawing;

    [Serializable]
    public class FallingObject
    {
        private const int BrickWidth = 50;
        private const int BrickHeight = 55;

        [NonSerialized]
        public Graphics Canvas;

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public float Velocity { get; set; }

        public FallingObject(int x, int y, Graphics canvas, float objectVelocity)
        {
            X = x;
            Y = y;
            Width = BrickWidth;
            Height = BrickHeight;
            Velocity = objectVelocity;
            Canvas = canvas;
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
    }
}
