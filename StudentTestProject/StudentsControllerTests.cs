using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentApp.Controllers;
using StudentApp.Interfaces;
using StudentApp.Models.Dtos;
using StudentApp.Models.Entities;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentAppRepository> _mockRepo;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockRepo = new Mock<IStudentAppRepository>();
            _controller = new StudentsController(_mockRepo.Object, null);
        }

        [Fact]
        public async Task AddStudent_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.AddStudent(new CreateStudentDto());

            // Assert
            var actionResult = Assert.IsType<ActionResult<ICollection<Student>>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Need to fill required fields", badRequestResult.Value);
        }

        

        [Fact]
        public async Task GetStudent_ReturnsNotFound_WhenNoStudentsExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetStudent()).ReturnsAsync(new List<Student>());

            // Act
            var result = await _controller.GetStudent();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetStudentById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetStudentById(It.IsAny<int>())).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.GetStudentById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Student not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetStudentById(It.IsAny<int>())).ReturnsAsync((Student)null);

            // Act
            var result = await _controller.DeleteStudent(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ICollection<Student>>>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Student not found", notFoundResult.Value);
        }


        [Fact]
        public async Task UpdateStudent_ReturnsBadRequest_WhenRequestIsEmpty()
        {
            // Act
            var result = await _controller.UpdateStudent(1, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Empty request", badRequestResult.Value);
        }
    }
}
