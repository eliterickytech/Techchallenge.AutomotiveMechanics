using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly Mock<DbSet<User>> _mockSet = new Mock<DbSet<User>>();
        private readonly Mock<ApplicationDbContext> _mockContext = new Mock<ApplicationDbContext>();
        private readonly Mock<IConfiguration> _mockConfiguration = new Mock<IConfiguration>();

        public UserRepositoryTests()
        {
            _mockContext.Setup(m => m.Users).Returns(_mockSet.Object);
        }

        [Fact]
        public async Task UserExists_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var userRepository = new UserRepository(_mockContext.Object, _mockConfiguration.Object);
            _mockSet.Setup(m => m.AnyAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

            // Act
            var result = await userRepository.UserExists("test@example.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Register_ShouldSaveUser_WhenUserDoesNotExist()
        {
            // Arrange
            var userRepository = new UserRepository(_mockContext.Object, _mockConfiguration.Object);
            _mockSet.Setup(x => x.Add(It.IsAny<User>()));
            _mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var user = new User { Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = await userRepository.Register(user, "password123");

            // Assert
            Assert.Equal(1, result);
        }
    }
}
