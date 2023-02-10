using System;
using System.Reflection;
using UnityEditor;

namespace RagdollCrush
{
    public static class CustomAndroidPlatformIconKind
    {
        public static PlatformIconKind GetLegacy() => 
            CreatePlatformIconKind(0, "Legacy", "An legacy launcher icon represents your app on the device's Home screen and in the launcher window on devices running Android 7.1 (API level 25) and earlier.", BuildTargetGroup.Android);

        public static PlatformIconKind GetRound() => 
            CreatePlatformIconKind(1, "Round (API 25)", "An round launcher icon represents your app on a device's Home screen and in the launcher (if the launcher supports round icons) window on devices running Android 7.1 (API level 25).", BuildTargetGroup.Android);

        public static PlatformIconKind GetAdaptive() =>
            CreatePlatformIconKind(2, "Adaptive (API 26)", "An adaptive launcher icon represents your app on a device's Home screen and in the launcher window on devices running Android 8.0 (API level 26) and later.", BuildTargetGroup.Android, new string[2]
            {
                "Background",
                "Foreground"
            });

        private static PlatformIconKind CreatePlatformIconKind(int kind, 
            string kindString, 
            string description, 
            BuildTargetGroup platform, 
            string[] customLayerLabels = null)
        {
            object[] args = new object[] { kind, kindString, description, platform, customLayerLabels };
            Type[] argsTypes = new Type[] { typeof(int), typeof(string), typeof(string), typeof(BuildTargetGroup), typeof(string[]) };
            
            ConstructorInfo constructor = typeof(PlatformIconKind).GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance, null, argsTypes, default);
            return (PlatformIconKind)constructor?.Invoke(args);
        }
    }
}