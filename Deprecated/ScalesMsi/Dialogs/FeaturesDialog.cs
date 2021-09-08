// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The logical equivalent of the standard Features dialog. Though it implement slightly
    /// different user experience as it has checkboxes bound to the features instead of icons context menu
    /// as MSI dialog has.
    /// </summary>
    public partial class FeaturesDialog : ManagedForm
    {
        /*https://msdn.microsoft.com/en-us/library/aa367536(v=vs.85).aspx
         * ADDLOCAL - list of features to install
         * REMOVE - list of features to uninstall
         * ADDDEFAULT - list of features to set to their default state
         * REINSTALL - list of features to repair*/

        #region Private fields and properties

        private FeatureItem[] _features;
        /// <summary>
        /// Помощник приложения.
        /// </summary>
        private readonly AppHelper _app = AppHelper.Instance;
        /// <summary>
        /// The collection of the features selected by user as the features to be installed.
        /// </summary>
        private List<string> UserSelectedItems { get; set; }
        /// <summary>
        /// The initial/default set of selected items (features) before user made any selection(s).
        /// </summary>
        private List<string> InitialUserSelectedItems { get; set; }
        private bool _isAutoCheckingActive;
        private readonly WixSharpHelper _wixSharpHelper = WixSharpHelper.Instance;

        #endregion

        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="FeaturesDialog"/> class.
        /// </summary>
        public FeaturesDialog()
        {
            InitializeComponent();
            label1.MakeTransparentOn(banner);
            label2.MakeTransparentOn(banner);
        }

        private void FeaturesDialog_Load(object sender, EventArgs e)
        {
            string drawTextOnlyProp = Runtime.Session.Property("WixSharpUI_TreeNode_TexOnlyDrawing");

            bool drawTextOnly = true;

            if (drawTextOnlyProp.IsNotEmpty())
            {
                if (string.Compare(drawTextOnlyProp, "false", StringComparison.InvariantCultureIgnoreCase) == 0)
                    drawTextOnly = false;
            }
            else
            {
                var dpi = (int)(CreateGraphics().DpiY);
                if (dpi == 96) // the checkbox custom drawing is only compatible with 96 DPI
                    drawTextOnly = false;
            }

            ReadOnlyTreeNode.Behavior.AttachTo(featuresTree, drawTextOnly);

            banner.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner");
            BuildFeaturesHierarchy();

            ResetLayout();
        }

        private void ResetLayout()
        {
            // The form controls are properly anchored and will be correctly resized on parent form
            // resizing. However the initial sizing by WinForm runtime doesn't a do good job with DPI
            // other than 96. Thus manual resizing is the only reliable option apart from going WPF.

            float ratio = banner.Image.Width / (float)banner.Image.Height;
            topPanel.Height = (int)(banner.Width / ratio);
            topBorder.Top = topPanel.Height + 1;

            var upShift = (int)(next.Height * 2.3) - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height += upShift;

            middlePanel.Top = topBorder.Bottom + 5;
            middlePanel.Height = bottomPanel.Top - 5 - middlePanel.Top;

            featuresTree.Width = (int)(middlePanel.Width / 3.0 * 1.75);

            descriptionPanel.Left = featuresTree.Right + 10;
            descriptionPanel.Width = middlePanel.Width - descriptionPanel.Left - 10;

            featuresTree.Nodes[0].EnsureVisible();
        }

        #endregion

        #region Public and private methods

        private void BuildFeaturesHierarchy()
        {
            _features = Runtime.Session.Features;

            //build the hierarchy tree
            var rootItems = _features.Where(x => x.ParentName.IsEmpty())
                                    .OrderBy(x => x.RawDisplay)
                                    .ToArray();

            var itemsToProcess = new Queue<FeatureItem>(rootItems); //features to find the children for

            while (itemsToProcess.Any())
            {
                var item = itemsToProcess.Dequeue();

                //create the view of the feature
                var view = new ReadOnlyTreeNode
                {
                    Text = item.Title,
                    Tag = item, //link view to model
                    IsReadOnly = item.DisallowAbsent,
                    DefaultChecked = item.DefaultIsToBeInstalled(),
                    Checked = item.DefaultIsToBeInstalled()
                };

                item.View = view;

                if (item.Parent != null && item.Display != FeatureDisplay.hidden)
                    (item.Parent.View as TreeNode)?.Nodes.Add(view); //link child view to parent view

                // even if the item is hidden process all its children so the correct hierarchy is established

                // find all children
                _features.Where(x => x.ParentName == item.Name)
                        .ForEach(c =>
                                 {
                                     c.Parent = item; //link child model to parent model
                                     itemsToProcess.Enqueue(c); //schedule for further processing
                                 });

                if (UserSelectedItems != null)
                    view.Checked = UserSelectedItems.Contains((view.Tag as FeatureItem)?.Name);

                if (item.Display == FeatureDisplay.expand)
                    view.Expand();
            }

            //add views to the treeView control
            rootItems.Where(x => x.Display != FeatureDisplay.hidden)
                     .Select(x => x.View)
                     .Cast<TreeNode>()
                     .ForEach(node => featuresTree.Nodes.Add(node));

            InitialUserSelectedItems = _features.Where(x => x.IsViewChecked())
                                               .Select(x => x.Name)
                                               .OrderBy(x => x)
                                               .ToList();

            _isAutoCheckingActive = true;
        }

        private void SaveUserSelection()
        {
            UserSelectedItems = _features.Where(x => x.IsViewChecked())
                                        .Select(x => x.Name)
                                        .OrderBy(x => x)
                                        .ToList();
        }

        private void back_Click(object sender, EventArgs e)
        {
            SaveUserSelection();
            Shell.GoPrev();
        }

        private void next_Click(object sender, EventArgs e)
        {
            bool userChangedFeatures = UserSelectedItems?.JoinBy(",") != InitialUserSelectedItems.JoinBy(",");
            if (userChangedFeatures)
            {
                var itemsToRemove = _features.Where(x => !x.IsViewChecked()).Select(x => x.Name).JoinBy(",");
                var itemsToInstall = _features.Where(x => x.IsViewChecked()).Select(x => x.Name).JoinBy(",");
                if (itemsToRemove.Any())
                    Runtime.Session["REMOVE"] = itemsToRemove;
                if (itemsToInstall.Any())
                    Runtime.Session["ADDLOCAL"] = itemsToInstall;
                // Заполнить настройку фич.
                _wixSharpHelper.SetSettingsFeatures(_features.Where(x => x.IsViewChecked()).Select(x => x.Name).ToList());
            }
            else
            {
                Runtime.Session["REMOVE"] = "";
                Runtime.Session["ADDLOCAL"] = "";
            }

            SaveUserSelection();
            Shell.GoNext();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Shell.Cancel();
        }

        private void featuresTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            description.Text = e.Node.FeatureItem().Description.LocalizeWith(Runtime.Localize);
        }

        private void featuresTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_isAutoCheckingActive)
            {
                _isAutoCheckingActive = false;
                bool newState = e.Node.Checked;
                var queue = new Queue<TreeNode>();
                queue.EnqueueRange(e.Node.Nodes.ToArray());

                while (queue.Any())
                {
                    var node = queue.Dequeue();
                    node.Checked = newState;
                    queue.EnqueueRange(node.Nodes.ToArray());
                }

                if (e.Node.Checked)
                {
                    var parent = e.Node.Parent;
                    while (parent != null)
                    {
                        parent.Checked = true;
                        parent = parent.Parent;
                    }
                }

                _isAutoCheckingActive = true;
            }
        }

        private void reset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _isAutoCheckingActive = false;
            _features.ForEach(f => f.ResetViewChecked());
            _isAutoCheckingActive = true;
        }

        #endregion
    }
}