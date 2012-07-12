using System;
using System.Collections.Generic;
using GoGameTests;

internal class FullySurroundedRemovableDetector : RemovableDetector
{
    private RemovalArgs _args;

    public override List<Tuple<int, int>> Detect(RemovalArgs args)
    {
        _args = args;
        int x = args.X;
        int y = args.Y;
        bool fullySurrounded = IsFullySurroundedBy(x, y, args.Rules.GetOppositeColor(x, y));
        if (fullySurrounded)
        {
            return new List<Tuple<int, int>>() { new Tuple<int, int>(x, y) };
        }

        return new List<Tuple<int, int>>();
    }

    public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
    {
        int EDGE = Rules.EDGE;
        Board Board = _args.Board;

        bool surroundedOnLeft = (x < EDGE || Board.GetStoneColor(x - 1, y) == surroundingStoneColor);
        bool surroundedOnRight = (x > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x + 1, y) == surroundingStoneColor);
        bool surroundedOnBottom = (y > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x, y + 1) == surroundingStoneColor);
        bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1) == surroundingStoneColor);


        bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnTop && surroundedOnBottom;

        return fullySurrounded;
    }
}