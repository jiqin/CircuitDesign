namespace Startup
{
    partial class Form1
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
            this.button_design = new System.Windows.Forms.Button();
            this.button_load_analysis = new System.Windows.Forms.Button();
            this.button_pg_analysis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_design
            // 
            this.button_design.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_design.Location = new System.Drawing.Point(34, 25);
            this.button_design.Name = "button_design";
            this.button_design.Size = new System.Drawing.Size(365, 79);
            this.button_design.TabIndex = 0;
            this.button_design.Text = "电路图设计";
            this.button_design.UseVisualStyleBackColor = true;
            this.button_design.Click += new System.EventHandler(this.button_design_Click);
            // 
            // button_load_analysis
            // 
            this.button_load_analysis.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_load_analysis.Location = new System.Drawing.Point(34, 131);
            this.button_load_analysis.Name = "button_load_analysis";
            this.button_load_analysis.Size = new System.Drawing.Size(365, 79);
            this.button_load_analysis.TabIndex = 1;
            this.button_load_analysis.Text = "负载响应分析";
            this.button_load_analysis.UseVisualStyleBackColor = true;
            this.button_load_analysis.Click += new System.EventHandler(this.button_load_analysis_Click);
            // 
            // button_pg_analysis
            // 
            this.button_pg_analysis.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_pg_analysis.Location = new System.Drawing.Point(34, 240);
            this.button_pg_analysis.Name = "button_pg_analysis";
            this.button_pg_analysis.Size = new System.Drawing.Size(365, 79);
            this.button_pg_analysis.TabIndex = 2;
            this.button_pg_analysis.Text = "电源与地响应分析";
            this.button_pg_analysis.UseVisualStyleBackColor = true;
            this.button_pg_analysis.Click += new System.EventHandler(this.button_pg_analysis_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 346);
            this.Controls.Add(this.button_pg_analysis);
            this.Controls.Add(this.button_load_analysis);
            this.Controls.Add(this.button_design);
            this.Name = "Form1";
            this.Text = "电路设计";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_design;
        private System.Windows.Forms.Button button_load_analysis;
        private System.Windows.Forms.Button button_pg_analysis;
    }
}

