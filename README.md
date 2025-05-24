# Index
- [Technical Changelog](#changelog)
- [Todo](#todo)
- [antWriter](#antwriter)

---

## <a id="changelog"></a>Changelog (Current version: **0.4.1-pre-alpha**)

## 0.4.0-pre-alpha [24-05-2025]
- Implemented core file operations (`Save`, `Load`, `Save As`) in `EditorWindow` using standard WPF file dialogs.
- Integrated Windows `SaveFileDialog` for "Save As" with current file path updating post-save.
- Added autosave mechanism triggered on switching between recent files to minimize data loss risks.
- Developed persistent recent files management via `List<string>` serialized for session retention.
- Dynamically generate recent file buttons with display filename and full file path stored in `Button.Tag`.
- Wired button click events to load files by reading file paths from `Tag`.
- Introduced `InternalSave` method for encapsulating save logic and improving code modularity.
- Removed obsolete test code from Settings window module for codebase cleanliness.
- Implemented active file button highlighting by modifying `Background` property based on current file context.
- Enabled button interaction disabling using `IsHitTestVisible = false` to retain visual style while blocking input.
- Added hex color parsing support for button backgrounds via `BrushConverter`.

#### 0.4.1-pre-alpha [24-05-2025]
- Optimized autosave routine for improved reliability and performance.

## 0.3.0-pre-alpha [24-05-2025]
- Completed app navigation routing using MVVM patterns for seamless window transitions.
- Enhanced application lifecycle management to improve stability and resource handling.
- Developed modular, extensible Settings window with data binding and validation.
- Established global resource dictionary using `DynamicResource` for centralized theming and styles.
- Refactored window navigation structure for improved UX flow.
- Replaced static font references with dynamic resource binding for flexible font management.
- Prepared EditorWindow navbar infrastructure to support future feature integration.
- Updated color palette assets for UI consistency and accessibility.
- Resolved UI scaling issues via `ViewBox` to maintain responsiveness across resolutions.

#### 0.3.1-pre-alpha [24-05-2025]
- Integrated PowerShell build automation script including build logging for CI support.

## 0.2.0-pre-pre-alpha [23-05-2025]
- Refactored codebase for reduced complexity and improved maintainability.
- Deprecated `UserControl` components in favor of streamlined custom controls.
- Initiated implementation of basic app routing logic.

## 0.1.0-pre-pre-alpha [21-05-2025]
- Added skeletal menu structure without functional implementation.
- Built foundational modular UI components to facilitate reuse.
- Optimized project build configuration and toolchain.

#### 0.1.1-pre-pre-alpha [22-05-2025]
- Enhanced code readability through systematic commenting and documentation.


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
