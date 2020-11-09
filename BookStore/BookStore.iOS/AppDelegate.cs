using Foundation;
using System.Linq;
using UIKit;

namespace BookStore.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override void OnResignActivation(UIApplication application)
        {
            #region Protecting Sensitive Data in the Background
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.ExtraDark);
            var blurEffectView = new UIVisualEffectView(blurEffect)
            {
                Frame = application.KeyWindow.Subviews.First().Bounds,
                AutoresizingMask = UIViewAutoresizing.FlexibleDimensions,
                Tag = 12
            };
            application.KeyWindow.Subviews.Last().AddSubview(blurEffectView);
            #endregion

            base.OnResignActivation(application);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            #region Protecting Sensitive Data in the Background
            var sub = uiApplication.KeyWindow?.Subviews.Last();
            if (sub == null)
                return;
            foreach (var vv in sub.Subviews)
            {
                if (vv.Tag == 12)
                    vv.RemoveFromSuperview();
            }
            #endregion

            base.OnActivated(uiApplication);
        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
