using PremierLeague.Common;
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
    public sealed partial class MainPage : LayoutAwarePage
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
                         orderby t.PositionDifference descending
                         group t by GetGroupName(t.PositionDifference) into g
                         select new { Key = string.Format("{0} ({1})", g.Key, g.ToList().Count), Items = g }; ;
            groupData.Source = result;
        }

        private string GetGroupName(int difference)
        {
            if (difference > 4) return "Fantastic season";
            else if (difference > 1) return "Exceeding expectations";
            else if (difference < -4) return "Nightmare season";
            else if (difference < -1) return "Disappointing";
            else return "Doing OK";
        }
    }
}
