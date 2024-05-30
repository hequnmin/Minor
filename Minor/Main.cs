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
using System.Collections.ObjectModel;
using System.Linq;

using Newtonsoft.Json;
using log4net;

using AntsResource;
using AntsCore.Entity;
using AntsCore.Util;

using Minor.Lib;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;
using System.Security.AccessControl;

namespace Minor
{
    public partial class Main : Form
    {
        private ImageList image16 = new ImageList();
        private Nexus nexus = new Nexus();

        public Main()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            _Initialize();
        }

        private void _Initialize()
        {
            this.Load += _Load;

            btnOpen.Click += _Open;
            btnRun.Click += _Run;

            nexus.OnCommandComplete += Nexus_OnCommandComplete;
            nexus.OnExecuteComplete += Nexus_OnExecuteComplete;

            tvwTesting.AfterSelect += _AfterSelectNode;
        }

        private void _Load(object sender, EventArgs e)
        {
            ImagesLoad();

            //string filename = Path.Combine(Application.StartupPath, "Developer-系统变量与自定义变量.tst");
            //Testing testing = TestingHelper.Load(filename);
            //TestingLoad(testing);
        }

        private void _Open(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Open File",
                Filter = "Test File(*.tst)|*.tst|All File(*.*)|*.*",
                CheckFileExists = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                Testing testing = TestingHelper.Load(filename);
                TestingLoad(testing);
            }
        }

        private void _Run(object sender, EventArgs e)
        {
            if (tvwTesting.Nodes.Count >= 1)
            {
                btnOpen.Enabled = false;
                btnRun.Enabled = false;

                Task.Run(() =>
                {
                    Connection cn = new Connection
                    {
                        Channel = 12,
                        ConnectionID = 12,
                        TCPServerIP = "127.0.0.1",
                        TCPServerPort = 10012,
                        IsActive = true,
                    };

                    Testing testing = tvwTesting.Nodes[0].Tag as Testing;
                    ConnectPro pro = new ConnectPro(cn, testing);

                    nexus.Clear();
                    nexus.Execute(Nexus.ExecuteMode.Blend, pro);
                });
            }

        }

        private void ImagesLoad()
        {
            image16.ImageSize = new Size(16, 16);
            image16.Images.Add("fatcow16_application", Properties.Resources.fatcow16_application);
            image16.Images.Add("fatcow16_use_in_formula", Properties.Resources.fatcow16_use_in_formula);
            image16.Images.Add("fatcow16_application_side_boxes", Properties.Resources.fatcow16_application_side_boxes);
            image16.Images.Add("fatcow16_tick", Properties.Resources.fatcow16_tick);
            image16.Images.Add("fatcow16_tick_red", Properties.Resources.fatcow16_tick_red);

            tvwTesting.ImageList = image16;
            tvwTesting.StateImageList = image16;
        }

        private void TestingLoad(Testing testing)
        {
            if (testing == null) return;

            tvwTesting.Nodes.Clear();
            TreeNode treeTesing = new TreeNode
            {
                Name = testing.Product.ProductNO,
                Text = testing.Product.ProductNO,
                ToolTipText = $"ProNO.:{testing.Product.ProductNO}{Environment.NewLine}"
                              + $"Model:{testing.Model.ModelNO}",
                ImageKey = "fatcow16_application",
                Tag = testing,
            };
            tvwTesting.Nodes.Add(treeTesing);

            TreeNode treeVariables = new TreeNode
            {
                Name = "Variables",
                Text = "自定义变量",
                ImageKey = "fatcow16_use_in_formula",
                Tag = testing.Variables
            };
            treeTesing.Nodes.Add(treeVariables);
            foreach (AntsCore.Expression.Operand vari in testing.Variables)
            {
                TreeNode treeVari = new TreeNode
                {
                    Name = vari.Key,
                    Text = vari.Key,
                    Tag = vari,
                };
                treeVariables.Nodes.Add(treeVari);
            }

            TreeNode treeNodes = new TreeNode
            {
                Name = "Nodes",
                Text = "测试项目",
                ImageKey = "fatcow16_application_side_boxes",
                Tag = testing.Nodes,
            };
            treeTesing.Nodes.Add(treeNodes);
            foreach (Node node in testing.Nodes)
            {
                TreeNode treeNode = new TreeNode
                {
                    Name = node.GUID,
                    Text = node.Name,
                    Tag = node,
                };
                treeNodes.Nodes.Add(treeNode);

                foreach (CommandBase cmd in node.Commands)
                {
                    TreeNode treeCmd = new TreeNode
                    {
                        Name = cmd.GUID,
                        Text = cmd.Text,
                        Tag = cmd,
                    };
                    treeNode.Nodes.Add(treeCmd);

                    foreach (CommandIO io in cmd.CommandIOs)
                    {
                        TreeNode treeIO = new TreeNode
                        {
                            Name = io.Key,
                            Text = io.Name,
                            Tag = io,
                        };
                        treeCmd.Nodes.Add(treeIO);
                    }
                }
            }

            treeTesing.Expand();
            treeNodes.Expand();
        }

        private void _AfterSelectNode(object sender, TreeViewEventArgs e)
        {
            prop.SelectedObject = e.Node.Tag;
        }

        private void Nexus_OnCommandComplete(object sender, Nexus.CommandEventArgs e)
        {
            TreeNode treeCmd = tvwTesting.Nodes[0]
                ?.Nodes["Nodes"]
                ?.Nodes[e.Node.GUID]
                ?.Nodes[e.Command.GUID];
            if (treeCmd != null)
            {
                treeCmd.StateImageKey = e.Command.Result ? "fatcow16_tick" : "fatcow16_tick_red";
                foreach (TreeNode treeIO in treeCmd.Nodes)
                {
                    if (treeIO.Tag is CommandIO io)
                    {
                        treeIO.StateImageKey = e.Command.Result ? "fatcow16_tick" : "fatcow16_tick_red";
                    }
                }
            }
        }

        private void Nexus_OnExecuteComplete(object sender, Nexus.ExecuteEventArgs e)
        {
            btnOpen.Enabled = true;
            btnRun.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
