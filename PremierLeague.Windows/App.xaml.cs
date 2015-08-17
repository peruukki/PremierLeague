using PremierLeague.Common;
using PremierLeague.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace PremierLeague
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            var restoreState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
            var frame = await GetRootFrame(restoreState);
            if (frame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!frame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
        }

        /// <summary>
        /// Invoked when the application is activated to display search results.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected async override void OnSearchActivated(Windows.ApplicationModel.Activation.SearchActivatedEventArgs args)
        {
            // TODO: Register the Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
            // event in OnWindowCreated to speed up searches once the application is already running

            // If the Window isn't already using Frame navigation, insert our own Frame
            var restoreState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
            var initialLaunch = (Window.Current.Content == null) && !restoreState;
            var frame = await GetRootFrame(restoreState);
            if (!string.IsNullOrEmpty(args.QueryText))
            {
                frame.Navigate(typeof(SearchResultsPage), args.QueryText);
            }
            else if (initialLaunch)
            {
                frame.Navigate(typeof(MainPage), args);
            }
        }

        private async Task<Frame> GetRootFrame(bool restoreState)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                // Create a Frame to act as the navigation context and associate it with
                // a SuspensionManager key
                frame = new Frame();
                SuspensionManager.RegisterFrame(frame, "AppFrame");
                InitializeSearchPane();

                if (restoreState)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }
            }

            Window.Current.Content = frame;

            // Ensure the current window is active
            Window.Current.Activate();

            return frame;
        }

        private void InitializeSearchPane()
        {
            var searchPane = SearchPane.GetForCurrentView();
            searchPane.SuggestionsRequested +=
                new TypedEventHandler<SearchPane, SearchPaneSuggestionsRequestedEventArgs>(OnSearchPaneSuggestionsRequested);
            searchPane.ShowOnKeyboardInput = true;
        }

        private void OnSearchPaneSuggestionsRequested(SearchPane sender, SearchPaneSuggestionsRequestedEventArgs e)
        {
            var queryText = e.QueryText.ToLower();
            if (string.IsNullOrEmpty(queryText))
            {
                return;
            }

            var maxSuggestions = 5;
            var request = e.Request;
            var source = new DataSource();

            var matchingTeams = source.MatchingTeams(queryText);
            foreach (var team in matchingTeams)
            {
                request.SearchSuggestionCollection.AppendQuerySuggestion(team.Name);
                if (request.SearchSuggestionCollection.Size >= maxSuggestions)
                {
                    return;
                }
            }

            var matchingGroups = source.MatchingGroups(queryText);
            foreach (var grp in matchingGroups)
            {
                request.SearchSuggestionCollection.AppendQuerySuggestion(grp.Name);
                if (request.SearchSuggestionCollection.Size >= maxSuggestions)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}
