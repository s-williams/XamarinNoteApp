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
                foreach(NoteModel n in Notes)
                {
                    if(n.Id == Id)
                    {
                        n.Text = NoteText;
                        n.Title = NoteText.Length > 45 ? NoteText.Substring(0, 44) + "..." : NoteText;
                        n.Timestamp = DateTime.Now;
                        break;
                    }
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
