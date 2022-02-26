using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class EnumField<TEnum> : VisualElement<EnumField> where TEnum : Enum {

        public TEnum selected;
        
        public EnumField(string name, VisualElement template) : base(name, template) {
            element = template.Q<EnumField>(name);
        }
        
        public EnumField(string name, TEnum defaultValue, VisualElement template) : base(name, template) {
            selected = defaultValue;
            element = template.Q<EnumField>(name);
            element.value = defaultValue;
        }

        public EnumField(string name, TEnum defaultValue, EventCallback<EnumField<TEnum>> changeEvent, VisualElement template) : base(name, template) {
            selected = defaultValue;
            element = template.Q<EnumField>(name);
            element.value = defaultValue;
            element?.RegisterCallback<ChangeEvent<Enum>>(@event => {
                selected = (TEnum) @event.newValue;
                changeEvent(this);
            });
        }

    }
    
}
