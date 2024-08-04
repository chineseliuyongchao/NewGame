namespace Fight.Utils
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}