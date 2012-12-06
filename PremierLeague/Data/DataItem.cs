using Windows.UI.Xaml.Controls;

namespace PremierLeague.Data
{
    public class DataItem : ISearchResult
    {
        public DataItem(string name, int fftPrediction)
        {
            Name = name;
            FFTPrediction = fftPrediction;
        }

        #region ISearchResult
        public string Title { get { return Name; } }
        public string Subtitle { get { return CurrentPositionText; } }
        public string Description { get { return FFTPredictionText; } }
        public Image Image { get { return null; } }
        #endregion ISearchResult

        public string Name { get; private set; }
        public int CurrentPosition { get; set; }
        public int FFTPrediction { get; private set; }
        public int PositionDifference { get { return FFTPrediction - CurrentPosition; } }

        public string CurrentPositionText { get { return "Pos: " + CurrentPosition; } }
        public string FFTPredictionText
        {
            get
            {
                return string.Format("FFT: {0} ({1})", FFTPrediction,
                    PositionDifference.ToString("+#;-#;0"));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
