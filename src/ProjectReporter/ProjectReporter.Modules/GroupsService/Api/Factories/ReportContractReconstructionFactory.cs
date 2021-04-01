using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class ReportContractReconstructionFactory:IReportContractReconstructionFactory
    {
        public ReportContract Create(Report report) =>
            new()
            { 
                Id = report.Id,
                Done = report.Done,
                Issues = report.Issues,
                Planned = report.Planned
            };
    }
}