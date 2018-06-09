namespace Breakman
{
    public static class SharedExtensions
    {
        public static bool IsEmpty(this FallingObject fallingObject)
        {
            return fallingObject == null;
        }

        public static void Invalidate(this FallingObject fallingObject)
        {
            fallingObject = null;
        }
    }
}
