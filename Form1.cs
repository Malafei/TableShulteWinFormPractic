using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableShulteWinFormPractic
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private int[] Randomer = new int[25];

        private int glob = default;
        private int timeLeft = 0;
        private int level = 0;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += ShowTimer;
        }

        private void start()
        {
            panel1.Visible = true;
            switch (trackBar1.Value)
            {
                case 0:
                    timeLeft = 40;
                    break;
                case 1:
                    timeLeft = 30;
                    break;
                case 2:
                    timeLeft = 20;
                    break;
                default:
                    break;
            }
            glob = 1;
            level = timeLeft;

            SelectText();

            toolStripProgressBar1.Value = 0;
            timer1.Start();
        }

        private void SelectText()
        {
            for (int i = 0; i < Randomer.Length; ++i)
            {
                int j = random.Next(0, 16) % (i + 1);
                Randomer[i] = Randomer[j];
                Randomer[j] = i + 1;
            }
            Randomer.Select(i => new { I = i, sort = Guid.NewGuid() }).OrderBy(i => i.sort).Select(i => i.I);

            int a = 0;
            foreach (Control Cont in panel1.Controls)
            {
                Cont.Visible = true;
                Cont.Text = Randomer[a].ToString();
                a++;
            }
        }

        void ShowTimer(object vObject, EventArgs e)
        {
            if (timeLeft > 0)
            {
                --timeLeft;
                toolStripStatusLabel1.Text = $"Time: {timeLeft.ToString()} sec";
            }
            else
            {
                timer1.Stop();
                DialogResult res = MessageBox.Show("Time is up!", "Timer");
                Winer(res);
            }
        }

        private void Winer(DialogResult res)
        {
            if (res == DialogResult.OK)
            {
                toolStripProgressBar1.Value = 0;
                panel1.Visible = false;
                toolStripStatusLabel1.Text = $"Time: {0} sec";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (glob == int.Parse(btn.Text))
            {
                toolStripProgressBar1.Value++;
                glob++;
                btn.Visible = false;
            }
            if (toolStripProgressBar1.Value == toolStripProgressBar1.Maximum)
            {
                timer1.Stop();
                DialogResult res = MessageBox.Show($"Your time: {(level - timeLeft)} sec", "You win!", MessageBoxButtons.OK);
                Winer(res);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            start();
        }
    }
}
