using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models.Request
{
    public class StudentRequest
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Age { get; set; }
    }
}