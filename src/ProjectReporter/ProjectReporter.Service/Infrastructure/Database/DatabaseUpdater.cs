using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.GroupsService.Storage;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Service.Infrastructure.Database
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseUpdater(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UpdateDatabase()
        {
            var dbContexts = new List<DbContext>
            {
                (DbContext)_serviceProvider.GetService(typeof(UsersStorage)),
                (DbContext)_serviceProvider.GetService(typeof(GroupsStorage))
            };

            foreach (var context in dbContexts)
            {
                context.Database.Migrate();
            }

            Console.WriteLine("The database has been successfully updated.");
        }
    }
}
