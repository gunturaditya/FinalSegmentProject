﻿using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Handler;
using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Data
{
    public class AccountRepository : BaseRepository<Account, string, MyContexts>, IAccountRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProfillingRepository _profilingRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public AccountRepository(MyContexts context,
        IUniversityRepository universityRepository,
        IEducationRepository educationRepository,
        IEmployeeRepository employeeRepository,
        IProfillingRepository profilingRepository,
        IAccountRoleRepository accountRoleRepository,
        IStudentRepository studentRepository,
        IDepartmentRepository departmentRepository) : base(context)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _profilingRepository = profilingRepository;
            _accountRoleRepository = accountRoleRepository;
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task RegisterAsync(RegisterVM registerVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var university = await _universityRepository.InsertAsync(new University
                {
                    Name = registerVM.UniversityName
                });

                var education = new Education
                {
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.GPA,
                    UniversityId = university.Id,
                };
                await _educationRepository.InsertAsync(education);
                //Department
                var department = await _departmentRepository.InsertAsync(new Department
                {
                    Name = registerVM.DepartmentName
                });
               
                // Employee
                var employee = new Employee
                {
                    Nik = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    PhoneNumber = registerVM.PhoneNumber,
                    Email = registerVM.Email,
                    HiringDate = DateTime.Now,
                    DepartmentId = department.Id,
                };
                await _employeeRepository.InsertAsync(employee);
                // Account
                var account = new Account
                {
                    AccountId = registerVM.NIK,
                    Password = Hashing.HashPassword(registerVM.Password)
                };
                await InsertAsync(account);
                // Profiling
                var profiling = new Profiling
                {
                    ProfilingId = registerVM.NIK,
                    EducationId = education.Id,
                };
                await _profilingRepository.InsertAsync(profiling);
                // AccountRole
                var accountRole = new AccountRole
                {
                    RoleId = 1,
                    AccountId = registerVM.NIK,
                };
                await _accountRoleRepository.InsertAsync(accountRole);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }

/*        public async Task RegisterStudentAsync(RegisterStudentVM registerStudentVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var university = new University
                {
                    Name = registerStudentVM.UniversityName
                };
                if (await _universityRepository.IsNameExistAsync(registerStudentVM.UniversityName))
                {
                    var univData = _universityRepository.GetByNameAsync(registerStudentVM.UniversityName);
                    university.Id = univData.Id;
                }
                else
                {
                    await _universityRepository.InsertAsync(university);
                }

                var education = new Education
                {
                    Major = registerStudentVM.Major,
                    Degree = registerStudentVM.Degree,
                    Gpa = registerStudentVM.GPA,
                    UniversityId = university.Id,
                };
                await _educationRepository.InsertAsync(education);

                var student = new Student
                {
                    StartDate = registerStudentVM.StartDate,
                    EndDate = registerStudentVM.EndDate
                };
                await _studentRepository.InsertAsync(student);

                // Employee
                var employee = new Employee
                {
                    Id = registerStudentVM.Id,
                    FirstName = registerStudentVM.FirstName,
                    LastName = registerStudentVM.LastName,
                    BirthDate = registerStudentVM.BirthDate,
                    Gender = registerStudentVM.Gender,
                    PhoneNumber = registerStudentVM.PhoneNumber,
                    Email = registerStudentVM.Email,
                    HiringDate = DateTime.Now,
                    StudentId = student.Id
                };
                await _employeeRepository.InsertAsync(employee);
                // Account
                var account = new Account
                {
                    AccountId = registerStudentVM.Id,
                    Password = registerStudentVM.Password,
                };
                await InsertAsync(account);
                // Profiling
                var profiling = new Profiling
                {
                    ProfilingId = registerStudentVM.Id,
                    EducationId = education.Id,
                };
                await _profilingRepository.InsertAsync(profiling);
                // AccountRole
                var accountRole = new AccountRole
                {
                    RoleId = 1,
                    AccountId = registerStudentVM.Id,
                };
                await _accountRoleRepository.InsertAsync(accountRole);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }*/

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getAccounts = await GetAllAsync();

            var getUserData = getEmployees.Join(getAccounts,
                                                e => e.Nik,
                                                a => a.AccountId,
                                                (e, a) => new LoginVM
                                                {
                                                    Email =  e.Email,
                                                    Password = a.Password
                                                })
                                          .FirstOrDefault(ud => ud.Email == loginVM.Email);

            

            if (getUserData == null)
            {
                return false;
            }

            return getUserData is not null && Hashing.ValidatePassword(loginVM.Password, getUserData.Password);
        }
    }
}
