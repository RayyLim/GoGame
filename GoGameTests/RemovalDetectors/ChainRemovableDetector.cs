using System;
using System.Collections.Generic;
using System.Linq;
using GoGameTests;

    [Serializable]
internal class ChainRemovableDetector : RemovableDetector
{
    private RemovalArgs _args;

    public override List<Tuple<int, int>> Detect(RemovalArgs args)
    {
        _args = args;
        int x = args.X;
        int y = args.Y;



        StoneColor oppositeColor = args.Rules.GetOppositeColor(x, y);

        List<Tuple<int, int>> toCheck = GetCheckPointsFor(x, y);
        List<Tuple<int, int>> results = new List<Tuple<int, int>>();
        toCheck.ForEach(delegate(Tuple<int, int> itemNextToCurrrent)
                            {

                                bool almostSurrounded = IsAlmostFullySurrounded(x, y, oppositeColor);
                                int xNextTome = itemNextToCurrrent.Item1;
                                int yNextTome = itemNextToCurrrent.Item2;

                                bool AlmostSurroundedToSomePosition = IsAlmostFullySurrounded(xNextTome, yNextTome, oppositeColor);

                                if (almostSurrounded && AlmostSurroundedToSomePosition)
                                {
                                    results.Add(new Tuple<int, int>(x, y));
                                    results.Add(new Tuple<int, int>(xNextTome, yNextTome));

                                }
                                IEnumerable<Tuple<int, int>> toCheckChain = GetCheckPointsFor(xNextTome, yNextTome).Where(tuple => args.Board.GetStoneColor(tuple.Item1, tuple.Item2) == args.Board.GetStoneColor(x,y));
                                foreach (var chainTuple in toCheckChain)
                                {
                                    int xNextTomeChained = chainTuple.Item1;
                                    int yNextTomeChained = chainTuple.Item2;

                                    bool AlmostSurroundedToSomePositionChain = IsAlmostFullySurrounded(xNextTomeChained, yNextTomeChained, oppositeColor);

                                    if (AlmostSurroundedToSomePositionChain)
                                    {
                                        results.Add(new Tuple<int, int>(xNextTomeChained, yNextTomeChained));

                                    }
                                }

                            });
        return results;

    }

    public bool IsAlmostFullySurrounded(int x, int y, StoneColor surroundingStoneColor)
    {
        int EDGE = Rules.EDGE;
        Board Board = _args.Board;

        if (Board.GetStoneColor(x, y) == StoneColor.Empty)
        {
            return false;
        }

        bool surroundedOnLeft = (x < EDGE || Board.GetStoneColor(x - 1, y) == surroundingStoneColor);
        bool surroundedOnRight = (x > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x + 1, y) == surroundingStoneColor);
        bool surroundedOnBottom = (y > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x, y + 1) == surroundingStoneColor);
        bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1) == surroundingStoneColor);

        int surroundedEdges = 0;
        if (surroundedOnTop) surroundedEdges++;
        if (surroundedOnRight) surroundedEdges++;
        if (surroundedOnLeft) surroundedEdges++;
        if (surroundedOnBottom) surroundedEdges++;


        bool surroundedOnLeftSameColor = (x < EDGE || Board.GetStoneColor(x - 1, y) == Board.GetStoneColor(x, y));
        bool surroundedOnRightSameColor = (x > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x + 1, y) == Board.GetStoneColor(x, y));
        bool surroundedOnBottomSameColor = (y > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x, y + 1) == Board.GetStoneColor(x, y));
        bool surroundedOnTopSameColor = (y < EDGE || Board.GetStoneColor(x, y - 1) == Board.GetStoneColor(x, y));

        int surroundedEdgesSameColor = 0;
        if (surroundedOnTopSameColor) surroundedEdgesSameColor++;
        if (surroundedOnRightSameColor) surroundedEdgesSameColor++;
        if (surroundedOnLeftSameColor) surroundedEdgesSameColor++;
        if (surroundedOnBottomSameColor) surroundedEdgesSameColor++;

        bool isAlmostFullySurrounded = (surroundedEdges + surroundedEdgesSameColor) == 4  && surroundedEdgesSameColor>=1;

        return isAlmostFullySurrounded;

    }

    private List<Tuple<int, int>> GetCheckPointsFor(int x, int y)
    {
        return new List<Tuple<int, int>>()
                   {
                       new Tuple<int, int>(x+1,y), 
                       new Tuple<int, int>(x, y-1)};
    }
}