using System.Reflection;
using General.Extensions;
using UnityEngine;

namespace BuildChecker
{
    public class AppMetricaChecker
    {
        #region Fields
        
        private const BindingFlags FieldsFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private const string APIKeyFieldName = "ApiKey";

        #endregion
        
        #region Methods

        public static bool Check(DataForServicesChecking data)
        {
#if APP_METRICA
             AppMetrica appMetrica = default;
             for (int i = 0; i < data.objectsForCheck.Length && appMetrica.IsNull(); i++)
                 appMetrica = 
                     data.objectsForCheck[i].GetComponent<AppMetrica>() ?? 
                     data.objectsForCheck[i].GetComponentInChildren<AppMetrica>();
             if (appMetrica.NotNull())
             {
                 ServicesCheckerMessages.LogAppMetricaFound();
                 return CheckAppMetricaKey(data, appMetrica);
             }

             ServicesCheckerMessages.LogAppMetricaNotFound();
            return false;
#endif
            return true;
        }

        private static bool CheckAppMetricaKey(DataForServicesChecking data, AppMetrica appMetrica)
        {
#if APP_METRICA
            FieldInfo fieldInfo = appMetrica.GetType().GetField(APIKeyFieldName, FieldsFlags);
            if (fieldInfo == null)
            { 
                ServicesCheckerMessages.AppMetricaApiKeyFieldNotFound();
                return false;
            }
            else ServicesCheckerMessages.AppMetricaApiKeyFieldFound();

            string apiKey = (string) fieldInfo.GetValue(appMetrica);
            if (string.IsNullOrEmpty(apiKey))
            {
                Debug.LogError($"AppMetrica api key is invalid!");
                return false;
            }
#endif
            return true;
        }

        #endregion
    }
}