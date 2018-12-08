using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public static class Rest {

    public static IEnumerator Get<T>(string api, string queryParams, Action<T> resolve, Action<RestError> reject) {
        UnityWebRequest request = UnityWebRequest.Get("localhost:8000" + api + "?" + queryParams);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError){
            reject(new RestError((int) request.responseCode, request.error));
        } else {
            resolve(JsonUtility.FromJson<T>(request.downloadHandler.text));
        }
    }

    public static void Post() {

    }

    public static void Put() {

    }

    public static void Delete() {

    }
}
