using LahmaOnline.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LahmaOnline.Helper
{
    public class UserInfo
    {
        public async Task<BLL.M.Identity.UserProfile> GetUserProfile()
        {
            try
            {
                BLL.M.Identity.UserProfile userProfile = new BLL.M.Identity.UserProfile
                {
                    ArFullName = Xamarin.Essentials.Preferences.Get("ArFullName", string.Empty),
                    EnFullName = Xamarin.Essentials.Preferences.Get("EnFullName", string.Empty),
                    FstName = Xamarin.Essentials.Preferences.Get("FstName", string.Empty),
                    SecName = Xamarin.Essentials.Preferences.Get("SecName", string.Empty),
                    ThirdName = Xamarin.Essentials.Preferences.Get("ThirdName", string.Empty),
                    FamilyName = Xamarin.Essentials.Preferences.Get("FamilyName", string.Empty),
                    Password = string.Empty,
                    Email = Xamarin.Essentials.Preferences.Get("Email", string.Empty),
                    UserName = Xamarin.Essentials.Preferences.Get("UserName", string.Empty),
                    Address = Xamarin.Essentials.Preferences.Get("Address", string.Empty),
                    Bod = Xamarin.Essentials.Preferences.Get("Bod", DateTime.Now),
                    ImageUrl = Xamarin.Essentials.Preferences.Get("ImageUrl", string.Empty),
                    Gender = Xamarin.Essentials.Preferences.Get("Gender", 1),
                    Mobile = Xamarin.Essentials.Preferences.Get("Mobile", string.Empty),
                    NationalityId = Xamarin.Essentials.Preferences.Get("NationalityId", 1)

                };
                switch (AppStatics.Language)
                {
                    case 1:
                        userProfile.FullName = userProfile.EnFullName;
                        break;
                    case 2:
                        userProfile.FullName = userProfile.ArFullName;
                        break;
                }
                return userProfile;
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    return await GetUserProfile();
                }
                return null;
            }
        }

        public async void SetUserProfile(BLL.M.Identity.UserProfile userProfile)
        {
            try
            {
                AppStatics.UserProfile = userProfile;

                Xamarin.Essentials.Preferences.Set("ArFullName", string.IsNullOrWhiteSpace(userProfile.ArFullName) ? string.Empty : userProfile.ArFullName);
                Xamarin.Essentials.Preferences.Set("EnFullName", string.IsNullOrWhiteSpace(userProfile.EnFullName) ? string.Empty : userProfile.EnFullName);
                Xamarin.Essentials.Preferences.Set("FstName", string.IsNullOrWhiteSpace(userProfile.FstName) ? string.Empty : userProfile.FstName);
                Xamarin.Essentials.Preferences.Set("SecName", string.IsNullOrWhiteSpace(userProfile.SecName) ? string.Empty : userProfile.SecName);
                Xamarin.Essentials.Preferences.Set("ThirdName", string.IsNullOrWhiteSpace(userProfile.ThirdName) ? string.Empty : userProfile.ThirdName);
                Xamarin.Essentials.Preferences.Set("FamilyName", string.IsNullOrWhiteSpace(userProfile.FamilyName) ? string.Empty : userProfile.FamilyName);
                Xamarin.Essentials.Preferences.Set("Email", string.IsNullOrWhiteSpace(userProfile.Email) ? string.Empty : userProfile.Email);
                Xamarin.Essentials.Preferences.Set("UserName", string.IsNullOrWhiteSpace(userProfile.UserName) ? string.Empty : userProfile.UserName);
                Xamarin.Essentials.Preferences.Set("Address", string.IsNullOrWhiteSpace(userProfile.Address) ? string.Empty : userProfile.Address);
                Xamarin.Essentials.Preferences.Set("Bod", userProfile.Bod == null ? DateTime.Now : userProfile.Bod.Value);
                Xamarin.Essentials.Preferences.Set("ImageUrl", string.IsNullOrWhiteSpace(userProfile.ImageUrl) ? string.Empty : userProfile.ImageUrl);
                Xamarin.Essentials.Preferences.Set("Gender", userProfile.Gender);
                Xamarin.Essentials.Preferences.Set("Mobile", string.IsNullOrWhiteSpace(userProfile.Mobile) ? string.Empty : userProfile.Mobile);
                Xamarin.Essentials.Preferences.Set("NationalityId", userProfile.NationalityId);
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    SetUserProfile(userProfile);
                }
            }
        }
        public void DeleteUserProfile()
        {
            try
            {
                Xamarin.Essentials.Preferences.Remove("ArFullName");
                Xamarin.Essentials.Preferences.Remove("EnFullName");
                Xamarin.Essentials.Preferences.Remove("FstName");
                Xamarin.Essentials.Preferences.Remove("SecName");
                Xamarin.Essentials.Preferences.Remove("ThirdName");
                Xamarin.Essentials.Preferences.Remove("FamilyName");
                Xamarin.Essentials.Preferences.Remove("Email");
                Xamarin.Essentials.Preferences.Remove("UserName");
                Xamarin.Essentials.Preferences.Remove("Address");
                Xamarin.Essentials.Preferences.Remove("Bod");
                Xamarin.Essentials.Preferences.Remove("ImageUrl");
                Xamarin.Essentials.Preferences.Remove("Gender");
                Xamarin.Essentials.Preferences.Remove("Mobile");
                Xamarin.Essentials.Preferences.Remove("NationalityId");
                AppStatics.UserProfile = new BLL.M.Identity.UserProfile();
                MainPage.CountBagCart = 0;
                App.CreateNewGuid();
            }
            catch
            {

            }
        }
        public async void SetLanguage(int languageValue)
        {
            try
            {
                Xamarin.Essentials.Preferences.Set("Language", languageValue);
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    SetLanguage(languageValue);
                }
            }
        }
        public async Task<int> GetLanguage()
        {
            try
            {
                return Xamarin.Essentials.Preferences.Get("Language", 1);
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    return await GetLanguage();
                }
                return 1;
            }
        }

        public void SaveLastReciveLocation(string LocationName, double Latitude, double Longitude)
        {
            Xamarin.Essentials.Preferences.Set("NameReciveLocation", LocationName);
            Xamarin.Essentials.Preferences.Set("Latitude", Latitude);
            Xamarin.Essentials.Preferences.Set("Longitude", Longitude);
        }
    }
}
