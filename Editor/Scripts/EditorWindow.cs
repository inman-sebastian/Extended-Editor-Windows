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
        
        public static ExtendedEditorWindow<T> Window;

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
        
        protected abstract bool includeTemplateFiles { get; }

        protected static void OpenWindow(bool utility = false, bool focus = true) {
            Window = GetWindow<T>(utility, "", focus) as ExtendedEditorWindow<T>;
        }

        private void OnGUI() {

            if (Window != null) {
                Window.titleContent = new GUIContent {
                    text = title
                };
            }

            if (panels.Count > 0 && !_loadedPanels) {
                
                foreach (var panel in panels) {
                    var window = GetWindow(panel.type);
                    this.Dock(window, panel.position);
                }

                _loadedPanels = true;
                
            }

            OnUpdate();

        }

        private void CreateGUI() {

            if (includeTemplateFiles) {
             
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
                
            }

            OnCreate();
            
        }

        public VisualElement<VisualElement> VisualElement(string elementName) {
            return new VisualElement<VisualElement>(elementName, rootVisualElement);
        }
        
        public Label Label(string elementName) {
            return new Label(elementName, rootVisualElement);
        }
        
        public Image Image(string elementName) {
            return new Image(elementName, rootVisualElement);
        }
        
        public Field<TFieldType> Field<TFieldType>(string fieldName) {
            return new Field<TFieldType>(fieldName, rootVisualElement);
        }
        
        public Field<TFieldType> Field<TFieldType>(string fieldName, TFieldType defaultValue) {
            return new Field<TFieldType>(fieldName, defaultValue, rootVisualElement);
        }

        public Field<TFieldType> Field<TFieldType>(string fieldName, TFieldType defaultValue, EventCallback<Field<TFieldType>> changeEvent) {
            return new Field<TFieldType>(fieldName, defaultValue, changeEvent, rootVisualElement);
        }
        
        public EnumField<TEnumType> EnumField<TEnumType>(string fieldName) where TEnumType : Enum {
            return new EnumField<TEnumType>(fieldName, rootVisualElement);
        }
        
        public EnumField<TEnumType> EnumField<TEnumType>(string fieldName, TEnumType defaultValue) where TEnumType : Enum {
            return new EnumField<TEnumType>(fieldName, defaultValue, rootVisualElement);
        }

        public EnumField<TEnumType> EnumField<TEnumType>(string fieldName, TEnumType defaultValue, EventCallback<EnumField<TEnumType>> changeEvent) where TEnumType : Enum {
            return new EnumField<TEnumType>(fieldName, defaultValue, changeEvent, rootVisualElement);
        }
        
        public ObjectField<TAssetType> ObjectField<TAssetType>(string fieldName) where TAssetType : class {
            return new ObjectField<TAssetType>(fieldName, rootVisualElement);
        }
        
        public ObjectField<TAssetType> ObjectField<TAssetType>(string fieldName, EventCallback<ObjectField<TAssetType>> changeEvent) where TAssetType : class {
            return new ObjectField<TAssetType>(fieldName, changeEvent, rootVisualElement);
        }

        public Button Button(
            string buttonName, 
            Action<Button> clickEvent) {
            return new Button(buttonName, clickEvent, rootVisualElement);
        }

        public void SendEvent<TWindow>(string eventName) where TWindow : EditorWindow {
            GetWindow<TWindow>().SendEvent(EditorGUIUtility.CommandEvent(eventName));
        }

        public void EventCallback(string eventName, Action callback) {
            if (Event.current.commandName == eventName) {
                callback();
            }
        }

        protected virtual void OnCreate() { }
        
        protected virtual void OnUpdate() { }

    }

}
