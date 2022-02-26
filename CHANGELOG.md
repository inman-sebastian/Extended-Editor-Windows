# Changelog
All notable changes to this project will be documented in this file.

## [1.0.3] - 2022-02-25

### Added

- Added the ability to dock windows to one another.

## [1.0.2] - 2022-02-25

### Changed

- The package no longer requires UnityCoroutines as a dependency.
- Broke stylesheets out into their own, easy to manage files.
- Added the ability to customize the editor window styles by overriding USS style variables.
- Updated README to reflect changes to style variables.

## [1.0.1] - 2022-02-25

### Added

- Added new `FileGenerator` helper class that asynchronously generates template files when creating a new Extended Editor Window.
- Added a new tool for creating new Extended Editor Windows automatically. The tool can be found by going to `Assets > Create > Extended Editor Window`.

## [1.0.0] - 2022-02-24

### Changed

- Updated namespace from `I` to `ExtendedEditorWindow`
- Helper classes now automatically derive the `rootVisualElement` element from the active editor window instead of requiring it as a parameter.

## [1.0.0] - 2022-02-24

### Added

- First commit