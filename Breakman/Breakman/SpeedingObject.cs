namespace Breakman
{
    using Properties;
    using System;
    using System.Drawing;

    [Serializable]
    public class SpeedingObject :  FallingObject
    {
        private const float SpeedingObjectVelocity = 10;

        public SpeedingObject(int x, int y, Graphics canvas) : base(x, y, canvas, SpeedingObjectVelocity)
        {
            Paint();
        }

        public void Paint()
        {
            Image image = Resources.SpeedUp;

            Canvas.DrawImage(image, X, Y, Width, Height);
        }
    }
}
