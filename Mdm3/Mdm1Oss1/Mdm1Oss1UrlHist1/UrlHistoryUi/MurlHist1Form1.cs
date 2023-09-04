using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Mdm.Oss.UrlUtil;
using Mdm.Oss.UrlUtil.Hist;
using Mdm.Oss.ShellUtil;
using Mdm.Oss.ShellUtil.BaseUtil;
using Mdm.Oss.ShellUtil.FileUtil;

namespace Mdm.Oss.UrlUtil {
    namespace Hist {
        /// <summary>
        /// Summary description for MurlHist1Form1.
        /// </summary>
        // public class MurlHist1Form1 : System.Windows.Forms.Form
        public partial class MurlHist1Form1 : Form {
            #region Form Declarations
            private System.Windows.Forms.DataGrid dgUrlDataGrid;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Button Enumerate;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.TextBox textBoxFilter;
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.Container components = null;
            private System.Windows.Forms.Button resetFilter;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.Button Delete;
            private System.Windows.Forms.Button Add;
            private System.Windows.Forms.TextBox textBoxTitle;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.TextBox textBoxURL;
            private System.Windows.Forms.Button Query;
            private System.Windows.Forms.GroupBox groupBox2;
            private System.Windows.Forms.GroupBox groupBox3;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.TextBox textBoxQueriedURL;
            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.TextBox textBoxResult;
            #endregion

            UrlHistoryWrapperClass.STATURLEnumerator enumerator;
            UrlHistoryWrapperClass urlHistory;
            ArrayList lUrlHistList;

