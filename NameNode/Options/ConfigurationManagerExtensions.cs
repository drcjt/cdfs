using System;
using System.Collections.Specialized;
using System.Linq;

namespace NameNode.Options
{
    public static class ConfigurationManagerExtensions
    {
        /// <summary>
        /// Gets a specific config key's value from a name value collection. If the config key doesn't exist
        /// then the defaultValue is returned.
        /// </summary>
        /// <typeparam name="T">type of the config key's value</typeparam>
        /// <param name="nameValuePairs">the collection to get the value from</param>
        /// <param name="configKey">config key to get value for</param>
        /// <param name="defaultValue">default value to use if config key doesn't exist</param>
        /// <returns></returns>
        public static T GetValue<T>(this NameValueCollection nameValuePairs, string configKey, T defaultValue)
        {
            T returnValue;

            if (nameValuePairs.AllKeys.Contains(configKey))
            {
                string tmpValue = nameValuePairs[configKey];
                returnValue = (T)Convert.ChangeType(tmpValue, typeof(T));
            }
            else
            {
                returnValue = defaultValue;
            }

            return returnValue;
        }
    }
}
