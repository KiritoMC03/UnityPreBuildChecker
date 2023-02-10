using UnityEditor;
using UnityEngine;

namespace RagdollCrush
{
    public static class BuildPropertiesCheckerMessages
    {
        public const string ProjectSettingsIncorrect = "The project has incorrect settings, see the console.";
        public const string CorrectColor = "<color=#53DB39>";
        public const string IncorrectColor = "<color=#DB3D39>";
        public const string ColorClose = "</color>";

        public static void LogCorrect(string message) => 
            Debug.Log($"{CorrectColor}{message}{ColorClose}");
        
        public static void LogIncorrect(string message) => 
            Debug.LogWarning($"{IncorrectColor}{message}{ColorClose}");

        public static void LogUnityVersionIncorrect(string targetVersion) => 
            LogIncorrect($"Unity editor version is incorrect! Target editor version is {targetVersion}");
        
        public static void LogUnityVersionCorrect() => 
            LogCorrect($"Unity editor version is correct!");

        public static void LogPlatformIncorrect(BuildTarget targetPlatform) => 
            LogIncorrect($"Current build platform is incorrect! Target platform is {targetPlatform}");

        public static void LogPlatformCorrect(BuildTarget targetPlatform) => 
            LogCorrect($"Current build platform ({targetPlatform}) is correct!");

        public static void IconOfKindNotFound(PlatformIconKind currentKind) => 
            LogIncorrect($"Icon of platform kind {currentKind} not found. Please, set the icon.");

        public static void IconOfKindCorrect(PlatformIconKind currentKind) => 
            LogCorrect($"Icon of platform kind {currentKind} is correct.");

        public static void LogOrientationIncorrent(UIOrientation targetScreenOrientation) => 
            LogIncorrect($"Screen orientation is incorrect. Require {targetScreenOrientation} screen orientation.");

        public static void LogOrientationCorrent(UIOrientation targetScreenOrientation) => 
            LogCorrect($"Screen orientation ({targetScreenOrientation}) is correct.");

        public static void LogPackageIdIncorrect(string requiredPackageNamePart) => 
            LogIncorrect($"Package identifier format is incorrect. Package identifier must contains \"{requiredPackageNamePart}\" part.");

        public static void LogPackageIdCorrect(string packageID) => 
            LogCorrect($"Package identifier format ({packageID}) is correct.");

        public static void LogBundleVersionIncorrect(BuildTarget targetPlatform, int digitsInVersion) => 
            LogIncorrect($"Bundle version format incorrect. {targetPlatform} require {digitsInVersion} digits in bundle version");

        public static void LogBundleVersionCorrect(string bundleVersion) => 
            LogCorrect($"Bundle version ({bundleVersion}) format is correct.");

        public static void LogScriptingBackendIncorrect(BuildTarget targetPlatform, ScriptingImplementation scriptingBackend) => 
            LogIncorrect($"Scripting backend incorrect. {targetPlatform} require {scriptingBackend} scripting backend.");

        public static void LogScriptingBackendCorrect(ScriptingImplementation scriptingBackend) => 
            LogCorrect($"Scripting backend ({scriptingBackend}) is correct.");

        public static void LogApiCompatibilityIncorrect(BuildTarget targetPlatform, ApiCompatibilityLevel apiCompatibility) => 
            LogIncorrect($"Api compatibility incorrect. {targetPlatform} require {apiCompatibility} api compatibility level.");

        public static void LogApiCompatibilityCorrect(ApiCompatibilityLevel apiCompatibility) => 
            LogCorrect($"Api compatibility ({apiCompatibility}) is correct.");

        public static void LogInvalidBuildArchitecture(AndroidArchitecture targetArchitectures) => 
            LogIncorrect($"Target android architectures is incorrect. Require {targetArchitectures} architectures.");

        public static void LogValidBuildArchitecture(AndroidArchitecture targetArchitectures) => 
            LogCorrect($"Target android architectures ({targetArchitectures} is correct.");

        public static void LogKeystoreNotUsed() => 
            LogIncorrect($"Custom keystore not used. Please, enable custom keystore in Player Settings.");

        public static void LogKeystoreUsed() => 
            LogCorrect($"Custom keystore is used.");

        public static void LogUnityLogoIsActive() => 
            LogIncorrect($"Unity logo is enabled. Please, disable unity logo showing.");

        public static void LogUnityLogoIsInactive() => 
            LogCorrect($"Unity logo is disabled.");
    }
}