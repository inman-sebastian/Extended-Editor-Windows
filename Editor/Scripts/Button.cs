using System;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Button : VisualElement<UnityEngine.UIElements.Button> {

        public bool enabled {
            get => element.enabledSelf;
            set => element.SetEnabled(value);
        }
        
        public Button(string name, VisualElement template) : base(name, template) {
            element = template.Q<UnityEngine.UIElements.Button>(name);
        }

        public Button(string name, Action<Button> clickEvent, VisualElement template) : base(name, template) {
            element = template.Q<UnityEngine.UIElements.Button>(name);
            element?.RegisterCallback<ClickEvent>(e => clickEvent(this));
        }

    }
    
}
