using UnityEngine;

namespace MiniGolf.MVCS.Services
{

    public interface ILocaleService 
    {
        void SetAllTexts(GameObject root);
        string ProcessString(string template);

        bool CanChange();
        SystemLanguage GetCurrentLanguage();
        void SetCurrentLanguage(SystemLanguage lang);
        
    }
}