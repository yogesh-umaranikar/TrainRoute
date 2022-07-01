using System;
using System.Collections.Generic;
using System.Text;
using TrainRoute.BusinessEntities;
using TrainRoute.Interfaces;

namespace TrainRoute.Classes
{
    public class SearchTrainConfig //: ITrainConfigs
    {
        private Func<ITrip, bool> _breakCondition;
        private Func<ITrip, bool> _addRouteCondition;
        private IGraph _graph;
        private ITrip _trip;
        private bool _shouldBreak;
        private List<ITrip> _possibleTrips = new List<ITrip>();
        private int _shortestDistance = int.MaxValue;     

        public SearchTrainConfig defineGraph(IGraph graph)
        {
            _graph = graph;
            return this;
        }

        public SearchTrainConfig breakExecutionCriteria(Func<ITrip, bool> breakCondition)
        {
            _breakCondition = breakCondition;
            return this;
        }

        public SearchTrainConfig addRouteCondition(Func<ITrip, bool> addRouteCondition)
        {
            _addRouteCondition = addRouteCondition;
            return this;
        }

        public SearchTrainConfig trip(ITrip trip)
        {
            _trip = trip;
            return this;
        }

        public SearchTrainConfig withLastNode(Nodes lastNode)
        {
            return this;
        }

        public SearchTrainConfig breakAfterAddingRoute(bool shouldBreak)
        {
            _shouldBreak = shouldBreak;
            return this;
        }        

        public IList<ITrip> run(IList<IEdge> edges)
        {
            dfs(_trip, edges);
            return _possibleTrips;
        }

        public IList<ITrip> runS(IList<IEdge> edges, char end)
        {
            FindShortest(_trip, edges, end);
            return _possibleTrips;
        }

        private void dfs(ITrip trip, IList<IEdge> edges)
        {
            if (_breakCondition(trip))
                return;

            if (_addRouteCondition(trip))
            {
                _possibleTrips.Add(trip);
                if (_shouldBreak)
                    return;
            }

            foreach (IEdge edge in _graph.GetRoutesStartingFrom(trip.LastNode()))
            {
                var t = new Trip(trip.StartNode(), _graph.Edges);
                t.AddRange(trip.Route);
                t.AddEdge(edge);
                dfs(t, edges);
            }
        }

        private void FindShortest(ITrip trip, IList<IEdge> edges, char end)
        {
            if (IsShortest(trip, end))
            {
                SetShortest(trip);
                return;
            }

            foreach (IEdge neighbour in _graph.GetRoutesStartingFrom(trip.LastNode()))
            {
                if (HasVisited(trip, neighbour))
                    continue;
                var t = new Trip(trip.StartNode(), _graph.Edges);
                t.AddRange(trip.Route);
                t.AddEdge(neighbour);
                FindShortest(t, edges, end);
            }
        }

        private bool HasVisited(ITrip trip, IEdge neighbour)
        {
            return trip.Contains(neighbour);
        }

        private void SetShortest(ITrip trip)
        {
            _shortestDistance = trip.GetDistance();
        }

        private bool IsShortest(ITrip trip, char end)
        {
            if (!trip.IsEmpty() && EndsWithExpectedNode(trip, end))
                return trip.GetDistance() < _shortestDistance;
            return false;
        }

        private bool EndsWithExpectedNode(ITrip trip, char end)
        {
            return (trip.LastNode().Code.Equals(end));
        }
    }
}
