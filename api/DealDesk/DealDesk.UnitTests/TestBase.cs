using DealDesk.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DealDesk.UnitTests
{
    [TestClass]
    public abstract class TestBase
    {
        protected AppDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database per test
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _context = new AppDbContext(options);
        }
    }
}
