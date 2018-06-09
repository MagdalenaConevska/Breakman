namespace Breakman
{
    public static class SharedExtensions
    {
        public static bool IsEmpty(this FallingObject fallingObject)
        {
            return fallingObject == null;
        }
    }
}
