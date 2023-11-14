using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Console;
using CLI.DAO;
using CLI.Storage.Serialization;

namespace CLI.Model;

public class Address : ISerializable, IConsoleWriteRead
{
    public Address() { }

    public Address(string street, string number, string city, string country)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;
    }

    public string Street { get; set; }

    public string Number { get; set; }

    public string City { get; set;}

    public string Country { get; set; }

    public void Copy(Address obj)
    {
        Street = obj.Street;
        Number = obj.Number;
        City = obj.City;
        Country = obj.Country;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Street,
            Number,
            City,
            Country
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Street = values[0];
        Number = values[1];
        City = values[2];
        Country = values[3];
    }

    public string GenerateClassHeader()
    {
        return $" {"Street",25} | {"Number",6} | {"City",15} | {"Country",15} |";
    }

    public override string ToString()
    {
        return $" {Street,25} | {Number,6} | {City,15} | {Country,15} |";
    }
}
