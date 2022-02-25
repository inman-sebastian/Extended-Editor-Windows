using UnityEngine;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Image {

        private readonly UnityEngine.UIElements.Image _element;
        
        public Texture2D texture {
            set => _element.image = value;
        }

        public Image(string name) {
            
            _element = UnityEditor.EditorWindow.focusedWindow.rootVisualElement.Q<UnityEngine.UIElements.Image>(name);
            
        }

    }
    
}
