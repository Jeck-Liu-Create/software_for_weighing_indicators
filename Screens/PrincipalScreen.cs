﻿using System;
using System.Windows.Forms;
using weighting_soft.Services;

namespace weighting_soft
{
    public partial class PrincipalScreen : Form
    {
        readonly SerialCommunication sc;
        ConfigFile cf;

        public PrincipalScreen()
        {
            InitializeComponent();            
            cf = new ConfigFile();
            cf.ReadConfiguration();
            sc = new SerialCommunication(cf.configPort, cf.configBaud);
            sc.Connect();
            lblWeight.Text = cf.configPort;
            InitTimer();
        }

        public void InitTimer()
        {
            Timer timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 50; //Every 50mills check the data from port
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWeight.Text = sc.GetDataLabel();
            lblPort.Text = sc.serialPort;
        }
    }
}
