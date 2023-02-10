using System.Collections.Generic;
using UnityEditor;

namespace RagdollCrush
{
    public static class BuildCheckingUtils
    {
        #region Methods

        public static List<PlatformIconKind> GetIconKinds(BuildTarget buildTarget)
        {
            List<PlatformIconKind> result = new List<PlatformIconKind>(5);
            switch (buildTarget)
            {
#if UNITY_ANDROID
                case BuildTarget.Android :
                    result.Add(CustomAndroidPlatformIconKind.GetAdaptive());
                    result.Add(CustomAndroidPlatformIconKind.GetLegacy());
                    result.Add(CustomAndroidPlatformIconKind.GetRound());
                    return result;
#elif UNITY_IOS
                case BuildTarget.iOS : 
                    result.Add(iOSPlatformIconKind.Application);
                    result.Add(iOSPlatformIconKind.Marketing);
                    result.Add(iOSPlatformIconKind.Notification);
                    result.Add(iOSPlatformIconKind.Settings);
                    result.Add(iOSPlatformIconKind.Spotlight);
                    return result;
                case BuildTarget.tvOS : 
                    result.Add(tvOSPlatformIconKind.App);
                    result.Add(tvOSPlatformIconKind.TopShelfImage);
                    return result;
#endif
                default:
                    return result;
            }
        }

        public static ScriptingImplementation GetScriptingBackend() => 
            PlayerSettings.GetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup);
        
        public static Il2CppCompilerConfiguration GetIl2CppCompilerConfiguration() => 
            PlayerSettings.GetIl2CppCompilerConfiguration(EditorUserBuildSettings.selectedBuildTargetGroup);

        public static BuildTargetGroup ConvertBuildTargetToGroup(BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                case BuildTarget.iOS:
                    return BuildTargetGroup.iOS;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneLinux:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneLinuxUniversal:
                    return BuildTargetGroup.Standalone;
                case BuildTarget.Android:
                    return BuildTargetGroup.Android;
                case BuildTarget.WebGL:
                    return BuildTargetGroup.WebGL;
                case BuildTarget.WSAPlayer:
                    return BuildTargetGroup.WSA;
                case BuildTarget.Tizen:
                    return BuildTargetGroup.Tizen;
                case BuildTarget.PSP2:
                    return BuildTargetGroup.PSP2;
                case BuildTarget.PS4:
                    return BuildTargetGroup.PS4;
                case BuildTarget.PSM:
                    return BuildTargetGroup.PSM;
                case BuildTarget.XboxOne:
                    return BuildTargetGroup.XboxOne;
                case BuildTarget.N3DS:
                    return BuildTargetGroup.N3DS;
                case BuildTarget.WiiU:
                    return BuildTargetGroup.WiiU;
                case BuildTarget.tvOS:
                    return BuildTargetGroup.tvOS;
                case BuildTarget.Switch:
                    return BuildTargetGroup.Switch;
                case BuildTarget.NoTarget:
                default:
                    return BuildTargetGroup.Standalone;
            }
        }
        
        #endregion
    }
}