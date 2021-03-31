﻿using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class RepositoryReportModelMapper: IRepositoryReportModelMapper
    {
        public Report Map(ReportContract contract) =>
            new(contract.Done,
                contract.Planned,
                contract.Issues,
                id: contract.Id);
    }
}