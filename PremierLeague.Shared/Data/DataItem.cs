using Windows.UI.Xaml.Controls;

namespace PremierLeague.Data
{
    public class DataItem : ISearchResult
    {
        public DataItem(string name, int urheiluSanomatPrediction, int helsinginSanomatPrediction)
        {
            Name = name;
            UrheiluSanomatPrediction = urheiluSanomatPrediction;
            HelsinginSanomatPrediction = helsinginSanomatPrediction;
        }

        #region ISearchResult
        public string Title { get { return Name; } }
        public string Subtitle { get { return CurrentPositionText; } }
        public string Description { get { return UrheiluSanomatPredictionText + ", " + HelsinginSanomatPredictionText; } }
        public Image Image { get { return null; } }
        #endregion ISearchResult

        public string Name { get; private set; }
        public int CurrentPosition { get; set; }
        public int UrheiluSanomatPrediction { get; private set; }
        public int HelsinginSanomatPrediction { get; private set; }
        public double PositionDifference { get { return (UrheiluSanomatPrediction + HelsinginSanomatPrediction) / 2 - CurrentPosition; } }

        public string CurrentPositionText { get { return "Pos: " + CurrentPosition; } }

        public string UrheiluSanomatPredictionText { get { return PositionDifferenceText("US", UrheiluSanomatPrediction, CurrentPosition); } }
        public string HelsinginSanomatPredictionText { get { return PositionDifferenceText("HS", HelsinginSanomatPrediction, CurrentPosition); } }

        private string PositionDifferenceText(string label, int prediction, int position)
        {
            return string.Format("{0}: {1} ({2})", label, prediction, (prediction - position).ToString("+#;-#;0"));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
