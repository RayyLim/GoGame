using System;
using System.Collections.Generic;

    [Serializable]
internal abstract class RemovableDetector
{
    public abstract List<Tuple<int, int>> Detect(RemovalArgs args);


}