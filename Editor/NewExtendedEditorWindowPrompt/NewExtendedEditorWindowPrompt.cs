using System.Collections;
using UnityEditor;
using ExtendedEditorWindows;
using Unity.EditorCoroutines.Editor;

public class NewExtendedEditorWindowPrompt : ExtendedEditorWindow<NewExtendedEditorWindowPrompt> {
        
    [MenuItem("Assets/Create/Extended Editor Window", false, 51)]
    public static void Open() => OpenWindow("New Editor Window");

    private string _windowTitle;
    private string _windowFileName;

    protected override void Initialize() {
        
        var createEditorWindowButton = Button("CreateEditorWindow", button => {
            button.enabled = false;
            this.StartCoroutine(CreateDirectories());
            this.StartCoroutine(CreateFiles());
            button.enabled = true;
        });

        createEditorWindowButton.enabled = false;
        
        Field("WindowTitle", "", field => {
            _windowTitle = field.value;
            _windowFileName = field.value.Replace(" ", "");
            createEditorWindowButton.enabled = field.value != "";
        });

    }

    private IEnumerator CreateDirectories() {
        
        // Create the Editor folder if it doesn't exist.
        if (!AssetDatabase.IsValidFolder("Assets/Editor")) {
            AssetDatabase.CreateFolder("Assets", "Editor");
        }

        // Create the Windows folder if it doesn't exist.
        if (!AssetDatabase.IsValidFolder("Assets/Editor/Windows")) {
            AssetDatabase.CreateFolder("Assets/Editor", "Windows");
        }
            
        // Create the Extended Editor Window folder if it doesn't exist.
        if (!AssetDatabase.IsValidFolder($"Assets/Editor/Windows/{_windowFileName}")) {
            AssetDatabase.CreateFolder("Assets/Editor/Windows", _windowFileName);
        }

        yield return null;
        
    }

    private IEnumerator CreateFiles() {
        
        // Generate the C# script file.
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
            "Packages/com.sebastian-inman.extended-editor-windows/Templates~/ExtendedEditorWindow.cs.txt", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.cs"
        );
        
        // Generate the USS stylesheet.
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
            "Packages/com.sebastian-inman.extended-editor-windows/Templates~/ExtendedEditorWindow.uss.txt", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.uss"
        );
        
        // Generate the UXML template.
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
            "Packages/com.sebastian-inman.extended-editor-windows/Templates~/ExtendedEditorWindow.uxml.txt", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.uxml"
        );

        AssetDatabase.Refresh();

        yield return null;

    }
        
}