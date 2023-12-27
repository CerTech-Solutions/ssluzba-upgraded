using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class GradeDTO
    {
        public GradeDTO()
        {
            student = new StudentDTO();
            subject = new SubjectDTO();
        }   

        public GradeDTO(Grade grade, StudentDTO studentDTO)
        {
            id = grade.Id;
            student = studentDTO;
            subject = new SubjectDTO(grade.Subject);
            gradeValue = grade.GradeValue;
            passDate = grade.PassDate.ToDateTime(TimeOnly.Parse("00:00"));
        }

        public GradeDTO(GradeDTO gradeDTO)
        { 
            id = gradeDTO.Id;
            student = gradeDTO.Student;
            subject = gradeDTO.Subject;
            gradeValue = gradeDTO.GradeValue;
            passDate = gradeDTO.PassDate;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;

                }
            }
        }   

        private StudentDTO student;
        public StudentDTO Student
        {
            get { return student; }
            set
            {
                if (value != student)
                {
                    student = value;

                }
            }   
        }

        private SubjectDTO subject;
        public SubjectDTO Subject
        {
            get { return subject; }
            set
            {
                if (value != subject)
                {
                    subject = value;
                }
            }
        }   

        private int gradeValue;
        public int GradeValue
        {
            get { return gradeValue; }
            set
            {
                if (value != gradeValue)
                {
                    gradeValue = value;
                }
            }
        }

        private DateTime passDate;
        public DateTime PassDate
        {
            get { return passDate; }
            set
            {
                if (value != passDate)
                {
                    passDate = value;
                }
            }
        }

        public String PassDateString
        {
            get { return passDate.Date.ToString("dd/MM/yyyy"); }
        }
    }
}
