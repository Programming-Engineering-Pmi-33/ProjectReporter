using ProjectReporter.Modules.GroupsService.Api.Contracts;
using ProjectReporter.Modules.GroupsService.Repository.Models;

namespace ProjectReporter.Modules.GroupsService.Api.Factories
{
    public class ProjectContractReconstructionFactory: IProjectContractReconstructionFactory
    {
        public ProjectContract Create(Project project) =>
            new()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                GitLink = project.GitLink
            };
    }
}