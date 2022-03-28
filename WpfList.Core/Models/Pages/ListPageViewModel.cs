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
        private  WpfListController DbControler { get; set; }

        public ListPageViewModel()
        {
            AddNewUserCommand = new ComandHelper(AddNewUsesr);
            DeleteUserComand = new ComandHelper(DeleteUser);
            EditUserComand = new ComandHelper(EditUser);
            DbControler = new WpfListController(new WpfListDbContext());
            GetAllUser();
        }

        public async void AddNewUsesr()
        {
            var inputCorrect = CheckInputsCorrect(NewUserName, NewUserSurname, NewUserRole);
            if(inputCorrect)
            {
              
                var newDbUser = new AplicationUserModel {

                    IsSelected = false,
                    Name = NewUserName,
                    Role = NewUserSurname,
                    Surname = NewUserRole
                };
                var addedUser = await DbControler.AddUser(newDbUser);
                UserList.Add(AplicationUserMapper.MappUser(addedUser));
                CleanInputs();
            }
            
        }

        public void DeleteUser()
        {
            var userToDelete = SelectedUsers();
            foreach (var user in userToDelete)
            {
                 DbControler.DeleteUsers(user.Id);
                UserList.Remove(user);
            }
            DbControler.SaveChanges();
        }

        public void EditUser()
        {
            var inputCorrect = CheckInputsCorrect(NewUserName, NewUserSurname, NewUserRole);
            if (inputCorrect)
            {
                var userToEdit = SelectedUsers();
                if (userToEdit.Count > 0 && userToEdit.Count < 2)
                {
                    DeleteUser();
                    AddNewUsesr();
                    CleanInputs();
                }
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
                return false;
            if (name == null || surname == null || role == null)
                return false;
            return true;
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
