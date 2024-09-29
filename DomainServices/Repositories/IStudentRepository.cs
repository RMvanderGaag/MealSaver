using Domain;

namespace DomainServices.Repositories;

public interface IStudentRepository
{
    Student GetStudentById(Guid id);
    Student GetStudentByEmail(string email);
}