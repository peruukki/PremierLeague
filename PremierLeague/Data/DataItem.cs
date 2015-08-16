using Windows.UI.Xaml.Controls;

namespace PremierLeague.Data
{
    public class DataItem : ISearchResult
    {
        public DataItem(string name, int urheiluSanomatPrediction)
        {
            Name = name;
            UrheiluSanomatPrediction = urheiluSanomatPrediction;
        }

        #region ISearchResult
        public string Title { get { return Name; } }
        public string Subtitle { get { return CurrentPositionText; } }
        public string Description { get { return UrheiluSanomatPredictionText; } }
        public Image Image { get { return null; } }
        #endregion ISearchResult

        public string Name { get; private set; }
        public int CurrentPosition { get; set; }
        public int UrheiluSanomatPrediction { get; private set; }
        public int PositionDifference { get { return UrheiluSanomatPrediction - CurrentPosition; } }

        public string CurrentPositionText { get { return "Pos: " + CurrentPosition; } }
        public string UrheiluSanomatPredictionText
        {
            get
            {
                return string.Format("US: {0} ({1})", UrheiluSanomatPrediction,
                    PositionDifference.ToString("+#;-#;0"));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