            public MurlHist1Form1() {
                //
                // Required for Windows Form Designer support
                //
                InitializeComponent();
                urlHistory = new UrlHistoryWrapperClass();
                enumerator = urlHistory.GetEnumerator2();
                lUrlHistList = new ArrayList();


            }

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            protected override void Dispose(bool disposing) {
                if (disposing) {
                    if (components != null) {
                        components.Dispose();
                    }
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code
            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent() {
                this.dgUrlDataGrid = new System.Windows.Forms.DataGrid();
                this.panel1 = new System.Windows.Forms.Panel();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.textBoxResult = new System.Windows.Forms.TextBox();
                this.label4 = new System.Windows.Forms.Label();
                this.label5 = new System.Windows.Forms.Label();
                this.textBoxQueriedURL = new System.Windows.Forms.TextBox();
                this.Query = new System.Windows.Forms.Button();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.textBoxFilter = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.Enumerate = new System.Windows.Forms.Button();
                this.resetFilter = new System.Windows.Forms.Button();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.button1 = new System.Windows.Forms.Button();
                this.Delete = new System.Windows.Forms.Button();
                this.Add = new System.Windows.Forms.Button();
                this.textBoxTitle = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.textBoxURL = new System.Windows.Forms.TextBox();
                ((System.ComponentModel.ISupportInitialize)(this.dgUrlDataGrid)).BeginInit();
                this.panel1.SuspendLayout();
                this.groupBox3.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.groupBox1.SuspendLayout();
                this.SuspendLayout();
                // 
                // dgUrlDataGrid
                // 
                this.dgUrlDataGrid.DataMember = "";
                this.dgUrlDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dgUrlDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
                this.dgUrlDataGrid.Location = new System.Drawing.Point(0, 277);
                this.dgUrlDataGrid.Name = "dgUrlDataGrid";
                this.dgUrlDataGrid.Size = new System.Drawing.Size(760, 265);
                this.dgUrlDataGrid.TabIndex = 0;
                // 
                // panel1
                // 
                this.panel1.Controls.Add(this.groupBox3);
                this.panel1.Controls.Add(this.groupBox2);
                this.panel1.Controls.Add(this.groupBox1);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel1.Location = new System.Drawing.Point(0, 0);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(760, 277);
                this.panel1.TabIndex = 1;
                // 
                // groupBox3
                // 
                this.groupBox3.Controls.Add(this.textBoxResult);
                this.groupBox3.Controls.Add(this.label4);
                this.groupBox3.Controls.Add(this.label5);
                this.groupBox3.Controls.Add(this.textBoxQueriedURL);
                this.groupBox3.Controls.Add(this.Query);
                this.groupBox3.Location = new System.Drawing.Point(16, 182);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(728, 87);
                this.groupBox3.TabIndex = 6;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "Query URL";
                // 
                // textBoxResult
                // 
                this.textBoxResult.Location = new System.Drawing.Point(56, 52);
                this.textBoxResult.Name = "textBoxResult";
                this.textBoxResult.ReadOnly = true;
                this.textBoxResult.Size = new System.Drawing.Size(664, 20);
                this.textBoxResult.TabIndex = 8;
                // 
                // label4
                // 
                this.label4.Location = new System.Drawing.Point(8, 52);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(48, 25);
                this.label4.TabIndex = 7;
                this.label4.Text = "Result:";
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(8, 26);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(35, 13);
                this.label5.TabIndex = 1;
                this.label5.Text = "URL: ";
                // 
                // textBoxQueriedURL
                // 
                this.textBoxQueriedURL.Location = new System.Drawing.Point(56, 17);
                this.textBoxQueriedURL.Name = "textBoxQueriedURL";
                this.textBoxQueriedURL.Size = new System.Drawing.Size(600, 20);
                this.textBoxQueriedURL.TabIndex = 0;
                // 
                // Query
                // 
                this.Query.Location = new System.Drawing.Point(664, 17);
                this.Query.Name = "Query";
                this.Query.Size = new System.Drawing.Size(56, 25);
                this.Query.TabIndex = 6;
                this.Query.Text = "Search";
                this.Query.Click += new System.EventHandler(this.Query_Click);
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.textBoxFilter);
                this.groupBox2.Controls.Add(this.label1);
                this.groupBox2.Controls.Add(this.Enumerate);
                this.groupBox2.Controls.Add(this.resetFilter);
                this.groupBox2.Location = new System.Drawing.Point(16, 9);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(728, 52);
                this.groupBox2.TabIndex = 5;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Enumerat URL";
                // 
                // textBoxFilter
                // 
                this.textBoxFilter.Location = new System.Drawing.Point(136, 17);
                this.textBoxFilter.Name = "textBoxFilter";
                this.textBoxFilter.Size = new System.Drawing.Size(488, 20);
                this.textBoxFilter.TabIndex = 2;
                // 
                // label1
                // 
                this.label1.Location = new System.Drawing.Point(96, 17);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(40, 25);
                this.label1.TabIndex = 1;
                this.label1.Text = "Filter:";
                // 
                // Enumerate
                // 
                this.Enumerate.Location = new System.Drawing.Point(8, 17);
                this.Enumerate.Name = "Enumerate";
                this.Enumerate.Size = new System.Drawing.Size(75, 25);
                this.Enumerate.TabIndex = 0;
                this.Enumerate.Text = "Enumerate";
                this.Enumerate.Click += new System.EventHandler(this.Enumerate_Click);
                // 
                // resetFilter
                // 
                this.resetFilter.Location = new System.Drawing.Point(640, 17);
                this.resetFilter.Name = "resetFilter";
                this.resetFilter.Size = new System.Drawing.Size(75, 25);
                this.resetFilter.TabIndex = 3;
                this.resetFilter.Text = "Reset Filter";
                this.resetFilter.Click += new System.EventHandler(this.resetFilter_Click);
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.button1);
                this.groupBox1.Controls.Add(this.Delete);
                this.groupBox1.Controls.Add(this.Add);
                this.groupBox1.Controls.Add(this.textBoxTitle);
                this.groupBox1.Controls.Add(this.label2);
                this.groupBox1.Controls.Add(this.label3);
                this.groupBox1.Controls.Add(this.textBoxURL);
                this.groupBox1.Location = new System.Drawing.Point(16, 69);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(728, 104);
                this.groupBox1.TabIndex = 4;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Add or Delete URL";
                // 
                // button1
                // 
                this.button1.Location = new System.Drawing.Point(552, 61);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(144, 25);
                this.button1.TabIndex = 6;
                this.button1.Text = "Clear All the History";
                this.button1.Click += new System.EventHandler(this.button1_Click);
                // 
                // Delete
                // 
                this.Delete.Enabled = false;
                this.Delete.Location = new System.Drawing.Point(432, 61);
                this.Delete.Name = "Delete";
                this.Delete.Size = new System.Drawing.Size(75, 25);
                this.Delete.TabIndex = 5;
                this.Delete.Text = "Delete";
                this.Delete.Click += new System.EventHandler(this.Delete_Click);
                // 
                // Add
                // 
                this.Add.Location = new System.Drawing.Point(312, 61);
                this.Add.Name = "Add";
                this.Add.Size = new System.Drawing.Size(75, 25);
                this.Add.TabIndex = 4;
                this.Add.Text = "Add";
                this.Add.Click += new System.EventHandler(this.Add_Click);
                // 
                // textBoxTitle
                // 
                this.textBoxTitle.Location = new System.Drawing.Point(64, 61);
                this.textBoxTitle.Name = "textBoxTitle";
                this.textBoxTitle.Size = new System.Drawing.Size(224, 20);
                this.textBoxTitle.TabIndex = 3;
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(16, 61);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(33, 13);
                this.label2.TabIndex = 2;
                this.label2.Text = "Title: ";
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(16, 26);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(35, 13);
                this.label3.TabIndex = 1;
                this.label3.Text = "URL: ";
                // 
                // textBoxURL
                // 
                this.textBoxURL.Location = new System.Drawing.Point(64, 26);
                this.textBoxURL.Name = "textBoxURL";
                this.textBoxURL.Size = new System.Drawing.Size(648, 20);
                this.textBoxURL.TabIndex = 0;
                // 
                // MurlHist1Form1
                // 
                this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
                this.ClientSize = new System.Drawing.Size(760, 542);
                this.Controls.Add(this.dgUrlDataGrid);
                this.Controls.Add(this.panel1);
                this.Name = "MurlHist1Form1";
                this.Text = "Url History Demo";
                this.Load += new System.EventHandler(this.MurlHist1Form1_Load);
                this.Resize += new System.EventHandler(this.MurlHist1Form1_Resize);
                ((System.ComponentModel.ISupportInitialize)(this.dgUrlDataGrid)).EndInit();
                this.panel1.ResumeLayout(false);
                this.groupBox3.ResumeLayout(false);
                this.groupBox3.PerformLayout();
                this.groupBox2.ResumeLayout(false);
                this.groupBox2.PerformLayout();
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                this.ResumeLayout(false);

            }
            #endregion
            #region Main
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main() {
                Application.Run(new MurlHist1Form1());
            }
            #endregion

