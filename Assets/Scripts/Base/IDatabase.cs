namespace Base
{
    public interface IDatabase
    {
        void Save();
        void Load();
        T GetData<T>(string key);
        
        
    }
}