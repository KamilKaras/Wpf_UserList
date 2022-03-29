using System.ComponentModel;
using WpfList.Core;

namespace WpfList
{
    public abstract class UserActionHandling : INotifyPropertyChanged, IUserActionHandling
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

        protected void OnPermit(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected void InputAlert(string message, string state)
        {
            AlertMessage = message;
            VisibilityState = state;
            OnPermit(nameof(VisibilityState));
            OnPermit(nameof(AlertMessage));
        }

        protected void FillToEdit(AplicationUser user)
        {
            NewUserName = user.Name;
            NewUserSurname = user.Surname;
            NewUserRole = user.Role;
            OnPermit(nameof(NewUserName));
            OnPermit(nameof(NewUserSurname));
            OnPermit(nameof(NewUserRole));
        }

        protected void ButtonsVisible(string addState, string acceptState)
        {
            AddButtonVisible = addState;
            AcceptButtonVisible = acceptState;
            OnPermit(nameof(AddButtonVisible));
            OnPermit(nameof(AcceptButtonVisible));
        }

        protected void CleanInputs()
        {
            NewUserName = string.Empty;
            NewUserSurname = string.Empty;
            NewUserRole = string.Empty;
            OnPermit(nameof(NewUserName));
            OnPermit(nameof(NewUserSurname));
            OnPermit(nameof(NewUserRole));
        }

        protected bool CheckInputsCorrect(string name, string surname, string role)
        {
            if (name == string.Empty || surname == string.Empty || role == string.Empty)
            {
                InputAlert("Please fill all input fields", "Visible");
                return false;
            }
            InputAlert("", "Hidden");
            return true;
        }

    }
}
