using DAL.EF;
using DAL.Entities;
using DAL.Tests.Repositories;
using Microsoft.EntityFrameworkCore;



namespace DAL.Tests
{
    public class RepositoryUnitTests
    {
        // Тестує, чи виклик методу Create репозиторію коректно додає об'єкт Sanatorium до DbSet у контексті бази даних.
        [Fact]
        public void Create_InputSanatoriumInstance_CalledAddMethodOfDBSetWithSanatoriumInstance()
        {
            // Arrange   Створення опцій контексту бази даних
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // Створення макету контексту та DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            // Налаштовуємо mockContext, щоб при виклику Set він повертав двійник mockDBSet класу DBSet<Sanatorium>
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // Створення репозиторію для тестування
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // Очікуваний об'єкт Sanatorium
            Sanatorium expectedSanatorium = new Mock<Sanatorium>().Object;

            // Act
            repository.Create(expectedSanatorium);

            // Assert  
            mockDbSet.Verify(dbSet => dbSet.Add(expectedSanatorium), Times.Once());
        }

        // Тестує, чи виклик методу Get репозиторію повертає коректний об'єкт Sanatorium за його ідентифікатором.
        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange 
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // Створення макету контексту та DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // Створення репозиторію для тестування
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // Очікуваний об'єкт Sanatorium з певним ідентифікатором
            Sanatorium expectedSanatorium = new Sanatorium { SanatoriumID = 1 };

            // Налаштування поведінки макету DbSet
            mockDbSet.Setup(mock => mock.Find(expectedSanatorium.SanatoriumID)).Returns(expectedSanatorium);

            // Act
            var actualSanatorium = repository.Get(expectedSanatorium.SanatoriumID);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedSanatorium.SanatoriumID), Times.Once());
            Assert.Equal(expectedSanatorium, actualSanatorium);
        }

        // Тестує, чи виклик методу Delete репозиторію коректно видаляє об'єкт Sanatorium за його ідентифікатором.
        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // Створення макету контексту та DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // Створення репозиторію для тестування
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // Очікуваний об'єкт Sanatorium з певним ідентифікатором
            Sanatorium expectedSanatorium = new Sanatorium { SanatoriumID = 1 };

            // Налаштування поведінки макету DbSet
            mockDbSet.Setup(mock => mock.Find(expectedSanatorium.SanatoriumID)).Returns(expectedSanatorium);

            // Act
            repository.Delete(expectedSanatorium.SanatoriumID);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedSanatorium.SanatoriumID), Times.Once());
            mockDbSet.Verify(dbSet => dbSet.Remove(It.Is<Sanatorium>(e => e == expectedSanatorium)), Times.Once());
        }
    }
}