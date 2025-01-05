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
using Microsoft.VisualBasic.ApplicationServices;

namespace UnitTests.UnitTests.TestBL.DestroitMethod
{
    [AllureOwner("VHD")]
    [AllureParentSuite("Services Unit tests")]
    [AllureSuite("UserServices Unit tests")]
    [AllureSubSuite("UserService unit tests Destroit Method")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class UserServiceUnitTests
    {
        private DBFixture fixture = new DBFixture();
        private UserObjectMother userOM = new UserObjectMother();
        [AllureBefore]
        public UserServiceUnitTests() { }
        [Fact]
        public void TestLoginSuccessDestroitMethod()
        {

            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            var actual = userService.LogIn(user.Login, user.Password);

            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestLoginFailureDestroitMethod()
        {
            var user = userOM.CreateClient().WithId(200).WithLogin("test 200").WithPassword("test 200").BuildCoreModel();
            fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            Assert.Throws<UserNotFoundException>(() => userService.LogIn(user.Login, user.Password));
        }
        [Fact]
        public void TestRegisterSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateClient().WithId(11).WithName("abc").WithLogin("abcaapp").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role);

            var actual = _userRepo.GetUser(user.Id);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestRegisterFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            Assert.Throws<UserExistException>(() => userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role));
        }
        [Fact]
        public void TestChangePasswordSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            var user = userService.GetUser(users.First().Id);

            userService.ChangePassword(user.Login, "new");

            var actual = _userRepo.GetUser(user.Login);
            Assert.Equivalent(user.Password, actual.Password);
        }
        [Fact]
        public void TestChangePasswordDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = userOM.CreateClient().WithId(100).WithLogin("abcaapp").BuildCoreModel();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);


            Assert.Throws<UserNotFoundException>(() => userService.ChangePassword("dsflkfj", "fsdkjlf"));
        }
        [Fact]
        public void TestGetAllDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            var actual = userService.getAllUsers();

            Assert.Equivalent(users, actual);

        }
        [Fact]
        public void TestGetUserByIdSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            var actual = userService.GetUser(user.Id);

            Assert.Equivalent(user, actual);
        }

        [Fact]
        public void TestGetUserByIdFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            Assert.Throws<UserNotFoundException>(() => userService.GetUser(100));
        }

        [Fact]
        public void TestDeleteUserSuccessDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            var user = users.First();
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            userService.DeleteUser(user.Id);

            var actual = _userRepo.GetUser(user.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestDeleteUserFailureDestroitMethod()
        {
            var users = fixture.AddUsers(10);
            IUserRepository _userRepo = new UserRepository(fixture._dbContextFactory.get_db_context());
            var userService = new UserService(_userRepo);

            Assert.Throws<UserNotFoundException>(() => userService.DeleteUser(100));
        }
    }
}
