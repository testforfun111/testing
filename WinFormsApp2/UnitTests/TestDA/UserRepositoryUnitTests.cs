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

namespace UnitTests.UnitTests.TestDA
{
    [AllureOwner("VHD")]
    [AllureSuite("DA Unit Tests")]
    [AllureSubSuite("UserRepositoty Unit tests")]
    [TestCaseOrderer(ordererTypeName: "UnitTests.Order.RandomOrder",
        ordererAssemblyName: "UnitTests")]
    public class UserRepositoryUnitTests
    {
        private IUserRepository _userRepository;
        private DBFixture _dbFixture;
        private UserObjectMother _userObjectMother;
        public UserRepositoryUnitTests()
        {
            _dbFixture = new DBFixture();
            _userRepository = new UserRepository(_dbFixture._dbContextFactory.get_db_context());
            _userObjectMother = new UserObjectMother();
        }
        [Fact]
        public void TestGetUserById()
        {
            var users = _dbFixture.AddUsers(10);
            var expected = users[0];

            var actual = _userRepository.GetUser(expected.Id);

            Assert.Equivalent(expected, actual);
        }
        [Fact]
        public void TestGetUserByLogin()
        {
            var users = _dbFixture.AddUsers(10);
            var expected = users.First();

            var actual = _userRepository.GetUser(expected.Login);

            Assert.Equivalent(expected, actual);
        }
        
        [Fact]
        public void TestDelete()
        {
            var users = _dbFixture.AddUsers(10);
            var user = users.First();

            _userRepository.DelUser(user);

            var actual = _userRepository.GetUser(user.Id);
            Assert.Null(actual);
        }
        [Fact]
        public void TestUpdate()
        {
            var users = _dbFixture.AddUsers(10);
            var user = _userObjectMother.CreateUser(users.First().Id, "abc").WithLogin("testt").WithPassword("123").BuildCoreModel();

            _userRepository.UpdateUser(user);

            var actual = _userRepository.GetUser(user.Id);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestAddUser()
        {
            var users = _dbFixture.AddUsers(10);
            var user = _userObjectMother.CreateClient().BuildCoreModel();

            _userRepository.AddUser(user);

            var actual = _userRepository.GetUser(11);
            Assert.Equivalent(user, actual);
        }
        [Fact]
        public void TestGetAll()
        {
            var users = _dbFixture.AddUsers(10);

            var actual = _userRepository.GetAll();

            Assert.Equivalent(users, actual);
        }
    }
}
