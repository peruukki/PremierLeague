namespace PremierLeague.Data
{
    public class DataItem
    {
        public DataItem(string name, int currentPosition, int fftPrediction)
        {
            Name = name;
            CurrentPosition = currentPosition;
            FFTPrediction = fftPrediction;
        }

        public string Name { get; private set; }
        public int CurrentPosition { get; private set; }
        public int FFTPrediction { get; private set; }

        public string CurrentPositionText { get { return "Pos: " + CurrentPosition; } }
        public string FFTPredictionText
        {
            get
            {
                return string.Format("FFT: {0} ({1})", FFTPrediction, FFTPrediction - CurrentPosition);
            }
        }
    }
}
