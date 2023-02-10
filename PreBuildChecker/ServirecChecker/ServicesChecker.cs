using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildChecker
{
    public class ServicesChecker
    {
        #region Methods
        
        public static bool Check(DataForServicesChecking data)
        {
            data.objectsForCheck = GetObjectsForCheck();
#if Il2CPP_RELEASE
            data.isRelease = true;
#else
            data.isRelease = false;
#endif
            
            return
                AdjustChecker.Check(data) &
                AppMetricaChecker.Check(data);
        }

        private static GameObject[] GetObjectsForCheck()
        {
            Scene mainScene = SceneManager.GetSceneAt(0);
            GameObject[] rootGameObjects = mainScene.GetRootGameObjects();
            return rootGameObjects;
        }

        #endregion
    }

    public static class ServicesCheckerMessages
    {
        public const string CorrectColor = "<color=#53DB39>";
        public const string IncorrectColor = "<color=#DB3D39>";
        public const string ColorClose = "</color>";

        #region Methods
        
        public static void LogCorrect(string message) => 
            Debug.Log($"{CorrectColor}{message}{ColorClose}");
        
        public static void LogIncorrect(string message) => 
            Debug.LogWarning($"{IncorrectColor}{message}{ColorClose}");

        public static void LogAdjustNotFound() => LogCorrect("Adjust component on main scene not found!");
        public static void LogAdjustFound() => LogCorrect("Adjust component on main scene is found!");

        public static void RequireAdjustEnvironment(string buildMode, string environment) => 
            LogIncorrect($"For {buildMode} build need to set {environment} environment for adjust!");

        public static void AdjustTokenInvalid() => LogIncorrect("Adjust app token on main scene is invalid.");
        public static void AdjustTokenValid() => LogCorrect("Adjust app token on main scene is valid.");
        public static void LogAppMetricaNotFound() => LogIncorrect("AppMetrica component on main scene not found!");
        public static void LogAppMetricaFound() => LogCorrect("AppMetrica component on main scene found!");
        
        public static void AppMetricaApiKeyFieldNotFound() => 
            LogIncorrect($"AppMetrica component struct is incorrect! Field ApiKey field not found.");

        public static void AppMetricaApiKeyFieldFound() => 
            LogCorrect($"AppMetrica component struct is correct! Field ApiKey field found.");

        #endregion
    }
    
    public class DataForServicesChecking
    {
        public GameObject[] objectsForCheck;
        public bool isRelease;
    }
}