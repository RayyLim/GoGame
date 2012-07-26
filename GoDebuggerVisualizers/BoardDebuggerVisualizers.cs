using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using GoDebuggerVisualizers;
using GoGameTests;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(DebuggerSide),
    typeof(VisualizerObjectSource),
    Target = typeof(Board),
    Description = "Board Visualizer")]

namespace GoGameTests
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService,
                                     IVisualizerObjectProvider objectProvider)
        {
            var board = (objectProvider.GetObject()) as Board;

            new GoBoardForm(board).ShowDialog();

        }
    }
}
