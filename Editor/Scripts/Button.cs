using System;
using UnityEngine.UIElements;

namespace I {
    
    public class Button {

        private readonly UnityEngine.UIElements.Button _element;
        
        public bool enabled {
            set => _element.SetEnabled(value);
        }

        public Button(string name, VisualElement template, Action<Button> clickEvent) {
            _element = template.Q<UnityEngine.UIElements.Button>(name);
            _element.RegisterCallback<ClickEvent>(e => clickEvent(this));
        }

    }
    
}
