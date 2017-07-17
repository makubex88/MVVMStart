using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModel
{
    public class AddContactViewModel : INotifyPropertyChanged
    {
        //constructor
        public AddContactViewModel()
        {
            //commands
            LaunchWebsiteCmd = new Command(LaunchWebsite, () => !IsBusy);
            SaveContactCmd = new Command(async () => await SaveContact(), () => !IsBusy);
        }
        
        //initial values
        string breed = "Shih Tzu";
        string pet = "Zestee";
        bool isAttending;
        bool isBusy;

        //event for property changed.
        public event PropertyChangedEventHandler PropertyChanged;   

        //automatically called the property that was passed in
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool IsAttending
        {
            get
            {
                return isAttending;
            }
            set
            {
                isAttending = value;

                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string Breed
        {
            get
            {
                return breed;
            }
            set
            {
                breed = value;
                //sample condition
                if (breed == "Askal")
                {
                    IsBusy = true;
                }
                else
                {
                    IsBusy = false;
                }

                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string Pet
        {
            get
            {
                return pet;
            }
            set
            {
                pet = value;

                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }        

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
                LaunchWebsiteCmd.ChangeCanExecute();
                SaveContactCmd.ChangeCanExecute();
            }
        }

        public string DisplayMessage
        {
            get
            {
                return $"Your pet {pet} is a {Breed}. Your pet {(isAttending ? "will" : "will not")} attend to a clinic schedule.";
            }
        }

        public Command LaunchWebsiteCmd { get; }
        public Command SaveContactCmd { get; }

        void LaunchWebsite()
        {
            try
            {
                Device.OpenUri(new Uri($"https://www.google.com.ph/#q={breed}"));
            }
            catch
            {
                
            }
        }

        async Task SaveContact()
        {
            IsBusy = true;
            await Task.Delay(4000);

            IsBusy = false;

            await Application.Current.MainPage.DisplayAlert("Save", "Contact is saved", "OK");
        }
    }
}
