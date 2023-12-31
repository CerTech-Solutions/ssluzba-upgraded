using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class SubjectDTO : INotifyPropertyChanged
    {
        public SubjectDTO()
        {
            _professor = new ProfessorDTO();
        }

        public SubjectDTO(ProfessorDTO professor)
        {
            _professor = professor;
        }

        public SubjectDTO(Subject s)
        {
            id = s.Id;
            code = s.Code;
            name = s.Name;
            semester = s.Semester;
            yearOfStudy = s.YearOfStudy;
            if(s.Professor != null)
                _professor = new ProfessorDTO(s.Professor);
            ects = s.Ects;
        }

        public SubjectDTO(Subject s, ProfessorDTO professor)
        {
            id = s.Id;
            code = s.Code;
            name = s.Name;
            semester = s.Semester;
            yearOfStudy = s.YearOfStudy;
            _professor = professor;
            ects = s.Ects;
        }

        public SubjectDTO(SubjectDTO s)
        {
            id = s.Id;
            code = s.Code;
            name = s.Name;
            semester = s.Semester;
            yearOfStudy = s.YearOfStudy;
            _professor = s.Professor;
            ects = s.Ects;
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
                    OnPropertyChanged();
                }
            }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (value != code)
                {
                    code = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private SemesterEnum semester;
        public SemesterEnum Semester
        {
            get { return semester; }
            set
            {
                if (value != semester)
                {
                    semester = value;
                    OnPropertyChanged();
                }
            }
        }

        private int yearOfStudy;
        public int YearOfStudy
        {
            get { return yearOfStudy; }
            set
            {
                if (value != yearOfStudy)
                {
                    yearOfStudy = value;
                    OnPropertyChanged();
                }
            }
        }

        private ProfessorDTO _professor;
        public ProfessorDTO Professor
        {
            get { return _professor; }
            set
            {
                if (value != _professor)
                {
                    _professor = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ects;
        public int Ects
        {
            get { return ects; }
            set
            {
                if (value != ects)
                {
                    ects = value;
                    OnPropertyChanged();
                }
            }
        }

        public String CodeAndName
        {
            get { return code + " - " + name; }
        }

        public Subject ToSubject()
        {
            return new Subject(id, code, name, semester, yearOfStudy, _professor.ToProfessor(), ects);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
