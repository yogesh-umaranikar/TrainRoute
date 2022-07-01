using System;
using System.Collections.Generic;
using System.Text;
using TrainRoute.BusinessEntities;

namespace TrainRoute.Interfaces
{
    public interface IEdge
    {
        int Distance { get; }
        Nodes End { get; }
        Nodes Start { get; }
    }
}
