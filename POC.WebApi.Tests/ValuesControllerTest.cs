using Microsoft.AspNetCore.Mvc;
using POC.WebApi.Controllers;
using POC.WebApi.Models;
using POC.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

// Référence : https://code-maze.com/unit-testing-aspnetcore-web-api/#creatingtestingproject

namespace POC.WebApi.Tests
{
    public class ValuesControllerTest
    {
        ValuesController _controller;
        IValuesService _service;

        public ValuesControllerTest()
        {
            _service = new ValuesServiceFake();
            _controller = new ValuesController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<ValuesItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(999);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 2;

            // Act
            var okResult = _controller.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 2;

            // Act
            var okResult = _controller.Get(testId).Result as OkObjectResult;

            // Assert
            Assert.IsType<ValuesItem>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as ValuesItem).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ValuesItem()
            {
                Value = string.Empty
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            ValuesItem testItem = new ValuesItem()
            {
                Id = 4,
                Value = "Valeur4"
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            ValuesItem testItem = new ValuesItem()
            {
                Id = 4,
                Value = "Valeur4"
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ValuesItem;

            // Assert
            Assert.IsType<ValuesItem>(item);
            Assert.Equal("Valeur4", item.Value);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 0;

            // Act
            var badResponse = _controller.Delete(notExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = 2;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 2;

            // Act
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.Equal(2, _service.GetAllItems().Count());
        }
    }
}