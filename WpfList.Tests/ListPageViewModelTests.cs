using AutoFixture;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfList.Core;
using Xunit;

namespace WpfList.Tests
{
    public class ListPageViewModelTests
    {
        private Fixture _fixture;
        private ListPageViewModel _listPageViewModel;
        public ObservableCollection<AplicationUser> UserList { get; set; } = new ObservableCollection<AplicationUser>();
        public string NewUserName { get; set; } = "TestName";
        public string NewUserSurname { get; set; } = "TestSurname";
        public string NewUserRole { get; set; } = "TestRole";
        public ListPageViewModelTests()
        {
            _fixture = new Fixture();
            _listPageViewModel = new ListPageViewModel();
        }

        [Fact]
        public void SelectedUsers_ForThreeIsSelected_ShouldReturnThreeElementsList()
        {
            //arange
            var userList = new List<AplicationUser>()
            {
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=false},
                new AplicationUser(){IsSelected=false},
            };
            //act
            var selectedUsers = userList.Where(user => user.IsSelected).ToList();
            //assert
            Assert.Equal(3,selectedUsers.Count);
        }

        [Fact]
        public void CheckInputsCorrect_ForCorrectInputField_ShouldReturnTrue()
        {
            //arange
            
            //act
            var result = _listPageViewModel.CheckInputsCorrect(NewUserName, NewUserSurname, NewUserRole);
            //assert
            Assert.True(result);
        }

        [Fact]
        public void CheckInputsCorrect_ForIncorrectInputField_ShouldReturnFalse()
        {
            //arange
            var userName = "";
            var userSurname = "TestSurname";
            var userRole = "TestRole";
            //act
            var result = _listPageViewModel.CheckInputsCorrect(userName, userSurname, userRole);
            //assert
            Assert.False(result);
        }

        
    }
}
