using Student_Management_System.Domain;

namespace Student_Management_System.Data
{
    public class StudentRepository : IStudentRepository
    {
        private static List<Student> _student =new List<Student>();
        private static int _nextId = 1;

        public List<Student> GetAll()
        {
            return _student;
        }

        public Student GetById(int id)
        {
            var student = _student.FirstOrDefault(s => s.Id == id);
            return student;
        }

        public Student Add(Student student)
        {
            student.Id = _nextId++;
            _student.Add(student);
            return student;

        }

        public Student Update(int id, Student updatedstudent)
        {
            var existingStudent = _student.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return null;
            }
            existingStudent.Name = updatedstudent.Name;
            existingStudent.Age = updatedstudent.Age;
            existingStudent.Grade = updatedstudent.Grade;
            return existingStudent;
        }

        public bool Delete(int id)
        {
            var student = GetById(id);
            if (student == null)
            {
                return false;
            }
            _student.Remove(student);
            return true;

        }
    }
}
