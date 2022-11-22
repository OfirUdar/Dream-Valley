namespace Udar
{
    public interface ILoadable
    {
        public string Path { get; }

        public void SetSerialized(string data);
    }
}
