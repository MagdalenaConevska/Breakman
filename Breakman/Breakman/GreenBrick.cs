namespace Breakman
{
    using Properties;
    using System;

    [Serializable]
    public class GreenBrick : BrickBase
    {
        public GreenBrick(int x, int y) : base(x, y, Resources.GreenBrick)
        {

        }
    }
}
