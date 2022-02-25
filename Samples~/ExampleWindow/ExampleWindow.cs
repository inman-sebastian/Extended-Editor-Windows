using UnityEngine;
using ExtendedEditorWindows;

public class ExampleWindow : ExtendedEditorWindow<ExampleWindow> {
    
    public static void Open() => OpenWindow("Example Window");

    protected override void Initialize() {

        Field("ExampleInput", "", field => {
            Label("ExampleLabel").text = field.value;
        });

        Button("LogExampleInput", button => {
            Debug.Log(Label("ExampleLabel").text);
        });

    }
        
}
