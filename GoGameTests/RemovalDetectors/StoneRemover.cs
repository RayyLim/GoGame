using System;
using System.Collections.Generic;
using System.Diagnostics;
using GoGameTests;

    [Serializable]
public class StoneRemover
{
    private readonly RemovalArgs _args;

    public StoneRemover(RemovalArgs args)
    {
        _args = args;
    }


    public void Remove()
    {
        Remove(_args.X, _args.Y);
    }

    private void Remove(int x, int y)
    {
        StoneColor oppositeColor = _args.Rules.GetOppositeColor(x, y);


        RemoveByFullySurroundedStrategy();

        RemoveByChainSurroundedStrategy();

    }

    private void RemoveByChainSurroundedStrategy()
    {
        RemovableDetector detector = new ChainRemovableDetector();
        List<Tuple<int, int>> results = detector.Detect(_args);
        if (results.Count>3)
        {
            Debugger.Break();
        }
        RemoveAllItemsInList(results);
    }

    private void RemoveAllItemsInList(List<Tuple<int, int>> results)
    {
        results.ForEach(tuple => _args.Board.RemoveStone(tuple.Item1, tuple.Item2));
    }

    private void RemoveByFullySurroundedStrategy()
    {
        RemovableDetector detector = new FullySurroundedRemovableDetector();
        List<Tuple<int, int>> results = detector.Detect(_args);
        RemoveAllItemsInList(results);
    }
}