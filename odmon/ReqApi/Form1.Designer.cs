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
			this.textBearer = new System.Windows.Forms.TextBox();
			this.selectServer = new System.Windows.Forms.ComboBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtReq = new System.Windows.Forms.TextBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtRes = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listReq
			// 
			this.listReq.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listReq.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.listReq.FormattingEnabled = true;
			this.listReq.ItemHeight = 20;
			this.listReq.Location = new System.Drawing.Point(0, 89);
			this.listReq.Name = "listReq";
			this.listReq.Size = new System.Drawing.Size(287, 499);
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
			this.panel2.Controls.Add(this.textBearer);
			this.panel2.Controls.Add(this.selectServer);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(287, 89);
			this.panel2.TabIndex = 3;
			// 
			// textBearer
			// 
			this.textBearer.Location = new System.Drawing.Point(12, 46);
			this.textBearer.Name = "textBearer";
			this.textBearer.Size = new System.Drawing.Size(257, 27);
			this.textBearer.TabIndex = 5;
			// 
			// selectServer
			// 
			this.selectServer.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.selectServer.FormattingEnabled = true;
			this.selectServer.Items.AddRange(new object[] {
            "175.208.89.113",
            "58.225.62.101",
            "192.168.0.21",
            "localhost",
            "SureM"});
			this.selectServer.Location = new System.Drawing.Point(12, 12);
			this.selectServer.Name = "selectServer";
			this.selectServer.Size = new System.Drawing.Size(257, 28);
			this.selectServer.TabIndex = 0;
			this.selectServer.SelectedIndexChanged += new System.EventHandler(this.selectServer_SelectedIndexChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tabControl1.Location = new System.Drawing.Point(287, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(805, 588);
			this.tabControl1.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtReq);
			this.tabPage1.Controls.Add(this.panel3);
			this.tabPage1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tabPage1.Location = new System.Drawing.Point(4, 29);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(797, 555);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "send Message";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// txtReq
			// 
			this.txtReq.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtReq.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txtReq.Location = new System.Drawing.Point(3, 35);
			this.txtReq.Multiline = true;
			this.txtReq.Name = "txtReq";
			this.txtReq.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtReq.Size = new System.Drawing.Size(791, 517);
			this.txtReq.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(791, 32);
			this.panel3.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.button1.Location = new System.Drawing.Point(4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "send";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtRes);
			this.tabPage2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tabPage2.Location = new System.Drawing.Point(4, 29);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(797, 555);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Receive Message";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtRes
			// 
			this.txtRes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtRes.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.txtRes.Location = new System.Drawing.Point(3, 3);
			this.txtRes.Multiline = true;
			this.txtRes.Name = "txtRes";
			this.txtRes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtRes.Size = new System.Drawing.Size(791, 549);
			this.txtRes.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1092, 588);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Request Web Api";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listReq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox selectServer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtReq;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBearer;
    }
}

