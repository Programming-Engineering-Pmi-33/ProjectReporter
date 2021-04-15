using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using ProjectReporter.Modules.GroupsService.Repository;
using ProjectReporter.Modules.GroupsService.Repository.Factories;
using ProjectReporter.Modules.GroupsService.Storage;
using Task = System.Threading.Tasks.Task;

namespace ProjectReporter.UnitTests.GroupsService.Repository
{
    [TestFixture]
    class GroupsRepository_When_GettingReports
    {
        private GroupsStorage _storage;
        private Report _storageReport;

        [SetUp]
        public async Task Setup()
        {
            _storageReport = new Report
            {
                UserId = "user 1",
                Done = "done 1",
                Planned = "planned 1",
                Issues = "issues 1",
                Points = 1,
                ProjectId = 1,
                TaskId = 1
            };

            var options = new DbContextOptionsBuilder<GroupsStorage>()
                .UseInMemoryDatabase("project_reporter")
                .Options;
            _storage = new GroupsStorage(options);

            _storage.Reports.RemoveRange(_storage.Reports.ToList());
            await _storage.SaveChangesAsync();

            await _storage.Reports.AddAsync(_storageReport);
            await _storage.SaveChangesAsync();
        }

        [TearDown]
        public void Finish()
        {
            _storage.Dispose();
        }

        [Test]
        public async Task Then_FilteredReportModelsReturned()
        {
            // Arrange
            var wrongReport = new Report
            {
                ProjectId = 2,
                UserId = "user 2",
                TaskId = 2,
                Done = "done 2",
                Planned = "planned 2",
                Issues = "issues 2",
                Points = 2
            };
            await _storage.Reports.AddAsync(wrongReport);
            await _storage.SaveChangesAsync();

            var reconstructionFactory = Substitute.For<IStorageReportReconstructionFactory>();
            var reportModel =
                new Modules.GroupsService.Repository.Models.Report(1,
                1,
                "done 1",
                "planned 1",
                "issues 1",
                "user 1",
                1,
                _storage.Reports.First().Id);
            reconstructionFactory.Create(_storageReport)
                .Returns(reportModel);
            var repository = new GroupsRepository(_storage, null, null, null, null, reconstructionFactory, null, null, null, null, null);

            // Act
            var result = await repository.GetReport(_storage.Reports.First().Id);

            // Assert
            result.Should().Be(reportModel);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Arrange
            var reconstructionFactory = Substitute.For<IStorageReportReconstructionFactory>();
            var reportModel =
                new Modules.GroupsService.Repository.Models.Report(1,
                1,
                "done1",
                "planned 1",
                "issues 1",
                "user 1",
                1,
                1);
            reconstructionFactory.Create(_storageReport)
                .Returns(reportModel);
            var repository = new GroupsRepository(_storage, null, null, null, null, reconstructionFactory, null, null, null, null, null);

            // Act
            await repository.GetReport(_storage.Reports.First().Id);

            // Assert
            reconstructionFactory.Received().Create(_storageReport);
        }
    }
}
