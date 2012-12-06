using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PremierLeague.Data
{
    public class DataSource
    {
        public DataSource()
        {
            Teams = new List<DataItem>();
            Teams.Add(new DataItem("Arsenal", 4));
            Teams.Add(new DataItem("Aston Villa", 10));
            Teams.Add(new DataItem("Chelsea", 5));
            Teams.Add(new DataItem("Everton", 8));
            Teams.Add(new DataItem("Fulham", 12));
            Teams.Add(new DataItem("Liverpool", 6));
            Teams.Add(new DataItem("Man City", 1));
            Teams.Add(new DataItem("Man Utd", 2));
            Teams.Add(new DataItem("Newcastle", 7));
            Teams.Add(new DataItem("Norwich", 14));
            Teams.Add(new DataItem("QPR", 13));
            Teams.Add(new DataItem("Reading", 18));
            Teams.Add(new DataItem("Southampton", 17));
            Teams.Add(new DataItem("Stoke", 11));
            Teams.Add(new DataItem("Sunderland", 9));
            Teams.Add(new DataItem("Swansea", 20));
            Teams.Add(new DataItem("Tottenham", 3));
            Teams.Add(new DataItem("West Ham", 15));
            Teams.Add(new DataItem("Wigan", 16));
            Teams.Add(new DataItem("West Brom", 19));

            SetCurrentPositions();
        }

        public ICollection<DataItem> Teams { get; private set; }

        public IEnumerable<DataGroup> Groups
        {
            get
            {
                var result = from t in Teams
                             orderby t.PositionDifference descending
                             group t by GetGroupName(t.PositionDifference) into g
                             select new DataGroup(string.Format("{0} ({1})", g.Key, g.ToList().Count), g.ToList());
                return result;
            }
        }

        private string GetGroupName(int difference)
        {
            if (difference > 4) return "Fantastic season";
            else if (difference > 1) return "Exceeding expectations";
            else if (difference < -4) return "Nightmare season";
            else if (difference < -1) return "Disappointing";
            else return "Doing OK";
        }

        private void SetCurrentPositions()
        {
            string[] leagueTable =
                {
                    "Man Utd",
                    "Man City",
                    "Chelsea",
                    "Tottenham",
                    "West Brom",
                    "Everton",
                    "Swansea",
                    "West Ham",
                    "Stoke",
                    "Arsenal",
                    "Liverpool",
                    "Fulham",
                    "Norwich",
                    "Newcastle",
                    "Wigan",
                    "Aston Villa",
                    "Sunderland",
                    "Southampton",
                    "Reading",
                    "QPR"
                };

            var position = 1;
            foreach (var team in leagueTable)
            {
                var matchingItems = from item in Teams where item.Name.Equals(team) select item;
                Debug.Assert(matchingItems.ToList().Count == 1);
                foreach (var item in matchingItems) item.CurrentPosition = position++;
            }
        }
    }
}
