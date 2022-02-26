using UnityEditor;
using ExtendedEditorWindows;
using System.Threading.Tasks;
using System.Collections.Generic;

public class NewEditorWindow : ExtendedEditorWindow<NewEditorWindow> {

    private bool _busy = false;

    private string _windowTitle = "";
    private string _windowFileName = "";
    private string _windowMenuPath = "";

    private Field<string> _titleField;
    private Field<string> _menuPathField;
    private Button _createButton;

    private string _filePath;

    protected override string title => "New Editor Window";
    protected override bool includeTemplateFiles => true;
    protected override List<Panel> panels => new List<Panel>();
    
    [MenuItem("Assets/Create/Editor Window")]
    public static void OnOpen() => OpenWindow();

    protected override void OnCreate() {

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

    protected override void OnUpdate() {
        
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
        
        Close();

        _busy = false;

    }

    private async Task GenerateScriptFile() {
        
        await FileGenerator.Generate($"{_filePath}.cs", new [] {
            "using UnityEditor;",
            "using UnityEngine;",
            "using ExtendedEditorWindows;",
            "",
            "public class "+_windowFileName+" : ExtendedEditorWindow<"+_windowFileName+"> {",
            "",
            "    protected override string title => \""+_windowTitle+"\";",
            "    protected override List<Panel> panels => new List<Panel>();",
            "",
            "    [MenuItem(\"" + _windowMenuPath + "\")]",
            "    public static void OnOpen() => OpenWindow();",
            "",
            "    protected override void OnCreate() {",
            "",
            "        Field(\"ExampleInput\", \"\", field => {",
            "            Label(\"ExampleLabel\").text = field.value;",
            "        });",
            "",
            "    }",
            "",
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
            "",
            "    <VisualElement class=\"flex-1 flex flex-col\">",
            "        <TextField name=\"ExampleInput\" label=\"Example Text Field\" class=\"mb-16\" />",
            "        <Label name=\"ExampleLabel\" />",
            "    </VisualElement>",
            "",
            "</UXML>"
        });

    }
        
}
