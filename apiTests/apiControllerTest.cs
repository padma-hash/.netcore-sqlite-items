using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsAPI.Controllers;
using ItemsAPI.Model;
using ItemsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Xunit;



namespace apiTests
{
    public class apiControllerTest
    {
        ItemsController _controller;
        IItemRepository _service;


        public apiControllerTest()
        {
            _service = new FakeService();
            _controller = new ItemsController(_service);
      
        }


        [Fact]
        public  void GetById_ReturnsNotFoundResult_NonExistentId()
        {
            // Act
            var notFoundResult =_controller.GetItembyId(5);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }


        [Fact]
        public async Task GetAllItems_ReturnsOkResult()
        {
            // Act
            var actionResult = await _controller.GetItem();
          
            // Assert
            Assert.IsType<ActionResult<IEnumerable<Items>>>(actionResult);
           
        }

        [Fact]
        public void GetById_ValidId_ReturnsItem()
        {
            // Arrange
            var testId = 2;
            // Act
            var okResult =  _controller.GetItembyId(testId);
            var statusCodeResult = (IStatusCodeActionResult)okResult;
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(200, statusCodeResult.StatusCode);

        }
        [Fact]
        public async Task Create_ReturnsBadRequest_NameMissing()
        {
            // Arrange
            var newItem = new Items()
            {
               
                Price = 100
            };
       
            // Act
            var badResponse = await _controller.Create(newItem);
            var statusCodeResult = (IStatusCodeActionResult) badResponse;
            // Assert
        
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_NameNotAlphanumericSpace()
        {
            // Arrange
            var newItem = new Items()
            {
                ItemName = "*&^&^%%$FAH",
                Price = 100
            };

            // Act
            var badResponse = await _controller.Create(newItem);
            var statusCodeResult = (IStatusCodeActionResult)badResponse;
            // Assert

            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_PriceNull()
        {
            // Arrange
            var newItem = new Items()
            {
                ItemName = "*&^&^%%$FAH",
                
            };

            // Act
            var badResponse = await _controller.Create(newItem);
            var statusCodeResult = (IStatusCodeActionResult)badResponse;
            // Assert

            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsOkRequest_ValidInputs()
        {
            // Arrange
            var newItem = new Items()
            {
                ItemName = "Item4",
                Price = 100
            };

            // Act
            var OKResponse = await _controller.Create(newItem);
            var statusCodeResult = (IStatusCodeActionResult)OKResponse;
            // Assert

            Assert.Equal(201, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task Delete_ReturnsOkRequest_ValidInputs()
        {
            // Arrange
            var deleteItemId = 1;

            // Act
            var OKResponse = await _controller.Delete(deleteItemId);
            var statusCodeResult = (IStatusCodeActionResult)OKResponse;
            // Assert

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task Delete_Returns400Request_BadInputs()
        {
            // Arrange
            var deleteItemId = 38;

            // Act
            var BadResponse = await _controller.Delete(deleteItemId);
            var statusCodeResult = (IStatusCodeActionResult)BadResponse;
            // Assert

            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Update_Returns400Request_InvalidId()
        {
            // Arrange
            var updatedItems = new Items()
            {
                ItemId = 789,
                ItemName = "Item4",
                Price = 100
            };
           

            // Act
            var BadResponse = await _controller.Update(updatedItems);
            var statusCodeResult = (IStatusCodeActionResult)BadResponse;
            // Assert

            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Update_Returns200Request_ValidRequest()
        {
            // Arrange
            var updatedItems = new Items()
            {
                ItemId = 1,
                ItemName = "Item4",
                Price = 100
            };

            // Act
            var OkResponse = await _controller.Update(updatedItems);
            var statusCodeResult = (IStatusCodeActionResult)OkResponse;
            // Assert

            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task Update_Returns400Request_InvalidRequest()
        {
            // Arrange
            var updatedItems = new Items()
            {
                ItemId = 1,
                ItemName = "%#%^^^^#%$",
                Price = 100
            };

            // Act
            var BadResponse = await _controller.Update(updatedItems);
            var statusCodeResult = (IStatusCodeActionResult)BadResponse;
            // Assert

            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MaxPrice_Returns200Request_ValidItem()
        {
            // Arrange
            var itemName = "Item2";
            // Act
            var okResponse = await _controller.MaxPrice(itemName);
            var statusCodeResult = (IStatusCodeActionResult)okResponse;
            // Assert

            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MaxPrice_Returns400Request_InvalidItem()
        {
            // Arrange
            var itemName = "Item90";
            // Act
            var NotFoundResponse = await _controller.MaxPrice(itemName);
            var statusCodeResult = (IStatusCodeActionResult)NotFoundResponse;
            // Assert

            Assert.Equal(404, statusCodeResult.StatusCode);
        }
    }
}
