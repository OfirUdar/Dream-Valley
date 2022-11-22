namespace Udar
{
    public interface ISaveManager
    {
        public void Save(ISaveable saveable);
        public bool TrySave(ISaveable saveable);
    }
}
