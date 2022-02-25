using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Dropdown<TEnum> where TEnum : Enum {

        public TEnum selected;

        public Dropdown(string name, TEnum defaultValue, EventCallback<Dropdown<TEnum>> changeEvent) {
            
            var element = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<EnumField>(name);
            element.value = defaultValue;
            
            element.RegisterCallback<ChangeEvent<Enum>>(@event => {
                selected = (TEnum) @event.newValue;
                changeEvent(this);
            });
            
        }

    }
    
}
