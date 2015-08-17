using Windows.UI.Xaml.Controls;

namespace PremierLeague.Data
{
    interface ISearchResult
    {
        string Description { get; }
        Image Image { get; }
        string Subtitle { get; }
        string Title { get; }
    }
}
