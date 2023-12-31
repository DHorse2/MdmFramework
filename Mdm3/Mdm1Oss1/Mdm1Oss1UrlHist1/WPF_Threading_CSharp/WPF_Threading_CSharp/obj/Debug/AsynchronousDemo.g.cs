﻿#pragma checksum "..\..\AsynchronousDemo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0A34ED65A833DDB3F6AA2824EC2BBF28"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPF_Threading_CSharp {
    
    
    /// <summary>
    /// AsynchronousDemo
    /// </summary>
    public partial class AsynchronousDemo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.TextBlock synchronousCount;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.Button synchronousStart;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.TextBlock asynchronousCount;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.Button asynchronousStart;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.TextBlock visualIndicator;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.StackPanel lastStackPanel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.TextBlock wpfCount;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.Button wpfAsynchronousStart;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.Grid wpfProgressBarAndText;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.ProgressBar wpfProgressBar;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Controls.Button wpfAsynchronousCancel;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Media.Animation.BeginStoryboard myBeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\AsynchronousDemo.xaml"
        internal System.Windows.Media.Animation.Storyboard myStoryboard;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPF_Threading_CSharp;component/asynchronousdemo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AsynchronousDemo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.synchronousCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.synchronousStart = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\AsynchronousDemo.xaml"
            this.synchronousStart.Click += new System.Windows.RoutedEventHandler(this.SynchronousStart_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.asynchronousCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.asynchronousStart = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\AsynchronousDemo.xaml"
            this.asynchronousStart.Click += new System.Windows.RoutedEventHandler(this.AsynchronousStart_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.visualIndicator = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.lastStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.wpfCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.wpfAsynchronousStart = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\AsynchronousDemo.xaml"
            this.wpfAsynchronousStart.Click += new System.Windows.RoutedEventHandler(this.WPFAsynchronousStart_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.wpfProgressBarAndText = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.wpfProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 11:
            this.wpfAsynchronousCancel = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\AsynchronousDemo.xaml"
            this.wpfAsynchronousCancel.Click += new System.Windows.RoutedEventHandler(this.WPFAsynchronousCancel_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.myBeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 13:
            this.myStoryboard = ((System.Windows.Media.Animation.Storyboard)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
