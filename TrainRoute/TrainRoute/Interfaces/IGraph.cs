using System;
using System.Collections.Generic;
using System.Text;
using TrainRoute.BusinessEntities;

namespace TrainRoute.Interfaces
{
    public interface IGraph
    {
        IList<IEdge> Edges { get; }

        IEnumerable<IEdge> GetRoutesStartingFrom(Nodes startingNode);

        IEnumerable<IEdge> GetRouteFrom(IList<Nodes> nodes);

        void Add(IEdge edge);

        void AddRange(IList<IEdge> edges);

        int GetDistanceFromRoute(IList<Nodes> nodes);

        int GetDistanceFromRoute(IList<IEdge> route);
    }
}
