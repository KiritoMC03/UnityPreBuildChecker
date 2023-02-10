using System.Collections.Generic;
using System.Linq;
using General.Extensions;
using UnityEditor;
using UnityEngine;

namespace RagdollCrush
{
    public class DefaultBuildPropertiesChecker
    {
        #region Fields

        private const AndroidArchitecture TargetAndroidArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;

        #endregion
        
        #region Methods

        public static bool Check(BuildPropertiesCheckerSettings config)
        {
            if (CheckEditor(config) &&
                CheckPlayerSettings(config) &&
                BuildCheckingUtils.GetIl2CppCompilerConfiguration() == Il2CppCompilerConfiguration.Release 
                    ? CheckRelease() 
                    : CheckDebug())
                return true;
            
            return false;
        }

        private static bool CheckEditor(BuildPropertiesCheckerSettings config)
        {
            bool isSuccess = true;
            if (Application.unityVersion != config.unityVersion)
            {
                BuildPropertiesCheckerMessages.LogUnityVersionIncorrect(config.unityVersion);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogUnityVersionCorrect();

            if (EditorUserBuildSettings.activeBuildTarget != config.targetPlatform)
            {
                BuildPropertiesCheckerMessages.LogPlatformIncorrect(config.targetPlatform);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogPlatformCorrect(config.targetPlatform);

            return isSuccess;
        }

        private static bool CheckPlayerSettings(BuildPropertiesCheckerSettings config)
        {
            bool isSuccess = true;
            BuildTargetGroup buildTargetGroup = BuildCheckingUtils.ConvertBuildTargetToGroup(config.targetPlatform);
            
            if (!CheckIcons(config, buildTargetGroup)) isSuccess = false;

            if (PlayerSettings.defaultInterfaceOrientation != config.screenOrientation)
            {
                BuildPropertiesCheckerMessages.LogOrientationIncorrent(config.screenOrientation);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogOrientationCorrent(config.screenOrientation);

            string packageID = PlayerSettings.GetApplicationIdentifier(buildTargetGroup);
            if (!packageID.Contains(config.requiredPackageNamePart))
            {
                BuildPropertiesCheckerMessages.LogPackageIdIncorrect(config.requiredPackageNamePart);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogPackageIdCorrect(packageID);

            if (PlayerSettings.Android.useCustomKeystore == false)
            {
                BuildPropertiesCheckerMessages.LogKeystoreNotUsed();
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogKeystoreUsed();

            if (PlayerSettings.SplashScreen.showUnityLogo && config.dontShowUnityLogo)
            {
                BuildPropertiesCheckerMessages.LogUnityLogoIsActive();
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogUnityLogoIsInactive();

            if (!CheckPlatform(config, buildTargetGroup)) isSuccess = false;

            return isSuccess;
        }

        private static bool CheckIcons(BuildPropertiesCheckerSettings config, BuildTargetGroup buildTargetGroup)
        {
            bool isSuccess = true;
            List<PlatformIconKind> iconKinds = BuildCheckingUtils.GetIconKinds(config.targetPlatform);
            foreach (PlatformIconKind currentKind in iconKinds)
            {
                PlatformIcon[] icons = PlayerSettings.GetPlatformIcons(buildTargetGroup, currentKind);
                int texturesNumber = icons.Count(icon => icon.NotNull() && icon.GetTexture().NotNull());
                if (texturesNumber < 1)
                {
                    BuildPropertiesCheckerMessages.IconOfKindNotFound(currentKind);
                    isSuccess = false;
                }
                else BuildPropertiesCheckerMessages.IconOfKindCorrect(currentKind);
            }
            
            return isSuccess;
        }

        private static bool CheckPlatform(BuildPropertiesCheckerSettings config, BuildTargetGroup buildTargetGroup)
        {
            bool isSuccess = true;

            if (PlayerSettings.bundleVersion.Split('.').Length != config.digitsInVersion)
            {
                BuildPropertiesCheckerMessages.LogBundleVersionIncorrect(config.targetPlatform, config.digitsInVersion);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogBundleVersionCorrect(PlayerSettings.bundleVersion);

            if (PlayerSettings.GetScriptingBackend(buildTargetGroup) != config.scriptingBackend)
            {
                BuildPropertiesCheckerMessages.LogScriptingBackendIncorrect(config.targetPlatform, config.scriptingBackend);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogScriptingBackendCorrect(config.scriptingBackend);

            if (PlayerSettings.GetApiCompatibilityLevel(buildTargetGroup) != config.apiCompatibility)
            {
                BuildPropertiesCheckerMessages.LogApiCompatibilityIncorrect(config.targetPlatform, config.apiCompatibility);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogApiCompatibilityCorrect(config.apiCompatibility);
            
#if UNITY_ANDROID
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android &&
                PlayerSettings.Android.targetArchitectures != TargetAndroidArchitectures)
            {
                BuildPropertiesCheckerMessages.LogInvalidBuildArchitecture(TargetAndroidArchitectures);
                isSuccess = false;
            }
            else BuildPropertiesCheckerMessages.LogValidBuildArchitecture(TargetAndroidArchitectures);
#endif

            return isSuccess;
        }

        private static bool CheckRelease()
        {
            return true;
        }

        private static bool CheckDebug()
        {
            return true;
        }

        #endregion
    }
}