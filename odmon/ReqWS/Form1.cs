using System;
using System.Windows.Forms;

namespace ReqApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            selectServer.SelectedIndex = 3;

            DataMan.Instance.addReqItem(listReq);
            NetworkMan.Instance.txtLog = txtLog;

            DataMan.Instance.setServer(selectServer.SelectedIndex);
        }

        private void listReq_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listReq.SelectedItem == null) return;

            //if (textBearer.Text!=null )
            //{
            //    DataMan.Instance.UserToken = "Bearer " + textBearer.Text;
            //}

            //string _key = listReq.SelectedItem.ToString();
            //Clipboard.SetData(DataFormats.Text, _key);

            //DataMan.Instance.setReqItem(_key);
            //txtReq.Text = DataMan.Instance.jsonstr;

        }

        private void selectServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataMan.Instance.setServer(selectServer.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
			//NetworkMan.Instance.requestServer(txtReq.Text);

			if (textID.Text != null)
			{
				DataMan.Instance.deviceID = textID.Text;
			}

            string _key = listReq.SelectedItem.ToString();
			DataMan.Instance.setReqItem(_key);

		}
    }
}
