using System;
using System.Collections.Generic;

internal abstract class RemovableDetector
{
    public abstract List<Tuple<int, int>> Detect(RemovalArgs args);


}