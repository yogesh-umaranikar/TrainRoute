using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainRoute.BusinessEntities;
using TrainRoute.Classes;
using TrainRoute.Interfaces;


namespace TrainRoute.Services
{
    
    public class TripsSearchService
    {
        private IGraph _graph;
        public IList<IEdge> edges;
        public TripsSearchService(string townsGraph)
        {
            _graph = new Graph(townsGraph);
            edges = (IList<IEdge>)_graph.Edges;
        }
        public int GetDistanceFromRoute(string route)
        {
            IList<Nodes> nodes = InputInterpreter.GetNodesFrom(route).ToList();
            return _graph.GetDistanceFromRoute(nodes);
        }

        public IList<ITrip> GetRoutesWithExactNumberOfStops(char start, char end, int numberStops)
        {
            Func<ITrip, bool> breakCondition = (x => x.getNumberOfStops() > numberStops);
            Func<ITrip, bool> addRouteCondition =
                (x => (x.getNumberOfStops() == numberStops) && (x.StartNode().Equals(start) && (x.LastNode().Equals(end))));

            return GetPossibleRoutesFor(start, breakCondition, addRouteCondition);

        }

        public IList<ITrip> GetRoutesWithMaxNumberOfStops(char start, char end, int maxNumberOfStops)
        {
            Func<ITrip, bool> breakCondition = (x => x.getNumberOfStops() > maxNumberOfStops);
            Func<ITrip, bool> addRouteCondition =
                (x => (!x.IsEmpty()) && (x.StartNode().Code.Equals(start)) && (x.LastNode().Code.Equals(end)));

            return GetPossibleRoutesFor(start, breakCondition, addRouteCondition);
        }

        public ITrip GetShortestRouteBetween(char start, char end)
        {
            SearchTrainConfig config = new SearchTrainConfig();
            var search = config
                .defineGraph(_graph)
                .trip(new Trip(start, edges))
                .withLastNode(new Nodes(end));
            return search.runS(edges, end).Count > 0 ? search.runS(edges, end)[0] : null;
        }

        public IList<ITrip> GetRoutesWithDistanceLowerThan(int distance, char start, char end)
        {
            Func<ITrip, bool> breakCondition = (x => x.GetDistance() >= distance);
            Func<ITrip, bool> addRouteCondition =
                (x => (!x.IsEmpty()) && (x.StartNode().Equals(start)) && (x.LastNode().Equals(end)));

            return GetPossibleRoutesFor(start, breakCondition, addRouteCondition, false);
        }

        private IList<ITrip> GetPossibleRoutesFor(Nodes start, Func<ITrip, bool> breakCondition, Func<ITrip, bool> addRouteCondition)
        {
            return GetPossibleRoutesFor(start, breakCondition, addRouteCondition, true);
        }

        private IList<ITrip> GetPossibleRoutesFor(Nodes start, Func<ITrip, bool> breakCondition, Func<ITrip, bool> addRouteCondition, bool shouldBreak)
        {
            SearchTrainConfig config = new SearchTrainConfig();
            var search = config.defineGraph(_graph)
                 .breakExecutionCriteria(breakCondition)
                 .addRouteCondition(addRouteCondition)
                 .breakAfterAddingRoute(shouldBreak)
                 .trip(new Trip(start, edges));

            return search.run(edges);
        }



    }
}
