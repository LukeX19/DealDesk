using DealDesk.DataAccess.Entities;
using DealDesk.DataAccess.Exceptions;
using DealDesk.DataAccess.Repositories;

namespace DealDesk.UnitTests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests : TestBase
    {
        private ProductRepository _productRepository;

        [TestInitialize]
        public void Setup()
        {
            _productRepository = new ProductRepository(_context);
        }

        [TestMethod]
        public async Task Create_ValidProduct_AddsProductToDatabase()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StandardPrice = 100.0M };

            // Act
            var createdProductId = await _productRepository.Create(product);
            var retrievedProduct = await _productRepository.GetById(createdProductId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(product.Name, retrievedProduct.Name);
            Assert.AreEqual(product.StandardPrice, retrievedProduct.StandardPrice);
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Name = "Product 1", StandardPrice = 50.0M },
                new Product { Name = "Product 2", StandardPrice = 75.0M }
            };
            foreach (var product in products)
            {
                await _productRepository.Create(product);
            }

            // Act
            var retrievedProducts = await _productRepository.GetAll();

            // Assert
            Assert.AreEqual(products.Count, retrievedProducts.Count);
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsCorrectProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StandardPrice = 100.0M };
            var createdProductId = await _productRepository.Create(product);

            // Act
            var retrievedProduct = await _productRepository.GetById(createdProductId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(product.Name, retrievedProduct.Name);
            Assert.AreEqual(product.StandardPrice, retrievedProduct.StandardPrice);
        }

        [TestMethod]
        public async Task GetById_InvalidId_ThrowsEntityNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _productRepository.GetById(999);
            });
        }

        [TestMethod]
        public async Task Update_ValidProduct_UpdatesProductInDatabase()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StandardPrice = 100.0M };
            var createdProductId = await _productRepository.Create(product);
            var updatedProduct = new Product { Id = createdProductId, Name = "Updated Product", StandardPrice = 150.0M };

            // Act
            await _productRepository.Update(createdProductId, updatedProduct);
            var retrievedProduct = await _productRepository.GetById(createdProductId);

            // Assert
            Assert.AreEqual(updatedProduct.Name, retrievedProduct.Name);
            Assert.AreEqual(updatedProduct.StandardPrice, retrievedProduct.StandardPrice);
        }

        [TestMethod]
        public async Task Update_InvalidId_ThrowsEntityNotFoundException()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StandardPrice = 100.0M };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _productRepository.Update(999, product);
            });
        }

        [TestMethod]
        public async Task Delete_ValidId_RemovesProductFromDatabase()
        {
            // Arrange
            var product = new Product { Name = "Test Product", StandardPrice = 100.0M };
            var createdProductId = await _productRepository.Create(product);

            // Act
            await _productRepository.Delete(createdProductId);
            var products = await _productRepository.GetAll();

            // Assert
            Assert.AreEqual(0, products.Count);
        }

        [TestMethod]
        public async Task Delete_InvalidId_ThrowsEntityNotFoundException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await _productRepository.Delete(999);
            });
        }
    }
}
