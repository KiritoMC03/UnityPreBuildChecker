using UnityEditor;
using UnityEngine;

namespace RagdollCrush
{
    [CreateAssetMenu(fileName = "BuildPropertiesCheckerSettings", menuName = "Build/New Build Properties Checker Settings", order = 0)]
    public class BuildPropertiesCheckerSettings : ScriptableObject
    {
        public BuildPropertiesCheckerMode buildMode = BuildPropertiesCheckerMode.Default;
        public string unityVersion = "2020.3.38f1";
        public BuildTarget targetPlatform = BuildTarget.Android;
        public UIOrientation screenOrientation = UIOrientation.Portrait;
        public string requiredPackageNamePart = "com.balaganovrocks.";
        public bool dontShowUnityLogo = true;

#if UNITY_ANDROID
        public int digitsInVersion = 4;
        public ScriptingImplementation scriptingBackend = ScriptingImplementation.IL2CPP;
        public ApiCompatibilityLevel apiCompatibility = ApiCompatibilityLevel.NET_4_6;
#endif
        
#if UNITY_IOS
        public ScriptingImplementation scriptingBackend = ScriptingImplementation.IL2CPP;
#endif
    }
}