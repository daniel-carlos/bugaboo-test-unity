using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class BackendAPI : MonoBehaviour
{
    // public static string host = "https://mmorpg.mocklab.io/";
    public static string host = "localhost:3003/";

    static string URI(string route)
    {
        return $"{host}{route}";
    }

    static void SetHeaders(UnityWebRequest request, string jsonData = null)
    {
        request.SetRequestHeader("content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        if (jsonData != null)
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
    }

    public static void LogIfError(UnityWebRequest webRequest)
    {
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError(": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogWarning("HTTP Error: " + webRequest.error);
                break;
        }
    }

    public static IEnumerator Get(string uri, UnityAction<UnityWebRequest, JObject> callback = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URI(uri)))
        {
            yield return webRequest.SendWebRequest();
            string jsoNStr = webRequest.downloadHandler.text;
            LogIfError(webRequest);
            if (callback != null) callback(webRequest, (JObject)JToken.Parse(jsoNStr));
        }
    }

    public static IEnumerator Post(string uri, string data, UnityAction<UnityWebRequest, JObject> callback = null)
    {
        string fullURI = URI(uri);
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(fullURI, data))
        {
            SetHeaders(webRequest, data);
            yield return webRequest.SendWebRequest();
            string jsoNStr = webRequest.downloadHandler.text;
            LogIfError(webRequest);
            if (callback != null) callback(webRequest, (JObject)JToken.Parse(jsoNStr));
        }
    }

    public static IEnumerator Put(string uri, string data, UnityAction<UnityWebRequest, JObject> callback = null)
    {
        string fullURI = URI(uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Put(fullURI, data))
        {
            SetHeaders(webRequest, data);
            yield return webRequest.SendWebRequest();
            string jsoNStr = webRequest.downloadHandler.text;
            LogIfError(webRequest);
            if (callback != null) callback(webRequest, (JObject)JToken.Parse(jsoNStr));
        }
    }

    public static IEnumerator Delete(string uri, UnityAction<UnityWebRequest, JObject> callback = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Delete(URI(uri)))
        {
            yield return webRequest.SendWebRequest();
            string jsoNStr = webRequest.downloadHandler.text;
            LogIfError(webRequest);
            if (callback != null) callback(webRequest, (JObject)JToken.Parse(jsoNStr));
        }
    }
}
