using System;

namespace StudentApi.Models.Response
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}