using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Models;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTests
    {
        [Fact]
        public async Task Details_method_in_jobOffersController()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.JobOffer.Add(new JobOffer() { Id = 1, JobTitle = "job1" });
                context.SaveChanges();
                var controller = new JobOffersController(context);
                var result = await controller.Details(1);
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal(1, ((JobOffer)viewResult.Model).Id);
                Assert.Equal("job1", ((JobOffer)viewResult.Model).JobTitle);
            }
        }

        [Fact]
        public async Task Details_method_in_jobOffersController_should_throw_exception_when_id_is_null()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test1").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.JobOffer.Add(new JobOffer() { Id = 1, JobTitle = "job1" });
                context.SaveChanges();
                var controller = new JobOffersController(context);
                await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Details(null));
            }
        }

        [Fact]
        public async Task When_deleteConfirmed_jobOffer_should_be_deleted()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test2").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.JobOffer.Add(new JobOffer() { Id = 1, JobTitle = "job1" });
                context.JobOffer.Add(new JobOffer() { Id = 2, JobTitle = "job2" });

                context.SaveChanges();
                var controller = new JobOffersController(context);
                var result = await controller.DeleteConfirmed(1);

                Assert.Equal(1, context.JobOffer.Count());
            }
        }

        [Fact]
        public async Task When_Edit_ReturnsEditView_in_CompanyController()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test3").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.Companies.Add(new Company() { Id = 1, Name = "my company" });
                context.SaveChanges();
                var controller = new CompanyController(context);
                var result = await controller.Edit(1);
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal(1, ((Company)viewResult.Model).Id);
                Assert.Equal("my company", ((Company)viewResult.Model).Name);
            }
        }

        [Fact]
        public async Task Edit_method_in_CompanyController_should_throw_exception_when_id_is_null()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test4").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.Companies.Add(new Company() { Id = 1, Name = "my company" });
                context.SaveChanges();
                var controller = new CompanyController(context);
                await Assert.ThrowsAsync<InvalidOperationException>(() => controller.Edit(null));
            }
        }

        [Fact]
        public async Task When_Edit_ReturnsEditView_in_JobOfferController()
        {
            var options = new DbContextOptionsBuilder<WebApplication1Context>().UseInMemoryDatabase(databaseName: "Test5").Options;

            using (var context = new WebApplication1Context(options))
            {
                context.JobOffer.Add(new JobOffer() { Id = 1, JobTitle = "job1" });
                context.SaveChanges();
                var controller = new JobOffersController(context);
                var result = await controller.Edit(1);
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal(1, ((JobOffer)viewResult.Model).Id);
                Assert.Equal("job1", ((JobOffer)viewResult.Model).JobTitle);
            }
        }
    }


}
