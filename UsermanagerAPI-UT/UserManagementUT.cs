using AutoFixture;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Moq;
using MrPrice_Usermanager.Context;
using MrPrice_Usermanager.Models;
using MrPrice_Usermanager.Services;

namespace UsermanagerAPI_UT;

using Moq.EntityFrameworkCore;

public class UserManagementUT
{
    private Mock<UserDbContext> _dbContextMock;
    private Mock<DbSet<User>> _usersDb;

    [SetUp]
    public void Setup()
    {
        _dbContextMock = new Mock<UserDbContext>();
        _usersDb = new Mock<DbSet<User>>();
        var queryable = TestDataHelper.GetFakeEmployeeList().AsQueryable();
        _usersDb.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryable.Provider);
        _usersDb.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryable.Expression);
        _usersDb.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        _usersDb.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());


        // _dbContextMock.Setup(x => x.Users).ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());
        // _usersDb.Setup(x => x.Add(It.IsAny<User>()))
        //     .Callback<User>(s=>TestDataHelper.GetFakeEmployeeList().Add(s));
        _dbContextMock.Setup(x => x.Users).Returns(_usersDb.Object);

        // _dbContextMock.Setup<DbSet<User>>(x => x.Users)
        //     .ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());
    }

    [Test]
    public async Task GetEmployees()
    {
        //Arrange

        //Act
        Logger<UserManagementService> umsLogger = new Logger<UserManagementService>(new LoggerFactory());
        IUserManagementService userManagementService = new UserManagementService(_dbContextMock.Object, umsLogger);
        var users = (userManagementService.GetUsers());
        //Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task AddUser_SuccessFullyAddNewUser()
    {
        //Arrange
        var dbContextMock = new Mock<UserDbContext>();
        var usersDb = new Mock<DbSet<User>>();

        // dbContextMock.Setup(x => x.Users).ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());
        dbContextMock.Setup(x => x.Users).Returns(_usersDb.Object);

        //Act
        Logger<UserManagementService> umsLogger = new Logger<UserManagementService>(new LoggerFactory());
        IUserManagementService userManagementService = new UserManagementService(_dbContextMock.Object, umsLogger);
        var status = (userManagementService.AddUser("John", "Deere", "JDeere@gmail.com"));
        var userCount = (userManagementService.UserCount());
        var users = (userManagementService.GetUsers());
        var nUser = new User("John", "Doe", "J.D@gmail.com");
        //Assert
        usersDb.Verify(x => x.Add(nUser), Times.Once);
        dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        // Assert.That(status, Is.EqualTo(true));
        // Assert.That(userCount, Is.EqualTo(3));
    }

    [Test]
    public async Task GetUseCount()
    {
        //Arrange

        //Act
        Logger<UserManagementService> umsLogger = new Logger<UserManagementService>(new LoggerFactory());
        IUserManagementService userManagementService = new UserManagementService(_dbContextMock.Object, umsLogger);
        var count = (userManagementService.UserCount());
        //Assert
        Assert.That(count, Is.EqualTo(2));
    }

    [Test]
    public async Task AddUser_FailsOnExistingUser()
    {
        //Arrange

        //Act
        Logger<UserManagementService> umsLogger = new Logger<UserManagementService>(new LoggerFactory());
        IUserManagementService userManagementService = new UserManagementService(_dbContextMock.Object, umsLogger);
        var status = (userManagementService.AddUser("John", "Doe", "J.D@gmail.com"));
        var nUser = new User("John", "Doe", "J.D@gmail.com");
        //Assert
        _usersDb.Verify(x => x.Add(It.Is<User>(y => y == nUser)), Times.Once);
        _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.That(status, Is.EqualTo(false));
    }
}