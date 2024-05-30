using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Minor.Lib
{
    public class TreeViewPro : TreeView
    {

        /*1节点被选中 ,TreeView有焦点*/
        private readonly SolidBrush _brush1 = new SolidBrush(Color.FromArgb(209, 232, 255));//填充颜色
        private readonly Pen _pen1 = new Pen(Color.FromArgb(102, 167, 232), 1);//边框颜色

        /*2节点被选中 ,TreeView没有焦点*/
        private readonly SolidBrush _brush2 = new SolidBrush(Color.FromArgb(247, 247, 247));
        private readonly Pen _pen2 = new Pen(Color.FromArgb(222, 222, 222), 1);

        /*3 MouseMove的时候 画光标所在的节点的背景*/
        private readonly SolidBrush _brush3 = new SolidBrush(Color.FromArgb(229, 243, 251));
        private readonly Pen _pen3 = new Pen(Color.FromArgb(112, 192, 231), 1);

        public const int WM_PRINTCLIENT = 0x0318;
        public const int PRF_CLIENT = 0x00000004;

        //private bool _arrowKeyUp = false;
        //private bool _arrowKeyDown = false;

        private readonly string _base64Collapse = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAALtJREFUeNpi/P//PwMlgImBQjCIDWhsbPzQ0NDwn5GRkQGGSTLg379//AkJiQxVVVX/yXIBKHYOHTnKkJGZxVBZWYnTEBZ8pvPz8zOcPHWaoaS0jIGZmRlkCCPRBoBcwMfPx/D7zx+Gv3//kBcLggICDCqKigz9fX0MLS0tJiR5gYmJieHjxw8Mq1euYGhvbwdpPkuSC378+HFo/969DG1tbTg1gwAjrrwAjHcVUDgia8amlnHoZyaAAAMAUZxDgEaeCkwAAAAASUVORK5CYII=";
        private readonly string _base64Expand = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAL1JREFUeNpi/P//PwMlgImBQjAMDGBBF8jOzj7+588fi79//zLAApiRkZGBmZmZgYWF5cTUqVMt8RoA1GhRUlrGwMXFA2KDxUCav337wtDb022Brp4RPRqBthknJyWdiY6NY/jy7TtYjIeLk2Hp4kUMc+fNMwGqP4uiAWQAMoYC4+Tk5P9r160HYxAbJIZNPQuOsDk7d+5cE6Cfz4A4N27ciAWJYVOIzQvIXGMofR2Iv8FcgNeA0aRMOgAIMADo32ORwCadLgAAAABJRU5ErkJggg==";

        private readonly Image _imageCollapse;
        private readonly Image _imageExpand;

        private bool _showGrid;

        public bool ShowGrid
        {
            get => _showGrid;
            set => _showGrid = value;
        }

        public TreeViewPro()
        {
//#if DEBUG
//            _showGrid = true;
//#endif

            _imageCollapse = LoadImage(_base64Collapse);
            _imageExpand = LoadImage(_base64Expand);

            //双缓存防止屏幕抖动
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            this.FullRowSelect = true;
            this.HotTracking = true;
            this.HideSelection = false;
            this.ShowLines = false;
            this.ShowNodeToolTips = true;

            //this.ItemHeight = this.ImageList == null ;

            //this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            TreeViewHitTestInfo hitTestInfo = this.HitTest(e.Location);
            if (hitTestInfo.Node != null)
            {

                // 击中CheckBox范围
                if (this.CheckBoxes)
                {
                    Rectangle checkBoxBounds = new Rectangle(8, hitTestInfo.Node.Bounds.Top + (this.ItemHeight - 16) / 2, 13, 13);
                    if (checkBoxBounds.Contains(e.Location))
                    {
                        hitTestInfo.Node.Checked = !hitTestInfo.Node.Checked;
                        this.Invalidate();
                    }
                }
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            TreeNode node = e.Node;
            Rectangle bounds = e.Bounds;
            Graphics g = e.Graphics;
            
            //if ((e.State & TreeNodeStates.Hot) != 0)
            //{
            //    Console.WriteLine($"{node.Text} Hot");
            //}

            //if ((e.State & TreeNodeStates.Selected) != 0)
            //{
            //    Console.WriteLine($"{node.Text} Selected");
            //}

            if (_showGrid) g.DrawRectangle(new Pen(Color.Red), bounds);

            #region 1     选中的节点背景=========================================
            Rectangle boundsSelected = bounds;
            boundsSelected.Width--;
            boundsSelected.Height--;

            if (node.IsSelected)
            {
                if (this.Focused)
                {
                    g.FillRectangle(_brush1, boundsSelected);
                    if (_showGrid) g.DrawRectangle(_pen1, boundsSelected);
                }
                else
                {
                    g.FillRectangle(_brush2, boundsSelected);
                    if (_showGrid) g.DrawRectangle(_pen2, boundsSelected);
                }
            }
            else if ((e.State & TreeNodeStates.Hot) != 0 && node.Text != "")      //|| currentMouseMoveNode == e.Node)
            {
                g.FillRectangle(_brush3, boundsSelected);
                if (_showGrid) g.DrawRectangle(_pen3, boundsSelected);
            }
            else
            {
                g.FillRectangle(Brushes.White, boundsSelected);
            }
            #endregion

            #region State & CheckBoxes ==========================================================
            Rectangle boundsState = new Rectangle(8, bounds.Top + (this.ItemHeight - 16) / 2, 0, 0);
            if (this.StateImageList != null && this.StateImageList.Images.Count > 0)
            {
                Size size = this.StateImageList.ImageSize;
                boundsState.Width = size.Width;
                boundsState.Height = size.Height;
                if (!string.IsNullOrEmpty(node.StateImageKey))
                {
                    Image imageState = this.StateImageList.Images[0];
                    if (this.StateImageList.Images.ContainsKey(node.StateImageKey))
                    {
                        imageState = this.StateImageList.Images[node.StateImageKey];
                    }
                    g.DrawImage(imageState, boundsState);
                }
            }
            else
            {
                if (this.CheckBoxes)
                {
                    Size size = CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.CheckedNormal);
                    boundsState.Width = size.Width;
                    boundsState.Height = size.Height;
                    CheckBoxRenderer.DrawCheckBox(g, boundsState.Location, boundsState, string.Empty, this.Font, false, node.Checked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
                    if (_showGrid) g.DrawRectangle(new Pen(Color.Red), boundsState);
                }
            }
            #endregion


            #region 2 重绘+-号==========================================
            Rectangle boundsToggle = new Rectangle(boundsState.Right + node.Level * this.Indent, bounds.Top + (bounds.Height - 16) / 2, 16, 16);
            if (_showGrid) g.DrawRectangle(new Pen(Color.Red), boundsToggle);

            if (node.IsExpanded)
            {
                g.DrawImage(_imageExpand, boundsToggle);
            }
            else if (!node.IsExpanded && node.Nodes.Count > 0)
            {
                g.DrawImage(_imageCollapse, boundsToggle);
            }
            #endregion

            #region 3   绘画节点图标===================================================================
            //图标矩形
            Image image = null;
            if (this.ImageList != null && this.ImageList.Images.Count > 0 && !string.IsNullOrEmpty(e.Node.ImageKey))
            {
                //image = this.ImageList.Images.ContainsKey(e.Node.ImageKey) ? this.ImageList.Images[e.Node.ImageKey] : this.ImageList.Images[0];
                image = this.ImageList.Images.ContainsKey(e.Node.ImageKey) ? this.ImageList.Images[e.Node.ImageKey] : null;
            }

            Rectangle boundsImage = new Rectangle(
                boundsToggle.Right + 4,
                bounds.Top + (this.ItemHeight - image?.Height ?? 0) / 2,
                image?.Width ?? 0,
                image?.Height ?? 0);
            if (image != null)
            {

                if (_showGrid) g.DrawRectangle(new Pen(Color.Red), boundsImage);
                
                g.DrawImage(image, boundsImage);
            }
            #endregion

            #region 4     绘画节点文本=========================================
            Rectangle boundsText = new Rectangle(
                boundsImage.Right + 5, 
                bounds.Top,
                bounds.Width - boundsImage.Width - boundsImage.Width,
                node.Bounds.Height);

            if (_showGrid) g.DrawRectangle(new Pen(Color.Red), boundsText);

            TextRenderer.DrawText(g, node.Text, this.Font, boundsText, this.ForeColor,TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            if (this.ShowNodeToolTips) node.ToolTipText = node.Text;

            //画子节点个数 (111)
            if (node.GetNodeCount(true) > 0)
            {
                g.DrawString($"({node.GetNodeCount(true)})",
                    node.TreeView.Font,
                    Brushes.Gray,
                    boundsText.Right - 4,
                    boundsText.Top - 2);
            }

            #endregion

            //base.OnDrawNode(e);
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            //// 切换节点的展开状态
            //TreeNode node = e.Node;
            //if (node != null)
            //{
            //    node.Toggle();
            //}
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);
            //if (e.Node != null)
            //{
            //    //禁止选中空白项
            //    if (e.Node.Text == "")
            //    {
            //        //响应上下键
            //        if (_arrowKeyUp)
            //        {
            //            if (e.Node.PrevNode != null && e.Node.PrevNode.Text != "")
            //                this.SelectedNode = e.Node.PrevNode;
            //        }

            //        if (_arrowKeyDown)
            //        {
            //            if (e.Node.NextNode != null && e.Node.NextNode.Text != "")
            //                this.SelectedNode = e.Node.NextNode;
            //        }

            //        e.Cancel = true;
            //    }
            //}
        }

        /// <summary>
        /// 防止在选择设，treeNode闪屏
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= 0x02000000;// Turn on WS_EX_COMPOSITED 
                }
                return cp;

            }
        }

        private Image LoadImage(string src)
        {
            byte[] bytes = Convert.FromBase64String(src);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        private void DrawStringRectangleFFormat(Graphics graphics, string textToDraw, Font font, Brush brush, RectangleF rect)
        {
            //string textToDraw = "Your text here";     // 要绘制的文本字符串
            //RectangleF rect = new RectangleF(10, 10, 200, 100); // 要绘制文本的矩形

            //Font font = SystemFonts.DefaultFont;                      // 字体定义
            //Brush brush = Brushes.Black;            // 实心黑色画笔
            

            // 设置字符串的格式，使其在矩形内居中
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // 使用字体、画笔和目标矩形将字符串绘制到屏幕
            graphics.DrawString(textToDraw, font, brush, rect, format);
        }

        private void DrawStringFormat(Graphics graphics, Rectangle rect, string text, Font font, bool autoEllipsis)
        {
            //// 创建Graphics对象
            //Graphics graphics = CreateGraphics();
            //// 要绘制的字符串
            //string text = "This is a long string that may exceed the maximum width";
            //// 字体
            //Font font = new Font("Arial", 12);

            //// 最大宽度
            //float maxWidth = 100;

            // 测量字符串的宽度
            SizeF textSize = graphics.MeasureString(text, font);

            // 如果字符串的宽度超出了最大宽度，则截断并添加省略号
            if (autoEllipsis && textSize.Width > rect.Width)
            {
                // 计算省略号的宽度
                SizeF ellipsisSize = graphics.MeasureString("...", font);
                // 截取可用宽度
                float availableWidth = rect.Width - ellipsisSize.Width;
                // 查找适合的截断位置
                int truncateIndex = 0;
                float currentWidth = 0;

                foreach (char c in text)
                {
                    SizeF charSize = graphics.MeasureString(c.ToString(), font);

                    if (currentWidth + charSize.Width <= availableWidth)
                    {
                        currentWidth += charSize.Width;
                        truncateIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                // 截断字符串并添加省略号
                text = text.Substring(0, truncateIndex) + "...";
            }

            // 设置字符串的格式，使其在矩形内居中
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;

            // 绘制字符串
            graphics.DrawString(text, font, Brushes.Black, rect, format);
        }
    }
}
