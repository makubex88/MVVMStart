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
        string name = "Jeric";
        string website = "https://blog.xamarin.com";
        bool bestFriend;
        bool isBusy;

        //event for property changed.
        public event PropertyChangedEventHandler PropertyChanged;   

        //automatically called the property that was passed in
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool BestFriend
        {
            get
            {
                return bestFriend;
            }
            set
            {
                bestFriend = value;

                OnPropertyChanged();

                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                //sample condition
                if (name == "James")
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

        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;

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
                return $"Your new friend is named {Name} and {(bestFriend ? "is" : "is not")} your best friend  ";
            }
        }

        public Command LaunchWebsiteCmd { get; }
        public Command SaveContactCmd { get; }

        void LaunchWebsite()
        {
            try
            {
                Device.OpenUri(new Uri(website));
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
