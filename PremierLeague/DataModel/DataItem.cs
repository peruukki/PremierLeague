namespace PremierLeague.Data
{
    public class DataItem
    {
        public DataItem(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
