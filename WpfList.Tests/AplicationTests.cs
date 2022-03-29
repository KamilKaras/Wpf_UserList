using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfList.Core;
using Xunit;

namespace WpfList.Tests
{
    public class AplicationTests
    {
        ListPageViewModel listPageViewModel = new ListPageViewModel();
        [Fact]
        public void SelectedUsers_ForThreeIsSelectedUsers_ShouldReturnThreeElementsList()
        {
            //arange
            ObservableCollection<AplicationUser> userList = new ObservableCollection<AplicationUser>()
            {
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=true},
                new AplicationUser(){IsSelected=false},
                new AplicationUser(){IsSelected=false},
            };
            //act
            var selectedUsers = listPageViewModel.SelectedUsers(userList);
            //assert
            Assert.Equal(3,selectedUsers.Count);
        }

        [Fact]
        public void CheckInput_ShouldReturnTrue()
        {

            //act
            var trueReturn = listPageViewModel.CheckInputsCorrect("NameTest", "SurnameTest", "RoleTest");
            //assert
            Assert.True(trueReturn);
        }

        [Fact]
        public void CheckInput_ShouldReturnFalse()
        {
            //arange
            var listPageViewModel = new ListPageViewModel();
            //act
            var falseReturn = listPageViewModel.CheckInputsCorrect("", "SurnameTest", "RoleTest");
            //assert
            Assert.False(falseReturn);
        }
    }
}
