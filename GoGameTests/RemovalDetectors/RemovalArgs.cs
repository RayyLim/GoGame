using System;
using GoGameTests;

    [Serializable]
public class RemovalArgs
{
    public int X { get; set; }
    public int Y { get; set; }
    public Rules Rules { get; set; }
    public Board Board { get; set; }
}