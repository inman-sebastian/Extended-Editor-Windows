# Extended Editor Windows

## Description

[Extended Editor Windows](https://github.com/sebastian-inman-design/Extended-Editor-Windows) is a collection of helper classes that extend the default Unity Editor Windows.

## Installation

- Open the Package Manager within Unity by going to `Window > Package Manager`.  
- Click the **+** icon in the upper left hand corner of the window and select `"Add package from git URL..."`.  
- Paste in the following Git URL: [https://github.com/sebastian-inman-design/Extended-Editor-Windows.git](https://github.com/sebastian-inman-design/Extended-Editor-Windows.git)

### Folder structure

The folder structure of a newly created Extended Editor Window is as follows:

```
MyExtendedEditorWindow/         # Root directory.
|- MyExtendedEditorWindow.cs    # The script that handles creation and logic of the window.
|- MyExtendedEditorWindow.uss   # The StyleSheet for the template.
|- MyExtendedEditorWindow.uxml  # The template containing the markup for the window.
```

## Samples

### Sample CS Template

```c#
using UnityEngine;
using ExtendedEditorWindows;

public class ExampleWindow : EditorWindow<ExampleWindow> {
    
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
```

### Sample UXL Template

```xml
<?xml version="1.0" encoding="utf-8"?>
<UXML xmlns="UnityEngine.UIElements" xmlns:Editor="UnityEditor.UIElements" class="h-full">

    <VisualElement class="flex-1 flex flex-col">
        <TextField name="ExampleInput" label="Example Text Field" class="mb-16" />
        <Label name="ExampleLabel" />
    </VisualElement>

    <VisualElement class="footer-actions">
        <Button name="LogExampleInput" text="Log Example Input" />
    </VisualElement>

</UXML>
```

## Unity References

- [EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
- [VisualElement](https://docs.unity3d.com/ScriptReference/UIElements.VisualElement.html)
- [Wikipedia: Markdown](http://wikipedia.org/wiki/Markdown)