using ProjectReporter.Modules.GroupsService.Storage;
using ProjectReporter.Modules.UsersService.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CryptRand = System.Security.Cryptography.RandomNumberGenerator;

namespace ProjectReporter.Modules.TestData
{
    public class DataGenerator
    {
        private GroupsStorage _groupsStorage;
        private UsersStorage _usersStorage;

        private static int minStatus = 0;
        private static int maxStatus = 5;
        private static int maxPoint = 100;
        private static int maxCountOfUsersInGroup = 8;
        private static int minNamesLen = 3;
        private static int maxNamesLen = 12;
        private static int minFacultLen = 4;
        private static int maxFacultLen = 50;
        private static int saltLen = 6;
        private static int heshLen = 32;

        private Random rand;
        public DataGenerator(GroupsStorage groupsStorage, UsersStorage usersStorage)
        {
            _groupsStorage = groupsStorage;
            _usersStorage = usersStorage;

            rand = new Random();
        }
        private string GetRandStr(int minLen, int maxLen, bool useDigit = false)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (useDigit)
            {
                valid += "1234567890";
            }

            StringBuilder res = new StringBuilder();
            int length = CryptRand.GetInt32(minLen, maxLen + 1);
            for (int i = 0; i < length; i++)
            {
                res.Append(valid[CryptRand.GetInt32(valid.Length)]);
            }

            return res.ToString();
        }
        public void AddUsers(int countUsers, int countTeachers, int countAdmins)
        {
            var faculty = new Faculty { Name = GetRandStr(minFacultLen, maxFacultLen, false) };

            var department = new Department
            {
                Name = GetRandStr(minFacultLen, maxFacultLen, false),
                Faculty = faculty
            };

            var group = new AcademicGroup
            {
                Name = GetRandStr(minNamesLen, maxNamesLen, true),
                Department = department
            };
            for (int i = 0; i < countUsers; i++)
            {
                var student = new Student
                {
                    FirstName = GetRandStr(minNamesLen, maxNamesLen, false),
                    LastName = GetRandStr(minNamesLen, maxNamesLen, false),
                    Email = GetRandStr(minNamesLen, maxNamesLen, true),
                    PasswordHash = GetRandStr(heshLen, heshLen, true),
                    GitLink = GetRandStr(minNamesLen, maxNamesLen, true),
                    Group = group
                };

                _usersStorage.Students.Add(student);
            }

            for (int i = 0; i < countTeachers; i++)
            {
                var teacher = new Teacher
                {
                    FirstName = GetRandStr(minNamesLen, maxNamesLen, false),
                    LastName = GetRandStr(minNamesLen, maxNamesLen, false),
                    Email = GetRandStr(minNamesLen, maxNamesLen, true),
                    PasswordHash = GetRandStr(heshLen, heshLen, true),
                    Department = department
                };

                _usersStorage.Teachers.Add(teacher);
            }

            for (int i = 0; i < countAdmins; i++)
            {
                var admin = new Admin
                {
                    FirstName = GetRandStr(minNamesLen, maxNamesLen, false),
                    LastName = GetRandStr(minNamesLen, maxNamesLen, false),
                    Email = GetRandStr(minNamesLen, maxNamesLen, true),
                    PasswordHash = GetRandStr(heshLen, heshLen, true),
                    SecretKey = GetRandStr(minNamesLen, maxNamesLen, true)
                };

                _usersStorage.Admins.Add(admin);
            }

            _usersStorage.SaveChanges();
        }
        public void AddGroups(int groups, int progects, int tasks)//
        {
            List<User> users = _usersStorage.Users.ToList();
            if (users.Count < 5)
                return;
            for (int i = 0; i < groups; i++)
            {
                var group = new Group
                {
                    Name = GetRandStr(minNamesLen, maxNamesLen, false),
                    Description = GetRandStr(minFacultLen, maxFacultLen, false),
                    Status = rand.Next(minStatus, maxStatus),
                    OwnerId = users[rand.Next(users.Count)].Id,
                    GitLink = GetRandStr(minNamesLen, maxNamesLen, true),
                    Projects = new List<Project>(),
                    Members = new List<GroupMember>(),
                    Tasks = new List<Task>()
                };
                for (int j = 0; j < rand.Next(maxCountOfUsersInGroup); j++)
                {
                    group.Members.Add(
                        new GroupMember
                        {
                            UserId = users[rand.Next(users.Count)].Id,
                            InviterId = users[rand.Next(users.Count)].Id,
                            Group = group,
                            Guid = GetRandStr(minNamesLen, maxNamesLen, false)
                        }
                        ); 
                }
                _groupsStorage.Groups.Add(group);
            }
          
            for (int i = 0; i < progects; i++)
            {
                var currGroup = _groupsStorage.Groups.ElementAt(rand.Next(1, _groupsStorage.Groups.Count()));
                var project = new Project
                {
                    Name = GetRandStr(minNamesLen, maxNamesLen, false),
                    Description = GetRandStr(minFacultLen, maxFacultLen, false),
                    GitLink = GetRandStr(minNamesLen, maxNamesLen, true),
                    Group = currGroup,
                    Members = new List<ProjectMember>(),
                    Reports = new List<Report>()
                };
                for (int j = 0; j < rand.Next(maxCountOfUsersInGroup); j++)
                {
                    project.Members.Add(
                        new ProjectMember
                        { 
                            UserId = users[rand.Next(users.Count)].Id,
                            Project = project
                        }
                        );
                }
                currGroup.Projects.Add(project);
            }

            for (int i = 0; i < tasks; i++)
            {
                var currGroup = _groupsStorage.Groups.ElementAt(rand.Next(1, _groupsStorage.Groups.Count()));
                var task = new Task
                {
                    Name = GetRandStr(minNamesLen, maxNamesLen, false),
                    Description = GetRandStr(minFacultLen, maxFacultLen, false),
                    Points = rand.Next(maxPoint),
                    Group = currGroup,
                    Reports = new List<Report>()
                };
                currGroup.Tasks.Add(task);
            }

            _usersStorage.SaveChanges();
        }
    }
}

