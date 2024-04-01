namespace Base
{
    public interface IContainer
    {
        public void Resolve<T>(T component, string id = null) where T : class;
        public T GetComponent<T>(string id = null) where T : class;

    }
}