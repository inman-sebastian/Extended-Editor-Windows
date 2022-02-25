using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Label : VisualElement<UnityEngine.UIElements.Label> {
        
        public string text {
            get => element.text;
            set => element.text = value;
        }

        public Label(string name, VisualElement template) : base(name, template) {
            element = template.Q<UnityEngine.UIElements.Label>(name);
        }

    }
    
}
