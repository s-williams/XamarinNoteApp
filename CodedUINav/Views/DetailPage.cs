using Xamarin.Forms;

namespace CodedUINav
{
    public class DetailPage : ContentPage
    {
        public DetailPage(DetailPageViewModel viewModel)
        {
            BindingContext = viewModel;

            Title = "Notes Detail";

            BackgroundColor = Color.PowderBlue;

            var noteEditor = new Editor
            {
                Placeholder = "Enter Note",
                BackgroundColor = Color.White,
                Margin = new Thickness(10)
            };
            noteEditor.SetBinding(Editor.TextProperty, nameof(DetailPageViewModel.NoteText));

            var saveButton = new Button
            {
                Text = "Save",
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10),
                BackgroundColor = Color.Green,
                TextColor = Color.White

            };
            saveButton.SetBinding(Button.CommandProperty, nameof(DetailPageViewModel.SaveCommand));

            var deleteButton = new Button
            {
                Text = "Delete",
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10),
                BackgroundColor = Color.Red,
                TextColor = Color.White
            };
            deleteButton.SetBinding(Button.CommandProperty, nameof(DetailPageViewModel.DeleteCommand));

            var grid = new Grid
            {
                Margin = new Thickness(20, 40),

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(4, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                }
            };

            grid.Children.Add(noteEditor, 0, 0);
            Grid.SetColumnSpan(noteEditor, 2);

            grid.Children.Add(saveButton, 0, 1);
            grid.Children.Add(deleteButton, 1, 1);

            Content = grid;
        }
    }
}
