using General.Extensions;
using UnityEngine;

#if ADJUST
using com.adjust.sdk;
#endif

namespace BuildChecker
{
    internal class AdjustChecker
    {
        #region Methods

        internal static bool Check(DataForServicesChecking data)
        {
#if ADJUST
            
            Adjust adjust = default;
            for (int i = 0; i < data.objectsForCheck.Length && adjust.IsNull(); i++)
                adjust = 
                    data.objectsForCheck[i].GetComponent<Adjust>() ?? 
                    data.objectsForCheck[i].GetComponentInChildren<Adjust>();
            if (adjust.NotNull())
            {
                ServicesCheckerMessages.LogAdjustFound();
                return
                    CheckEnvironment(data, adjust) &
                    CheckKey(data, adjust);
            }
            
            ServicesCheckerMessages.LogAdjustNotFound();
            return false;
#endif
            return true;
        }

#if ADJUST
        private static bool CheckEnvironment(DataForServicesChecking data, Adjust adjust)
        {
            switch (data.isRelease)
            {
                case true when adjust.environment == AdjustEnvironment.Sandbox:
                    ServicesCheckerMessages.RequireAdjustEnvironment("Release", "Production");
                    return false;
                case false when adjust.environment == AdjustEnvironment.Production:
                    ServicesCheckerMessages.RequireAdjustEnvironment("Debug", "Sandbox");
                    return false;
                default:
                    return true;
            }
        }
#endif

#if ADJUST
        private static bool CheckKey(DataForServicesChecking data, Adjust adjust)
        {
            if (string.IsNullOrEmpty(adjust.appToken))
            {
                ServicesCheckerMessages.AdjustTokenInvalid();
                return false;
            }

            ServicesCheckerMessages.AdjustTokenValid();
            return true;
        }
#endif

        #endregion
    }
}