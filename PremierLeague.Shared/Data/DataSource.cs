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
            Teams.Add(new DataItem("Arsenal", 2, 4));
            Teams.Add(new DataItem("Aston Villa", 15, 15));
            Teams.Add(new DataItem("Bournemouth", 17, 18));
            Teams.Add(new DataItem("Chelsea", 4, 1));
            Teams.Add(new DataItem("Crystal Palace", 11, 14));
            Teams.Add(new DataItem("Everton", 7, 7));
            Teams.Add(new DataItem("Leicester", 19, 16));
            Teams.Add(new DataItem("Liverpool", 6, 5));
            Teams.Add(new DataItem("Man City", 1, 2));
            Teams.Add(new DataItem("Man Utd", 3, 3));
            Teams.Add(new DataItem("Newcastle", 14, 12));
            Teams.Add(new DataItem("Norwich", 16, 19));
            Teams.Add(new DataItem("Southampton", 8, 9));
            Teams.Add(new DataItem("Stoke", 9, 8));
            Teams.Add(new DataItem("Sunderland", 18, 17));
            Teams.Add(new DataItem("Swansea", 12, 10));
            Teams.Add(new DataItem("Tottenham", 5, 6));
            Teams.Add(new DataItem("Watford", 20, 20));
            Teams.Add(new DataItem("West Ham", 10, 11));
            Teams.Add(new DataItem("West Brom", 13, 13));

            SetCurrentPositions();
        }

        public ICollection<DataItem> Teams { get; private set; }
        public IEnumerable<DataItem> MatchingTeams(string queryText)
        {
            return from team in Teams where team.Name.ToLower().Contains(queryText) select team;
        }

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
        public IEnumerable<DataGroup> MatchingGroups(string queryText)
        {
            return from grp in Groups where grp.Name.ToLower().Contains(queryText) select grp;
        }

        private string GetGroupName(double difference)
        {
            if (difference > 4) return "Fantastic";
            else if (difference > 1) return "Exceeded expectations";
            else if (difference < -4) return "Nightmare";
            else if (difference < -1) return "Disappointed";
            else return "OK";
        }

        private void SetCurrentPositions()
        {
            string[] leagueTable =
                {
                    "Man City",
                    "Leicester",
                    "Liverpool",
                    "Man Utd",
                    "Everton",
                    "Swansea",
                    "Crystal Palace",
                    "West Ham",
                    "Norwich",
                    "Aston Villa",
                    "Arsenal",
                    "Watford",
                    "Stoke",
                    "Tottenham",
                    "Newcastle",
                    "Chelsea",
                    "Southampton",
                    "West Brom",
                    "Bournemouth",
                    "Sunderland"
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
