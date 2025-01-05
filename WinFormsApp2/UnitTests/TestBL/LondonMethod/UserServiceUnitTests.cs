using Allure.Xunit.Attributes;
using Allure.Xunit.Attributes.Steps;
//using Castle.Core.Logging;
using Exceptions;
using Interfaces;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging.Abstractions;
using UnitTests.Fixture;
using UnitTests.ObjectMothers;
using Xunit;
using Moq;

namespace UnitTests.UnitTests.TestBL.LondonMethod
{
    [AllureOwner("VHD")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("UserServices Unit tests")]
    [AllureSubSuite("UserService unit tests London Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class UserServiceUnitTests
    {
        private ServiceFixture fixture = new ServiceFixture();
        private UserObjectMother userOM = new UserObjectMother();
        [AllureBefore]
        public UserServiceUnitTests() { }
        [Fact]
        public void TestLoginSuccess()
        {
            var user = userOM.CreateUser(7, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Returns(user);
            var userService = new UserService(_userRepoMock.Object);

            var actual = userService.LogIn(user.Login, user.Password);

            Assert.Equal(user, actual);
            _userRepoMock.Verify(m => m.GetUser(user.Login), Times.Once());
        }
        [Fact]
        public void TestLoginFailure()
        {
            var user = userOM.CreateUser(7, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Throws(new UserNotFoundException());
            var userService = new UserService(_userRepoMock.Object);

            Assert.Throws<UserNotFoundException>(() => userService.LogIn(user.Login, user.Password));
            _userRepoMock.Verify(m => m.GetUser(user.Login), Times.Once());
        }
        [Fact]
        public void TestRegisterSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateUser(11, "abec").WithPhone("ef").WithLogin("abdc").BuildCoreModel();

            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(s => s.AddUser(It.IsAny<User>()))
                            .Callback((User user) => users.Add(user)).Verifiable();
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Returns(users.Find(u => u.Login == user.Login));
            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(user);
            _userRepoMock.Setup(m => m.CountAllUsers()).Returns(users.Count());
            var userService = new UserService(_userRepoMock.Object);
            userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role);
            var actual = userService.GetUser(11);

            Assert.Equal(user, actual);
            _userRepoMock.Verify(m => m.GetUser(user.Login), Times.Once());
        }
        [Fact]
        public void TestRegisterFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users.First();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

            _userRepoMock.Setup(m => m.AddUser(user)).Callback((User user) => users.Add(user)).Verifiable();
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Returns(users.Find(u => u.Login == user.Login));
            _userRepoMock.Setup(m => m.CountAllUsers()).Returns(users.Count());
            var userService = new UserService(_userRepoMock.Object);

            Assert.Throws<UserExistException>(() => userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role));
            _userRepoMock.Verify(m => m.GetUser(user.Login), Times.Once());
        }
        [Fact]
        public void TestChangePasswordSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();


            _userRepoMock.Setup(s => s.UpdateUser(It.IsAny<User>()))
                                .Callback((User user) =>
                                {
                                    users.Remove(item: users.Find(e => e.Id == user.Id)!);
                                    users.Add(user);
                                }).Verifiable();

            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.Find(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Returns(user);
            var userService = new UserService(_userRepoMock.Object);

            userService.ChangePassword(user.Login, "abc");
            Assert.Equivalent(user.Password, "abc");
            _userRepoMock.Verify(m => m.UpdateUser(user), Times.Once());
        }
        [Fact]
        public void TestChangePasswordFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateUser(11, "test").BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.UpdateUser(user)).Callback(() =>
            {
                user.Password = "abc";
            });
            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.GetUser(user.Login)).Returns(users.FirstOrDefault(u => u.Login == user.Login));

            var userService = new UserService(_userRepoMock.Object);

            Assert.Throws<UserNotFoundException>(() => userService.ChangePassword(user.Login, user.Password));
            _userRepoMock.Verify(m => m.UpdateUser(user), Times.Never());
        }
        [Fact]
        public void TestGetAll()
        {
            var users = fixture.PrepareUsersForTest();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetAll()).Returns(users);
            var userService = new UserService(_userRepoMock.Object);

            var actual = userService.getAllUsers();

            Assert.Equal(users.Count, actual.Count);
            _userRepoMock.Verify(m => m.GetAll());
        }
        [Fact]
        public void TestGetUserByIdSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object);

            var actual = userService.GetUser(user.Id);

            Assert.Equal(user, actual);
            _userRepoMock.Verify(m => m.GetUser(user.Id), Times.Once());
        }

        [Fact]
        public void TestGetUserByIdFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateClient().WithId(11).BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();

            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            var userService = new UserService(_userRepoMock.Object);

            Assert.Throws<UserNotFoundException>(() => userService.GetUser(user.Id));
            _userRepoMock.Verify(m => m.GetUser(user.Id), Times.Once());
        }

        [Fact]
        public void TestDeleteUserSuccess()
        {
            var users = fixture.PrepareUsersForTest();
            var user = users[0];
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.DelUser(user)).Callback(() => users.Remove(user));
            var userService = new UserService(_userRepoMock.Object);

            userService.DeleteUser(user.Id);

            Assert.Equal(9, users.Count);
            _userRepoMock.Verify(m => m.DelUser(user), Times.Once());
        }
        [Fact]
        public void TestDeleteUserFailure()
        {
            var users = fixture.PrepareUsersForTest();
            var user = userOM.CreateClient().WithId(11).BuildCoreModel();
            Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
            _userRepoMock.Setup(m => m.GetUser(user.Id)).Returns(users.FirstOrDefault(u => u.Id == user.Id));
            _userRepoMock.Setup(m => m.DelUser(user)).Callback(() => users.Remove(user));
            var userService = new UserService(_userRepoMock.Object);


            Assert.Throws<UserNotFoundException>(() => userService.DeleteUser(user.Id));
            _userRepoMock.Verify(m => m.GetUser(user.Id), Times.Once());
            _userRepoMock.Verify(m => m.DelUser(user), Times.Never());
        }
    }
}
