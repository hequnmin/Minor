namespace Minor
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.imageSmall = new System.Windows.Forms.ImageList(this.components);
            this.imageState = new System.Windows.Forms.ImageList(this.components);
            this.treeViewPro1 = new Minor.Lib.TreeViewPro();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.HideSelection = false;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 40;
            this.treeView1.Location = new System.Drawing.Point(13, 313);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(511, 367);
            this.treeView1.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "computer");
            this.imageList.Images.SetKeyName(1, "desktop");
            this.imageList.Images.SetKeyName(2, "document");
            this.imageList.Images.SetKeyName(3, "download");
            this.imageList.Images.SetKeyName(4, "folder");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewPro1);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(938, 700);
            this.splitContainer1.SplitterDistance = 539;
            this.splitContainer1.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(395, 700);
            this.propertyGrid1.TabIndex = 0;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageSmall.Images.SetKeyName(0, "computer");
            this.imageSmall.Images.SetKeyName(1, "desktop");
            this.imageSmall.Images.SetKeyName(2, "download");
            this.imageSmall.Images.SetKeyName(3, "folder");
            this.imageSmall.Images.SetKeyName(4, "document");
            // 
            // imageState
            // 
            this.imageState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageState.ImageStream")));
            this.imageState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageState.Images.SetKeyName(0, "resultset_next");
            this.imageState.Images.SetKeyName(1, "tick");
            this.imageState.Images.SetKeyName(2, "tick_red");
            // 
            // treeViewPro1
            // 
            this.treeViewPro1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeViewPro1.FullRowSelect = true;
            this.treeViewPro1.HideSelection = false;
            this.treeViewPro1.HotTracking = true;
            this.treeViewPro1.Indent = 20;
            this.treeViewPro1.ItemHeight = 24;
            this.treeViewPro1.Location = new System.Drawing.Point(13, 13);
            this.treeViewPro1.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewPro1.Name = "treeViewPro1";
            this.treeViewPro1.ShowGrid = false;
            this.treeViewPro1.ShowLines = false;
            this.treeViewPro1.ShowNodeToolTips = true;
            this.treeViewPro1.Size = new System.Drawing.Size(511, 284);
            this.treeViewPro1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 700);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.TreeViewPro treeViewPro1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ImageList imageSmall;
        private System.Windows.Forms.ImageList imageState;
    }
}

