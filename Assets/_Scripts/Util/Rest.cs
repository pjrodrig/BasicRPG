using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public static class Rest {

    private static string baseUrl = "localhost:8000";

    ///<summary>
    /// GET request
    ///</summary>
    ///<param name="api">api endpoint path ex. "/user"</param>
    ///<param name="queryParams">query params to append to url. Must be URL encoded!! ex. "username=sp3c%21alCh%40racter%24" //sp3c!alCh@racter$</param>
    ///<param name="resolve">callback for successful request"</param>
    ///<param name="reject">callback for unsuccessful request</param>
    /// <example>
    /// <code>
    /// StartCoroutine(Rest.Get(API.user, "username=" + UnityWebRequest.EscapeURL("sp3c%21alCh%40racter%24"), new Action<User>(delegate (User user) {
    ///     Debug.Log(user);
    /// }), new Action<RestError>(delegate (RestError err) {
    ///     Debug.Log(err.message);
    /// })));
    /// </code>
    /// </example>
    public static IEnumerator Get<T>(string api, string queryParams, Action<T> resolve, Action<RestError> reject) {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + api + (queryParams == null ? "" : "?" + queryParams));
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError){
            reject(new RestError((int) request.responseCode, request.downloadHandler.text));
        } else {
            resolve(JsonUtility.FromJson<T>(request.downloadHandler.text));
        }
    }

    ///<summary>
    /// POST request
    ///</summary>
    ///<param name="api">api endpoint path ex. "/user"</param>
    ///<param name="queryParams">query params to append to url. Must be URL encoded!! ex. "username=sp3c%21alCh%40racter%24" //sp3c!alCh@racter$</param>
    ///<param name="body">body to send in request. Must be a serializable object!!</param>
    ///<param name="resolve">callback for successful request"</param>
    ///<param name="reject">callback for unsuccessful request</param>
    /// <example>
    /// <code>
    /// StartCoroutine(Rest.Post(API.user, "", new User("username=sp3c%21alCh%40racter%24"), new Action<User>(delegate (User user) {
    ///     Debug.Log(user);
    /// }), new Action<RestError>(delegate (RestError err) {
    ///     Debug.Log(err.message);
    /// })));
    /// </code>
    /// </example>
    public static IEnumerator Post<T>(string api, string queryParams, System.Object body, Action<T> resolve, Action<RestError> reject) {
        UnityWebRequest request = new UnityWebRequest(baseUrl + api + (queryParams == null ? "" : "?" + queryParams));
        request.SetRequestHeader("Content-Type", "application/json");
        request.method = "POST";
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(body)));
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError){
            reject(new RestError((int) request.responseCode, request.downloadHandler.text));
        } else {
            resolve(JsonUtility.FromJson<T>(request.downloadHandler.text));
        }
    }

    ///<summary>
    /// PUT request
    ///</summary>
    ///<param name="api">api endpoint path ex. "/user"</param>
    ///<param name="queryParams">query params to append to url. Must be URL encoded!! ex. "username=sp3c%21alCh%40racter%24" //sp3c!alCh@racter$</param>
    ///<param name="body">body to send in request. Must be a serializable object!!</param>
    ///<param name="resolve">callback for successful request"</param>
    ///<param name="reject">callback for unsuccessful request</param>
    /// <example>
    /// <code>
    /// StartCoroutine(Rest.Put(API.user, "", userToUpdate, new Action<User>(delegate (User updatedUser) {
    ///     Debug.Log(updatedUser);
    /// }), new Action<RestError>(delegate (RestError err) {
    ///     Debug.Log(err.message);
    /// })));
    /// </code>
    /// </example>
    public static IEnumerator Put<T>(string api, string queryParams, System.Object body, Action<T> resolve, Action<RestError> reject) {
        UnityWebRequest request = new UnityWebRequest(baseUrl + api + (queryParams == null ? "" : "?" + queryParams));
        request.SetRequestHeader("Content-Type", "application/json");
        request.method = "PUT";
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(body)));
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError){
            reject(new RestError((int) request.responseCode, request.downloadHandler.text));
        } else {
            resolve(JsonUtility.FromJson<T>(request.downloadHandler.text));
        }
    }

    ///<summary>
    /// DELETE request
    ///</summary>
    ///<param name="api">api endpoint path ex. "/user"</param>
    ///<param name="queryParams">query params to append to url. Must be URL encoded!! ex. "username=sp3c%21alCh%40racter%24" //sp3c!alCh@racter$</param>
    ///<param name="resolve">callback for successful request"</param>
    ///<param name="reject">callback for unsuccessful request</param>
    /// <example>
    /// <code>
    /// StartCoroutine(Rest.Get(API.user, "userId=1", new Action<User>(delegate (User user) {
    ///     Debug.Log("User was deleted");
    /// }), new Action<RestError>(delegate (RestError err) {
    ///     Debug.Log(err.message);
    /// })));
    /// </code>
    /// </example>
    public static IEnumerator Delete(string api, string queryParams, Action<bool> resolve, Action<RestError> reject) {
        UnityWebRequest request = UnityWebRequest.Delete(baseUrl + api + (queryParams == null ? "" : "?" + queryParams));
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError){
            reject(new RestError((int) request.responseCode, request.downloadHandler.text));
        } else {
            resolve(true);
        }
    }
}
