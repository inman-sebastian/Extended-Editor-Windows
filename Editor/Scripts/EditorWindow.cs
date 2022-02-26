using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ExtendedEditorWindows {

    public struct Panel {
        public Type type;
        public Docker.DockPosition position;
    }

    public abstract class ExtendedEditorWindow<T> : EditorWindow where T : EditorWindow {
        
        private static ExtendedEditorWindow<T> Window;
        
        private bool _loadedPanels = false;

        private readonly string[] Stylesheets = {
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/variables.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/helpers.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/window.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/field.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/button.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/colorfield.uss",
            "Packages/com.sebastian-inman.extended-editor-windows/Editor/Styles/helpbox.uss"
        };

        protected abstract List<Panel> panels { get; }

        protected new abstract string title { get; }

        protected static void OpenWindow(bool utility = false, bool focus = true) {
            var window = GetWindow<T>(utility, "", focus) as ExtendedEditorWindow<T>;
        }

        private void OnGUI() {

            if (Window == null) {
                Window = this;
                Window.titleContent = new GUIContent {
                    text = title
                };
            }

            if (panels.Count <= 0 || _loadedPanels) return;
            
            foreach (var panel in panels) {
                var window = GetWindow(panel.type);
                this.Dock(window, panel.position);
            }

            _loadedPanels = true;

            OnUpdate();

        }

        private void CreateGUI() {

            // Programatically get the path of the editor window.
            var editorScript = MonoScript.FromScriptableObject(this);
            var editorPath = AssetDatabase.GetAssetPath(editorScript).Replace(".cs", "");
            
            // Define the paths for all UXML and USS assets associated with the editor window.
            var editorTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{editorPath}.uxml");
            var editorStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{editorPath}.uss");

            // Add the template for this editor window.
            rootVisualElement.Add(editorTemplate.Instantiate());

            // Add global stylesheets to this editor window.
            foreach(var stylesheet in Stylesheets) {
                var asset = AssetDatabase.LoadAssetAtPath<StyleSheet>(stylesheet);
                rootVisualElement.styleSheets.Add(asset);
            }
            
            // Add the stylesheet for this editor window.
            rootVisualElement.styleSheets.Add(editorStyleSheet);

            OnCreate();
            
        }

        protected static void CloseWindow() {
            Window.Close();
        }

        protected VisualElement<VisualElement> VisualElement(string elementName) {
            return new VisualElement<VisualElement>(elementName, rootVisualElement);
        }
        
        protected Label Label(string elementName) {
            return new Label(elementName, rootVisualElement);
        }
        
        protected Image Image(string elementName) {
            return new Image(elementName, rootVisualElement);
        }

        protected Field<TFieldType> Field<TFieldType>(
            string fieldName, 
            TFieldType defaultValue, 
            EventCallback<Field<TFieldType>> changeEvent) {
            return new Field<TFieldType>(fieldName, defaultValue, changeEvent, rootVisualElement);
        }
        
        protected Dropdown<TEnumType> Dropdown<TEnumType>(
            string fieldName,
            TEnumType defaultValue, 
            EventCallback<Dropdown<TEnumType>> changeEvent) where TEnumType : Enum {
            return new Dropdown<TEnumType>(fieldName, defaultValue, changeEvent, rootVisualElement);
        }
        
        protected Asset<TAssetType> Asset<TAssetType>(
            string selectorName, 
            EventCallback<Asset<TAssetType>> changeEvent) where TAssetType : class {
            return new Asset<TAssetType>(selectorName, changeEvent, rootVisualElement);
        }

        protected Button Button(
            string buttonName, 
            Action<Button> clickEvent) {
            return new Button(buttonName, clickEvent, rootVisualElement);
        }

        protected void SendEvent<TWindow>(string eventName) where TWindow : EditorWindow {
            GetWindow<TWindow>().SendEvent(EditorGUIUtility.CommandEvent(eventName));
        }

        protected void EventCallback(string eventName, Action callback) {
            if (Event.current.commandName == eventName) {
                callback();
            }
        }

        protected virtual void OnCreate() { }
        
        protected virtual void OnUpdate() { }

    }

}
