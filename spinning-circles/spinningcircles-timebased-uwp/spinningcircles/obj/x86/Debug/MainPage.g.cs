﻿#pragma checksum "C:\Users\fabferri\Desktop\blog\blog1\spinningcircles-timebased-uwp\spinningcircles\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AF072C47ED7169E7929C119368ABDA92"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace spinningcircles
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    this.Windows1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // MainPage.xaml line 13
                {
                    this.canv1 = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 3: // MainPage.xaml line 14
                {
                    this.sliderMainRadius = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.sliderMainRadius).ValueChanged += this.sliderMainRadius_ValueChanged;
                }
                break;
            case 4: // MainPage.xaml line 15
                {
                    this.sliderSecondaryRadius = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.sliderSecondaryRadius).ValueChanged += this.sliderSecondaryRadius_ValueChanged;
                }
                break;
            case 5: // MainPage.xaml line 16
                {
                    this.sliderCirclesSize = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.sliderCirclesSize).ValueChanged += this.sliderCirclesSize_ValueChanged;
                }
                break;
            case 6: // MainPage.xaml line 17
                {
                    this.sliderAngularVelocity = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.sliderAngularVelocity).ValueChanged += this.sliderAngularVelocity_ValueChanged;
                }
                break;
            case 7: // MainPage.xaml line 18
                {
                    this.sliderTotalCirclesNumber = (global::Windows.UI.Xaml.Controls.Slider)(target);
                    ((global::Windows.UI.Xaml.Controls.Slider)this.sliderTotalCirclesNumber).ValueChanged += this.sliderTotalCirclesNumber_ValueChanged;
                }
                break;
            case 8: // MainPage.xaml line 19
                {
                    this.textboxMainRadius = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // MainPage.xaml line 20
                {
                    this.textboxSecondaryRadius = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // MainPage.xaml line 21
                {
                    this.textboxCirclesSize = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // MainPage.xaml line 22
                {
                    this.textboxAngularVelocity = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // MainPage.xaml line 23
                {
                    this.textboxTotalCirclesNumbe = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

