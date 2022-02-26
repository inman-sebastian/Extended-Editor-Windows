# Changelog
All notable changes to the [Extended Editor Windows](https://github.com/sebastian-inman-design/Extended-Editor-Windows) Unity package will be documented here.


### v1.0.4 - 2022-02-26

- Replaced the `Initialize` method with `OnCreate`, which is no longer a required abstract method, but instead an optional virtual method.
- Added the `OnUpdate` method, which replaces the default `OnGUI` EditorWindow method.
- Added a virtual list of dockable window "panels".
- Updated changelog formatting.


### v1.0.3 - 2022-02-25

- Added the ability to dock windows to one another.


### v1.0.2 - 2022-02-25

- The package no longer requires UnityCoroutines as a dependency.
- Broke stylesheets out into their own, easy to manage files.
- Added the ability to customize the editor window styles by overriding USS style variables.
- Updated README to reflect changes to style variables.


### v1.0.1 - 2022-02-25

- Added new `FileGenerator` helper class that asynchronously generates template files when creating a new Extended Editor Window.
- Added a new tool for creating new Extended Editor Windows automatically. The tool can be found by going to `Assets > Create > Extended Editor Window`.


### v1.0.0 - 2022-02-24

- Updated namespace from `I` to `ExtendedEditorWindow`
- Helper classes now automatically derive the `rootVisualElement` element from the active editor window instead of requiring it as a parameter.


### v1.0.0 - 2022-02-24

- First commit