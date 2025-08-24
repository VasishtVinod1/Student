

using Student_Management_System.Domain;

namespace Student_Management_System.Data
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(int id);
        Student Add(Student student);
        Student Update(int id, Student updatedstudent);
        bool Delete(int id);
    }
}
