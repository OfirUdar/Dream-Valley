namespace Udar
{
    public interface ISaveable
    {
        public string Path { get; }

        public string GetSerialized();
    }

}
