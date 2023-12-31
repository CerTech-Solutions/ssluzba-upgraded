using CLI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class DepartmentDTO : INotifyPropertyChanged
    {
        public DepartmentDTO()
        {
            professors = new ObservableCollection<ProfessorDTO>();
            chief = new ProfessorDTO();
        }

        public DepartmentDTO(Department d)
        {
            id = d.Id;
            code = d.Code;
            name = d.Name;
            chief = new ProfessorDTO(d.Chief);
            professors = new ObservableCollection<ProfessorDTO>(d.Professors.Select(p => new ProfessorDTO(p)).ToList());
        }

        public DepartmentDTO(DepartmentDTO d)
        {
            id = d.Id;
            code = d.Code;
            name = d.Name;
            chief = new ProfessorDTO(d.Chief);
            professors = new ObservableCollection<ProfessorDTO>(d.Professors);
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

        private ProfessorDTO chief;
        public ProfessorDTO Chief
        {
            get { return chief; }
            set
            {
                if (value != chief)
                {
                    chief = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<ProfessorDTO> professors;
        public ObservableCollection<ProfessorDTO> Professors
        {
            get { return professors; }
            set
            {
                if (value != professors)
                {
                    professors = value;
                    OnPropertyChanged();
                }
            }
        }

        public Department ToDepartment()
        {
            return new Department(0, code, name, chief.ToProfessor());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
