namespace SCA_Load
{
    partial class FormErrorInfo
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
            this.richTextBox_error = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox_error
            // 
            this.richTextBox_error.Location = new System.Drawing.Point(12, 12);
            this.richTextBox_error.Name = "richTextBox_error";
            this.richTextBox_error.Size = new System.Drawing.Size(568, 510);
            this.richTextBox_error.TabIndex = 0;
            this.richTextBox_error.Text = "";
            // 
            // FormErrorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 534);
            this.Controls.Add(this.richTextBox_error);
            this.Name = "FormErrorInfo";
            this.Text = "结果不匹配";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_error;
    }
}