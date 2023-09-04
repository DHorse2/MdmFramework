using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AnalyzeIE.NET
{
	/// <summary>
	/// Resizable dialog with context menu, containing DOM treeview.
	/// This class is not public so it is not exposed to COM.
	/// </summary>
	class CMainDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView m_DOMTree;
		private System.Windows.Forms.ImageList m_ilist;
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// offset of treeview control from the right border of the dialog
		/// </summary>
		private int m_right; 
		/// <summary>
		/// offset of treeview control from the bottom of the dialog
		/// </summary>
		private int m_bottom; 

		private MSHTML.IHTMLDocument2 m_IDoc2;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem menuItemExpandAll;
		private System.Windows.Forms.MenuItem menuItemCollapseAll;
		private const string nullText = "null";

		public CMainDlg( MSHTML.IHTMLDocument2 IDoc2)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_IDoc2 = IDoc2;
		}

		private void SetTitle()
		{
			// sometimes location property does not exist if accessed from another thread
			try
			{
				MSHTML.IHTMLLocation ILoc = m_IDoc2.location;
				if ( ILoc != null)
					Text = ILoc.href;
			}
			catch
			{
				Text = m_IDoc2.url; // use URL instead
			}
		}

		/// <summary>
		/// Inserts the root HTMLDOM node into treeview
		/// </summary>
		/// <returns>true if all went ok</returns>
		private bool PrepareDOMTree()
		{
			bool bRet = false;
			try
			{
				MSHTML.IHTMLDocument3 IDoc3 = (MSHTML.IHTMLDocument3)m_IDoc2;
				try
				{
					MSHTML.IHTMLElement rootElement = IDoc3.documentElement;
					try
					{
						MSHTML.IHTMLDOMNode rootNode = (MSHTML.IHTMLDOMNode)rootElement;
						InsertDOMNode( rootNode, m_DOMTree.Nodes);
						bRet = true;
					}
					catch
					{
					}
				}
				catch
				{
				}
			}
			catch // InvalidCastException (from HRESULT for no such interface supported)
			{
			}
			return bRet;
		}

		/// <summary>
		/// finds image index in image list for given HTMLDOM node type
		/// </summary>
		/// <param name="type">HTMLDOM node type</param>
		/// <param name="expanded">true if image index is for expanded item</param>
		/// <returns>image index</returns>
		private int ImageIndexForType( int type, bool expanded)
		{
			if ( type == 1) // ELEMENT (type code == 1) always has a null value
				return expanded ? 1 : 0;
			else if ( type == 3)
				return expanded ? 3 : 2;
			else
				return expanded ? 5 : 4;
		}

		/// <summary>
		/// inserts HTMLDOM node with proper text, image index and possibly one dummy child
		/// real children are inserted in beforeexpand event response so dummy child
		/// serves to assure beforeexpand event will be fired
		/// </summary>
		/// <param name="INode">node to insert</param>
		/// <param name="parentCollection">where to insert it</param>
		private void InsertDOMNode( MSHTML.IHTMLDOMNode INode, TreeNodeCollection parentCollection)
		{
			string valueText;
			int type = INode.nodeType;
			int imageidx = ImageIndexForType( type, false);

			if ( type == 1) // ELEMENT (type code == 1) always has a null value
				valueText = nullText;
			else
				valueText = INode.nodeValue == null ? nullText : INode.nodeValue.ToString();
			string nodeText = INode.nodeName + "; value = " + valueText;
			TreeNode thisNode = new TreeNode( nodeText, imageidx, imageidx);
			thisNode.Tag = INode;

			bool bHasChildren = INode.hasChildNodes();
			if ( !bHasChildren)
			{
				try
				{
					MSHTML.IHTMLAttributeCollection IColl = 
						(MSHTML.IHTMLAttributeCollection)INode.attributes;
					bHasChildren = IsAnyAttributeSet( IColl);
				}
				catch  // InvalidCastException
				{
				}
			}
			
			if ( bHasChildren)
				thisNode.Nodes.Add( new TreeNode()); // dummy node so that it is expandable 
			parentCollection.Add( thisNode);
		}

		/// <summary>
		/// checks if any of attrubutes in collection are set
		/// </summary>
		/// <param name="IAttrColl">attribute cllection</param>
		/// <returns>true if at least one attribute is set</returns>
		private bool IsAnyAttributeSet( MSHTML.IHTMLAttributeCollection IAttrColl)
		{
			bool bRet = false;
			object pos;
			MSHTML.IHTMLDOMAttribute attribute;
			int numChildren = IAttrColl.length;

			for ( int i = 0; i < numChildren; i++)
			{
				pos = i;
				try
				{
					attribute = (MSHTML.IHTMLDOMAttribute)IAttrColl.item( ref pos);
					if ( attribute.specified)
					{
						bRet = true;
						break;
					}
				}
				catch
				{
				}
			}
			return bRet;
		}

		/// <summary>
		/// inserts one attribute collection node where node's Tag
		/// will keep collection interface for later use
		/// </summary>
		/// <param name="IAttrColl">collection interface to insert with node</param>
		/// <param name="parentCollection">where to insert the node</param>
		private void InsertAttributeCollection( MSHTML.IHTMLAttributeCollection IAttrColl, 
			TreeNodeCollection parentCollection)
		{
			const string attrCollText = "Attributes";

			TreeNode thisNode = new TreeNode( attrCollText, 6, 6);
			thisNode.Tag = IAttrColl;

			// if we called this method then there are children - add dummy for expandability
			thisNode.Nodes.Add( new TreeNode()); 
			parentCollection.Add( thisNode);
		}

		/// <summary>
		/// inserts HTMLDOM node children into treeview
		/// note that both HTMLDOM nodes and attributes are possible as children of one HTMLDOM node
		/// </summary>
		/// <param name="INode">parent HTMLDOM node</param>
		/// <param name="parentCollection">corresponding tree node collection</param>
		private void InsertDOMNodeChildren( MSHTML.IHTMLDOMNode INode, TreeNodeCollection parentCollection)
		{
			try
			{
				MSHTML.IHTMLAttributeCollection attrColl = (MSHTML.IHTMLAttributeCollection)INode.attributes;
				if ( IsAnyAttributeSet( attrColl))
					InsertAttributeCollection( attrColl, parentCollection);
			}
			catch
			{
			}

			if ( INode.hasChildNodes())
			{
				try
				{
					MSHTML.IHTMLDOMChildrenCollection childColl = (MSHTML.IHTMLDOMChildrenCollection)INode.childNodes;
					int numChildren = 0;
					if ( ( numChildren = childColl.length) > 0)
					{
						MSHTML.IHTMLDOMNode nodeItem;
						for ( int i = 0; i < numChildren; i++)
						{
							try
							{
								nodeItem = (MSHTML.IHTMLDOMNode)childColl.item( i);
								InsertDOMNode( nodeItem, parentCollection);
							}
							catch
							{
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// inserts all specified attributes as children of attribute collection node
		/// nodes can have a big collection of unspecified attributes and we ignore those
		/// </summary>
		/// <param name="IAttrColl">attribute collection interface</param>
		/// <param name="parentCollection">corresponding treeview collection</param>
		void InsertAttributes( MSHTML.IHTMLAttributeCollection IAttrColl, TreeNodeCollection parentCollection)
		{
			int numChildren = IAttrColl.length;
			object pos;
			MSHTML.IHTMLDOMAttribute attribute;

			for ( int i = 0; i < numChildren; i++)
			{
				pos = i;
				try
				{
					attribute = (MSHTML.IHTMLDOMAttribute)IAttrColl.item( ref pos);
					if ( attribute.specified)
						InsertAttribute( attribute, parentCollection);
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// inserts a single HTMLDOM attribute into treeview
		/// </summary>
		/// <param name="IAttribute">interface of the atribute to insert</param>
		/// <param name="parentCollection">where in treeview to insert it as tree node</param>
		private void InsertAttribute( MSHTML.IHTMLDOMAttribute IAttribute, TreeNodeCollection parentCollection)
		{
			string valueText = valueText = IAttribute.nodeValue == null ? nullText : IAttribute.nodeValue.ToString();
			string nodeText = IAttribute.nodeName + "; value = " + valueText;
			TreeNode thisNode = new TreeNode( nodeText, 8, 8);
			parentCollection.Add( thisNode);
		}

		public void UpdateDOM()
		{
			SetTitle();
			// Supress repainting the TreeView until all the objects have been created.
			m_DOMTree.BeginUpdate();
			m_DOMTree.Nodes.Clear();
			PrepareDOMTree();
			m_DOMTree.Select();
			m_DOMTree.EndUpdate();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CMainDlg));
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItemExpandAll = new System.Windows.Forms.MenuItem();
			this.menuItemCollapseAll = new System.Windows.Forms.MenuItem();
			this.m_ilist = new System.Windows.Forms.ImageList(this.components);
			this.m_DOMTree = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuItemExpandAll,
																						this.menuItemCollapseAll});
			// 
			// menuItemExpandAll
			// 
			this.menuItemExpandAll.Index = 0;
			this.menuItemExpandAll.Text = "Expand All";
			this.menuItemExpandAll.Click += new System.EventHandler(this.OnExpandAllClicked);
			// 
			// menuItemCollapseAll
			// 
			this.menuItemCollapseAll.Index = 1;
			this.menuItemCollapseAll.Text = "Collapse All";
			this.menuItemCollapseAll.Click += new System.EventHandler(this.OnCollapseAllClicked);
			// 
			// m_ilist
			// 
			this.m_ilist.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.m_ilist.ImageSize = new System.Drawing.Size(24, 16);
			this.m_ilist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilist.ImageStream")));
			this.m_ilist.TransparentColor = System.Drawing.Color.Cyan;
			// 
			// m_DOMTree
			// 
			this.m_DOMTree.ContextMenu = this.contextMenu;
			this.m_DOMTree.ImageList = this.m_ilist;
			this.m_DOMTree.Location = new System.Drawing.Point(8, 8);
			this.m_DOMTree.Name = "m_DOMTree";
			this.m_DOMTree.Size = new System.Drawing.Size(208, 248);
			this.m_DOMTree.TabIndex = 0;
			this.m_DOMTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterCollapse);
			this.m_DOMTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeBeforeExpand);
			// 
			// CMainDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 269);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.m_DOMTree});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(232, 296);
			this.Name = "CMainDlg";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Resize += new System.EventHandler(this.OnResize);
			this.Load += new System.EventHandler(this.OnLoad);
			this.Closed += new System.EventHandler(this.OnClosed);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// deletes all node children after it collapses and, if it really had children,
		/// adds one dummy nonde so node will be expandable next time
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">contains treeview node that collapsed</param>
		private void TreeAfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = e.Node;
			if ( node.GetNodeCount( false) > 0)
			{
				try
				{
					MSHTML.IHTMLDOMNode nodeDOM = (MSHTML.IHTMLDOMNode)node.Tag;
					node.ImageIndex = node.SelectedImageIndex = ImageIndexForType( nodeDOM.nodeType, false);
				}
				catch
				{
					node.ImageIndex = node.SelectedImageIndex = 6;
				}
				node.Nodes.Clear(); // remove all nodes
				node.Nodes.Add( new TreeNode()); // dummy node so that it is expandable 
			}
		}

		/// <summary>
		/// after removing the dummy child node that assured expandability,
		/// adds real children of HTMLDOM or attribute collection nodes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">contains the node for which event occured</param>
		private void TreeBeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			node.Nodes.Clear(); // remove dummy node
			MSHTML.IHTMLDOMNode nodeDOM;
			MSHTML.IHTMLAttributeCollection AttrColl;

			try	// if it is HTMLDOM node
			{
				nodeDOM = (MSHTML.IHTMLDOMNode)node.Tag;
				node.ImageIndex = node.SelectedImageIndex = ImageIndexForType( nodeDOM.nodeType, true);
				InsertDOMNodeChildren( nodeDOM, node.Nodes);

			}
			catch // InvalidCastException (from HRESULT no such interface supported)
			{
				try // it may be HTMLAttributeCollection node
				{
					AttrColl = (MSHTML.IHTMLAttributeCollection)node.Tag;
					node.ImageIndex = node.SelectedImageIndex = 7;
					InsertAttributes( AttrColl, node.Nodes);
				}
				catch
				{
				}
			}
		}

		/// <summary>
		/// records current offsets of treeview from right and bottom
		/// dialog borders for later resizing and updates DOM tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoad(object sender, System.EventArgs e)
		{
			m_right = Width - m_DOMTree.Width - m_DOMTree.Left; 
			m_bottom = Height - m_DOMTree.Height - m_DOMTree.Top; 
			UpdateDOM();		
		}

		/// <summary>
		/// resizes treeview so to keep the same offsets from the right and bottom
		/// borders of the dialog as they were when dialog was loaded
		/// top left corner of treeview will remain fixed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnResize(object sender, System.EventArgs e)
		{
			m_DOMTree.Width = Width - m_DOMTree.Left - m_right;
			m_DOMTree.Height = Height - m_DOMTree.Top - m_bottom;
		}

		/// <summary>
		/// currently does nothing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClosed(object sender, System.EventArgs e)
		{
				
		}

		/// <summary>
		/// context menu item click handler. Expands all children of selected or top node
		/// </summary>
		private void OnExpandAllClicked(object sender, System.EventArgs e)
		{
			m_DOMTree.BeginUpdate();
			TreeNode node = m_DOMTree.SelectedNode;
			if ( node == null)
				node = m_DOMTree.TopNode;
			node.ExpandAll();
			m_DOMTree.EndUpdate();
		}

		/// <summary>
		/// context menu item click handler. Collapses all children of selected or top node
		/// </summary>
		private void OnCollapseAllClicked(object sender, System.EventArgs e)
		{
			m_DOMTree.BeginUpdate();
			TreeNode node = m_DOMTree.SelectedNode;
			if ( node == null)
				node = m_DOMTree.TopNode;
			node.Collapse();
			m_DOMTree.EndUpdate();
		}
	}
}

