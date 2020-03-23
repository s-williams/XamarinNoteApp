using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace CodedUINav
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Notes = new ObservableCollection<NoteModel>();

            SaveNoteCommand = new Command(() =>
            {
                Notes.Add(new NoteModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = NoteText,
                    Title = Common.createTitle(NoteText),
                    Timestamp = DateTime.Now,
                    Time = DateTime.Now.ToString("dd MMM yyyy")
                });
                NoteText = string.Empty;
            },
            () => !string.IsNullOrEmpty(NoteText));

            NoteSelectedCommand = new Command(async () =>
            {
                if (SelectedNote is null)
                    return;

                var detailViewModel = new DetailPageViewModel
                {
                    NoteText = SelectedNote.Text,
                    Notes = Notes,
                    Id = SelectedNote.Id
                };

                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(detailViewModel));

                SelectedNote = null;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteText)));

                SaveNoteCommand.ChangeCanExecute();
            }
        }

        NoteModel selectedNote;
        public NoteModel SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }

        public ObservableCollection<NoteModel> Notes { get; }

        public Command NoteSelectedCommand { get; }
        public Command SaveNoteCommand { get; }
    }
}
