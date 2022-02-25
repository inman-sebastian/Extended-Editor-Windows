using UnityEngine.UIElements;
using UnityEngine;

namespace I {
    
    public class Image {

        private readonly UnityEngine.UIElements.Image _element;
        
        public Texture2D texture {
            set => _element.image = value;
        }

        public Image(string name, VisualElement template) {
            _element = template.Q<UnityEngine.UIElements.Image>(name);
        }

    }
    
}
