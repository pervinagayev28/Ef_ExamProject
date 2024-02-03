﻿#pragma checksum "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C11C103015C34BF912D902710A3F8CE163EF9306"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using System.Windows.Shell;
using Whatsapp.Views.ViewWindows;


namespace Whatsapp.Views.ViewWindows {
    
    
    /// <summary>
    /// WindowProfile
    /// </summary>
    public partial class WindowProfile : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Whatsapp.Views.ViewWindows.WindowProfile window;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Password;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ImageUrl;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Bio;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid OpenedImageGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Whatsapp;V1.0.0.0;component/views/viewwindows/windowprofile.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.window = ((Whatsapp.Views.ViewWindows.WindowProfile)(target));
            
            #line 11 "..\..\..\..\..\Views\ViewWindows\WindowProfile.xaml"
            this.window.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.WindowMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ImageUrl = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Bio = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.OpenedImageGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

