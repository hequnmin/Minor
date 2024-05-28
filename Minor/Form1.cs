using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Minor.Lib;

namespace Minor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode computer1 = new TreeNode("computer");
            TreeNode desktop1 = new TreeNode("desktop");
            TreeNode document1 = new TreeNode("document");
            TreeNode download1 = new TreeNode("download");

            computer1.ImageKey = "computer";
            desktop1.ImageKey = "desktop";
            document1.ImageKey = "document";
            download1.ImageKey = "download";

            computer1.SelectedImageKey = "computer";
            desktop1.SelectedImageKey = "desktop";
            document1.SelectedImageKey = "document";
            download1.SelectedImageKey = "download";

            computer1.StateImageKey = "tick";
            desktop1.StateImageKey = "tick";
            document1.StateImageKey = "tick";
            download1.StateImageKey = "abc";

            computer1.Nodes.Add(desktop1);
            computer1.Nodes.Add(document1);
            computer1.Nodes.Add(download1);

            TreeNode folder11 = new TreeNode("Workspace");
            folder11.ImageKey = "folder";
            folder11.SelectedImageKey = "folder";
            folder11.StateImageKey = "tick_red";
            TreeNode folder12 = new TreeNode("Blueway ATE 2020");
            folder12.ImageKey = "folder";
            folder12.SelectedImageKey = "folder";
            folder12.StateImageKey = "tick_red";
            document1.Nodes.Add(folder11);
            document1.Nodes.Add(folder12);
            //treeViewPro1.ImageList = imageSmall;
            treeViewPro1.StateImageList = imageState;
            treeViewPro1.Nodes.Add(computer1);

            TreeNode computer2 = new TreeNode("computer");
            TreeNode desktop2 = new TreeNode("desktop");
            TreeNode document2 = new TreeNode("document");
            TreeNode download2 = new TreeNode("download");

            computer2.ImageKey = "computer";
            desktop2.ImageKey = "desktop";
            document2.ImageKey = "document";
            download2.ImageKey = "download";

            computer2.SelectedImageKey = "computer";
            desktop2.SelectedImageKey = "desktop";
            document2.SelectedImageKey = "document";
            download2.SelectedImageKey = "download";

            computer2.StateImageKey = "tick";

            computer2.Nodes.Add(desktop2);
            computer2.Nodes.Add(document2);
            computer2.Nodes.Add(download2);

            treeView1.ImageList = imageList;
            treeView1.StateImageList = imageState;
            treeView1.Nodes.Add(computer2);
        }
    }
}
