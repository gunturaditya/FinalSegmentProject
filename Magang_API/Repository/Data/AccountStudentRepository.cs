using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Handler;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;

namespace Magang_API.Repository.Data
{
    public class AccountStudentRepository : BaseRepository<AccountStudent, string, MyContexts>, IAccountStudentRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAccountStudentRoleRepository _studentRoleRepository;
        public AccountStudentRepository(MyContexts context,
        IUniversityRepository universityRepository,
        IStudentRepository studentRepository,IAccountStudentRoleRepository studentRoleRepository
            ) : base(context)
        {
            _universityRepository = universityRepository;
            _studentRepository = studentRepository;
            _studentRoleRepository = studentRoleRepository;
        }

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getStudent = await _studentRepository.GetAllAsync();
            var getAccounts = await GetAllAsync();

            var getUserData = getStudent.Join(getAccounts,
                                                e => e.Nim,
                                                a => a.AccountStudentId,
                                                (e, a) => new LoginVM
                                                {
                                                    Email = e.Email,
                                                    Password = a.Password
                                                })
                                          .FirstOrDefault(ud => ud.Email == loginVM.Email);



            if (getUserData == null)
            {
                return false;
            }

            return getUserData is not null && Hashing.ValidatePassword(loginVM.Password, getUserData.Password);
        }

        public async Task RegisterStudentAsync(RegisterStudentVM registerStudentVM)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var university = await _universityRepository.InsertAsync(new University
                {
                    Name = registerStudentVM.UniversityName
                });


                // Employee
                var student = new Student
                {
                    Nim = registerStudentVM.NIM,
                    FirstName = registerStudentVM.FirstName,
                    LastName = registerStudentVM.LastName,
                    BirthDate = registerStudentVM.BirthDate,
                    Email = registerStudentVM.Email,
                    Degree = registerStudentVM.Degree,
                    Gpa = registerStudentVM.GPA,
                    Score = 0,
                    Major = registerStudentVM.Major,
                    UniversitasId = university.Id,
                    PhoneNumber = registerStudentVM.PhoneNumber,

                };
                await _studentRepository.InsertAsync(student);
                // Account
                var account = new AccountStudent
                {
                    AccountStudentId = registerStudentVM.NIM,
                    Password = Hashing.HashPassword(registerStudentVM.Password)
                };
                await InsertAsync(account);
               
                // AccountRole
                var accountRole = new AccountStudentRole
                {
                    RoleStudentId = 3,
                    AccountStudentId = registerStudentVM.NIM,
                };
                await _studentRoleRepository.InsertAsync(accountRole);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
