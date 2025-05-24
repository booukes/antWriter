# Index
- [Changelog](#changelog)
- [antWriter](#antwriter)

---

## <a id="changelog"></a>Changelog (Current version: **0.4.1-pre-alpha**)

## 0.4.0-pre-alpha [24-05-2025]
- Implemented Save, Load, and Save As functionality in `EditorWindow`.
- Save As now uses a Windows file dialog and updates the current path after saving.
- Added autosave when switching between recent files to prevent data loss.
- Added persistent recent files system stored in a `List<T>` with dynamic UI updates.
- Added dynamic recent file buttons showing filenames with full paths stored in `Tag`.
- Implemented click handler to load files using button `Tag`.
- Set up `InternalSave` function for cleaner logic separation.
- Cleaned up Settings window testing code.
- Created function to highlight the active file button by changing its background color.
- Enabled button disabling without visual changes using `IsHitTestVisible = false`.
- Added support for HEX color backgrounds using `BrushConverter`.

#### 0.4.1-pre-alpha [24-05-2025]
- Improved autosave function.

## 0.3.0-pre-alpha [24-05-2025]
- Finalized app routing implementation.
- Improved lifecycle management system.
- Added a fully functional, modular Settings window.
- Introduced global `DynamicResource` variable library.
- Enhanced window navigation and application structure.
- Refactored font switching to use dynamic resource binding.
- Prepared `EditorWindow` navbar for future features.
- Refreshed default color palette.
- Fixed responsiveness issues using `ViewBox`.

#### 0.3.1-pre-alpha [24-05-2025]
- Added PowerShell build script with build logging functionality.

## 0.2.0-pre-pre-alpha [23-05-2025]
- Simplified codebase structure.
- Removed `UserControl` components.
- Started work on basic app routing.

## 0.1.0-pre-pre-alpha [21-05-2025]
- Added a basic, non-functional menu.
- Created first modular components for future reuse.
- Optimized build setup.

#### 0.1.1-pre-pre-alpha [22-05-2025]
- Commented code for improved readability.

---

## <a id="antwriter"></a>antWriter
### The poet's assistant.
