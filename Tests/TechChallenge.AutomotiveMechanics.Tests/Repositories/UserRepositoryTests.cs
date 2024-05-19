using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechChallenge.AutomotiveMechanics.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UserRepositoryTests()
        {
            // Configure as opções do contexto usando um banco de dados em memória
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenUserExistsAndPasswordMatches()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var user = new User
                {
                    Id = 1,
                    Name = "Test User",
                    Email = "test@example.com"
                };
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new UserRepository(context, null);
                var token = await repository.Login("test@example.com", "password");

                // Assert
                Assert.NotNull(token);
                Assert.NotEmpty(token);
            }
        }

        [Fact]
        public async Task Register_ReturnsUserId_WhenUserDoesNotExist()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new UserRepository(context, null);
                var user = new User
                {
                    Name = "Test User",
                    Email = "test@example.com"
                };

                // Act
                var userId = await repository.Register(user, "password");

                // Assert
                Assert.NotEqual(0, userId);
            }
        }

        [Fact]
        public async Task UserExists_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var user = new User
                {
                    Name = "Test User",
                    Email = "test@example.com"
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new UserRepository(context, null);
                var userExists = await repository.UserExists("test@example.com");

                // Assert
                Assert.True(userExists);
            }
        }

        [Fact]
        public async Task UserExists_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                // Act
                var repository = new UserRepository(context, null);
                var userExists = await repository.UserExists("nonexistent@example.com");

                // Assert
                Assert.False(userExists);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
