using System;
using System.Collections.Generic;
using System.Text;
using TrainRoute.BusinessEntities;

namespace TrainRoute.Interfaces
{

    public interface ITrip
    {
        IList<IEdge> Route { get; }
        int getNumberOfStops();
        Nodes LastNode();
        Nodes StartNode();
        bool IsEmpty();
        bool Contains(IEdge edge);
        void AddEdge(IEdge edge);
        int GetDistance();

    }

}
