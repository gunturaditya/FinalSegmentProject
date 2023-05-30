using Magang_API.Base;
using Magang_API.Contexts;

using Magang_API.Models;

using Magang_API.Repository.Contracts;
using Magang_API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Repository.Data
{
    public class StudentRepository : BaseRepository<Student, string, MyContext>, IStudentRepository
    {
       
        public StudentRepository(MyContext context) : base(context)
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

        public async Task<IEnumerable<StudentProfilVM>> GetAllStudentProfil()
        {
            var getdata = (from s in _context.Students

                          select new StudentProfilVM()
                          {
                              Nim = s.Nim,
                              FullName = string.Concat(s.FirstName + " " + s.LastName),
                              Email = s.Email,
                              University = s.Universitas.Name,
                              Major = s.Major,
                              Degree = s.Degree,
                              Gpa = Convert.ToDecimal(s.Gpa),
                              Mentor = s.Status.Mentor.FirstName+" "+s.Status.Mentor.LastName,
                              Department = s.Status.Department.Name,
                              Status = s.Status.Status1,
                              StartDate = s.Status.StartDate,
                              EndDate = s.Status.EndDate,
                          }).ToListAsync();
            return await getdata;
        }

        public async Task<IEnumerable<dynamic>> GetAllStudentsFalseAproval()
        {
            var students = (from a in _context.Students
                            let fullname = a.FirstName + " " + a.LastName
                            where a.IsApproval == false
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
                                a.Document,
                            }).ToListAsync();
            return await students;
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
                               a.Document,
                           }).ToListAsync();
            return await students;
        }

        public async Task<IEnumerable<dynamic>> GetAllStudentsTrueAproval()
        {
            var students = (from a in _context.Students
                            
                            let fullname = a.FirstName + " " + a.LastName
                            where a.IsApproval == true && a.Status == null
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
                                a.Document,
                            }).ToListAsync();
            return await students;
        }

        public async Task<IEnumerable<dynamic>> GetStudentByNik(string nik)
        {
            var data = await _context.Statuses.Where(x=>x.MentorId == nik).ToListAsync();
            var students = await GetAllStudentProfil();
            var getdata = from student in students
                          join x in data
                          on student.Nim equals x.StudentId
                          select new
                          {
                              student.Nim,
                              student.FullName,
                              student.Email,
                              student.University,
                              student.Major,
                              student.Degree,
                              student.Gpa,
                              student.StartDate, 
                              student.EndDate,
                              student.Status,
                              student.Mentor,
                              x.MentorId
                             
                          };
            return getdata;
        }
        public async Task<IEnumerable<dynamic>> GetStudentPenilaian(string nik)
        {
            var data = await _context.Statuses.Where(x => x.MentorId == nik).ToListAsync();
            var students = await GetAllStudentProfil();
            var getdata = from student in students
                          join x in data
                          on student.Nim equals x.StudentId
                          where x.EndDate <= DateTime.Now && x.Status1 == true
                          let enddate = DateOnly.FromDateTime(x.EndDate.Value)
                          let datenow = DateOnly.FromDateTime(DateTime.Now)
                          select new
                          {
                              student.Nim,
                              student.FullName,
                              student.Email,
                              student.University,
                              student.Major,
                              student.Degree,
                              student.Gpa,
                              student.StartDate,
                              enddate,
                              datenow,
                              student.Status,
                              student.Mentor,
                              x.MentorId

                          };
            return getdata;

        }

        public async Task<IEnumerable<dynamic>> GetStudentByNim(string nim)
        {
            var data = await GetAllStudentProfil();
            var students = await _context.Students.Where(a=>a.Nim == nim).ToListAsync();
            var getdata = from d in data
                          join s in students
                          on d.Nim equals s.Nim
                          select new
                          {
                              s.Nim,
                              d.FullName,
                              d.Email,
                              d.University,
                              d.Major,
                              d.Degree,
                              d.Gpa,
                              d.StartDate,
                              d.EndDate,
                              d.Mentor,
                              s.Score
                          };
            return getdata;
        }

        public async Task<IEnumerable<StudentChart>> GetStudentCharts()
        {
            var student = await GetAllStudentProfil();

            var getdata = (from s in student
                          where s.Status == true
                          group s by(s.University,s.Status) into g
                          select new StudentChart
                          {
                              UniversitasName = g.Key.University,
                              Count = g.Count(),
                          }).ToList();
            return getdata;
        }

        public async Task<int> GetStudentCountAprovalAsync()
        {
            var students = await _context.Students.Where(x => x.IsApproval == true && x.Status == null).CountAsync();

            return students;
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
                FullName = string.Concat(student.FirstName, " ", student.LastName),
                Status = student.IsApproval.ToString()
            };
        }


    }
}
