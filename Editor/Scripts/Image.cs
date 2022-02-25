using UnityEngine;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {
    
    public class Image : VisualElement<UnityEngine.UIElements.Image> {

        public Texture2D texture {
            get => element.image as Texture2D;
            set => element.image = value;
        }

        public Image(string name, VisualElement template) : base(name, template) {
            element = template.Q<UnityEngine.UIElements.Image>(name);
        }

    }
    
}
