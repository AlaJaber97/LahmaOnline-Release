using LahmaOnline.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LahmaOnline.Helper
{
    public class REtoken
    {

        public async Task<string> GetUserToken()
        {
            try
            {
                var token = await Xamarin.Essentials.SecureStorage.GetAsync("oauth_token");
                if (token != null)
                {
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    return await GetUserToken();
                }
                return null;
            }
        }
        public async Task SetUserToken(string token)
        {
            try
            {
                DeleteUserToken();                
                await Xamarin.Essentials.SecureStorage.SetAsync("oauth_token", token);
                AppStatics.Token = await GetUserToken();
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    await SetUserToken(token);
                }
            }
        }
        public async void DeleteUserToken()
        {
            try
            {
                var tokenList = await Xamarin.Essentials.SecureStorage.GetAsync("oauth_token");
                ;
                if (tokenList != null)
                {
                    Xamarin.Essentials.SecureStorage.Remove("oauth_token");
                    AppStatics.Token = null;
                }
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    DeleteUserToken();
                }
            }
        }

        public async Task<(Guid idGest, int idUser)> GetUserID()
        {
            try
            {
                var idUser = await Xamarin.Essentials.SecureStorage.GetAsync("oauth_ID");
                var idGest = await Xamarin.Essentials.SecureStorage.GetAsync("oauth_idGest");
                if (idUser != null)
                {
                    if(idGest != null)
                        return (Guid.Parse(idGest),int.Parse(idUser));
                    else
                        return (default(Guid), int.Parse(idUser));
                }
                else
                {
                    return (default(Guid),-1);
                }
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    return await GetUserID();
                }
                return (default(Guid), -1);
            }
        }
        public async Task SetUserID(Guid idGest,int idUser)
        {
            try
            {
               
                DeleteUserID();
                await Xamarin.Essentials.SecureStorage.SetAsync("oauth_idGest", idGest.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("oauth_ID", idUser.ToString());
                AppStatics.UserID = idUser;
                AppStatics.GestID = idGest;
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    await SetUserID(idGest,idUser);
                }
            }
        }
        public async void DeleteUserID()
        {
            try
            {
                var idUser = await Xamarin.Essentials.SecureStorage.GetAsync("oauth_ID");

                if (idUser != null)
                {
                    Xamarin.Essentials.SecureStorage.Remove("oauth_ID");
                    AppStatics.UserID = -1;
                }
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                var boolean = await App._nav.DisplayAlert(MultiLanguage.MLResource.Permission, MultiLanguage.MLResource.PermissionMessage, MultiLanguage.MLResource.Allow, MultiLanguage.MLResource.Denied);
                if (boolean)
                {
                    DependencyService.Get<IAuthorizeCamera>().Authorized();
                    DeleteUserID();
                }
            }
        }
    }
}