            #region GetHistoryItems
            void GetHistoryItems() {
                lUrlHistList.Clear();
                lUrlHistList.TrimToSize();
                int iListCn = 0;
                enumerator.Reset();
                while (enumerator.MoveNext()) {
                    iListCn++;
                    lUrlHistList.Add(enumerator.Current);
                }
                enumerator.Reset();

                enumerator.GetUrlHistory(lUrlHistList);

                if (lUrlHistList.Count != 0) {
                    // lUrlHistList.Sort();
                    // lUrlHistList.Sort((IComparer) tmp);
                    lUrlHistList.Sort(SortUrlAscendingHelper.SortUrlAscending());
                    // lUrlHistList.Sort(SortFileTimeAscendingHelper.SortFileTimeAscending());
                }
            }
            #endregion

            #region Enumerate Button Click
            private void Enumerate_Click(Object sender, System.EventArgs e) {
                enumerator.Reset();
                if (textBoxFilter.Text.Length != 0) {
                    enumerator.SetFilter(textBoxFilter.Text, STATURLFLAGS.STATURLFLAG_ISTOPLEVEL);
                }

                GetHistoryItems();

                CurrencyManager cm = (CurrencyManager)this.dgUrlDataGrid.BindingContext[lUrlHistList];
                if (cm != null) {
                    cm.Refresh();
                }


                GC.Collect();

            }
            #endregion

