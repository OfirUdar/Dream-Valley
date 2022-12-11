namespace Udar
{

    public interface ILoadManager
    {
        public void Load(ILoadable loadable);
        public bool TryLoad(ILoadable loadable);
        public string[] LoadFoldersPaths(string path);

    }
}
