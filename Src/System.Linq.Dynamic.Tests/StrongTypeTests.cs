using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Tests.Helpers;

namespace System.Linq.Dynamic.Tests
{
    [TestClass]
    public class StrongTypeTests
    {
        public class UserInfo
        {
            public UserInfo(string userName)
            {
                UserName = userName ?? string.Empty;
            }

            public string UserName { get; }

            public override bool Equals(object obj)
            {
                if (obj is UserInfo ui)
                {
                    return this.UserName == ui.UserName;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return UserName.GetHashCode();
            }
        }

        [TestMethod]
        public void Select()
        {
            //Arrange
            var testList = User.GenerateSampleModels(100);
            var qry = testList.AsQueryable();

            //Act
            var userInfoes = qry.Select<UserInfo>("UserInfo(UserName)");

            //Assert
#if NET35
            CollectionAssert.AreEqual(
                testList.Select(u => new UserInfo(u.UserName)).ToArray(),
                userInfoes.ToArray());
#else
            CollectionAssert.AreEqual(
                testList.Select(u => new UserInfo(u.UserName)).ToArray(), 
                userInfoes.ToArray());
#endif
        }
    }
}
