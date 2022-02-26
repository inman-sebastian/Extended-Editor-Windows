using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Field<T> : VisualElement<BaseField<T>> {

        public T value;
        
        public Field(string name, VisualElement template) : base(name, template) {
            element = template.Q<BaseField<T>>(name);
        }
        
        public Field(string name, T defaultValue, VisualElement template) : base(name, template) {
            element = template.Q<BaseField<T>>(name);
            element.value = defaultValue;
        }

        public Field(string name, T defaultValue, EventCallback<Field<T>> changeEvent, VisualElement template) : base(name, template) {
            element = template.Q<BaseField<T>>(name);
            element.value = defaultValue;
            OnChange(changeEvent);
        }

        public void OnChange(EventCallback<Field<T>> changeEvent) {
            element.RegisterCallback<ChangeEvent<T>>(@event => {
                value = @event.newValue;
                changeEvent(this);
            });
        }

        public void ClampValue(T minValue, T maxValue) {
            if (float.Parse(element.value.ToString()) < float.Parse(minValue.ToString())) {
                element.value = minValue;
            }
            if (float.Parse(element.value.ToString()) > float.Parse(maxValue.ToString())) {
                element.value = maxValue;
            }
        }
        
    }
    
}
