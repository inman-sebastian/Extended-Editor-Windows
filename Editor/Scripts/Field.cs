using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Field<T> {

        public readonly BaseField<T> field;
        public T value;

        public Field(string name, T defaultValue, EventCallback<Field<T>> changeEvent) {
            
            field = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<BaseField<T>>(name);
            field.value = defaultValue;
            
            field.RegisterCallback<ChangeEvent<T>>(@event => {
                value = @event.newValue;
                changeEvent(this);
            });
            
        }

        public void ClampValue(T minValue, T maxValue) {

            if (float.Parse(field.value.ToString()) < float.Parse(minValue.ToString())) {
                field.value = minValue;
            }

            if (float.Parse(field.value.ToString()) > float.Parse(maxValue.ToString())) {
                field.value = maxValue;
            }

        }
        
    }
    
}
