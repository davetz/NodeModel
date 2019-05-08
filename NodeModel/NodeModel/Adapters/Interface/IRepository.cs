namespace NodeModel
{
    //************************************************
    // This should be refined so that the repository
    // is less intertwined with the NodeModel.Chef
    //************************************************
    public interface IRepository
    {
        string Name { get; }
        string FullName { get; }
        void Read(Chef chef);
        void Write(Chef chef);
    }
}
