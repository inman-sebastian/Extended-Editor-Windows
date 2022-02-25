using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Dropdown<TEnum> : VisualElement<EnumField> where TEnum : Enum {

        public TEnum selected;

        public Dropdown(string name, TEnum defaultValue, EventCallback<Dropdown<TEnum>> changeEvent, VisualElement template) : base(name, template) {
            
            element = template.Q<EnumField>(name);
            element.value = defaultValue;
            
            element.RegisterCallback<ChangeEvent<Enum>>(@event => {
                selected = (TEnum) @event.newValue;
                changeEvent(this);
            });
            
        }

    }
    
}
