using System.Collections.Generic;

namespace PremierLeague.Data
{
    public class DataSource
    {
        public DataSource()
        {
            Items = new List<DataItem>();
            Items.Add(new DataItem("Arsenal", 6, 4));
            Items.Add(new DataItem("Aston Villa", 18, 10));
            Items.Add(new DataItem("Chelsea", 4, 5));
            Items.Add(new DataItem("Everton", 5, 8));
            Items.Add(new DataItem("Fulham", 10, 12));
            Items.Add(new DataItem("Liverpool", 11, 6));
            Items.Add(new DataItem("Man City", 2, 1));
            Items.Add(new DataItem("Man Utd", 1, 2));
            Items.Add(new DataItem("Newcastle", 14, 7));
            Items.Add(new DataItem("Norwich", 13, 14));
            Items.Add(new DataItem("QPR", 20, 13));
            Items.Add(new DataItem("Reading", 19, 18));
            Items.Add(new DataItem("Southampton", 17, 17));
            Items.Add(new DataItem("Stoke", 12, 11));
            Items.Add(new DataItem("Sunderland", 16, 9));
            Items.Add(new DataItem("Swansea", 9, 20));
            Items.Add(new DataItem("Tottenham", 7, 3));
            Items.Add(new DataItem("West Ham", 8, 15));
            Items.Add(new DataItem("Wigan", 15, 16));
            Items.Add(new DataItem("West Brom", 3, 19));
        }

        public ICollection<DataItem> Items { get; private set; }
    }
}
