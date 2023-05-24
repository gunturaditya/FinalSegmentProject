using Magang_API.Base;
using Magang_API.Context;
using Magang_API.Handler;
using Magang_API.Model;
using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class AccountStudentRepository : BaseRepository<AccountStudent, string, MyContexts>, IAccountStudentRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountStudentRoleRepository _studentRoleRepository;
        private readonly IStudentRepository _studentRepository;
        public AccountStudentRepository(MyContexts context,
        IUniversityRepository universityRepository,
        IAccountStudentRoleRepository studentRoleRepository,IStudentRepository studentRepository
            ) : base(context)
        {
            _universityRepository = universityRepository;
            _studentRoleRepository = studentRoleRepository;
            _studentRepository = studentRepository;
        }

        public async Task<int> DeleteAccount(string id)
        {
            await _studentRoleRepository.DeleteAsync(id);
            await DeleteAsync(id);

            return await _studentRepository.DeleteAsync(id);

        }

        public async Task<bool> LoginAsync(LoginVM loginVM)
        {
            var getStudent = await _context.Students.ToListAsync();
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

                // Student
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
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
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
