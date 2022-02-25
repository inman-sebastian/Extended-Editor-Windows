using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Element {

        private readonly VisualElement _element;
        
        public bool visible {
            set {
                if (value) {
                    RemoveClass("hidden");
                    AddClass("flex");
                } else {
                    RemoveClass("flex");
                    AddClass("hidden");
                }
                _element.visible = value;
            }
        }
        
        public bool focusable {
            set => _element.focusable = value;
        }

        public Element(string name) {
            
            _element = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<VisualElement>(name);
            
        }
        
        public void AddClass(string className) {
            
            _element.AddToClassList(className);
            
        }
        
        public void RemoveClass(string className) {
            
            _element.RemoveFromClassList(className);
            
        }

        public void ToggleClass(string className) {
            
            if (_element.ClassListContains(className)) {
                RemoveClass(className);
            } else {
                AddClass(className);
            }
            
        }

    }
    
}
