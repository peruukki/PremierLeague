using System.Collections.Generic;

namespace PremierLeague.Data
{
    public class DataSource
    {
        public DataSource()
        {
            Items = new List<DataItem>();
            Items.Add(new DataItem("Arsenal"));
            Items.Add(new DataItem("Liverpool"));
            Items.Add(new DataItem("Tottenham"));
        }

        public ICollection<DataItem> Items { get; private set; }
    }
}
