using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class EmployeeRepository : BaseRepository<Employee, string, MyContexts>, IEmployeeRepository
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        public EmployeeRepository(MyContexts context, IAccountRoleRepository accountRoleRepository) : base(context)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public async Task<IEnumerable<dynamic>> GetDataEmployePembina()
        {
            var getProfil = await GetDataProfile();
          
            var data = (from a in getProfil
                       join r in _context.AccountRoles
                       on a.Nik equals r.AccountId
                       join x in _context.Roles
                       on r.RoleId equals x.Id
                       where r.RoleId == 1
                       select new
                       {
                           a.Nik,
                           a.FullName,
                           a.Gender,
                           a.HiringDate,
                           a.Email,
                           a.University,
                           a.Gpa,
                           a.Major,
                           x.Name
                       }).ToList();

            return data;

        }

        public async Task<IEnumerable<EmployeeProfileVM>> GetDataProfile()
        {
            var getData = (from e in _context.Educations
                           join u in _context.Universities
                          on e.UniversityId equals u.Id
                           join p in _context.Profilings
                           on e.Id equals p.EducationId
                           join em in _context.Employees
                           on p.ProfilingId equals em.Nik
                           join ac in _context.Accounts
                           on em.Nik equals ac.AccountId
                           join acr in _context.AccountRoles
                           on ac.AccountId equals acr.AccountId
                           select new EmployeeProfileVM()
                           {
                               Nik=em.Nik,
                               FullName = string.Concat(em.FirstName+" "+em.LastName),
                               Email= em.Email,
                               Gender= em.Gender,
                               University= u.Name,
                               Degree= e.Degree,
                               Gpa= (decimal)e.Gpa,
                               HiringDate = em.HiringDate,
                               Major = e.Major
                           }).ToListAsync();

            return await getData;
        }

        public async Task<IEnumerable<dynamic>> GetDataProfileBynik(string nik)
        {
            var getEmployee = await _context.Employees.Where(x=>x.Nik==nik).ToListAsync();
            var getprofil = await GetDataProfile();
            var getdata = from e in getprofil
                          join a in getEmployee
                          on e.Nik equals a.Nik
                          select new
                          {
                              e.Nik,
                              e.FullName,
                              a.FirstName,
                              a.LastName,
                              e.HiringDate,
                              e.Gender,
                              e.Gpa,
                              e.Major,
                              e.Email,
                              e.Degree,
                              e.University
                          };

            return getdata ;

        }

        public async Task<IEnumerable<dynamic>> GetEmployeeByIdDepartment(int id)
        {
            var departments = await _context.Departments.Where(x=>x.Id==id).ToListAsync();

            var employee = await GetAllAsync();

            var getdata = from e in employee
                          join d in departments
                          on e.DepartmentId equals d.Id
                          let fullname = string.Concat(e.FirstName + " " + e.LastName)
                          select new
                          {
                              e.Nik,
                              fullname,
                              d.Name
                          };
            return getdata ;
        }

        public async Task<UserDataVM> GetUserDataByEmailAsync(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            return new UserDataVM
            {
                Nik = employee!.Nik,
                Email = employee.Email,
                FullName = string.Concat(employee.FirstName, " ", employee.LastName)
            };
        }
    }
}
