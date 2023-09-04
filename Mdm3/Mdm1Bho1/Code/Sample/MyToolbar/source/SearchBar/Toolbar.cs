using System;
using System.Windows.Forms;
using System.Diagnostics;
using BandObjectLib;
using System.Runtime.InteropServices;
using log4net;
using Microsoft.Win32;

namespace SearchBar
{
	/// <summary>
	/// This is a band object which is hosted in the IE browser as the taskbar toolbar not
    /// the explorer bar. This band object is registered in the system registry as a com server
    /// and its guid is added as key "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\SearchBar" 
    /// (No need to GAC SearchBar.dll as msi installer will locate the dll itself,
    /// but to install the toolbar manually you need to register the dll and all of it's dependent dlls
    /// in the GAC)
	/// </summary>
    //If you change the Guid value below, replace it at all the places it is being used using the find and replace option.
    [Guid("C9A6357B-25CC-4bcf-96C1-78736985D412")]
    [BandObject("SearchBar", BandObjectStyle.Horizontal | BandObjectStyle.ExplorerToolbar, HelpText = "SearchBar")]
    [ComVisible(true), ClassInterface(ClassInterfaceType.None)]
    public class Toolbar : BandObject
    {
        #region Variables
        private string activeSearchUrl = GoogleSearchURL;
        private ILog log = Logger.GetLogger(typeof(Toolbar));

        private System.Windows.Forms.ContextMenu contextMenuSearch;
        private System.Windows.Forms.MenuItem menuItemGoogle;
        private System.Windows.Forms.MenuItem menuItemYahoo;
        private System.Windows.Forms.MenuItem menuItemMSN;
        private System.Windows.Forms.MenuItem menuItemAsk;
        private ToolStripContainer toolStripContainer1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

        #region Consts
        //search URLs
        private const string GoogleSearchURL = "http://www.google.com/search?q=$utf8query&amp;sourceid=SearchBar&amp;ie=utf-8&amp;oe=utf-8";
        private const string YahooSearchURL = "http://search.yahoo.com/search?p=$utf8query&amp;ei=UTF-8&amp;fr=FP-tab-web-t&amp;cop=mss&amp;tab=";
        private const string MSNSearchURL = "http://search.msn.com/results.aspx?q=$utf8query";
        private const string AskSearchURL = "http://web.ask.com/web?q=$utf8query";

        private ToolStrip toolStripLeft;
        private ToolStripSplitButton toolStripSplitBtnSelectEngine;
        private ToolStripMenuItem selectGoogleSearch;
        private ToolStripMenuItem selectYahooSearch;
        private ToolStripMenuItem selectMSNSearch;
        private ToolStripMenuItem selectAskSearch;
        private ToolStripTextBox textSearchbox;
        private ToolStripButton toolStripBtnNavigate;
        private ToolStripSeparator toolStripSepInviteAndToggle;
        #endregion

