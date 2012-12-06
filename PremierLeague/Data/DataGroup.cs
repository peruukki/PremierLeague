using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace PremierLeague.Data
{
    public class DataGroup : ISearchResult
    {
        public DataGroup(string name, ICollection<DataItem> items)
        {
            Name = name;
            Items = items;
        }

        #region ISearchResult
        public string Title { get { return Name; } }
        public string Subtitle { get { return string.Format("{0} teams", Items.Count); } }
        public string Description { get { return ""; } }
        public Image Image { get { return null; } }
        #endregion ISearchResult

        public string Name { get; private set; }
        public ICollection<DataItem> Items { get; private set; }
    }
}
