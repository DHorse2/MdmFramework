using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;

namespace Mdm {
    namespace World {

        public partial class Form1 : Form {
            internal InfoForm InfoForm1;
            internal InfoWindow InfoWindow1;
            public Form1() {
                InitializeComponent();
            }

            private void TestForm(object sender, EventArgs e) {
                TestFormLoad();
                TestSecondForm();
            }

            private void TestFormLoad() {
                InfoWindow1 = new InfoWindow();
                InfoForm1 = new InfoForm();
            }
            public void TestSecondForm() {
                // Hide the form
                InfoForm1.InfoFormTest.Hide();
                // Add a new button (3)
                InfoForm1.button3 = new System.Windows.Forms.Button();
                InfoForm1.button3.Text = "Hello";
                // Set the position of the button based on the location of button1.
                InfoForm1.button3.Location
                   = new System.Drawing.Point(InfoForm1.button2.Left, InfoForm1.button2.Height + InfoForm1.button2.Top + 10);
                // Add button2 to the form.
                InfoForm1.InfoFormTest.Controls.Add(InfoForm1.button3);
                // Display the form as a modal dialog box.
                // InfoFormTest.ShowDialog();
                InfoForm1.InfoFormTest.Show();
            }
        }

        public class InfoWindow {
            internal System.Windows.Window InfoWindowTest;
            // internal System.Windows.Controls.Border InfoWindowTestBorder;
            internal System.Windows.Thickness InfoWindowTestThickness;

            public InfoWindow() {
                InfoWindowTest = new System.Windows.Window();
                InfoWindowTest.Title = "Main Window in Code Only";
                InfoWindowTest.Width = 300;
                InfoWindowTest.Height = 300;

                InfoWindowTest.WindowStartupLocation = WindowStartupLocation.Manual;
                InfoWindowTest.Left = 500;
                InfoWindowTest.Top = 100;

                InfoWindowTest.WindowStyle = WindowStyle.None;
                // InfoWindowTest.ResizeMode = 
                // InfoWindowTest.OverridesDefaultStyle = true;

                InfoWindowTest.BorderBrush = System.Windows.Media.Brushes.Black;

                // InfoWindowTestBorder = new System.Windows.Controls.Border();
                // InfoWindowTestBorder.Width = 400;
                // InfoWindowTestBorder.Height = 400;
                //
                InfoWindowTestThickness = new Thickness();
                InfoWindowTestThickness.Bottom = 1;
                InfoWindowTestThickness.Left = 1;
                InfoWindowTestThickness.Right = 1;
                InfoWindowTestThickness.Top = 1;
                //
                InfoWindowTest.BorderThickness = InfoWindowTestThickness;

                InfoWindowTest.Margin = new Thickness(1);

                InfoWindowTest.Background = System.Windows.Media.Brushes.White;
                // Task Bug Opacity Can't access window...
                // InfoWindowTest.AllowsTransparency = true;
                // InfoWindowTest.Opacity = 0.35;

                InfoWindowTest.ShowInTaskbar = true;

                InfoWindowTest.Show();
            }


        }

        public class InfoForm {

            internal Form InfoFormTest;
            internal System.Windows.Thickness InfoFormTestThickness;
            System.Drawing.Color InfoFormTestBackColor;
            System.Drawing.Color InfoFormTestBackTransparentColor;

            // internal GroupBox IftGroupBoxAll;
            internal System.Windows.Forms.Button button1;
            internal System.Windows.Forms.Button button2;
            internal System.Windows.Forms.Button button3;
            internal System.Windows.Forms.TextBox IftTextBox1;