            #region GetHistoryItems
            void CreateDataGridTable() {

                dgUrlDataGrid.DataSource = lUrlHistList;

                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = "ArrayList";

                int colwidth = (dgUrlDataGrid.ClientSize.Width - ts.RowHeaderWidth - SystemInformation.VerticalScrollBarWidth) / 5;
                // URL
                DataGridTextBoxColumn cs = new DataGridTextBoxColumn();
                cs.MappingName = "URL";
                cs.HeaderText = "URL";
                cs.Width = colwidth;
                ts.GridColumnStyles.Add(cs);
                // Title
                cs = new DataGridTextBoxColumn();
                cs.MappingName = "Title";
                cs.HeaderText = "Title";
                cs.Width = colwidth;
                ts.GridColumnStyles.Add(cs);
                // Last Visited
                cs = new DataGridTextBoxColumn();
                cs.MappingName = "LastVisited";
                cs.HeaderText = "LastVisited";
                cs.Width = colwidth;
                ts.GridColumnStyles.Add(cs);
                // Last Updated
                cs = new DataGridTextBoxColumn();
                cs.MappingName = "LastUpdated";
                cs.HeaderText = "LastUpdated";
                cs.Width = colwidth;
                ts.GridColumnStyles.Add(cs);
                // Expires
                cs = new DataGridTextBoxColumn();
                cs.MappingName = "Expires";
                cs.HeaderText = "Expires";
                cs.Width = colwidth;
                ts.GridColumnStyles.Add(cs);

                dgUrlDataGrid.TableStyles.Clear();
                dgUrlDataGrid.TableStyles.Add(ts);
                // DataGrid Events
                dgUrlDataGrid.CurrentCellChanged += new System.EventHandler(dgUrlDataGridCurrentCellChanged);
                dgUrlDataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgUrlDataGridMouseDown);
            }
            #endregion

            #region Form dgUrlDataGrid DataGrid Events
            // UrlDataGrid Cell Changed Event
            private void dgUrlDataGridCurrentCellChanged(Object sender, System.EventArgs e) {
                TestdgUrlDataGridCurrentCellChanged();
            }
            private void TestdgUrlDataGridCurrentCellChanged() {
                MessageBox.Show("Col is " + dgUrlDataGrid.CurrentCell.ColumnNumber
                   + ", Row is " + dgUrlDataGrid.CurrentCell.RowNumber
                   + ", Value is " + dgUrlDataGrid[dgUrlDataGrid.CurrentCell]);
            }

