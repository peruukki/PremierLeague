using System.Collections.Generic;

namespace PremierLeague.Data
{
    public class DataSource
    {
        public DataSource()
        {
            Items = new List<DataItem>();
            Items.Add(new DataItem("Arsenal", 4));
            Items.Add(new DataItem("Aston Villa", 10));
            Items.Add(new DataItem("Chelsea", 5));
            Items.Add(new DataItem("Everton", 8));
            Items.Add(new DataItem("Fulham", 12));
            Items.Add(new DataItem("Liverpool", 6));
            Items.Add(new DataItem("Man City", 1));
            Items.Add(new DataItem("Man Utd", 2));
            Items.Add(new DataItem("Newcastle", 7));
            Items.Add(new DataItem("Norwich", 14));
            Items.Add(new DataItem("QPR", 13));
            Items.Add(new DataItem("Reading", 18));
            Items.Add(new DataItem("Southampton", 17));
            Items.Add(new DataItem("Stoke", 11));
            Items.Add(new DataItem("Sunderland", 9));
            Items.Add(new DataItem("Swansea", 20));
            Items.Add(new DataItem("Tottenham", 3));
            Items.Add(new DataItem("West Ham", 15));
            Items.Add(new DataItem("Wigan", 16));
            Items.Add(new DataItem("West Brom", 19));
        }

        public ICollection<DataItem> Items { get; private set; }
    }
}