            public InfoForm() {
                // Create a new instance of the form.
                InfoFormTest = new Form();
                InfoFormTestThickness = new Thickness();
                InfoFormTestThickness.Bottom = 1;
                InfoFormTestThickness.Left = 1;
                InfoFormTestThickness.Right = 1;
                InfoFormTestThickness.Top = 1;
                //
                //
                // Define the border style of the form to a dialog box.
                // InfoFormTest.FormBorderStyle = FormBorderStyle.None;
                // InfoFormTest.FormBorderStyle = FormBorderStyle.FixedDialog;
                // InfoFormTest.FormBorderStyle = FormBorderStyle.FixedSingle;
                // InfoFormTest.FormBorderStyle = FormBorderStyle.Sizable;
                // InfoFormTest.FormBorderStyle = FormBorderStyle.Fixed3D;
                // InfoFormTest.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                InfoFormTest.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                // InfoFormTest.Modal = false;

                // Set the start position of the form to the center of the screen.
                InfoFormTest.StartPosition = FormStartPosition.Manual;
                InfoFormTest.Left = 200;
                InfoFormTest.Top = 200;

                // Set the MaximizeBox to false to remove the maximize box.
                InfoFormTest.MaximizeBox = false;
                // Set the MinimizeBox to false to remove the minimize box.
                InfoFormTest.MinimizeBox = false;

                InfoFormTest.Margin = new Padding(1);

                InfoFormTestBackTransparentColor = System.Drawing.Color.White;
                InfoFormTestBackColor = System.Drawing.Color.LightBlue;

                InfoFormTest.BackColor = InfoFormTestBackTransparentColor;
                // Task Bug Opacity Can't access window...
                // InfoFormTest.AllowsTransparency = true;
                // InfoWindowTest.Opacity = 0.35;
                InfoFormTest.TransparencyKey = System.Drawing.Color.White;

                InfoFormTest.ShowInTaskbar = true;

                //
                // Form Controls
                //
                // IftGroupBoxAll = new GroupBox();
                // Create two buttons to use as the accept and cancel buttons.
                button1 = new System.Windows.Forms.Button();
                button2 = new System.Windows.Forms.Button();
                IftTextBox1 = new System.Windows.Forms.TextBox();

                // IftGroupBoxAll
                // Set the FlatStyle of the GroupBox.
                // IftGroupBoxAll.FlatStyle = FlatStyle.Popup;
                // IftGroupBoxAll.Dock = DockStyle.Fill;

                // button1
                // Set the text of button1 to "OK".
                button1.Text = "OK";
                // Set the position of the button on the form.
                button1.Location = new System.Drawing.Point(10, 10);
                // button2
                // Set the text of button2 to "Cancel".
                button2.Text = "Cancel";
                // Set the position of the button based on the location of button1.
                button2.Location
                   = new System.Drawing.Point(button1.Left, button1.Height + button1.Top + 10);
                // Set the caption bar text of the form.   
                InfoFormTest.Text = "My Dialog Box";
                // Display a help button on the form.
                InfoFormTest.HelpButton = true;
                //
                IftTextBox1.Text = "";
                IftTextBox1.Location
                    = new System.Drawing.Point(button1.Left, button1.Height + button1.Top + 100);
                IftTextBox1.Width = 300;
                IftTextBox1.Height = 20;

                //
                // Form Control Button Hooks
                //
                // Set the accept button of the form to button1.
                InfoFormTest.AcceptButton = button1;
                // Set the cancel button of the form to button2.
                InfoFormTest.CancelButton = button2;

                // Add buttons to group box
                // IftGroupBoxAll.Controls.Add(button1);
                // IftGroupBoxAll.Controls.Add(button2);
                // IftGroupBoxAll.Controls.Add(button3);
                //
                // Add buttons to the form.
                InfoFormTest.Controls.Add(button1);
                InfoFormTest.Controls.Add(button2);
                InfoFormTest.Controls.Add(IftTextBox1);
                // InfoFormTest.Controls.Add(IftGroupBoxAll);

                // Display the form as a modal dialog box.
                // InfoFormTest.ShowDialog();
                InfoFormTest.Show();
                //
                System.Drawing.Point IftWindowPos = new System.Drawing.Point();
                System.Drawing.Point IftWindowPos2 = new System.Drawing.Point();
                IftWindowPos = InfoFormTest.Location;
                IftWindowPos2.X = InfoFormTest.Left;
                IftWindowPos2.Y = InfoFormTest.Top;
                IftTextBox1.Text =
                    " X:" + IftWindowPos.X.ToString() +
                    " Y:" + IftWindowPos.Y.ToString() +
                    " X:" + IftWindowPos2.X.ToString() +
                    " Y:" + IftWindowPos2.Y.ToString()
                    ;
                // IftTextBox1.Location
                // = new Point(button1.Left, -100);
                /*
                                " X:" + InfoFormTest.Location.X.ToString() + 
                                " Y:" + InfoFormTest.Location.X.ToString()
                */

                // Set non transparent background colour for controls
                foreach (System.Windows.Forms.Control ControlItem in InfoFormTest.Controls) {
                    ControlItem.BackColor = InfoFormTestBackColor;
                }

                InfoFormTest.ResizeEnd += new EventHandler(this.InfoFormTest_ResizeEnd);

            }

            private void InfoFormTest_ResizeEnd(Object sender, EventArgs e) {

                System.Windows.Forms.MessageBox.Show("You are in the Form.ResizeEnd event.");

            }

        }
    }
}