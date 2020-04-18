using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloydApp
{
    public partial class Form1 : Form
    {
        TextBox[,] textBoxes = null;

        public Form1()
        {
            InitializeComponent();

            for(int i=2;i<=10;i++)
            {
                graphSizeBox.Items.Add(i);
            }
        }

        private void graphSizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int graphSize = Convert.ToInt32(graphSizeBox.SelectedItem);

            panel1.Controls.Clear();

            textBoxes = new TextBox[graphSize,graphSize];

            for (int i=0;i<graphSize;i++)
            { 
                for(int j=0;j<graphSize;j++)
                {

                    textBoxes[i,j] = new TextBox();
                    textBoxes[i, j].Size = new Size(40, 20);
                    textBoxes[i, j].Location = new Point(50 * j,50 + 40 *i);
                    
                    if(i == j)
                    {
                        textBoxes[i,j].Text = "0";
                    }

                    panel1.Controls.Add(textBoxes[i, j]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int graphSize = Convert.ToInt32(graphSizeBox.SelectedItem);
            int[,] graph = new int[graphSize, graphSize];

            if(textBoxes == null)
            {
                MessageBox.Show("Ivalid input!", "Error!", MessageBoxButtons.OK);
                return;
            }

            for(int i=0;i<graphSize;i++)
            {
                for(int j=0;j<graphSize;j++)
                {
                    if(!int.TryParse(textBoxes[i,j].Text,out graph[i,j]))
                    {
                        MessageBox.Show("Ivalid input!", "Error!", MessageBoxButtons.OK);
                        return;
                    }
                    if (graph[i, j] == -1)
                        graph[i, j] = 999;
                }
            }

            for(int k=0;k<graphSize;k++)
            {
                for(int i=0;i<graphSize;i++)
                {
                    for(int j=0;j<graphSize;j++)
                    {
                        
                        graph[i, j] = Math.Min(graph[i, j], graph[i, k] + graph[k, j]);
                        
                    }
                }
            }

            richTextBox1.Text = "";

            for(int i=0;i<graphSize;i++)
            {
                for(int j=0;j<graphSize;j++)
                {
                    if (graph[i, j] != -1 && i!=j)
                        if(graph[i,j] < 900)
                            richTextBox1.Text += i + " -> " + j + " := " + graph[i,j] + "\r\n";
                        else
                        {
                            richTextBox1.Text += i + " -> " + j + " := " + "INF" + "\r\n";
                        }
                }
            }
        }
    }
}
