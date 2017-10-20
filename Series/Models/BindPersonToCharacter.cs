using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Models
{
    class BindPersonToCharacter
    {
        public BindPersonToCharacter(People person, People character)
        {
            Person = person;
            Character = character;
        }

        public People Person { get; }
        public People Character { get; }
    }
}
