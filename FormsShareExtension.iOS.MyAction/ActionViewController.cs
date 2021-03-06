﻿using System;
using Foundation;
using UIKit;
using FormsShareExtension;
using Xamarin.Forms;

namespace MyAction
{
    public partial class ActionViewController : UIViewController
    {
        protected ActionViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initialize Xamarin.Forms framework
            global::Xamarin.Forms.Forms.Init();
            // Create an instance of XF page with associated View Model
            var xfPage = new MainPage();
            var viewModel = (MainPageViewModel)xfPage.BindingContext;
            viewModel.Message = "Welcome to XF Page created from an iOS Extension";
            // Override the behavior to complete the execution of the Extension when a user press the button
            viewModel.DoCommand = new Command(() => DoneClicked(this));
            // Convert XF page to a native UIViewController which can be consumed by the iOS Extension
            var newController = xfPage.CreateViewController();
            // Make sure the presentation style is set to full screen to avoid rendering the original entry point
            newController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            // Present new view controller as a regular view controller
            this.PresentModalViewController(newController, false);
        }

        partial void DoneClicked(NSObject sender)
        {
            // Return any edited content to the host app.
            // This template doesn't do anything, so we just echo the passed-in items.
            ExtensionContext.CompleteRequest(ExtensionContext.InputItems, null);
        }
    }
}
