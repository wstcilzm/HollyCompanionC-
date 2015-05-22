using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HollyCompanion.Data;

namespace HollyCompanion
{
    public interface IHolyRequest
    {
        /// <summary>
        /// Get introduction of Cortana 
        /// </summary>
        /// <returns></returns>
        string GetCortanaIntroduce();

        /// <summary>
        /// Set config app services
        /// </summary>
        /// <returns></returns>
        string ConfigAppsServices();

        /// <summary>
        /// Get Holy language preferences
        /// </summary>
        /// <returns></returns>
        ItemCollection GetLanguagePreferences();
        
        
    }
}
