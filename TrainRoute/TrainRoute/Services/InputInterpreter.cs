using System;
using System.Collections.Generic;
using TrainRoute.BusinessEntities;
using TrainRoute.Classes;
using TrainRoute.Interfaces;

namespace TrainRoute.Services
{
    public static class InputInterpreter
    {
        private const char GraphSeparator = ',';
        private const char NodeSeparator = '-';

        public static IEnumerable<IEdge> GetEdgesFrom(string townsGraph)
        {
            foreach (var rawEdge in townsGraph.Split(GraphSeparator))
            {
                yield return convertToEdge(rawEdge);

            }
        }

        private static IEdge convertToEdge(string edge)
        {
            edge = edge.Trim();
            Nodes start = new Nodes(edge[0]);
            Nodes end = new Nodes(edge[1]);
            int distance = Convert.ToInt32(edge[2].ToString());
            return new Edge(start, end, distance);
        }
         
        public static IEnumerable<Nodes> GetNodesFrom(string route)
        {
            foreach (var n in route.Split(NodeSeparator))
            {
                yield return new Nodes(Convert.ToChar(n));
            }
        }
    }
}