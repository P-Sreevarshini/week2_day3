using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Student
    {
        [Key]
    public long StudentId { get; set; }
    public string StudentName { get; set; }

    public string StudentMobileNumber { get; set; }
    public List<Enquiry> Enquiries { get; set; }
    public List<Course> Courses { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public User User { get; set; }
}
}