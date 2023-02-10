using System;
using BuildChecker;
using General.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace RagdollCrush
{
    public class BuildPropertiesChecker : IPreprocessBuildWithReport
    {
        #region Fields

        public const string SettingsPathInResources = "BuildPropertiesCheckerSettings";

        #endregion
        
        #region Methods

        [MenuItem("Tools/Check project properties")]
        public static void CheckProject() => CheckProject(new DataForServicesChecking());

        public static bool CheckProject(DataForServicesChecking dataForServicesChecking)
        {
            dataForServicesChecking ??= new DataForServicesChecking();
            try
            {
                BuildPropertiesCheckerSettings config = LoadConfig();
                if (!DefaultBuildPropertiesChecker.Check(config) || 
                    !ServicesChecker.Check(dataForServicesChecking))
                    return false;
            }
            catch (Exception exc)
            {
                Debug.LogError(exc);
                return false;
            }
            return true;
        }

        private static BuildPropertiesCheckerSettings LoadConfig()
        {
            BuildPropertiesCheckerSettings config = Resources.Load<BuildPropertiesCheckerSettings>(SettingsPathInResources);
            if (config.IsNull())
            {
                config = ScriptableObject.CreateInstance<BuildPropertiesCheckerSettings>();
                AssetDatabase.CreateAsset(config, $"Assets/Resources/{SettingsPathInResources}.asset");
            }

            return config;
        }

        #endregion
        
        #region IPreprocessBuildWithReport

        public int callbackOrder => 0;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            bool checkingSuccess = CheckProject(new DataForServicesChecking());
            if (!checkingSuccess) 
                throw new BuildFailedException(BuildPropertiesCheckerMessages.ProjectSettingsIncorrect);
        }

        #endregion
    }
}