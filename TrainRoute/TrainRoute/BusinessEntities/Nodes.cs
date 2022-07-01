using System;
using System.Collections.Generic;
using System.Text;

namespace TrainRoute.BusinessEntities
{
    public class Nodes
    {
        private char _code;

        public char Code
        {
            get { return _code; }
        }

        public Nodes(char code)
        {
            _code = code;
        }

        public static implicit operator char(Nodes city)
        {
            return city.Code;
        }

        public static implicit operator Nodes(char code)
        {
            return new Nodes(code);
        }
    }
}
