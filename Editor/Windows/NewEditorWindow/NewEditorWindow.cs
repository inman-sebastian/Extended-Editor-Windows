using UnityEditor;
using System.Threading.Tasks;
using ExtendedEditorWindows;

public class NewEditorWindow : ExtendedEditorWindow<NewEditorWindow> {
        
    [MenuItem("Assets/Create/Extended Editor Window", false, 51)]
    public static void Open() => OpenWindow("New Editor Window", true);

    private bool _busy = false;

    private string _windowTitle = "";
    private string _windowFileName = "";
    private string _windowMenuPath = "";

    private Field<string> _titleField;
    private Field<string> _menuPathField;
    private Button _createButton;

    private string _filePath;

    protected override void Initialize() {

        _createButton = Button("CreateEditorWindow", button => Generate());
        _createButton.enabled = false;

        _titleField = Field("Title", "", field => {
            _windowTitle = field.value;
            _windowFileName = field.value.Replace(" ", "");
            _filePath = $"Assets/Editor/Windows/{_windowFileName}/{_windowFileName}";
        });
        
        _menuPathField = Field("MenuPath", "", field => {
            _windowMenuPath = field.value;
        });

    }

    private void OnGUI() {
        
        _createButton.enabled = true;

        if (_windowTitle == "") _createButton.enabled = false;
        if (_windowMenuPath == "") _createButton.enabled = false;
        if(_busy) _createButton.enabled = false;

    }

    private async void Generate() {
        _busy = true;
        await CreateDirectories();
    }

    private async Task CreateDirectories() {
        
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

        await CreateFiles();

    }
    
    private async Task CreateFiles() {

        await GenerateScriptFile();
        await GenerateStyleFile();
        await GenerateTemplateFile();
        
        CloseWindow();

        _busy = false;

    }

    private async Task GenerateScriptFile() {
        
        await FileGenerator.Generate($"{_filePath}.cs", new [] {
            "using UnityEngine;",
            "using ExtendedEditorWindows;",
            "public class "+_windowFileName+" : ExtendedEditorWindow<"+_windowFileName+"> {",
            "    [UnityEditor.MenuItem(\"" + _windowMenuPath + "\")]",
            "    public static void Open() => OpenWindow(\""+_windowTitle+"\");",
            "    protected override void Initialize() {",
            "        Field(\"ExampleInput\", \"\", field => {",
            "            Label(\"ExampleLabel\").text = field.value;",
            "        });",
            "    }",
            "}"
        });

    }
    
    private async Task GenerateStyleFile() {
        
        await FileGenerator.Generate($"{_filePath}.uss", new [] {
            "#ExampleLabel {",
            "    color: dodgerblue;",
            "    font-size: 18px;",
            "}"
        });

    }
    
    private async Task GenerateTemplateFile() {
        
        await FileGenerator.Generate($"{_filePath}.uxml", new [] {
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
            "<UXML xmlns=\"UnityEngine.UIElements\" xmlns:Editor=\"UnityEditor.UIElements\" class=\"h-full\">",
            "    <VisualElement class=\"flex-1 flex flex-col\">",
            "        <TextField name=\"ExampleInput\" label=\"Example Text Field\" class=\"mb-16\" />",
            "        <Label name=\"ExampleLabel\" />",
            "    </VisualElement>",
            "</UXML>"
        });

    }
        
}
