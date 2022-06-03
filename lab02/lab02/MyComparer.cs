using lab02.models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02
{
    public class MyComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.Name} {x.Surname}", $"{y.Name} {y.Surname}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode( $"{obj.Name} {obj.Surname}");
        }
    }
}
