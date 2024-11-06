using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Exceptions;
using DealDesk.DataAccess.Repositories;

namespace DealDesk.UnitTests.Repositories
{
    [TestClass]
    public class CustomerRepositoryTests : TestBase
    {
        private CustomerRepository _customerRepository;

        [TestInitialize]
        public void Setup()
        {
            _customerRepository = new CustomerRepository(_context);
        }

        [TestMethod]
        public async Task Create_ValidCustomer_AddsCustomerToDatabase()
        {
            // Arrange
            var customer = new Customer { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };

            // Act
            var createdCustomerId = await _customerRepository.Create(customer);
            var retrievedCustomer = await _customerRepository.GetById(createdCustomerId);

            // Assert
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual(customer.Name, retrievedCustomer.Name);
            CollectionAssert.AreEqual(customer.DiscountStrategies.ToList(), retrievedCustomer.DiscountStrategies.ToList());
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1", DiscountStrategies = new List<string> { "volume" } },
                new Customer { Name = "Customer 2", DiscountStrategies = new List<string> { "seasonal" } }
            };
            foreach (var customer in customers)
            {
                await _customerRepository.Create(customer);
            }

            // Act
            var retrievedCustomers = await _customerRepository.GetAll();

            // Assert
            Assert.AreEqual(customers.Count, retrievedCustomers.Count);
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsCorrectCustomer()
        {
            // Arrange
            var customer = new Customer { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var createdCustomerId = await _customerRepository.Create(customer);

            // Act
            var retrievedCustomer = await _customerRepository.GetById(createdCustomerId);

            // Assert
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual(customer.Name, retrievedCustomer.Name);
            CollectionAssert.AreEqual(customer.DiscountStrategies.ToList(), retrievedCustomer.DiscountStrategies.ToList());
        }

        [TestMethod]
        public async Task GetById_InvalidId_ThrowsEntityNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _customerRepository.GetById(999);
            });
        }

        [TestMethod]
        public async Task Update_ValidCustomer_UpdatesCustomerInDatabase()
        {
            // Arrange
            var customer = new Customer { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var createdCustomerId = await _customerRepository.Create(customer);
            var updatedCustomer = new Customer { Id = createdCustomerId, Name = "Updated Customer", DiscountStrategies = new List<string> { "seasonal" } };

            // Act
            await _customerRepository.Update(createdCustomerId, updatedCustomer);
            var retrievedCustomer = await _customerRepository.GetById(createdCustomerId);

            // Assert
            Assert.AreEqual(updatedCustomer.Name, retrievedCustomer.Name);
            CollectionAssert.AreEqual(updatedCustomer.DiscountStrategies.ToList(), retrievedCustomer.DiscountStrategies.ToList());
        }

        [TestMethod]
        public async Task Update_InvalidId_ThrowsEntityNotFoundException()
        {
            // Arrange
            var customer = new Customer { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _customerRepository.Update(999, customer);
            });
        }

        [TestMethod]
        public async Task Delete_ValidId_RemovesCustomerFromDatabase()
        {
            // Arrange
            var customer = new Customer { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var createdCustomerId = await _customerRepository.Create(customer);

            // Act
            await _customerRepository.Delete(createdCustomerId);
            var customers = await _customerRepository.GetAll();

            // Assert
            Assert.AreEqual(0, customers.Count);
        }

        [TestMethod]
        public async Task Delete_InvalidId_ThrowsEntityNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _customerRepository.Delete(999);
            });
        }
    }
}
