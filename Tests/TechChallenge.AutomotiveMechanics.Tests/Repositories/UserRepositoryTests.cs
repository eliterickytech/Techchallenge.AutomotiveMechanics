using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Tests.FakeData;
using Bogus;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Faker<User> _userFaker;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _userFaker = UserFakeData.GetUserFaker();
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            var user = _userFaker.Generate();
            user.PasswordHash = new byte[] { 0x20, 0x30, 0x40, 0x50 };
            user.PasswordSalt = new byte[] { 0x20, 0x30, 0x40, 0x50 };
            var password = "TestPassword";

            _userRepositoryMock.Setup(repo => repo.Login(user.Email, password)).ReturnsAsync("fakeToken");

            var result = await _userRepositoryMock.Object.Login(user.Email, password);

            Assert.NotNull(result);
            Assert.Equal("fakeToken", result);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(repo => repo.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string)null);

            var result = await _userRepositoryMock.Object.Login("nonexistent@example.com", "password");

            Assert.Null(result);
        }

        [Fact]
        public async Task Register_ReturnsZero_WhenUserAlreadyExists()
        {
            var user = _userFaker.Generate();
            var password = "TestPassword";

            _userRepositoryMock.Setup(repo => repo.Register(user, password)).ReturnsAsync(0);

            var result = await _userRepositoryMock.Object.Register(user, password);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Register_ReturnsUserId_WhenUserDoesNotExist()
        {
            var user = _userFaker.Generate();
            var password = "TestPassword";

            _userRepositoryMock.Setup(repo => repo.Register(user, password)).ReturnsAsync(1);

            var result = await _userRepositoryMock.Object.Register(user, password);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UserExists_ReturnsTrue_WhenUserExists()
        {
            var user = _userFaker.Generate();

            _userRepositoryMock.Setup(repo => repo.UserExists(user.Email)).ReturnsAsync(true);

            var result = await _userRepositoryMock.Object.UserExists(user.Email);

            Assert.True(result);
        }

        [Fact]
        public async Task UserExists_ReturnsFalse_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(repo => repo.UserExists(It.IsAny<string>())).ReturnsAsync(false);

            var result = await _userRepositoryMock.Object.UserExists("mail@mail.com");

            Assert.False(result);
        }
    }
}
