using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CodedUINav
{
    public class DetailPageViewModel : INotifyPropertyChanged
    {
        public DetailPageViewModel()
        {
            ExitCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());

            SaveCommand = new Command(() =>
            {
                NoteModel foundNote = null;
                foreach(NoteModel n in Notes)
                {
                    if(n.Id == Id)
                    {
                        foundNote = n;
                        Notes.Remove(n);
                        break;
                    }
                }

                if (foundNote != null)
                {
                    foundNote.Text = NoteText;
                    foundNote.Title = NoteText.Length > 40 ? NoteText.Substring(0, 40) + "..." : NoteText;
                    foundNote.Timestamp = DateTime.Now;
                    foundNote.Time = DateTime.Now.ToString("dd MMM yyyy");
                    Notes.Add(foundNote);
                }


            },
            () => hasChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        bool hasChanged;
        private string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                if (noteText != null) hasChanged = true;
                noteText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteText)));
            }
        }
        public string Id { get; set; }

        public ObservableCollection<NoteModel> Notes { get; set; }

        public ICommand ExitCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
    }
}
