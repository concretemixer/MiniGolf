using UnityEngine;

namespace MiniGolf.MVCS.Services
{
    public interface IUIManager
    {
        TViewClass Show<TViewClass>(UIMap.Id _viewId);
        TViewClass Get<TViewClass>(UIMap.Id _viewId);
        GameObject Show(UIMap.Id _viewId);        
        void Hide(UIMap.Id _viewId);
        void HideAll();
    }
}