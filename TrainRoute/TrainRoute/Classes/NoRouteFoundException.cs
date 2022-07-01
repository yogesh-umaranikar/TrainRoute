using System;
using System.Collections.Generic;
using System.Text;

namespace TrainRoute.Classes
{
    public class NoRouteFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "NO SUCH ROUTE";
            }
        }
    }
}
