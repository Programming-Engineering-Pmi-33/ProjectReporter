using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjectReporter.Modules.GroupsService.Storage;
using ProjectReporter.Modules.UsersService.Storage;

namespace ProjectReporter.Service.Infrastructure.Database
{
    using CryptRand = System.Security.Cryptography.RandomNumberGenerator;

    public class DataGenerator : IDataGenerator
    {
        private readonly GroupsStorage _groupsStorage;
        private readonly UsersStorage _usersStorage;
        private readonly Random _rand;

        private const int MinStatus = 0;
        private const int MaxStatus = 5;
        private const int MaxPoint = 100;
        private const int MaxCountOfUsersInGroup = 8;
        private const int MinNamesLen = 3;
        private const int MaxNamesLen = 12;
        private const int MinFacultyLen = 4;
        private const int MaxFacultyLen = 50;
        private const int HashLen = 32;

        public DataGenerator(GroupsStorage groupsStorage, UsersStorage usersStorage)
        {
            _groupsStorage = groupsStorage;
            _usersStorage = usersStorage;

            _rand = new Random();
        }
        private static string GetRandStr(int minLen, int maxLen, bool useDigit = false)
        {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (useDigit)
            {
                valid += "1234567890";
            }

            var res = new StringBuilder();
            var length = CryptRand.GetInt32(minLen, maxLen + 1);
            for (var i = 0; i < length; i++)
            {
                res.Append(valid[CryptRand.GetInt32(valid.Length)]);
            }

            return res.ToString();
        }
        public void AddUsers(int countUsers, int countTeachers, int countAdmins)
        {
            var faculty = new Faculty { Name = GetRandStr(MinFacultyLen, MaxFacultyLen) };

            var department = new Department
            {
                Name = GetRandStr(MinFacultyLen, MaxFacultyLen),
                Faculty = faculty
            };

            var group = new AcademicGroup
            {
                Name = GetRandStr(MinNamesLen, MaxNamesLen, true),
                Department = department
            };
            for (var i = 0; i < countUsers; i++)
            {
                var student = new Student
                {
                    FirstName = GetRandStr(MinNamesLen, MaxNamesLen),
                    LastName = GetRandStr(MinNamesLen, MaxNamesLen),
                    Email = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    PasswordHash = GetRandStr(HashLen, HashLen, true),
                    GitLink = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    Group = group
                };

                _usersStorage.Students.Add(student);
            }

            for (var i = 0; i < countTeachers; i++)
            {
                var teacher = new Teacher
                {
                    FirstName = GetRandStr(MinNamesLen, MaxNamesLen),
                    LastName = GetRandStr(MinNamesLen, MaxNamesLen),
                    Email = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    PasswordHash = GetRandStr(HashLen, HashLen, true),
                    Department = department
                };

                _usersStorage.Teachers.Add(teacher);
            }

            for (var i = 0; i < countAdmins; i++)
            {
                var admin = new Admin
                {
                    FirstName = GetRandStr(MinNamesLen, MaxNamesLen),
                    LastName = GetRandStr(MinNamesLen, MaxNamesLen),
                    Email = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    PasswordHash = GetRandStr(HashLen, HashLen, true),
                    SecretKey = GetRandStr(MinNamesLen, MaxNamesLen, true)
                };

                _usersStorage.Admins.Add(admin);
            }

            _usersStorage.SaveChanges();
        }
        public void AddGroups(int groups, int projects, int tasks)
        {
            var users = _usersStorage.Users.ToList();
            if (users.Count < 5)
                return;
            for (var i = 0; i < groups; i++)
            {
                var group = new Group
                {
                    Name = GetRandStr(MinNamesLen, MaxNamesLen),
                    Description = GetRandStr(MinFacultyLen, MaxFacultyLen),
                    Status = _rand.Next(MinStatus, MaxStatus),
                    OwnerId = users[_rand.Next(users.Count)].Id,
                    GitLink = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    Projects = new List<Project>(),
                    Members = new List<GroupMember>(),
                    Tasks = new List<Task>()
                };
                for (var j = 0; j < _rand.Next(MaxCountOfUsersInGroup); j++)
                {
                    group.Members.Add(
                        new GroupMember
                        {
                            UserId = users[_rand.Next(users.Count)].Id,
                            InviterId = users[_rand.Next(users.Count)].Id,
                            Group = group,
                            Guid = GetRandStr(MinNamesLen, MaxNamesLen)
                        }
                        );
                }
                _groupsStorage.Groups.Add(group);
                _groupsStorage.SaveChanges();
            }

            for (var i = 0; i < projects; i++)
            {
                var groupId = _rand.Next(1, _groupsStorage.Groups.Count());
                var curGroup = _groupsStorage.Groups.Include(t => t.Projects).First(g => g.Id == groupId);
                var project = new Project
                {
                    Name = GetRandStr(MinNamesLen, MaxNamesLen),
                    Description = GetRandStr(MinFacultyLen, MaxFacultyLen),
                    GitLink = GetRandStr(MinNamesLen, MaxNamesLen, true),
                    Group = curGroup,
                    Members = new List<ProjectMember>(),
                    Reports = new List<Report>()
                };
                for (var j = 0; j < _rand.Next(MaxCountOfUsersInGroup); j++)
                {
                    project.Members.Add(
                        new ProjectMember
                        {
                            UserId = users[_rand.Next(users.Count)].Id,
                            Project = project
                        }
                        );
                }
                curGroup.Projects.Add(project);
                _groupsStorage.SaveChanges();
            }

            for (var i = 0; i < tasks; i++)
            {
                var groupId = _rand.Next(1, _groupsStorage.Groups.Count());
                var curGroup = _groupsStorage.Groups.Include(t => t.Tasks).First(g => g.Id == groupId);
                var task = new Task
                {
                    Name = GetRandStr(MinNamesLen, MaxNamesLen),
                    Description = GetRandStr(MinFacultyLen, MaxFacultyLen),
                    Points = _rand.Next(MaxPoint),
                    Group = curGroup,
                    Reports = new List<Report>()
                };
                curGroup.Tasks.Add(task);
                _groupsStorage.SaveChanges();
            }
        }
    }
}
