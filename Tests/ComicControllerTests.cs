using System.Threading.Tasks;
using ComicSTore.Controllers;
using ComicSTore.Data;
using ComicSTore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ComicSTore.Tests
{
    public class ComicControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new ComicController(context);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ValidComic_RedirectsToIndex()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new ComicController(context);
            var comic = new Comic
            {
                // ...existing properties...
                Title = "Test Comic",
                Author = "Author",
                Publisher = "Publisher",
                Price = 9.99M,
                Description = "Test description",
                CoverImageUrl = "http://example.com/image.jpg"
            };

            // Act
            var result = await controller.Create(comic);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
