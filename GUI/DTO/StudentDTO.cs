using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CLI.Model;
using Index = CLI.Model.Index;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged
    {
        private IndexDTO _index;
        private AddressDTO _address;

        public StudentDTO() 
        {
            _index = new IndexDTO();
            _address = new AddressDTO();
        }

        public StudentDTO(AddressDTO address, IndexDTO index)
        {
            _address = address;
            _index = index;
        }

        public StudentDTO(Student s)
        {
            id = s.Id;
            name = s.Name;
            surname = s.Surname;
            birthDate = s.BirthDate.ToDateTime(TimeOnly.Parse("00:00"));
            _address = new AddressDTO(s.Address);
            phoneNumber = s.PhoneNumber;
            email = s.Email;
            currentYear = s.CurrentYear;
            status = s.Status;
            gpa = s.GPA;
            _index = new IndexDTO(s.Index);
        }

        public StudentDTO(StudentDTO s)
        {
            id = s.Id;
            name = s.Name;
            surname = s.Surname;
            birthDate = s.BirthDate;
            _address = new AddressDTO(s.AddressDTO);
            phoneNumber = s.PhoneNumber;
            email = s.Email;
            currentYear = s.CurrentYear;
            status = s.Status;
            gpa = s.Gpa;
            _index = new IndexDTO(s.IndexDTO);
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

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value != birthDate)
                {
                    birthDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Address
        {
            get { return _address.ToString(); }
        }

        public AddressDTO AddressDTO
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private int currentYear;
        public int CurrentYear
        {
            get { return currentYear; }
            set
            {
                if (value != currentYear)
                {
                    currentYear = value;
                    OnPropertyChanged();
                }
            }
        }

        private StatusEnum status;
        public StatusEnum Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        private double gpa;
        public double Gpa
        {
            get { return gpa; }
            set
            {
                if (value != gpa)
                {
                    gpa = value;
                    OnPropertyChanged();
                }
            }
        }

        public IndexDTO IndexDTO
        {
            get { return _index;  }
            set
            {
                if (value != _index)
                {
                    _index = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Index
        {
            get { return _index.ToString(); }
        }

        public Student ToStudent()
        {
            return new Student(id, name, surname, DateOnly.FromDateTime(birthDate), _address.ToAddress(), phoneNumber, email, currentYear, status, gpa, _index.ToIndex());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class IndexDTO : INotifyPropertyChanged
    {
        private string courseLabel;
        public string CourseLabel
        {
            get { return courseLabel; }
            set
            {
                if (value != courseLabel)
                {
                    courseLabel = value;
                    OnPropertyChanged();
                }
            }
        }

        private int regNumber;
        public int RegNumber
        {
            get { return regNumber; }
            set
            {
                if (value != regNumber)
                {
                    regNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private int enrollmentYear;
        public int EnrollmentYear
        {
            get { return enrollmentYear; }
            set
            {
                if (value != enrollmentYear)
                {
                    enrollmentYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public IndexDTO() { }

        public IndexDTO(Index index)
        {
            courseLabel = index.CourseLabel;
            regNumber = index.RegNumber;
            enrollmentYear = index.EnrollmentYear;
        }

        public IndexDTO(IndexDTO index)
        {
            courseLabel = index.CourseLabel;
            regNumber = index.RegNumber;
            enrollmentYear = index.EnrollmentYear;
        }

        public Index ToIndex()
        {
            return new Index(courseLabel, regNumber, enrollmentYear);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String ToString()
        {
            return courseLabel + " " + regNumber + "/" + enrollmentYear;
        }
    }
}
