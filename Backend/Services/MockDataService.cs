using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class MockDAtaService
    {
        private readonly AppDbContext _db;

        public MockDAtaService(AppDbContext db)
        {
            _db = db;
        }
    
    public List<AcademicYear> GetMockData()
    {
        return new List<AcademicYear>
        {
            new AcademicYear
            {
                YearID = "AY2023-2024",
                YearSpan = "2023-2024",
                IsCurrent = true,
                AYTargetGenAve = 85.0,
                AYCalculatedGenAve = 87.5,
                UserID = "user123",
                Terms = new List<Term>
                {
                    new Term
                    {
                        TermID = "Term1",
                        Name = "First Term",
                        IsCurrent = true,
                        TermTargetGenAve = 85.0,
                        TermCalculatedGenAve = 88.0,
                        Courses = new List<Course>
                        {
                            new Course
                            {
                                CourseID = "Math101",
                                TermID = "Term1",
                                CourseCode = "MATH101",
                                Name = "Mathematics 101",
                                Units = 3,
                                ActualGrade = new Grade
                                {
                                    GradeID = "Grade1",
                                    CourseID = "Math101",
                                    TargetCourseGrade = 85.0,
                                    CourseGrade = 90.0,
                                }
                            },
                            new Course
                            {
                                CourseID = "Eng101",
                                TermID = "Term1",
                                CourseCode = "ENG101",
                                Name = "English 101",
                                Units = 3,
                                ActualGrade = new Grade
                                {
                                    GradeID = "Grade2",
                                    CourseID = "Eng101",
                                    TargetCourseGrade = 85.0,
                                    CourseGrade = 87.0
                                }
                            }
                        }
                    },         
                }
            }       
        };
    } 

    public List<User> GetMockUsers()
    {
        return new List<User>
        {
            new User
            {
                UserID = "user123",
                Email = "user123@example.com",
                Password = "iykyk",
                Name = "John Doe",
                University = "Batangas State University",
                Program = "Computer Science",
                PreferredScale = GradeScaleType.Percentage,
                Years = GetMockData()
            }
        };
    }
    }        
}