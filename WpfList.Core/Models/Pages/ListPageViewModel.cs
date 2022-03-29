using DbHandler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfList.Core
{
    public class ListPageViewModel : UserActionHandling
    {
        private WpfListController DbControler { get; set; }
        public ObservableCollection<AplicationUser> UserList { get; set; } = new ObservableCollection<AplicationUser>();
        public ICommand AddNewUserCommand { get; set; }
        public ICommand DeleteUserComand { get; set; }
        public ICommand EditUserComand { get; set; }
        public ICommand AcceptPendingEditions { get; set; }

        public ListPageViewModel() : base()
        {
            AddNewUserCommand = new ComandHelper(AddNewUsesr);
            DeleteUserComand = new ComandHelper(DeleteUser);
            EditUserComand = new ComandHelper(EditUser);
            AcceptPendingEditions = new ComandHelper(AcceptEdits);
            DbControler = new WpfListController(new WpfListDbContext());
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
            var userToDelete = SelectedUsers(UserList);
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
                var userToEditList = SelectedUsers(UserList);
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
    }
}
