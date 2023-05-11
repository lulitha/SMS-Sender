using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm.Server;

namespace SendingSMS
{
    public partial class SMS : Form
    {
        private GsmCommMain _GsmCom;
        public SMS()
        {
            InitializeComponent();
        }

        //----------CODE BY-------------\\
        // LULITHA GIHAN
        // lulitha.info@gmail.com
        // 94753333435
        //------------------------------//

        private void Form1_Load(object sender, EventArgs e)
        {
            //Assign Ports
            ComPort.Items.Add("COM1");
            ComPort.Items.Add("COM2");
            ComPort.Items.Add("COM3");
            ComPort.Items.Add("COM4");
            ComPort.Items.Add("COM5");
        }

       

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (ComPort.Text == "")
            {
                MessageBox.Show("Invalid Port Name");
                return;
            }
            _GsmCom = new GsmCommMain(ComPort.Text, 9600, 150);
            Cursor.Current = Cursors.Default;
            bool retry;
            do
            {
                retry = false;
                // trying to connect
                try 
                {
                    Cursor.Current = Cursors.WaitCursor;
                    _GsmCom.Open();


                    MessageBox.Show("Modem Connected Sucessfully..!");
                    btnConnect.Enabled = false;
                    lblStatus.Text = "Modem is connected..!";
                }
                catch (Exception)
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show(this, "GSM Modem is not available", "Check again",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                        retry = true;
                    else
                    { return; }
                }
            }
            while (retry);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Send SMS
                Cursor.Current = Cursors.WaitCursor;
                SmsSubmitPdu _PDU;
                Cursor.Current = Cursors.Default;               
                _PDU = new SmsSubmitPdu(txtSMS.Text, txtNumber.Text);
                int times = 1;
                for (int i = 0; i < times; i++)
                {
                    _GsmCom.SendMessage(_PDU);
                }

                MessageBox.Show("Message Sent Succesfully..!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
