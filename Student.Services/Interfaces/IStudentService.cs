using SMS.Domain.Repositories;
using SMS.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.Interfaces
{
    public interface IStudentService
    {
    Task<IEnumerable<StudentResponseDto>> GetAll();

    Task<StudentResponseDto?> GetById(int id);

    Task<StudentCreateDto> Add(StudentCreateDto dto);

    Task<StudentUpdatedto> Update(int id,StudentUpdatedto dto);

    Task<Student>DeleteById(int id);

    Task<StudentUpdatedto> Patch(int id, StudentUpdatedto dto);
    }
}
