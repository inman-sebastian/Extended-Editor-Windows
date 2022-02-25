using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Asset<T> where T : class {

        public T selected;

        public Asset(string name, EventCallback<Asset<T>> changeEvent) {
            
            var element = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<ObjectField>(name);
            
            element.RegisterCallback<ChangeEvent<Object>>(e => {
                selected = e.newValue as T;
                changeEvent(this);
            });
            
        }

    }
    
}
