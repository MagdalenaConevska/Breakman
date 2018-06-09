namespace Breakman
{
    using Properties;
    using System;

    [Serializable]
    public class RedBrick : BrickBase
    {
        public RedBrick(int x, int y) : base(x, y, Resources.RedBrick)
        {

        }
    }
}
