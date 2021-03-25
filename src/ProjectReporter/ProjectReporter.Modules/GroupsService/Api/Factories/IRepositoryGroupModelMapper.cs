﻿using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public interface IRepositoryGroupModelMapper
    {
        Group Map(GroupContract contract, string ownerId);
    }
}