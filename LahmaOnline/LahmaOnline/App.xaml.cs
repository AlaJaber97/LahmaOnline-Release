using LahmaOnline.Helper;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace LahmaOnline
{
    public partial class App : Application
    {
        public static NavigationPage _nav { get; set; }
        public static int HightScreenUseInDesign { get; } = 812;
        public static int WidthScreenUseInDesign { get; } = 375;
        public static double ScreenWidth { get; set; } = Device.info.ScaledScreenSize.Width;
        public static double ScreenHeight { get; set; } = Device.info.ScaledScreenSize.Height;


        public App()
        {             
            try
            {
                InitializeComponent();
                GetSettingApp();
                MainPage = _nav = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                Log.Warning("Error-Startup", $"this message error: {ex.Message} \n" +
                    $"this stack error: {ex.StackTrace}");
            }
        }


        private async void GetSettingApp()
        {
            //Get  Language
            AppStatics.Language = await new UserInfo().GetLanguage();
            switch (AppStatics.Language)
            {
                case 1:
                    AppStatics.IsRTL = false;
                    AppStatics.CluterLanguage = "en-US";
                    SetLocale(new CultureInfo("en-US"), AppStatics.IsRTL);
                    break;
                case 2:
                    AppStatics.IsRTL = true;
                    AppStatics.CluterLanguage = "ar-AE";
                    SetLocale(new CultureInfo("ar-AE"), AppStatics.IsRTL);
                    break;
            }

            AppStatics.UserID = (await new REtoken().GetUserID()).idUser;

            if (AppStatics.UserID == -1)
            {
                //If no user, then Get Guid for current user
                AppStatics.GestID = (await new REtoken().GetUserID()).idGest;
                if (AppStatics.GestID == default)
                    //If no Guid, then Create New Guid for user
                    CreateNewGuid();
            }
            else
            {
                //If ther user, Than Get Token and UserProfile for him
                AppStatics.Token = await new REtoken().GetUserToken();
                AppStatics.UserProfile = await new UserInfo().GetUserProfile();
            }
        }
        public static async void CreateNewGuid()
        {
            await new REtoken().SetUserID(Guid.NewGuid(), -1);
        }
        public static void SetLocale(CultureInfo ci, bool IsRTL)
        {
            MultiLanguage.MLResource.Culture = ci;
            if (!IsRTL)
                StyleViews.Styles.SetterStyle.ViewFlowDirection.Value = FlowDirection.LeftToRight;
            else
                StyleViews.Styles.SetterStyle.ViewFlowDirection.Value = FlowDirection.RightToLeft;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}