using Comapny.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Company.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Maximunm length of name 5")]
        [MinLength(5, ErrorMessage = "Minumim length of name 5")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age Must Be Bettween 22 and 60")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0,9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Addess Must be in form of '123-Street-City-County'")]

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000, 8000, ErrorMessage = "Salary Must Be Between 4000 & 8000")]

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        [EmailAddress(ErrorMessage = "Enter Email in a correct form")]

        public string Email { get; set; }
        [Phone]
        public string PhoneNuber { get; set; }

        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
