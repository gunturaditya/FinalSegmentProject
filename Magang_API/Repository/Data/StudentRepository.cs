using Magang_API.Base;
using Magang_API.Context;

using Magang_API.Model;

using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class StudentRepository : BaseRepository<Student, string, MyContexts>, IStudentRepository
    {
        public StudentRepository(MyContexts context) : base(context)
        {
        }

        public async Task<int> AprovalFalse(StudentAproval aproval)
        {
            var data = await GetByIdAsync(aproval.Nim);

            var student = new Student
            {
                Nim = aproval.Nim,
                BirthDate = data.BirthDate,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Degree = data.Degree,
                Email = data.Email,
                Gpa = data.Gpa,
                Major = data.Major,
                PhoneNumber = data.PhoneNumber,
                Score = data.Score,
                UniversitasId = data.UniversitasId,
                IsApproval = false
            };
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AprovalTrue(StudentAproval aproval)
        {
            var data = await GetByIdAsync(aproval.Nim);

            var student = new Student
            {
                Nim = aproval.Nim,
                BirthDate=data.BirthDate,
                FirstName=data.FirstName,
                LastName=data.LastName,
                Degree=data.Degree,
                Email=data.Email,
                Gpa=data.Gpa,
                Major=data.Major,
                PhoneNumber=data.PhoneNumber,
                Score=data.Score,
                UniversitasId=data.UniversitasId,
                IsApproval = true
            };
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<dynamic>> GetAllStudentsNoAproval()
        {


            var students = (from a in _context.Students
                           let fullname = a.FirstName+" "+a.LastName
                           where a.IsApproval == null
                           select new 
                           {
                               a.Nim,
                               fullname,
                               a.Email,
                               a.Major,
                               a.Universitas.Name,
                               a.Degree,
                               a.Gpa,
                               a.PhoneNumber,
                               
                           }).ToListAsync();
            return await students;
        }

        public async Task<int> GetStudentCountAsync()
        {

            var students = await _context.Students.Where(x=>x.IsApproval == null).CountAsync();

            return students;              
        }

        public async Task<IEnumerable<string>> GetUniversitasAsyncbyid(int id)
        {
            var getuniversitybyid = GetAllAsync().Result.Where(x => x.UniversitasId == id);
            var getUni = _context.Universities;

            var getname = from s in getuniversitybyid
                               join u in getUni on s.UniversitasId equals u.Id
                               select u.Name;

            return getname;
        }

        public async Task<UserDataVM> GetUserDataByEmailAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(e => e.Email == email);
            return new UserDataVM
            {
                Nik = student!.Nim,
                Email = student.Email,
                FullName = string.Concat(student.FirstName, " ", student.LastName)
            };
        }

    }
}
