using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainRoute.BusinessEntities;
using TrainRoute.Interfaces;

namespace TrainRoute.BusinessEntities
{
    public class Trip : ITrip
    {
        private Nodes _startNode;
        private IList<IEdge> _edges;

        public IList<IEdge> Route
        {
            get { return _edges; }
        }

        public Trip(Nodes startNode, IList<IEdge> edges)
        {
            _startNode = startNode;
            _edges = edges; // new List<IEdge>();
        }

        public int getNumberOfStops()
        {
            if (_edges.Any())
                return _edges.Count;
            return 0;
        }

        public Nodes LastNode()
        {
            if (!IsEmpty())
                return _edges.Last().End;
            return _startNode;
        }

        public Nodes StartNode()
        {
            return _startNode;
        }

        public bool IsEmpty()
        {
            return !_edges.Any();
        }

        public void AddEdge(IEdge edge)
        {
            //_edges = new List<IEdge>();
            _edges.Add(edge);
        }

        public int GetDistance()
        {
            var _distance = 0;
            foreach (var edge in Route)
            {
                _distance += edge.Distance;
            }
            return _distance;
        }

        public bool Contains(IEdge edge)
        {
            return _edges.Any(x => x.Start.Code == edge.Start.Code && x.End.Code == edge.End.Code);
        }

        public void AddRange(IList<IEdge> edges)
        {
            _edges = new List<IEdge>();
            IList<IEdge> tmpEdge = edges;
            foreach (var edge in tmpEdge)
            {
                AddEdge(edge);
            }
        }
    }
}