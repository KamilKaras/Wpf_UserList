using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using WpfList.Core;

namespace WpfList
{
    public abstract class UserActionHandling : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, eventArgs) => { };
        public string AddButtonVisible { get; set; }
        public string AcceptButtonVisible { get; set; }
        public string VisibilityState { get; set; }
        public string AlertMessage { get; set; }
        public string NewUserName { get; set; }
        public string NewUserSurname { get; set; }
        public string NewUserRole { get; set; }
     
        public UserActionHandling()
        {
            NewUserName = "";
            NewUserSurname = "";
            NewUserRole = "";
            VisibilityState = "Hidden";
            AddButtonVisible = "Visible";
            AcceptButtonVisible = "Hidden";
        }

        public void OnPermit(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void InputAlert(string message, string state)
        {
            AlertMessage = message;
            VisibilityState = state;
            OnPermit(nameof(VisibilityState));
            OnPermit(nameof(AlertMessage));
        }

        public void FillToEdit(AplicationUser user)
        {
            NewUserName = user.Name;
            NewUserSurname = user.Surname;
            NewUserRole = user.Role;
            OnPermit(nameof(NewUserName));
            OnPermit(nameof(NewUserSurname));
            OnPermit(nameof(NewUserRole));
        }

        public void ButtonsVisible(string addState, string acceptState)
        {
            AddButtonVisible = addState;
            AcceptButtonVisible = acceptState;
            OnPermit(nameof(AddButtonVisible));
            OnPermit(nameof(AcceptButtonVisible));
        }

        public void CleanInputs()
        {
            NewUserName = string.Empty;
            NewUserSurname = string.Empty;
            NewUserRole = string.Empty;
            OnPermit(nameof(NewUserName));
            OnPermit(nameof(NewUserSurname));
            OnPermit(nameof(NewUserRole));
        }

        public bool CheckInputsCorrect(string name, string surname, string role)
        {
            if (name == string.Empty || surname == string.Empty || role == string.Empty)
            {
                InputAlert("Please fill all input fields", "Visible");
                return false;
            }
            InputAlert("", "Hidden");
            return true;
        }
        public List<AplicationUser> SelectedUsers(ObservableCollection<AplicationUser> userList)
        {
            var selectedUsers = userList.Where(user => user.IsSelected == true).ToList();
            return selectedUsers;
        }
    }
}
