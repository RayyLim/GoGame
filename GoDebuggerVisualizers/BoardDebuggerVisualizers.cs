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
//
//            StringBuilder sb = new StringBuilder();
//
//            sb.AppendLine(" 1234567890123456789");
//
//            // Debugger.Launch();
//            for (int y = 1; y <= Board.BOARDSIZE; y++ )
//            {
//                int row = y;
//                if (row > 9)
//                    row = row%10;
//
//                sb.Append(row);
//
//
//                for(int x = 1; x <= Board.BOARDSIZE; x++)
//                {
//                    string stoneString;
//
//
//
//                    switch(board.GetStoneColor(x, y))
//                    {
//                        case StoneColor.Black:
//                            stoneString = "B";
//                            break;
//                        case StoneColor.White:
//                            stoneString = "W";
//                            break;
//                        default:
//                            stoneString = " ";
//                            break;
//                    }
//                    sb.Append(stoneString);
//                }
//                sb.AppendLine();
//            }
//            
//            MessageBox.Show(sb.ToString());

        }
    }


    public class BoardDebuggerVisualizers:DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
           var board =  (objectProvider.GetObject()) as Board;

            MessageBox.Show("dude");

        }
    }
}
