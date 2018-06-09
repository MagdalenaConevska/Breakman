namespace Breakman
{
    using Properties;
    using System;

    [Serializable]
    public class NormalBrick : BrickBase
    {
        public NormalBrick(int x, int y) : base(x, y, Resources.NormalBrick)
        {

        }
    }
}
