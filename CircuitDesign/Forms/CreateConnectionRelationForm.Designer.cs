namespace CircuitDesign
{
    partial class CreateConnectionRelationForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip_connection_relation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_connection_relation.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip_connection_relation
            // 
            this.contextMenuStrip_connection_relation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_0,
            this.ToolStripMenuItem_1,
            this.ToolStripMenuItem_2});
            this.contextMenuStrip_connection_relation.Name = "contextMenuStrip_connection_relation";
            this.contextMenuStrip_connection_relation.Size = new System.Drawing.Size(153, 92);
            // 
            // ToolStripMenuItem_0
            // 
            this.ToolStripMenuItem_0.Name = "ToolStripMenuItem_0";
            this.ToolStripMenuItem_0.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_0.Text = "0:不导通";
            this.ToolStripMenuItem_0.Click += new System.EventHandler(this.ToolStripMenuItem_0_Click);
            // 
            // ToolStripMenuItem_1
            // 
            this.ToolStripMenuItem_1.Name = "ToolStripMenuItem_1";
            this.ToolStripMenuItem_1.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_1.Text = "1:导通";
            this.ToolStripMenuItem_1.Click += new System.EventHandler(this.ToolStripMenuItem_1_Click);
            // 
            // ToolStripMenuItem_2
            // 
            this.ToolStripMenuItem_2.Name = "ToolStripMenuItem_2";
            this.ToolStripMenuItem_2.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_2.Text = "2:控制(0)";
            this.ToolStripMenuItem_2.Click += new System.EventHandler(this.ToolStripMenuItem_2_Click);
            // 
            // CreateConnectionRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "CreateConnectionRelation";
            this.Text = "CreateConnectionRelation";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CreateConnectionRelation_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CreateConnectionRelation_MouseClick);
            this.contextMenuStrip_connection_relation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_connection_relation;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_0;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_2;

    }
}