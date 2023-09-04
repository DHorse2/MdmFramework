namespace Mdm {
    namespace World {

        partial class Form1 {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing) {
                if (disposing && (components != null)) {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent() {
                this.components = new System.ComponentModel.Container();
                this.button1 = new System.Windows.Forms.Button();
                this.checkBox1 = new System.Windows.Forms.CheckBox();
                this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
                this.treeView1 = new System.Windows.Forms.TreeView();
                this.timer1 = new System.Windows.Forms.Timer(this.components);
                this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
                this.groupBox1.SuspendLayout();
                this.SuspendLayout();
                // 
                // button1
                // 
                this.button1.Location = new System.Drawing.Point(470, 20);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(40, 30);
                this.button1.TabIndex = 0;
                this.button1.Text = "button1";
                this.button1.UseVisualStyleBackColor = true;
                // 
                // checkBox1
                // 
                this.checkBox1.AutoSize = true;
                this.checkBox1.Location = new System.Drawing.Point(460, 90);
                this.checkBox1.Name = "checkBox1";
                this.checkBox1.Size = new System.Drawing.Size(80, 17);
                this.checkBox1.TabIndex = 1;
                this.checkBox1.Text = "checkBox1";
                this.checkBox1.UseVisualStyleBackColor = true;
                // 
                // checkedListBox1
                // 
                this.checkedListBox1.FormattingEnabled = true;
                this.checkedListBox1.Location = new System.Drawing.Point(420, 140);
                this.checkedListBox1.Name = "checkedListBox1";
                this.checkedListBox1.Size = new System.Drawing.Size(120, 19);
                this.checkedListBox1.TabIndex = 2;
                // 
                // treeView1
                // 
                this.treeView1.Location = new System.Drawing.Point(30, 70);
                this.treeView1.Name = "treeView1";
                this.treeView1.Size = new System.Drawing.Size(150, 220);
                this.treeView1.TabIndex = 3;
                // 
                // openFileDialog1
                // 
                this.openFileDialog1.FileName = "openFileDialog1";
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.checkedListBox2);
                this.groupBox1.Location = new System.Drawing.Point(250, 300);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(180, 100);
                this.groupBox1.TabIndex = 4;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "groupBox1";
                // 
                // checkedListBox2
                // 
                this.checkedListBox2.FormattingEnabled = true;
                this.checkedListBox2.Location = new System.Drawing.Point(30, 40);
                this.checkedListBox2.Name = "checkedListBox2";
                this.checkedListBox2.Size = new System.Drawing.Size(100, 19);
                this.checkedListBox2.TabIndex = 0;
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(575, 492);
                this.Controls.Add(this.groupBox1);
                this.Controls.Add(this.treeView1);
                this.Controls.Add(this.checkedListBox1);
                this.Controls.Add(this.checkBox1);
                this.Controls.Add(this.button1);
                this.Name = "Form1";
                this.Text = "Form1";
                this.Shown += new System.EventHandler(this.TestForm);
                this.groupBox1.ResumeLayout(false);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.CheckBox checkBox1;
            private System.Windows.Forms.CheckedListBox checkedListBox1;
            private System.Windows.Forms.TreeView treeView1;
            private System.Windows.Forms.Timer timer1;
            private System.Windows.Forms.SaveFileDialog saveFileDialog1;
            private System.Windows.Forms.OpenFileDialog openFileDialog1;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.CheckedListBox checkedListBox2;
        }
    }
}