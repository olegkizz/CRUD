using System.Collections.Generic;

namespace IdentityNLayer.Core.Entities
{
    public class Student : Person
    {
        public StudentType Type { get; set; }
    }
}