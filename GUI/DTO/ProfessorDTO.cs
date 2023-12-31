using CLI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged
    {
        private AddressDTO _address;

        public ProfessorDTO(AddressDTO adress)
        {
            _address = adress;
        }

        public ProfessorDTO()
        {
            _address = new AddressDTO();
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

        public ProfessorDTO(Professor p)
        {
            id = p.Id;
            name = p.Name;
            surname = p.Surname;
            birthDate = p.BirthDate.ToDateTime(TimeOnly.Parse("00:00"));
            phoneNumber= p.PhoneNumber;
            email = p.Email;
            idNumber = p.IdNumber;
            title = p.Title;
            serviceYears = p.ServiceYears;
            _address = new AddressDTO(p.Adresa);
            _subjects = new ObservableCollection<SubjectDTO>(p.Subjects.Select(s => new SubjectDTO(s, this)).ToList());
        }

        public ProfessorDTO(ProfessorDTO p)
        {
            id = p.Id;
            name = p.Name;
            surname = p.Surname;
            birthDate = p.BirthDate;
            phoneNumber = p.PhoneNumber;
            email = p.Email;
            idNumber = p.IdNumber;
            title = p.Title;
            serviceYears = p.ServiceYears;
            _address = new AddressDTO(p.AddressDTO);
            _subjects = p.Subjects;
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

        private string idNumber;
        public string IdNumber
        {
            get { return idNumber; }
            set
            {
                if (value != idNumber)
                {
                    idNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        private int serviceYears;
        public int ServiceYears
        {
            get { return serviceYears; }
            set
            {
                if (value != serviceYears)
                {
                    serviceYears = value;
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

        private ObservableCollection<SubjectDTO> _subjects;
        public ObservableCollection<SubjectDTO> Subjects
        {
            get { return _subjects; }
            set
            {
                if (value != _subjects)
                {
                    _subjects = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Professor ToProfessor()
        { 
            return new Professor(id, name, surname, DateOnly.FromDateTime(birthDate), _address.ToAddress(), phoneNumber, email, idNumber, title, serviceYears);
        }

        public String ToString()
        {
            return name + " " + surname;
        }

        public String NameSurname
        {
            get { return name + " " + surname; }
        }
    }
}
