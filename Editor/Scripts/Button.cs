using System;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Button {

        private readonly UnityEngine.UIElements.Button _element;
        
        public bool enabled {
            set => _element.SetEnabled(value);
        }

        public Button(string name, Action<Button> clickEvent) {
            
            _element = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<UnityEngine.UIElements.Button>(name);
            _element.RegisterCallback<ClickEvent>(e => clickEvent(this));
            
        }

    }
    
}
