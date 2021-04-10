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
            catch (DataException exception)
            {
                throw new DataException("File is not found or broken.", exception);
            }
        }

        private static Faculty ParseFaculty(string line)
        {
            try
            {
                var facultyName = line.Split(':')[0].Trim();
                var departmentNames = line.Split(':')[1].Split(';');
                var groupNames = line.Split(':')[2].Split(',');
                var departments = departmentNames.Select(dn => new Department
                {
                    Name = FirstCharToUpper(dn.Trim())
                })
                    .ToList();
                var groups = groupNames.Select(gn => new AcademicGroup
                {
                    Name = FirstCharToUpper(gn.Trim()),
                })
                    .ToList();
                return new Faculty { Name = FirstCharToUpper(facultyName), Departments = departments, AcademicGroups = groups };
            }
            catch
            {
                throw new DataException("Cannot parse faculties.");
            }
        }

        private static string FirstCharToUpper(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Text cannot be empty or white space.");
            }

            return text.First().ToString().ToUpper() + text.Substring(1);
        }
    }
}
