namespace Breakman
{
    using Properties;
    using System;
    using System.Drawing;

    [Serializable]
    public class KillingObject :  FallingObject
    {
        private const float KillingObjectVelocity = 10;

        public KillingObject(int x, int y, Graphics canvas) : base(x, y, canvas, KillingObjectVelocity)
        {                 
            Paint();
        }

        public void Paint()
        {
            Image image = Resources.Sword;

            Canvas.DrawImage(image, X, Y, Width, Height);          
        }
    }
}
