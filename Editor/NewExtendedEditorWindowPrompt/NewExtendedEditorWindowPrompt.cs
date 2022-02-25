using System.Collections;
using UnityEditor;
using ExtendedEditorWindows;

public class NewExtendedEditorWindowPrompt : ExtendedEditorWindow<NewExtendedEditorWindowPrompt> {
        
    [MenuItem("File/New Extended Editor Window")]
    public static void Open() => OpenWindow("New Extended Editor Window");

    private string _windowTitle;
    private string _windowFileName;

    protected override void Initialize() {

        Field("WindowTitle", "", field => {
            _windowTitle = field.value;
            _windowFileName = field.value.Replace(" ", "");
        });

        Button("CreateEditorWindow", button => {

            

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
        
        FileUtil.CopyFileOrDirectory(
            "Packages/com.sebastian-inman.extended-editor-windows/Samples/ExampleWindow/ExampleWindow.cs", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.cs"
        );
            
        FileUtil.CopyFileOrDirectory(
            "Packages/com.sebastian-inman.extended-editor-windows/Samples/ExampleWindow/ExampleWindow.uss", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.uss"
        );
            
        FileUtil.CopyFileOrDirectory(
            "Packages/com.sebastian-inman.extended-editor-windows/Samples/ExampleWindow/ExampleWindow.uxml", 
            $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}.uxml"
        );

        yield return null;

    }
        
}