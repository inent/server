namespace ReqApi
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.listReq = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.textID = new System.Windows.Forms.TextBox();
			this.selectServer = new System.Windows.Forms.ComboBox();
			this.txtLog = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listReq
			// 
			this.listReq.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listReq.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.listReq.FormattingEnabled = true;
			this.listReq.ItemHeight = 20;
			this.listReq.Location = new System.Drawing.Point(0, 108);
			this.listReq.Name = "listReq";
			this.listReq.Size = new System.Drawing.Size(287, 480);
			this.listReq.TabIndex = 2;
			this.listReq.SelectedIndexChanged += new System.EventHandler(this.listReq_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.listReq);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(287, 588);
			this.panel1.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.textID);
			this.panel2.Controls.Add(this.selectServer);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(287, 108);
			this.panel2.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.button1.Location = new System.Drawing.Point(12, 76);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "send";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textID
			// 
			this.textID.Location = new System.Drawing.Point(12, 46);
			this.textID.Name = "textID";
			this.textID.Size = new System.Drawing.Size(257, 27);
			this.textID.TabIndex = 5;
			// 
			// selectServer
			// 
			this.selectServer.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.selectServer.FormattingEnabled = true;
			this.selectServer.Items.AddRange(new object[] {
            "175.208.89.113",
            "58.225.62.101",
            "192.168.0.21",
            "localhost"});
			this.selectServer.Location = new System.Drawing.Point(12, 12);
			this.selectServer.Name = "selectServer";
			this.selectServer.Size = new System.Drawing.Size(257, 28);
			this.selectServer.TabIndex = 0;
			this.selectServer.SelectedIndexChanged += new System.EventHandler(this.selectServer_SelectedIndexChanged);
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(287, 0);
			this.txtLog.Margin = new System.Windows.Forms.Padding(2);
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(805, 588);
			this.txtLog.TabIndex = 4;
			this.txtLog.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1092, 588);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Request WebSocket";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listReq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox selectServer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}

