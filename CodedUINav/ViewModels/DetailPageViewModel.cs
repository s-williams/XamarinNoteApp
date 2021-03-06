﻿using System;
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
                    foundNote.Title = Common.createTitle(NoteText);
                    foundNote.Timestamp = DateTime.Now;
                    foundNote.Time = DateTime.Now.ToString("dd MMM yyyy");
                    Notes.Add(foundNote);
                }
                ExitCommand.Execute(null);
            },
            () => hasChanged);

            DeleteCommand = new Command(() =>
            {
                foreach (NoteModel n in Notes)
                {
                    if (n.Id == Id)
                    {
                        Notes.Remove(n);
                        break;
                    }
                }
                ExitCommand.Execute(null);
            });
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
