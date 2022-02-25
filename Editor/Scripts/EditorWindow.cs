using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {

    public abstract class EditorWindow<T> : EditorWindow where T : EditorWindow {

        private const string Styles = "../Styles/reset.uss";

        private void CreateGUI() {

            // Programatically get the path of the editor window.
            var editorScript = MonoScript.FromScriptableObject(this);
            var editorPath = AssetDatabase.GetAssetPath(editorScript).Replace(".cs", "");
            
            // Define the paths for all UXML and USS assets associated with the editor window.
            var editorTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{editorPath}.uxml");
            var globalStylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Styles);
            var editorStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{editorPath}.uss");

            // Instantiate the editor template and add stylesheets.
            rootVisualElement.Add(editorTemplate.Instantiate());
            rootVisualElement.styleSheets.Add(globalStylesheet);
            rootVisualElement.styleSheets.Add(editorStyleSheet);

            Initialize();
            
        }
        
        protected static void OpenWindow(string title) {
            GetWindow<T>(title).Show();
        }

        protected static void CloseWindow() {
            GetWindow<T>().Close();
        }

        /**
         * Creates a reference to a visual element within the editor window.
         */
        protected Element Element(string elementName) {
            return new Element(elementName);
        }
        
        /**
         * Creates a reference to an image within the editor window.
         */
        protected Image Image(string elementName) {
            return new Image(elementName);
        }

        /**
         * Creates a reference to a field element within the editor window.
         */
        protected Field<TFieldType> Field<TFieldType>(
            string fieldName, 
            TFieldType defaultValue, 
            EventCallback<Field<TFieldType>> changeEvent) {
            return new Field<TFieldType>(fieldName, defaultValue, changeEvent);
        }
        
        /**
         * Creates a reference to a field element within the editor window.
         */
        protected Dropdown<TEnumType> Dropdown<TEnumType>(
            string fieldName,
            TEnumType defaultValue, 
            EventCallback<Dropdown<TEnumType>> changeEvent) where TEnumType : Enum {
            return new Dropdown<TEnumType>(fieldName, defaultValue, changeEvent);
        }
        
        /**
         * Creates a reference to an object selector element within the editor window.
         */
        protected Asset<TAssetType> Asset<TAssetType>(
            string selectorName, 
            EventCallback<Asset<TAssetType>> changeEvent) where TAssetType : class {
            return new Asset<TAssetType>(selectorName, changeEvent);
        }

        /**
         * Creates a reference to a button element within the editor window.
         */
        protected Button Button(
            string buttonName, 
            Action<Button> clickEvent) {
            return new Button(buttonName, clickEvent);
        }

        protected void SendEvent<TWindow>(string eventName) where TWindow : EditorWindow {
            GetWindow<TWindow>().SendEvent(EditorGUIUtility.CommandEvent(eventName));
        }

        protected void EventCallback(string eventName, Action callback) {
            if (Event.current.commandName == eventName) {
                callback();
            }
        }

        protected abstract void Initialize();

    }

}
