﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestHR.Model
{
    public class Employee
    {

        [Key] // This marks EmployeeId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // This makes EmployeeId an identity column
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }
}
