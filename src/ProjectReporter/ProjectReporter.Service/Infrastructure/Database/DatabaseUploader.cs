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

                throw new DataException("File is not found or broken.");
            }
        }

        private static Faculty ParseFaculty(string line)
        {
            try
            {
                var facultyName = line.Split(':')[0].Trim();
                var departmentNames = line.Split(':')[1].Split(';');
                var departments = departmentNames.Select(dn => new Department
                {
                    Name = FirstCharToUpper(dn.Trim())
                })
                    .ToList();
                return new Faculty { Name = FirstCharToUpper(facultyName), Departments = departments };
            }
            catch
            {
                throw new ArgumentException("Faculty cannot be parsed.");
            }
        }

        private static string FirstCharToUpper(string text) =>
            text switch
            {
                null => throw new ArgumentNullException(nameof(text)),
                "" => throw new ArgumentException($"{nameof(text)} cannot be empty", nameof(text)),
                _ => text.First().ToString().ToUpper() + text.Substring(1)
            };
    }
}
