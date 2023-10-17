using System.ComponentModel.DataAnnotations;

namespace Company.PL.Models
{
	public class RegisterViewModel
	{
		public string Fname { get; set; }
		public string Lname { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "PassWord is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is Required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password Dosen't Match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
