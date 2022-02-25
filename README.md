# Extended Editor Windows

## Description

[Extended Editor Windows](https://github.com/sebastian-inman-design/Extended-Editor-Windows) is a Unity package containing a collection of helper classes that extend the default Editor Window, making it much easier to create and customize your own Editor Windows within Unity.

## Installation

- Open the Package Manager within Unity by going to `Window > Package Manager`.  
- Click the **+** icon in the upper left hand corner of the window and select `"Add package from git URL..."`.  
- Paste in the following Git URL: [https://github.com/sebastian-inman-design/Extended-Editor-Windows.git](https://github.com/sebastian-inman-design/Extended-Editor-Windows.git)
- Click `Add`

### Folder structure

The folder structure of a newly created Extended Editor Window is as follows:

```
MyExtendedEditorWindow/         # Root directory.
|- MyExtendedEditorWindow.cs    # The script that handles creation and logic of the window.
|- MyExtendedEditorWindow.uss   # The StyleSheet for the template.
|- MyExtendedEditorWindow.uxml  # The template containing the markup for the window.
```

## Samples

### Sample C# Template

```c#
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

## Customization
The following USS variables can be overwritten in order to customize the theme of your Editor Window:

### Window Variables

| Variable           | Default Value | Description                                       |
|--------------------|---------------|---------------------------------------------------|
| `--window-padding` | 16px          | The amount of padding the main editor window has. |

### Field Input Variables

| Variable                           | Default Value | Description                                                    |
|:-----------------------------------|:--------------|:---------------------------------------------------------------|
| `--field-input-height`             | 36px          | The height of the input field.                                 |
| `--field-input-padding`            | 8px           | The horizontal padding of the input field.                     |
| `--field-input-border-width`       | 1px           | The width of the input field border.                           |
| `--field-input-border-color`       | #111111       | The color of the input field border.                           |
| `--field-input-hover-boder-color`  | #666666       | The color of the input field border when the field is hovered. |
| `--field-input-focus-border-color` | #3A79BB       | The color of the input field border when the field is focused. |

## Unity References

- [EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)
- [VisualElement](https://docs.unity3d.com/ScriptReference/UIElements.VisualElement.html)
- [Unity Style Sheets (USS)](https://docs.unity3d.com/Manual/UIE-USS.html)
- [The UXML Format](https://docs.unity3d.com/Manual/UIE-WritingUXMLTemplate.html)