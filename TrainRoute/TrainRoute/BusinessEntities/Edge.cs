using TrainRoute.Classes;
using TrainRoute.Interfaces;

namespace TrainRoute.BusinessEntities
{
    public class Edge : IEdge
    {
        private Nodes _start;
        private Nodes _end;
        private int _distance;

        public Nodes Start
        {
            get { return _start; }
        }
        public Nodes End
        {
            get { return _end; }
        }

        public int Distance
        {
            get { return _distance; }
        }

        public Edge(Nodes start, Nodes end, int distance)
        {
            _start = start;
            _end = end;
            _distance = distance;
        }
    }
}