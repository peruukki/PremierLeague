namespace PremierLeague.Data
{
    public class DataItem
    {
        public DataItem(string name, int fftPrediction)
        {
            Name = name;
            FFTPrediction = fftPrediction;
        }

        public string Name { get; private set; }
        public int FFTPrediction { get; private set; }

        public string FFTPredictionText { get { return "FFT: " + FFTPrediction; } }
    }
}
