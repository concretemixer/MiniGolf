using UnityEngine;
using System;
using System.Collections.Generic;

namespace MiniGolf.MVCS.Services
{
    public class UIManager : IUIManager
    {
        [Inject(EntryPoint.Container.UI)]
        public GameObject uiRoot { get; set; }

        [Inject]
        public ILocaleService localeService { get; set; }

        private Dictionary<UIMap.Id, GameObject> uiElements = new Dictionary<UIMap.Id, GameObject>();

              public TType GetResource<TType>(string resourcePath) where TType : UnityEngine.Object
        {
            var resource = Resources.Load(resourcePath);
            if (resource == null)
                Resources.Load(resourcePath + ".prefab");
            return (TType)resource;
        }

        public TViewClass Get<TViewClass>(UIMap.Id _viewId) 
        {
            if (uiElements.ContainsKey(_viewId))
                return uiElements[_viewId].GetComponent<TViewClass>();
            return default(TViewClass);
        }

        public TViewClass Show<TViewClass>(UIMap.Id _viewId)
        {
            var instance = Show(_viewId);
            return instance.GetComponent<TViewClass>();
        }

        public GameObject Show(UIMap.Id _viewId)
        {
            Debug.Log("attempt to instantiate: " + _viewId.ToString());
                        
            if (uiElements.ContainsKey(_viewId))
            {
                Debug.Log("already exist: " + _viewId.ToString());
                return uiElements[_viewId];
            }

            var resourcePath = UIMap.GetPath(_viewId);
            var view = GetResource<GameObject>(resourcePath);

            var instance = GameObject.Instantiate(view as GameObject) as GameObject;
            instance.transform.SetParent(uiRoot.transform);
    	    instance.transform.localPosition = Vector3.zero;

            localeService.SetAllTexts(instance.gameObject);

            uiElements[_viewId] = instance;

            return instance;
        }

        public void Hide(UIMap.Id _viewId)
        {
            if (uiElements.ContainsKey(_viewId))
            {
                GameObject.Destroy(uiElements[_viewId]);
                uiElements.Remove(_viewId);
            }     
        }

        public void HideAll()
        {
            foreach(var id in uiElements.Keys)
                GameObject.Destroy(uiElements[id]);

            uiElements.Clear();
        }
    }
}