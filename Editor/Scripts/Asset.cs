using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace I {
    
    public class Asset<T> where T : class {

        public T selected;

        public Asset(
            string name, 
            VisualElement template, 
            EventCallback<Asset<T>> changeEvent) {
            
            var element = template.Q<ObjectField>(name);
            element.RegisterCallback<ChangeEvent<Object>>(e => {
                selected = e.newValue as T;
                changeEvent(this);
            });
            
        }

    }
    
}
