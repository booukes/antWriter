# Index
- [Changelog](#changelog)
- [Todo](#todo)
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

# TODO<a id="todo"></a>

### A collection of future ideas and features for development.

## ✍️ Editor Features
- [ ] Undo / Redo functionality
- [ ] Real-time autosave indicator (e.g., "Saved ✓" / "Saving…")
- [ ] Multi-tab document support
- [ ] Word / character / line counter
- [ ] "Rename file" functionality
- [ ] Document title detection from first line
- [ ] Highlight unsaved file button in recent list
- [ ] Custom font picker dialog
- [ ] Theme switcher (light/dark/custom)
- [ ] Markdown preview panel

## 📁 File Management
- [ ] File version history (local backups)
- [ ] Export as PDF or HTML
- [ ] "Open Folder" for workspace-wide editing
- [ ] Drag-and-drop file support
- [ ] File recovery after crash

## ⚙️ Settings
- [ ] Per-file editor settings (font, theme)
- [ ] Auto-indent and tab width configuration
- [ ] Custom keybindings support
- [ ] Reset settings to default button

## 🛠️ Utility & Features
- [ ] Command palette (Ctrl+P-like fuzzy actions)
- [ ] Typing sound effects toggle (distraction-free writing)
- [ ] Daily writing goal tracker
- [ ] Pomodoro timer integration
- [ ] Plugin system or scripting support

## ☁️ Cloud & Sharing (Experimental)
- [ ] Cloud sync with Google Drive / OneDrive
- [ ] Share document as link (via temp host or cloud)
- [ ] Live collaboration (multi-user editing)

## 🧪 Fun / Extras
- [ ] Hidden “typewriter” theme mode
- [ ] Ambient sound background feature
- [ ] "Zen mode" full-screen writing with no UI

---

> Add, revise, and cut freely as the project evolves.


---

## <a id="antwriter"></a>antWriter
### The poet's assistant.
