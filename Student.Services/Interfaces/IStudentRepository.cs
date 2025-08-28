using SMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.Interfaces
{
    public interface IStudentRepository
    {
    Task<Student> GetAll();    

    Task<Student> GetById(int id);

    Task<Student> Add(Student student);

    Task<Student> Update(Student student);

    Task<Student>DeleteById(int id);

    Task<Student>Patch(Student student);
    }
}
