using UnityEngine.UIElements;

namespace I {
    
    public class Field<T> {

        public BaseField<T> field;
        public T value;

        public Field(
            string name, 
            T defaultValue, 
            VisualElement template, 
            EventCallback<Field<T>> changeEvent) {
            field = template.Q<BaseField<T>>(name);
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
