using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PremierLeague
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var teams = new Data.DataSource().Items;
            var result = from t in teams
                         group t by GetGroupName(t.FFTPrediction) into g
                         select new { Key = g.Key, Items = g }; ;
            groupData.Source = result;
        }

        private string GetGroupName(int prediction)
        {
            if (prediction <= 4) return "Top four";
            else if (prediction <= 8) return "Near euro spot";
            else if (prediction <= 12) return "Midtable";
            else if (prediction <= 16) return "Safe";
            else return "Relegation battle";
        }
    }
}
