using System.Collections.Generic;
using System.Linq;
using WpfList.Core;
using Xunit;

namespace WpfList.Tests
{
    public class UserActionHandlingTests
    {

        [Fact]
        public void AddNewUser_ToTwoElementsList_ShouldAddNewUser()
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
            Assert.Equal(3, selectedUsers.Count);
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
    }
}
