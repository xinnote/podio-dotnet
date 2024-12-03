﻿using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class AppMarketService
    {
        private readonly Podio _podio;

        public AppMarketService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the categories available in the system.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-categories-37009 </para>
        /// </summary>
        /// <param name="only_used">
        ///     If true, returns only categories that are used by shares in the active users language. If
        ///     false, all categories are returned.Default value: true
        /// </param>
        /// <returns></returns>
        public async Task<AppMarketCategory> GetCategories(bool only_used = true)
        {
            var requestData = new Dictionary<string, string>()
            {
                {"only_used", only_used.ToString()}
            };
            string url = "/app_store/category/";
            return await _podio.Get<AppMarketCategory>(url, requestData);
        }

        /// <summary>
        ///     Returns all the orgs, that the user is member of, and that has shared private apps.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-orgs-with-private-shares-211715 </para>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetOrgsWithPrivateShares()
        {
            string url = "/app_store/org/";
            return await _podio.Get<List<Organization>>(url);
        }

        /// <summary>
        ///     Returns all the apps that the active user has shared.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-own-shares-38645 </para>
        /// </summary>
        /// <param name="type">The type of shares to return, either "app" or "pack", leave out for all shares</param>
        /// <param name="limit">The maximum number of results to return Default value: 6</param>
        /// <param name="offset">The offset into the returned results Default value: 0</param>
        /// <returns></returns>
        public async Task<AppMarketShares> GetOwnShares(string type, 
            long limit = 6, long offset = 0)
        {
            string url = string.Format("/app_store/{0}/own/", type);
            var requestData = new Dictionary<string, string>()
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"type", type}
            };
            return await _podio.Get<AppMarketShares>(url, requestData);
        }

        /// <summary>
        ///     Returns the recommended apps in the app market for the given "area". Current areas are: "web" and "mobile".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-recommended-shares-5340177 </para>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public async Task<AppMarketShares> GetRecommendedShares(string type, string area)
        {
            string url = string.Format("/app_store/{0}/recommended/{1}/", type, area);
            return await _podio.Get<AppMarketShares>(url);
        }

        /// <summary>
        ///     Returns the shared app from the app market with the given id. It will also return all comments and fivestar ratings
        ///     of the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-share-22505 </para>
        /// </summary>
        /// <param name="shareId"></param>
        /// <returns></returns>
        public async Task<AppMarketShare> GetShare(long shareId)
        {
            string url = string.Format("/app_store/{0}/v2", shareId);
            return await _podio.Get<AppMarketShare>(url);
        }

        /// <summary>
        ///     Returns the shares of the given object. The active users shares will be first followed by other users shares.
        ///     Besides that the shares will be sorted descending by when they were shared.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-share-by-reference-45002 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public async Task<List<AppMarketShare>> GetShareByReference(string refType, long refId)
        {
            string url = string.Format("/app_store/{0}/{1}/", refType, refId);
            return await _podio.Get<List<AppMarketShare>>(url);
        }

        /// <summary>
        ///     Returns all the apps in the app market created by the given user in the given language.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-shares-by-author-22497 </para>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="limit">The maximum number of results to return Default value: 6</param>
        /// <param name="offset">The offset into the returned results Default value :0</param>
        /// <param name="sort">
        ///     The sorting of the shares, either "install", "rating", "popularity", "recommended", "shared_on", or
        ///     "name".Default value: install
        /// </param>
        /// <returns></returns>
        public async Task<AppMarketShares> GetSharesByAuthor(string type, long userId, long limit = 6, long offset = 0,
            string sort = "install")
        {
            string url = string.Format("/app_store/{0}/author/{1}/", type, userId);
            var requestData = new Dictionary<string, string>()
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"sort", sort},
                {"type", type}
            };
            return await _podio.Get<AppMarketShares>(url, requestData);
        }

        /// <summary>
        ///     Returns the apps in the app market in the given category and language.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-shares-by-category-22498 </para>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <param name="limit">The maximum number of results to return Default value: 6</param>
        /// <param name="offset">The offset into the returned results Default value :0</param>
        /// <param name="sort">
        ///     The sorting of the shares, either "install", "rating", "popularity", "recommended", "shared_on", or
        ///     "name".Default value: install
        /// </param>
        /// <returns></returns>
        public async Task<AppMarketShares> GetSharesByCategory(string type, long categoryId, long limit = 6, long offset = 0,
            string sort = "install")
        {
            string url = string.Format("/app_store/{0}/category/{1}/", type, categoryId);
            var requestData = new Dictionary<string, string>
            {
                {"limt", limit.ToString()},
                {"offset", offset.ToString()},
                {"sort", sort},
                {"type", type}
            };
            return await _podio.Get<AppMarketShares>(url, requestData);
        }

        /// <summary>
        ///     Returns the top apps in the app market in the given language.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/get-top-shares-22496 </para>
        /// <param name="type"></param>
        /// <param name="limit">The maximum number of results to return Default value: 6</param>
        /// <param name="offset">The offset into the returned results Default value :0</param>
        /// <returns></returns>
        public async Task<AppMarketShares> GetTopShares(string type, long limit = 6, long offset = 0)
        {
            string url = string.Format("/app_store/{0}/top/", type);
            var requestData = new Dictionary<string, string>
            {
                {"limt", limit.ToString()},
                {"offset", offset.ToString()},
                {"type", type}
            };
            return await _podio.Get<AppMarketShares>(url, requestData);
        }

        /// <summary>
        ///     Installs the share with the given id on the space.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/install-share-22499 </para>
        /// <param name="shareId"></param>
        /// <param name="spaceId">The id of the space the shared app should be installed to</param>
        /// <param name="dependentShareIds">
        ///     The list of ids of the dependent shares that should also be installed. If not
        ///     specified, all dependencies will be installed
        /// </param>
        /// <returns></returns>
        public async Task<AppMarketShareInstall> InstallShare(long shareId, long spaceId, List<long> dependentShareIds)
        {
            string url = string.Format("/app_store/{0}/install", shareId);
            dynamic requestData = new
            {
                space_id = spaceId,
                dependencies = dependentShareIds
            };
            return await _podio.Post<AppMarketShareInstall>(url, requestData);
        }

        /// <summary>
        ///     Shares the app or pack in the app market.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/share-app-22504 </para>
        /// <param name="refType">The type of object to share, either "app" or "space"</param>
        /// <param name="refId">The id of the object to be shared</param>
        /// <param name="name">The name of the share (only valid for sharing of space)</param>
        /// <param name="abstracts">The abstract of the app</param>
        /// <param name="description">The description of the app</param>
        /// <param name="language">The language the app is written in</param>
        /// <param name="categoryId">The ids of the categories the app should be placed in</param>
        /// <param name="fileId">The file ids to use as screenshots for the app</param>
        /// <param name="videoId">The youtube id of a introduction video, if any</param>
        /// <param name="features">The list of features to enable</param>
        /// <param name="childrenId">The ids of the child shares that this share should include</param>
        /// <param name="scope">The scope the app should be shared with, either "public" or "private", defaults to "public"</param>
        /// <returns>The id of the newly created share</returns>
        public async Task<long> ShareApp(string refType, long refId, string name, string abstracts, string description,
            string language, long[] categoryId, long[] fileId, long videoId, List<string> features, long childrenId,
            string scope = "public")
        {
            string url = "/app_store/";
            dynamic requestData = new
            {
                ref_type = refType,
                ref_id = refId,
                name = name,
                abstracts = abstracts,
                description = description,
                language = language,
                category_ids = categoryId,
                file_ids = fileId,
                video = videoId,
                features = features,
                children = childrenId,
                scope = scope
            };
            dynamic response = await _podio.Post<dynamic>(url, requestData);
            return (long) response["share_id"];
        }

        /// <summary>
        ///     Unshares the given app from the app market
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/unshare-app-37917  </para>
        /// <param name="shareId"></param>
        public async Task<dynamic> UnshareApp(long shareId)
        {
            string url = string.Format("/app_store/{0}", shareId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Updates the share with changes to abstract, description, etc.
        /// </summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/app-market/update-share-38639 </para>
        /// <param name="shareId"></param>
        /// <param name="name">The name of the share, only valid if the share is a space</param>
        /// <param name="abstracts">The abstract of the share</param>
        /// <param name="description">The description of the share</param>
        /// <param name="language">The language the share is written in</param>
        /// <param name="categoryId">The ids of the categories the share should be placed in</param>
        /// <param name="fileId">The file ids to use as screenshots for the share</param>
        /// <param name="videoId">The youtube id of a introduction video, if any</param>
        public async Task<dynamic> UpdateShare(long shareId, string name, string abstracts, string description, string language,
            long[] categoryId, long[] fileId, long? videoId = null)
        {
            string url = string.Format("/app_store/{0}", shareId);
            dynamic requestData = new
            {
                name = name,
                abstracts = abstracts,
                description = description,
                language = language,
                category_ids = categoryId,
                file_ids = fileId,
                video = videoId
            };
             return await _podio.Put<dynamic>(url, requestData);
        }
    }
}