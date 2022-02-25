using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class VisualElement<T> : VisualElement where T : VisualElement {
        
        public T element;
        
        public new bool visible {
            get => element.visible;
            set {
                if (value) {
                    RemoveClass("hidden");
                    AddClass("flex");
                } else {
                    RemoveClass("flex");
                    AddClass("hidden");
                }
                element.visible = value;
            }
        }
        
        public new bool focusable {
            get => element.focusable;
            set => element.focusable = value;
        }
        
        public VisualElement(string name, VisualElement template) {
            element = template.Q<T>(name);
        }
        
        public void AddClass(string className) {
            element.AddToClassList(className);
        }
        
        public void RemoveClass(string className) {
            element.RemoveFromClassList(className);
        }

        public void ToggleClass(string className) {
            if (element.ClassListContains(className)) {
                RemoveClass(className);
            } else {
                AddClass(className);
            }
        }
        
    }
    
}
