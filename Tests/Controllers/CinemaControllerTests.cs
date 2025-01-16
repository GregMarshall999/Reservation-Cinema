using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Data;
using ReservationCinema.Models;
using Xunit;

namespace ReservationCinema.Tests.Controllers
{
    public class CinemaControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CinemaTestDb")
                .Options;
            var context = new ApplicationDbContext(options);

            context.Cinemas.RemoveRange(context.Cinemas);
            context.SaveChanges();

            context.Cinemas.AddRange(
                new Cinema { Id = 1, Nom = "Cinema One", Ville = "City A", Rue = "Street A", Numero = "123" },
                new Cinema { Id = 2, Nom = "Cinema Two", Ville = "City B", Rue = "Street B", Numero = "456" }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewWithListOfCinemas()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Cinema>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_ReturnsCinema_WhenIdIsValid()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Cinema>(viewResult.Model);
            Assert.Equal("Cinema One", model.Nom);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);

            var result = await controller.Details(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_AddsCinemaAndRedirects_WhenModelStateIsValid()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);
            var newCinema = new Cinema { Id = 3, Nom = "Cinema Three", Ville = "City C", Rue = "Street C", Numero = "789" };

            var result = await controller.Create(newCinema);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal(3, context.Cinemas.Count());
        }

        //Fails. Entity Tracking Issue?
        [Fact]
        public async Task Edit_UpdatesCinemaAndRedirects_WhenModelStateIsValid()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);
            var updatedCinema = new Cinema { Id = 1, Nom = "Updated Cinema", Ville = "City A", Rue = "Updated Street", Numero = "123" };

            //Failure Cause.
            //Can't track the update of an entity?
            var result = await controller.Edit(1, updatedCinema);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            var cinema = context.Cinemas.First(c => c.Id == 1);
            Assert.Equal("Updated Cinema", cinema.Nom);
            Assert.Equal("Updated Street", cinema.Rue);
        }
        
        //Fails, DbContext Issue?
        [Fact]
        public async Task Delete_RemovesCinemaAndRedirects_WhenIdIsValid()
        {
            //Failure Cause.
            //Context isn't flushed before the seeding?
            //Why does it flush with all the other tests?
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);

            var result = await controller.DeleteConfirmed(1);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Null(context.Cinemas.FirstOrDefault(c => c.Id == 1));
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var controller = new CinemaController(context);

            var result = await controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
