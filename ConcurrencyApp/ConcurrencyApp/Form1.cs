using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcurrencyApp
{
    public partial class Form1 : Form
    {

        private delegate void SetProgressBarValueEvent(int value);
        private bool completed = false;
        private int i = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if(completed)
            {
                completed = false;
            }


            Thread t = new Thread(new ThreadStart(
                FillProgressBar
                ));


            t.Start();
        }

        public void FillProgressBar()
        {
            while (!completed && i <= 100)
            {               
                RequiredInvoke(i++);
                Thread.Sleep(100);
            }

        }
        public void RequiredInvoke(int value)
        {
            if (pgbStatus.InvokeRequired)
            {
                SetProgressBarValueEvent progressBarValueEvent = new SetProgressBarValueEvent(SetProgressBarValue);
                BeginInvoke(progressBarValueEvent, new object[] { value });
            }
            else
            {
                SetProgressBarValue(value);
            }
        }

        public void SetProgressBarValue(int value)
        {
            
            pgbStatus.Value = value;
            
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            completed = true;
        }

        private void PtnStop_Click(object sender, EventArgs e)
        {
            pgbStatus.Value = 0;
            completed = true;
        }
    }
}
