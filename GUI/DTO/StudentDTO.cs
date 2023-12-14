using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CLI.Model;
using Index = CLI.Model.Index;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged
    {
        private IndexDTO _index;
        private AddressDTO _address;

        public StudentDTO(AddressDTO address, IndexDTO index)
        {
            _address = address;
            _index = index;
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

        // Address

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

        // Index

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

        public Index ToIndex()
        {
            return new Index(courseLabel, regNumber, enrollmentYear);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
