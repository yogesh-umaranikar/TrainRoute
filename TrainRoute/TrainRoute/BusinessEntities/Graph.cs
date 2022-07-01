using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainRoute.Classes;
using TrainRoute.Interfaces;
using TrainRoute.Services;

namespace TrainRoute.BusinessEntities
{
    public class Graph : IGraph
    {
        private IList<IEdge> _edges;
        public Graph(string townsGraph)
        {
            _edges = InputInterpreter.GetEdgesFrom(townsGraph).ToList();
        }

        public Graph(IList<IEdge> edges)
        {
            _edges = edges;
        }

        public IList<IEdge> Edges
        {
            get { return _edges; }
        }


        public IEnumerable<IEdge> GetRoutesStartingFrom(Nodes startingNode)
        {
            return _edges.Where(x => x.Start.Code.Equals(startingNode));
        }

        public IEnumerable<IEdge> GetRouteFrom(IList<Nodes> nodes)
        {
            for (int i = 0; i <= nodes.Count - 2; i++)
            {
                yield return GetEdge(nodes[i], nodes[i + 1]);

            }
        }

        public void Add(IEdge edge)
        {
            _edges.Add(edge);
        }

        public void AddRange(IList<IEdge> edges)
        {
            foreach (var e in edges)
            {
                Add(e);
            }
        }

        public int GetDistanceFromRoute(IList<Nodes> nodes)
        {
            return GetDistanceFromRoute(GetRouteFrom(nodes).ToList());
        }

        public int GetDistanceFromRoute(IList<IEdge> route)
        {
            int _distance = 0;
            foreach (var r in route)
            {
                _distance += r.Distance;
            }
            return _distance;
        }

        public IEdge GetEdge(Nodes start, Nodes end)
        {
            try
            {
                return _edges.Where(s => s.Start.Code == start.Code && s.End.Code == end.Code).First();
            }
            catch (Exception)
            {
                throw new NoRouteFoundException();
            }
        }
    }
}