        #region Ctor(s)
        public Toolbar()
        {
            log.Info("Inside ToolbarToolbar constructor.");
            InitializeComponent();

            textSearchbox.GotFocus += new EventHandler(textSearchbox_GotFocus);
            textSearchbox.Focus();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbar));
            this.contextMenuSearch = new System.Windows.Forms.ContextMenu();
            this.menuItemGoogle = new System.Windows.Forms.MenuItem();
            this.menuItemYahoo = new System.Windows.Forms.MenuItem();
            this.menuItemMSN = new System.Windows.Forms.MenuItem();
            this.menuItemAsk = new System.Windows.Forms.MenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitBtnSelectEngine = new System.Windows.Forms.ToolStripSplitButton();
            this.selectGoogleSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.selectYahooSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.selectMSNSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAskSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.textSearchbox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripBtnNavigate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSepInviteAndToggle = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuSearch
            // 
            this.contextMenuSearch.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemGoogle,
            this.menuItemYahoo,
            this.menuItemMSN,
            this.menuItemAsk});
            // 
            // menuItemGoogle
            // 
            this.menuItemGoogle.Index = 0;
            this.menuItemGoogle.Text = "Google";
            // 
            // menuItemYahoo
            // 
            this.menuItemYahoo.Index = 1;
            this.menuItemYahoo.Text = "Yahoo!";
            // 
            // menuItemMSN
            // 
            this.menuItemMSN.Index = 2;
            this.menuItemMSN.Text = "MSN";
            // 
            // menuItemAsk
            // 
            this.menuItemAsk.Index = 3;
            this.menuItemAsk.Text = "Ask";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(688, 0);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(688, 24);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripLeft);
            // 
            // toolStripLeft
            // 
            this.toolStripLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitBtnSelectEngine,
            this.textSearchbox,
            this.toolStripBtnNavigate,
            this.toolStripSepInviteAndToggle});
            this.toolStripLeft.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripLeft.Location = new System.Drawing.Point(0, 0);
            this.toolStripLeft.Name = "toolStripLeft";
            this.toolStripLeft.Size = new System.Drawing.Size(688, 25);
            this.toolStripLeft.Stretch = true;
            this.toolStripLeft.TabIndex = 2;
            // 
            // toolStripSplitBtnSelectEngine
            // 
            this.toolStripSplitBtnSelectEngine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitBtnSelectEngine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectGoogleSearch,
            this.selectYahooSearch,
            this.selectMSNSearch,
            this.selectAskSearch});
            this.toolStripSplitBtnSelectEngine.Image = global::SearchBar.Properties.Resources.Google;
            this.toolStripSplitBtnSelectEngine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitBtnSelectEngine.Name = "toolStripSplitBtnSelectEngine";
            this.toolStripSplitBtnSelectEngine.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitBtnSelectEngine.ToolTipText = "Select search engine from the list";
            // 
            // selectGoogleSearch
            // 
            this.selectGoogleSearch.Image = global::SearchBar.Properties.Resources.Google;
            this.selectGoogleSearch.Name = "selectGoogleSearch";
            this.selectGoogleSearch.Size = new System.Drawing.Size(112, 22);
            this.selectGoogleSearch.Text = "Google";
            this.selectGoogleSearch.Click += new System.EventHandler(this.selectGoogleSearch_Click);
            // 
            // selectYahooSearch
            // 
            this.selectYahooSearch.Image = global::SearchBar.Properties.Resources.Yahoo;
            this.selectYahooSearch.Name = "selectYahooSearch";
            this.selectYahooSearch.Size = new System.Drawing.Size(112, 22);
            this.selectYahooSearch.Text = "Yahoo";
            this.selectYahooSearch.Click += new System.EventHandler(this.selectYahooSearch_Click);
            // 
            // selectMSNSearch
            // 
            this.selectMSNSearch.Image = global::SearchBar.Properties.Resources.MSN;
            this.selectMSNSearch.Name = "selectMSNSearch";
            this.selectMSNSearch.Size = new System.Drawing.Size(112, 22);
            this.selectMSNSearch.Text = "MSN";
            this.selectMSNSearch.Click += new System.EventHandler(this.selectMSNSearch_Click);
            // 
            // selectAskSearch
            // 
            this.selectAskSearch.Image = global::SearchBar.Properties.Resources.Ask;
            this.selectAskSearch.Name = "selectAskSearch";
            this.selectAskSearch.Size = new System.Drawing.Size(112, 22);
            this.selectAskSearch.Text = "Ask";
            this.selectAskSearch.Click += new System.EventHandler(this.selectAskSearch_Click);
            // 
            // textSearchbox
            // 
            this.textSearchbox.Name = "textSearchbox";
            this.textSearchbox.Size = new System.Drawing.Size(100, 25);
            this.textSearchbox.ToolTipText = "Hit Enter to search and hold CTRL for new tab";
            this.textSearchbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSearchbox_KeyDown);
            // 
            // toolStripBtnNavigate
            // 
            this.toolStripBtnNavigate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripBtnNavigate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnNavigate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnNavigate.Image")));
            this.toolStripBtnNavigate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnNavigate.Name = "toolStripBtnNavigate";
            this.toolStripBtnNavigate.Size = new System.Drawing.Size(58, 22);
            this.toolStripBtnNavigate.Text = "Navigate";
            this.toolStripBtnNavigate.Click += new System.EventHandler(this.toolStripBtnNavigate_Click);
            // 
            // toolStripSepInviteAndToggle
            // 
            this.toolStripSepInviteAndToggle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSepInviteAndToggle.Name = "toolStripSepInviteAndToggle";
            this.toolStripSepInviteAndToggle.Size = new System.Drawing.Size(6, 25);
            // 
            // Toolbar
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.toolStripContainer1);
            this.MinSize = new System.Drawing.Size(624, 24);
            this.Name = "Toolbar";
            this.Size = new System.Drawing.Size(688, 24);
            this.Title = "";
            this.ExplorerAttached += new System.EventHandler(this.ToolbarToolbar_ExplorerAttached);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripLeft.ResumeLayout(false);
            this.toolStripLeft.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Handlers
        private void textSearchbox_GotFocus(object sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }

        private void ToolbarToolbar_ExplorerAttached(object sender, EventArgs e)
        {
            //Code here, if you want to initialize something as soon as the explorer get attached.
        }


        private void textSearchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string query = textSearchbox.Text.Trim();
                if (query.Length > 0)
                {
                    string url = PrepareSearchURL(activeSearchUrl, query);
                    object outobj = url;
                    object m_NullObject = null;
                    if (e.Control == true)
                    {
                        //open page in new tab.
                        object flag = BrowserNavConstants.navOpenInBackgroundTab;
                        this.Explorer.Navigate2(ref outobj, ref flag, ref m_NullObject, ref m_NullObject, ref m_NullObject);
                    }
                    else
                    {
                        this.Explorer.Navigate(url, ref m_NullObject, ref m_NullObject, ref m_NullObject, ref m_NullObject);
                    }
                }
            }

        }

        private void selectGoogleSearch_Click(object sender, EventArgs e)
        {
            activeSearchUrl = GoogleSearchURL;
            this.toolStripSplitBtnSelectEngine.Image = selectGoogleSearch.Image;
        }

        private void selectYahooSearch_Click(object sender, EventArgs e)
        {
            activeSearchUrl = YahooSearchURL;
            this.toolStripSplitBtnSelectEngine.Image = selectYahooSearch.Image;
        }

        private void selectMSNSearch_Click(object sender, EventArgs e)
        {
            activeSearchUrl = MSNSearchURL;
            this.toolStripSplitBtnSelectEngine.Image = selectMSNSearch.Image;
        }

        private void selectAskSearch_Click(object sender, EventArgs e)
        {
            activeSearchUrl = AskSearchURL;
            this.toolStripSplitBtnSelectEngine.Image = selectAskSearch.Image;
        }

        private void toolStripBtnNavigate_Click(object sender, EventArgs e)
        {
            NavigateUrl("http://www.codeproject.com/");
        }
        #endregion

        #region Helper Methods
        private void NavigateUrl(string url)
        {
            object nullObj = null;
            object flags = BrowserNavConstants.navOpenInNewWindow;
            this.Explorer.Navigate(url, ref flags, ref nullObj, ref nullObj, ref nullObj);
        }

        private string PrepareSearchURL(string url, string query)
        {
            try
            {
                query = query.Replace(" ", "+");
                url = url.Replace("$utf8query", query);
            }
            catch (Exception ex)
            {
                log.Error("Exception occured while replacing the query.", ex);
            }
            return url;
        }
        #endregion
    }
}
