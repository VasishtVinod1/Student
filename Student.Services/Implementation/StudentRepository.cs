using SMS.Services.Interfaces;
using SMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMS.Services.Implementation
{
    public class StudentRepository : IStudentRepository;
    {
        private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }


}
}








