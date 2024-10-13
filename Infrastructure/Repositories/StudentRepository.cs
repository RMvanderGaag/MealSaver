using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.EntityFramework;

public class StudentRepository(MealSaverEFDBContext context) : IStudentRepository
{
    public Student GetStudentById(Guid id)
    {
        return context.Students.SingleOrDefault(student => student.Id == id);
    }

    public Student GetStudentByEmail(string email)
    {
        return context.Students.SingleOrDefault(student => student.Email == email);
    }
}