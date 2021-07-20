using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QueuingSystem.model;

namespace Visual
{
    public partial class Form3 : System.Windows.Forms.Form
    {
        private Service pasport, viza, reg;
        private int ClientsPasport, ClientsViza, ClientsReg;
        public Form3(ref Service pasport, ref Service viza, ref Service reg)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(Form3_Paint);
            this.pasport = pasport;
            this.viza = viza;
            this.reg = reg;
            this.ClientsPasport = pasport.WaitngList().Count;
            this.ClientsViza = viza.WaitngList().Count;
            this.ClientsReg = reg.WaitngList().Count;
        }
        private void Form3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            ClientsP.Text = ClientsPasport.ToString();
            ClientsV.Text = ClientsViza.ToString();
            ClientsR.Text = ClientsReg.ToString();
            TimeP.Text = Time.average(pasport.WaitngList(), ClientsPasport).Get_time_string();
            TimeV.Text = Time.average(viza.WaitngList(), ClientsViza).Get_time_string();
            TimeR.Text = Time.average(reg.WaitngList(), ClientsReg).Get_time_string();
        }
    }
}
