﻿using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.Business.Services;
using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Interfaces;
using Moq;

namespace DealDesk.UnitTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private ProductService _productService;

        [TestInitialize]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_productRepositoryMock.Object, _customerRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Create_ValidProduct_ReturnsProductId()
        {
            // Arrange
            var productDto = new ProductRequest { Name = "Test Product", StandardPrice = 100M };
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };

            _mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _productRepositoryMock.Setup(repo => repo.Create(It.IsAny<Product>(), It.IsAny<CancellationToken>())).ReturnsAsync(product.Id);

            // Act
            var result = await _productService.Create(productDto);

            // Assert
            Assert.AreEqual(product.Id, result);
            _productRepositoryMock.Verify(repo => repo.Create(It.Is<Product>(p => p.Name == productDto.Name), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfProductResponses()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product", StandardPrice = 100M } };
            var productResponses = new List<ProductResponse> { new ProductResponse { Id = 1, Name = "Test Product", StandardPrice = 100M } };

            _productRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<ICollection<ProductResponse>>(products)).Returns(productResponses);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.AreEqual(productResponses.Count, result.Count);
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsProductResponse()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var productResponse = new ProductResponse { Id = 1, Name = "Test Product", StandardPrice = 100M };

            _productRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductResponse>(product)).Returns(productResponse);

            // Act
            var result = await _productService.GetById(1);

            // Assert
            Assert.AreEqual(productResponse.Id, result.Id);
        }

        [TestMethod]
        public async Task Update_ValidProduct_UpdatesProduct()
        {
            // Arrange
            var productDto = new ProductRequest { Name = "Updated Product", StandardPrice = 150M };
            var product = new Product { Id = 1, Name = productDto.Name, StandardPrice = productDto.StandardPrice };

            _mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _productRepositoryMock.Setup(repo => repo.Update(1, product, It.IsAny<CancellationToken>()));

            // Act
            await _productService.Update(1, productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Update(1, It.Is<Product>(p => p.Name == productDto.Name && p.StandardPrice == productDto.StandardPrice), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Delete_ValidId_DeletesProduct()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.Delete(1, It.IsAny<CancellationToken>()));

            // Act
            await _productService.Delete(1);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Delete(1, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetDiscountedPrice_NoDiscount_ReturnsTotalPrice()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string>() };
            var quantity = 2;
            var expectedTotalPrice = product.StandardPrice * quantity;

            _productRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            _customerRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

            // Act
            var result = await _productService.GetDiscountedPrice(1, quantity, 1);

            // Assert
            Assert.AreEqual(expectedTotalPrice, result);
        }

        [TestMethod]
        public async Task GetDiscountedPrice_VolumeDiscount_AppliesDiscount()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string> { "volume" } };
            var quantity = 10;
            var expectedTotalPrice = product.StandardPrice * quantity * 0.9M; // Volume Strategy Discount value

            _productRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            _customerRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

            // Act
            var result = await _productService.GetDiscountedPrice(1, quantity, 1);

            // Assert
            Assert.AreEqual(expectedTotalPrice, result, delta: 0.01M);
        }

        [TestMethod]
        public async Task GetDiscountedPrice_SeasonalDiscount_AppliesDiscount()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string> { "seasonal" } };
            var quantity = 5;
            var expectedTotalPrice = product.StandardPrice * quantity * 0.85M; // Seasonal Strategy Discount value

            _productRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            _customerRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

            // Act
            var result = await _productService.GetDiscountedPrice(1, quantity, 1);

            // Assert
            Assert.AreEqual(expectedTotalPrice, result, delta: 0.01M);
        }

        [TestMethod]
        public async Task GetDiscountedPrice_VolumeAndSeasonalDiscount_AppliesBothDiscounts()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string> { "volume", "seasonal" } };
            var quantity = 10;

            var volumeDiscountedPrice = product.StandardPrice * quantity * 0.9M; // Volume Strategy Discount value
            var expectedTotalPrice = volumeDiscountedPrice * 0.85M; // Seasonal Strategy Discount value

            _productRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            _customerRepositoryMock.Setup(repo => repo.GetById(1, It.IsAny<CancellationToken>())).ReturnsAsync(customer);

            // Act
            var result = await _productService.GetDiscountedPrice(1, quantity, 1);

            // Assert
            Assert.AreEqual(expectedTotalPrice, result, delta: 0.01M);
        }

        [TestMethod]
        public async Task GetDiscountedPrice_InvalidQuantity_ThrowsArgumentException()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Test Product", StandardPrice = 100M };
            var customer = new Customer { Id = 1, Name = "Test Customer", DiscountStrategies = new List<string>() };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _productService.GetDiscountedPrice(1, 0, 1);
            });
        }
    }
}
