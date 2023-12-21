using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class AddressDTO : INotifyPropertyChanged
    {
        public AddressDTO() { }


        public AddressDTO(String street, String number, String City, String Country)
        {
            this.street = street;
            this.number = number;
            this.city = city;
            this.country = country;
        }

        public AddressDTO(Address a)
        {
            street = a.Street;
            number = a.Number;
            city = a.City;
            country = a.Country;
        }

        public AddressDTO(AddressDTO a)
        {
            street = a.Street;
            number = a.Number;
            city = a.City;
            country = a.Country;
        }

        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                if (value != street)
                {
                    street = value;
                    OnPropertyChanged();
                }
            }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                if (value != number)
                {
                    number = value;
                    OnPropertyChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        public Address ToAddress()
        {
            return new Address(street, number, city, country);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String ToString()
        {
            return street + " " + number + ", " + city + ", " + country;
        }
    }
}