            // UrlDataGrid Mouse Down
            private void dgUrlDataGridMouseDown(Object sender, System.Windows.Forms.MouseEventArgs e) {
                DataGrid dgSenderDataGrid = (DataGrid)sender;
                System.Windows.Forms.DataGrid.HitTestInfo dgSenderHitTest;
                dgSenderHitTest = dgSenderDataGrid.HitTest(e.X, e.Y);
                String sTempMessage = "You clicked ";

                switch (dgSenderHitTest.Type) {
                    case System.Windows.Forms.DataGrid.HitTestType.None:
                        sTempMessage += "the background.";
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.Cell:
                        sTempMessage += "cell at row " + dgSenderHitTest.Row + ", col " + dgSenderHitTest.Column;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader:
                        sTempMessage += "the column header for column " + dgSenderHitTest.Column;
                        // TODO z$RelVs? dgUrlDataGridMouseDown COLUMN HEADER SORT CLICKED
                        String stmp1 = this.dgUrlDataGrid.DataBindings.ToString();
                        String stmp2 = dgSenderHitTest.Column.ToString();
                        switch (dgSenderHitTest.Column) {
                            case 0: // URL
                                lUrlHistList.Sort(SortUrlAscendingHelper.SortUrlAscending());
                                break;
                            case 1: // Title
                                lUrlHistList.Sort(SortTitleAscendingHelper.SortTitleAscending());
                                break;
                            case 2: // LastVisited
                                lUrlHistList.Sort(SortLastVisitedAscendingHelper.SortLastVisitedAscending());
                                break;
                            case 3: // LastUpdated
                                lUrlHistList.Sort(SortLastUpdatedAscendingHelper.SortLastUpdatedAscending());
                                break;
                            case 4: // Expires
                                lUrlHistList.Sort(SortExpiresAscendingHelper.SortExpiresAscending());
                                break;
                            default: // SortFileTimeAscending LastVisited
                                lUrlHistList.Sort(SortFileTimeAscendingHelper.SortFileTimeAscending()); // LastVisited
                                break;
                        }
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.RowHeader:
                        sTempMessage += "the row header for row " + dgSenderHitTest.Row;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ColumnResize:
                        sTempMessage += "the column resizer for column " + dgSenderHitTest.Column;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.RowResize:
                        sTempMessage += "the row resizer for row " + dgSenderHitTest.Row;
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.Caption:
                        sTempMessage += "the caption";
                        break;
                    case System.Windows.Forms.DataGrid.HitTestType.ParentRows:
                        sTempMessage += "the parent row";
                        break;
                }

                // TestdgUrlDataGridMouseDown(sTempMessage);
            }

            private void TestdgUrlDataGridMouseDown(String sTempMessage) {
                MessageBox.Show(sTempMessage
                  + " (Col is " + dgUrlDataGrid.CurrentCell.ColumnNumber
                  + ", Row is " + dgUrlDataGrid.CurrentCell.RowNumber
                  + ", Value is " + dgUrlDataGrid[dgUrlDataGrid.CurrentCell]
                  + ") ",
                  "Mdm.Oss.UrlUtil.Hist", MessageBoxButtons.OK
               );

            }
            #endregion

            #region Form Events
            // MurlHist1Form1_Load
            private void MurlHist1Form1_Load(Object sender, System.EventArgs e) {

                GetHistoryItems();
                CreateDataGridTable();

            }
            // resetFilter_Click
            private void resetFilter_Click(Object sender, System.EventArgs e) {
                textBoxFilter.Text = "";
            }
            // Add_Click
            private void Add_Click(Object sender, System.EventArgs e) {
                urlHistory.AddHistoryEntry(textBoxURL.Text, textBoxTitle.Text, ADDURL_FLAG.ADDURL_ADDTOHISTORYANDCACHE);
                resetFilter_Click(this, EventArgs.Empty);
                Enumerate_Click(this, EventArgs.Empty);

            }
            // Delete_Click
            private void Delete_Click(Object sender, System.EventArgs e) {
                urlHistory.DeleteHistoryEntry(textBoxURL.Text, 0);
            }
            // Query_Click
            private void Query_Click(Object sender, System.EventArgs e) {
                STATURL s = urlHistory.QueryUrl(textBoxQueriedURL.Text, STATURL_QUERYFLAGS.STATURL_QUERYFLAG_TOPLEVEL);
                if (s.pwcsUrl != null)
                    textBoxResult.Text = "\"" + textBoxQueriedURL.Text + "\" has been visited by the current user.";
                else
                    textBoxResult.Text = "\"" + textBoxQueriedURL.Text + "\" has not been visited by the current user.";
            }
            // button1_Click (Clear History)
            private void button1_Click(Object sender, System.EventArgs e) {
                if (DialogResult.OK == MessageBox.Show(this, "Are you sure ?", "Mdm.Oss.UrlUtil.Hist", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) {
                    urlHistory.ClearHistory();
                    resetFilter_Click(this, EventArgs.Empty);
                    Enumerate_Click(this, EventArgs.Empty);
                }
            }
            // MurlHist1Form1_Resize
            private void MurlHist1Form1_Resize(Object sender, System.EventArgs e) {
                CreateDataGridTable();
            }
            #endregion
        }
    }
}