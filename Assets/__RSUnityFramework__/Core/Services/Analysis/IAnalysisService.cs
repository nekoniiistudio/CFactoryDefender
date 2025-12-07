using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RSFramework.Service
{
    public interface IAnalysisService
    {
        void StartSession(string sessionName);
        void EndSession();
        void AddSample(string metricName, float value);
        float GetAverage(string metricName);
        void PrintReport();
        void Clear();
    }
    /*
    public interface IAuthenticationService
    {
        UniTask<UserDto> Login(string sessionName);
        void Logout();
    }


    public class Try
    {
        IAuthenticationService  _authService;
        async void  Start()
        {
            var dat = await _authService.Login("");

        }
    }
    public class AuthenticationService: IAuthenticationService
    {

        public UniTask<UserDto> Login(string sessionName)
        {
            string data = await PostJsonAsync("GET url", sessionName).;

        }

        public void Logout(string sessionName)
        {

        }
        public static async UniTask<string> PostJsonAsync(string url, string json)
        {
            var body = Encoding.UTF8.GetBytes(json);
            using var req = new UnityWebRequest(url, "POST");

            req.uploadHandler = new UploadHandlerRaw(body);
            req.downloadHandler = new DownloadHandlerBuffer();

            req.SetRequestHeader("Content-Type", "application/json");

            await req.SendWebRequest().ToUniTask();

#if UNITY_2020_1_OR_NEWER
            if (req.result != UnityWebRequest.Result.Success)
#else
        if (req.isNetworkError || req.isHttpError)
#endif
            {
                Debug.LogError(req.error);
                return null;
            }

            return req.downloadHandler.text;
        }
    }

    public class UserDto
    {
        string userID;
        string userName;


    }
    */
}

