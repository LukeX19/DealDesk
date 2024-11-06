using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.Business.Services;
using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;
using Moq;

namespace DealDesk.UnitTests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private CustomerService _customerService;

        [TestInitialize]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Create_ValidCustomer_ReturnsCustomerId()
        {
            // Arrange
            var customerDto = new CustomerRequest { Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = customerDto.DiscountStrategies };

            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);
            _customerRepositoryMock.Setup(repo => repo.Create(It.IsAny<Customer>(), It.IsAny<CancellationToken>())).ReturnsAsync(customer.Id);

            // Act
            var result = await _customerService.Create(customerDto);

            // Assert
            Assert.AreEqual(customer.Id, result);
            _customerRepositoryMock.Verify(repo => repo.Create(It.Is<Customer>(c => c.Name == customerDto.Name), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfCustomerResponses()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Customer 1", DiscountStrategies = new List<string> { "volume" } }
            };
            var customerResponses = new List<CustomerResponse>
            {
                new CustomerResponse { Id = 1, Name = "Customer 1", DiscountStrategies = new List<string> { "volume" } }
            };

            _customerRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(customers);
            _mapperMock.Setup(m => m.Map<ICollection<CustomerResponse>>(customers)).Returns(customerResponses);

            // Act
            var result = await _customerService.GetAll();

            // Assert
            Assert.AreEqual(customerResponses.Count, result.Count);
            CollectionAssert.AreEqual(customerResponses.ToList(), result.ToList());
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsCustomerResponse()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var customerResponse = new CustomerResponse { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };

            _customerRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(customer);
            _mapperMock.Setup(m => m.Map<CustomerResponse>(customer)).Returns(customerResponse);

            // Act
            var result = await _customerService.GetById(1);

            // Assert
            Assert.AreEqual(customerResponse.Id, result.Id);
            Assert.AreEqual(customerResponse.Name, result.Name);
        }

        [TestMethod]
        public async Task Update_ValidCustomer_UpdatesCustomer()
        {
            // Arrange
            var customerDto = new CustomerRequest { Name = "Updated Customer", DiscountStrategies = new List<string> { "seasonal" } };
            var customer = new Customer { Id = 1, Name = customerDto.Name, DiscountStrategies = customerDto.DiscountStrategies };

            _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);
            _customerRepositoryMock.Setup(repo => repo.Update(1, customer, It.IsAny<CancellationToken>()));

            // Act
            await _customerService.Update(1, customerDto);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Update(1, It.Is<Customer>(c => c.Name == customerDto.Name && c.DiscountStrategies.SequenceEqual(customerDto.DiscountStrategies)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Delete_ValidId_DeletesCustomer()
        {
            // Arrange
            _customerRepositoryMock.Setup(repo => repo.Delete(1, It.IsAny<CancellationToken>()));

            // Act
            await _customerService.Delete(1);

            // Assert
            _customerRepositoryMock.Verify(repo => repo.Delete(1, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
