using System;
using System.Data;
using System.IO;
using System.Linq;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Service.Infrastructure.Database
{
    public class DatabaseUploader : IDatabaseUploader
    {
        private readonly UsersStorage _storage;

        public DatabaseUploader(UsersStorage storage)
        {
            _storage = storage;
        }

        public void UploadFaculties(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                var faculties = lines.Select(ParseFaculty);
                _storage.Faculties.AddRange(faculties);
                _storage.SaveChanges();
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException)
                {
                    throw;
                }

                throw new DataException("There are duplicated faculties information.");
            }
        }

        private static Faculty ParseFaculty(string line)
        {
            try
            {
                var facultyName = line.Split(':')[0];
                var departmentNames = line.Split(':')[1].Split(',');
                var departments = departmentNames.Select(dn => new Department { Name = dn }).ToList();
                return new Faculty { Name = facultyName, Departments = departments };
            }
            catch
            {
                throw new ArgumentException("Faculty cannot be parsed.");
            }
        }
    }
}
