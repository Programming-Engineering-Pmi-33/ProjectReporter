using Microsoft.AspNetCore.SignalR;
using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class RepositoryReportModelMapper: IRepositoryReportModelMapper
    {
        public Report Map(ReportContract contract, string userId) =>
            new(contract.Done,
                contract.Planned,
                contract.Issues,
                userId,
                id: contract.Id);
    }
}