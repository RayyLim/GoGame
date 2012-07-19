using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoGameTests;

namespace GoDebuggerVisualizers
{
    public partial class GoBoardForm : Form
    {
        private readonly Board _board;
        public DataTable dt;

        public GoBoardForm(Board board)
        {
            _board = board;
            InitializeComponent();
        }

        private void GoBoardForm_Load(object sender, EventArgs e)
        {
            dt = new DataTable("Board");

            List<string> columnList = new List<string>();
            for(int i = 0; i <= 19; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(String));
                columnList.Add((i%10).ToString());
            }

            dt.Rows.Add(columnList.ToArray());

            for (int y = 1; y <= Board.BOARDSIZE; y++ )
            {
                int row = y%10;

                List<String> rowList = new List<string>();
                rowList.Add(row.ToString());
                for(int x = 1; x <= Board.BOARDSIZE; x++)
                {
                    string stoneString;



                    switch(_board.GetStoneColor(x, y))
                    {
                        case StoneColor.Black:
                            stoneString = "B";
                            break;
                        case StoneColor.White:
                            stoneString = "W";
                            break;
                        default:
                            stoneString = " ";
                            break;
                    }
                    rowList.Add(stoneString);
                }

                dt.Rows.Add(rowList.ToArray());
            }


            dataGridView1.DataSource = dt;
            dataGridView1.AutoSize = true;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
            
            this.ClientSize = new System.Drawing.Size(dataGridView1.Width, dataGridView1.Height);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
