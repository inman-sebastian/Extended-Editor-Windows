using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace I {
    
    public class Dropdown<TEnum> where TEnum : Enum {

        public TEnum selected;

        public Dropdown(
            string name, 
            TEnum defaultValue, 
            VisualElement template, 
            EventCallback<Dropdown<TEnum>> changeEvent) {
            
            var element = template.Q<EnumField>(name);
            element.value = defaultValue;
            
            element.RegisterCallback<ChangeEvent<Enum>>(@event => {
                selected = (TEnum) @event.newValue;
                changeEvent(this);
            });
            
        }

    }
    
}
