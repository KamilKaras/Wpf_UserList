using DbHandler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfList.Core
{
    public class ListPageViewModel : BaseModel
    {
        public ObservableCollection<AplicationUser> UserList { get; set; } = new ObservableCollection<AplicationUser>();  
        public string NewUserName { get; set; }
        public string NewUserSurname { get; set; }
        public string NewUserRole { get; set; }
        public ICommand AddNewUserCommand { get; set; }
        public ICommand DeleteUserComand { get; set; }
        public ICommand EditUserComand { get; set; }
        public ICommand AcceptPendingEditions { get; set; }
        public string AddButtonVisible { get; set; }
        public string AcceptButtonVisible { get; set; }
        public string VisibilityState { get; set; }
        public string AlertMessage { get; set; }
        private  WpfListController DbControler { get; set; }

        public ListPageViewModel()
        {
            AddNewUserCommand = new ComandHelper(AddNewUsesr);
            DeleteUserComand = new ComandHelper(DeleteUser);
            EditUserComand = new ComandHelper(EditUser);
            AcceptPendingEditions = new ComandHelper(AcceptEdits);
            DbControler = new WpfListController(new WpfListDbContext());
            NewUserName = "";
            NewUserSurname = "";
            NewUserRole = "";
            VisibilityState = "Hidden";
            AddButtonVisible = "Visible";
            AcceptButtonVisible = "Hidden";
            GetAllUser();
        }

        public async void AddNewUsesr()
        {
            var inputCorrect = CheckInputsCorrect(NewUserName, NewUserSurname, NewUserRole);
            if (inputCorrect)
            {
                var newDbUser = new AplicationUserModel {

                    IsSelected = false,
                    Name = NewUserName,
                    Role = NewUserRole,
                    Surname = NewUserSurname
                };
                var addedUser = await DbControler.AddUser(newDbUser);
                UserList.Add(AplicationUserMapper.MappUser(addedUser));
                CleanInputs();
            } 
        }

        public void DeleteUser()
        {
            var userToDelete = SelectedUsers();
            if(userToDelete.Count>=1)
            {
                foreach (var user in userToDelete)
                {
                    DbControler.DeleteUsers(user.Id);
                    UserList.Remove(user);
                }
                DbControler.SaveChanges();
                InputAlert("", "Hidden");
                ButtonsVisible("Visible", "Hidden");
                CleanInputs();
            }
            else
            {
                InputAlert("Please select users to delete", "Visible");
            }
        }

        public void EditUser()
        {
            {
                var userToEditList = SelectedUsers();
                var userToEdit = userToEditList.Find(user => user.IsSelected == true);
                if (userToEditList.Count > 0 && userToEditList.Count < 2)
                {
                    ButtonsVisible("Hidden", "Visible");
                    InputAlert("", "Hidden");
                    FillToEdit(userToEdit);
                }
                else
                {
                    InputAlert("You should edit one user", "Visible");
                }
            }
        }
        public void AcceptEdits()
        {
            var inputCorrect = CheckInputsCorrect(NewUserName, NewUserSurname, NewUserRole);
            if (inputCorrect)
            {
                ButtonsVisible("Visible", "Hidden");
                AddNewUsesr();
                DeleteUser();
                InputAlert("", "Hidden");
                CleanInputs();
            }
        }
        public async void GetAllUser()
        {
            var userFromDb = await DbControler.GetAllUser();
            foreach(var user in userFromDb)
            {
                UserList.Add(AplicationUserMapper.MappUser(user));
            }
        }

        public List<AplicationUser> SelectedUsers()
        {
            var selectedUsers = UserList.Where(user => user.IsSelected).ToList();
            return selectedUsers;
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
    }
}
