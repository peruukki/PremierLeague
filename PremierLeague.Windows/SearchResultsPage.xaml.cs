﻿using PremierLeague.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240

namespace PremierLeague
{
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsPage : PremierLeague.Common.LayoutAwarePage
    {

        public SearchResultsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var queryText = (navigationParameter as string).ToLower();

            // Application-specific searching logic.  The search process is responsible for
            // creating a list of user-selectable result categories:
            //
            // filterList.Add(new Filter("<filter name>", <result count>));
            //
            // Only the first filter, typically "All", should pass true as a third argument in
            // order to start in an active state.  Results for the active filter are provided
            // in Filter_SelectionChanged below.
            var filterList = new List<Filter>();
            var source = new DataSource();

            var matchingTeams = source.MatchingTeams(queryText).ToList<ISearchResult>();
            if (matchingTeams.Count > 0)
            {
                filterList.Add(new Filter("Teams", matchingTeams));
            }

            var matchingGroups = source.MatchingGroups(queryText).ToList<ISearchResult>();
            if (matchingGroups.Count > 0)
            {
                filterList.Add(new Filter("Categories", matchingGroups));
            }

            // Ensure that filters always exist so that Filter_SelectionChanged gets called
            if (filterList.Count != 1)
            {
                var allMatches = new List<ISearchResult>();
                allMatches.AddRange(matchingTeams);
                allMatches.AddRange(matchingGroups);
                filterList.Insert(0, new Filter("All", allMatches, true));
            }

            // Communicate results through the view model
            this.DefaultViewModel["QueryText"] = '\u201c' + queryText + '\u201d';
            this.DefaultViewModel["Filters"] = filterList;
            this.DefaultViewModel["ShowFilters"] = true;
        }

        /// <summary>
        /// Invoked when a filter is selected using the ComboBox in snapped view state.
        /// </summary>
        /// <param name="sender">The ComboBox instance.</param>
        /// <param name="e">Event data describing how the selected filter was changed.</param>
        void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Determine what filter was selected
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                // Mirror the results into the corresponding Filter object to allow the
                // RadioButton representation used when not snapped to reflect the change
                selectedFilter.Active = true;

                // Respond to the change in active filter by setting this.DefaultViewModel["Results"]
                // to a collection of items with bindable Image, Title, Subtitle, and Description properties
                this.DefaultViewModel["Results"] = selectedFilter.Matches;

                // Ensure results are found
                object results;
                ICollection resultsCollection;
                if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as ICollection) != null &&
                    resultsCollection.Count != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }
            }

            // Display informational text when there are no search results.
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        /// <summary>
        /// Invoked when a filter is selected using a RadioButton when not snapped.
        /// </summary>
        /// <param name="sender">The selected RadioButton instance.</param>
        /// <param name="e">Event data describing how the RadioButton was selected.</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // Mirror the change into the CollectionViewSource used by the corresponding ComboBox
            // to ensure that the change is reflected when snapped
            if (filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        /// <summary>
        /// View model describing one of the filters available for viewing search results.
        /// </summary>
        private sealed class Filter : PremierLeague.Common.BindableBase
        {
            public Filter(string name, ICollection<ISearchResult> matches, bool active = false)
            {
                this.Name = name;
                this.Matches = matches;
                this.Active = active;
            }

            public string Name { get; private set; }
            public ICollection<ISearchResult> Matches { get; private set; }
            public int MatchCount { get { return Matches.Count; } }
            public bool Active { get; set; }
            public string Description { get { return ToString(); } }

            public override string ToString()
            {
                return String.Format("{0} ({1})", Name, MatchCount);
            }
        }
    }
}