using Allure.Xunit.Attributes;
using ItegrationalTests.Fixture;
using Models;
using Services;
using Repository;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ObjectMothers;
using Xunit;

namespace ItegrationalTests.IntegrationalTests
{
    [AllureOwner("VHD")]
    [AllureSuite("Integrational Tests")]
    [AllureSubSuite("UserIntegrational Tests")]
    public class UserIntegrationTests
    {
        private IntegrationFixture _fixture;
        private UserService _userService;
        private UserObjectMother userOM = new UserObjectMother();
        public UserIntegrationTests()
        {
            _fixture = new IntegrationFixture();
            var _userRepo = new UserRepository(_fixture._dbContextFactory.get_db_context());
            _userService = new UserService(_userRepo);
        }
        [SkippableFact]
        public void TestLogin()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var users = _fixture.AddUsers();
            var user = users.First();

            var actual = _userService.LogIn(user.Login, user.Password);

            Assert.Equal(user.Login, actual.Login);
            Assert.Equal(user.Password, actual.Password);
        }
        [SkippableFact]
        public void TestRegister()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var users = _fixture.AddUsers();

            var user = userOM.CreateClient().WithId(101).WithName("abc").WithLogin("abcaapp").BuildCoreModel();
            var cnt = _fixture._dbContextFactory.get_db_context().users.Count();

            _userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role);

            Assert.Equal(cnt + 1, _fixture._dbContextFactory.get_db_context().users.Count());
        }
        [SkippableFact]
        public void TestChangePassword()
        {
            Skip.If(IntegrationFixture.SkipTest);
            var users = _fixture.AddUsers();
            var user = users.First();

            _userService.ChangePassword(user.Login, "new");

            var userExpected = _userService.getUserByLogin(user.Login);
            Assert.Equal("new", userExpected.Password);
        }
        [SkippableFact]
        public void TestGetAll()
        {
            var users = _fixture.AddUsers();

            var actual = _userService.getAllUsers();

            Assert.Equal(users.Count(), actual.Count);

        }
    }
}
