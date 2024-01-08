using DAL.EF;
using DAL.Entities;
using DAL.Tests.Repositories;
using Microsoft.EntityFrameworkCore;



namespace DAL.Tests
{
    public class RepositoryUnitTests
    {
        // �����, �� ������ ������ Create ���������� �������� ���� ��'��� Sanatorium �� DbSet � �������� ���� �����.
        [Fact]
        public void Create_InputSanatoriumInstance_CalledAddMethodOfDBSetWithSanatoriumInstance()
        {
            // Arrange   ��������� ����� ��������� ���� �����
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // ��������� ������ ��������� �� DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            // ����������� mockContext, ��� ��� ������� Set �� �������� ������ mockDBSet ����� DBSet<Sanatorium>
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // ��������� ���������� ��� ����������
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // ���������� ��'��� Sanatorium
            Sanatorium expectedSanatorium = new Mock<Sanatorium>().Object;

            // Act
            repository.Create(expectedSanatorium);

            // Assert  
            mockDbSet.Verify(dbSet => dbSet.Add(expectedSanatorium), Times.Once());
        }

        // �����, �� ������ ������ Get ���������� ������� ��������� ��'��� Sanatorium �� ���� ���������������.
        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange 
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // ��������� ������ ��������� �� DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // ��������� ���������� ��� ����������
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // ���������� ��'��� Sanatorium � ������ ���������������
            Sanatorium expectedSanatorium = new Sanatorium { SanatoriumID = 1 };

            // ������������ �������� ������ DbSet
            mockDbSet.Setup(mock => mock.Find(expectedSanatorium.SanatoriumID)).Returns(expectedSanatorium);

            // Act
            var actualSanatorium = repository.Get(expectedSanatorium.SanatoriumID);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedSanatorium.SanatoriumID), Times.Once());
            Assert.Equal(expectedSanatorium, actualSanatorium);
        }

        // �����, �� ������ ������ Delete ���������� �������� ������� ��'��� Sanatorium �� ���� ���������������.
        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<SanatoriumDbContext>().Options;

            // ��������� ������ ��������� �� DbSet
            var mockContext = new Mock<SanatoriumDbContext>(opt);
            var mockDbSet = new Mock<DbSet<Sanatorium>>();
            mockContext.Setup(context => context.Set<Sanatorium>()).Returns(mockDbSet.Object);

            // ��������� ���������� ��� ����������
            var repository = new TestSanatoriumRepository(mockContext.Object);

            // ���������� ��'��� Sanatorium � ������ ���������������
            Sanatorium expectedSanatorium = new Sanatorium { SanatoriumID = 1 };

            // ������������ �������� ������ DbSet
            mockDbSet.Setup(mock => mock.Find(expectedSanatorium.SanatoriumID)).Returns(expectedSanatorium);

            // Act
            repository.Delete(expectedSanatorium.SanatoriumID);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedSanatorium.SanatoriumID), Times.Once());
            mockDbSet.Verify(dbSet => dbSet.Remove(It.Is<Sanatorium>(e => e == expectedSanatorium)), Times.Once());
        }
    }
}