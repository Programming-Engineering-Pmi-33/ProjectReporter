﻿using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public interface IProjectContractReconstructionFactory
    {
        ProjectContract Create(Project project);
    }
}