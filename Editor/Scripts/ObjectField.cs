using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class ObjectField<T> : VisualElement<ObjectField> where T : class {

        public T selected;
        
        public ObjectField(string name, VisualElement template) : base(name, template) {
            element = template.Q<ObjectField>(name);
            selected = element?.value as T;
        }

        public ObjectField(string name, EventCallback<ObjectField<T>> changeEvent, VisualElement template) : base(name, template) {
            element = template.Q<ObjectField>(name);
            OnChange(changeEvent);
        }

        public void OnChange(EventCallback<ObjectField<T>> changeEvent) {
            element?.RegisterCallback<ChangeEvent<Object>>(e => {
                selected = e.newValue as T;
                changeEvent(this);
            });
        }

    }
    
}
