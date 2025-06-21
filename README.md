# Index
- [Installation & Features](#install)
- [Full Changelog](#changelog)
- [License](#license)

---

## <a id="install"></a> Install guide and app overview (Current version: **1.0.2-stable**)

### What is antWriter?
antWriter is a ***modern text editor*** made for poets and alike with Zen mode (distraction-free), autosave, multi-document support,  live character counter and a constantly expanding list of customization options.

## antWriter — Installation Guide
> 🔒 **Note**: antWriter may be flagged by antivirus software on first install due to being unsigned. This is a false positive — once verified by Avast or Windows Defender, the app runs clean. You can verify the integrity of the installer manually or run it in a sandbox for peace of mind.
### 🖥 Requirements
- Windows 10 or later
- [.NET Desktop Runtime 6.0 or newer](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)  
  (Make sure to install the **Desktop Runtime**, not just the SDK)

### 📥 Installation Steps
1. Download `awInstaller-x64-<version>.exe` from the release folder or your source.> 👉 [Click here to download the latest version](https://github.com/booukes/antWriter/releases)
2. Run the installer and follow the on-screen instructions.
3. Optionally, check the box to create a desktop shortcut during installation.
4. Once installed, launch `antWriter` from the Start Menu or Desktop.

### ❌ Uninstallation
- Go to *Control Panel > Programs and Features* and uninstall **antWriter**, or
- Use the *Uninstall antWriter* shortcut from the Start Menu.

## 🚀 Features
Our **features list** is and will be rapidly growing through the updates!
For now, the most important ones include:

- ✍️ Simple, elegant user interface  
- 🧘 Zen Mode – hides all GUI elements for a distraction-free experience  
- 🎨 Custom themes & backgrounds (including **Kitty** mode 🐱)  
- 💾 Automatic file saving, even covering app crashes, so you will NEVER lose your work!  
- 📂 Multi-document support
- 🔢 Live character count

---

## <a id="changelog"></a>Full Changelog (Current version: **1.0.2-stable**)

# 1.0.0-stable [17-06-2025]

- Removed concurrency and `async` from the startup task, making the app more robust.
- Cleaned up the directory, making the app 8mb instead of 55mb.
- Fixed the 1mb threshold not working.
- Cleaned up logs from duplicate `AS Event` messages.
- Resource refactoring.
- Many little bugs removed.

#### 1.0.1-stable [19-06-2025]
- Moved away from the `Metro` Framework, now only dependent on one element.
- Changed app logo to a more concise one.
- Logo now generated statically.
- Removed the option of logo changing, it was just cluttering the settings.

#### 1.0.2-stable [21-06-2025]
- App now runs in `borderless fullscreen`.
- Fixed flickering on window changing.
- Created an installer with `ISScript`.
- Font selection box now shows the fonts style, instead of just the name.
  
## 0.9.0-stable [04-06-2025]

- The editor now supports dragging and dropping of files.
- Refactored zen mode and themes.
- Themes now are implemented into the navbar instead of the whole editor, making the UX better and clearer.
- Removed console from release.

## 0.7.0-rc [28-05-2025]
- Completely rewrote all of the `EditorWindow` code and most of the C# codebase.
- Completely changed file input/output operations to use asynchronous programming with `async` and `await`, leveraging methods like `File.ReadAllTextAsync` and `File.WriteAllTextAsync` to prevent UI freezing during disk access. This ensures large file loads and saves do not block the main thread, significantly improving user experience.
- Introduced async-based architecture across the app, paving the way for more complex features and performance improvements in future updates.
- While the async implementation added a lot more complexity to the codebase, the overall restructuring of it has made the project significantly more maintainable and scalable. Without the new structure, async would have been a nightmare to manage—now it's just barely tolerable.
- Implemented an autosave mechanism using a `DispatcherTimer` that triggers asynchronous save operations at regular intervals. This ensures data is backed up in the background without interrupting user interaction or degrading UI responsiveness.
- Significantly improved logging reliability by restructuring the log creation logic, ensuring that exceptions do not prevent critical logs from being recorded.
- Reworked the character counting logic to operate asynchronously in parallel with file I/O. This enables accurate, real-time updates of text statistics even for large documents, without affecting UI performance.
- Fixed issues with `StaticResources` not applying correctly, restoring consistent styling across UI components.
- Added new warnings and UX prompts, such as "Please use Save As first!" and file load confirmations to improve usability and reduce user error.
- Resolved conflicts between UI rendering and background data operations introduced, of course by async logic, ensuring smoother interaction and reduced visual glitches.

#### 0.7.1-rc2[31-05-2025]
- Optimized the codebase using `Lazy` variables and set up lazy-loading config file which improved the boot time from 650-200ms in prod.
- Enriched log output with caller and added more logging events.
- Started preparing for public release, works have started on logs refactoring with separate sink parameters.
- Added 1mb file size limit, will be removed later in development after the addition of serial file loading.
- Removed legacy github link code.

#### 0.7.2-rc3[1-06-2025]
- Added ZenMode functionality and cleaned up some resources.
#### 0.7.3-rc4[1-06-2025]
- Fixed a critical bug that sometimes occured when the async loading method tried to access a non-existant file.
#### 0.7.4-stable[1-06-2025]
- Prepared binary for release, source code has yet to be cleaned up.
- The released binary is completely stable, no known bugs yet.
- Added ZenThemes, to expand later.
- ZenThemes looks for now very crankily, but it is fully stable.
- Begun work on globalizing app messages

## 0.6.0-beta[26-05-2025]
- Designed and implemented a robust global resource architecture using a hybrid of dynamic and static `App.xaml` resources, with all UI variables (colors, spacing, font sizes, etc.) sourced from a centralized JSON configuration at runtime.
- Performed a full UI/UX overhaul including layout restructuring, refined color theming, and improved responsiveness via grid-based adaptive containers and uniform resource bindings.
- Added support for dual logos with an in-app toggle, including a dynamically generated logo system to bypass WPF bitmap limitations.
- Converted all eligible UI components to consume shared static resources, eliminating redundancy and ensuring visual consistency across views.
- Integrated additional system fonts into the application’s resource context for internal use and improved platform styling fidelity.
- Removed deprecated GitHub titlebar link to streamline interface and reduce non-functional distractions.
- Developed a “No Recent Files” detection system to inform users when the recent documents list is empty.
- Fixed a critical bug in the New File logic that caused infinite duplication when triggered without an active document.
- Enhanced application startup with personalized greeting using the current Windows username.
- Simplified and refactored menu code to reduce event handler complexity and improve maintainability.
- Removed redundant constructor arguments from `SettingsWindow`, reducing unnecessary overhead and improving clarity.
- Addressed various minor bugs and inconsistencies across the UI and data logic layers.

#### 0.6.1-beta[26-05-2025]
- Fixed some resources not persisting after clean start
#### 0.6.2-beta[26-05-2025]
- Added projects folder, which the app defaults to when loading files. Also little easter egg in the files:)
#### 0.6.3-beta[27-05-2025]
- Fixed file saving logic route.
## 0.5.0-alpha [25-05-2025]
- Migrated WPF Framework to `MahApps.Metro`, migrating the main window to `MetroWindow` to leverage modern theming, preparing for ui overhaul, extended controls, and a unified UX.  
- Integrated` MahApps.Metro.IconPacks` providing scalable GitHub icons linked to interactive buttons.  
- Implemented `MahApps.Controls` into settings.
- Implemented a `JSON`-Based ConfigManager for persisting user settings automatically (font size, font family) on startup and exit.  
- Integrated Serilog with console and rolling file sinks, including a customized output template for timestamped, leveled, and exception-aware logging.  
- Allocated a debug console dynamically via `kernel32.dll` AllocConsole for enhanced runtime diagnostics.  
- Extended startup diagnostics to capture `.NET` runtime version and memory usage metrics.  
- Refactored startup logic in `App.xaml.cs` to improve modularity and clarity of application initialization.

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

#### 0.4.3-pre-alpha, 0.4.4-pre-alpha [25-05-2025]
- Minor fixes, build script optimizations.

#### 0.4.2-pre-alpha [24-05-2025]
- Added new file function.
- App is now warning the user, he is writing in a volatile memory-fed state.
- Added function to display filename above editor window.

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

<a id="license"></a> 
## 📄 License

This project is licensed under the **Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)** license.

You are free to:
- **Share** — copy and redistribute the material in any medium or format  
- **Adapt** — remix, transform, and build upon the material

Under the following terms:
- **Attribution** — You must give appropriate credit, provide a link to the license, and indicate if changes were made.
- **NonCommercial** — You may not use the material for commercial purposes.
- **ShareAlike** — If you remix, transform, or build upon the material, you must distribute your contributions under the same license as the original.

### 🤝 Additional Permissions

If you would like to use this project in a way not covered by the license — including **commercial use** or **alternative licensing** — feel free to contact me. I may grant separate permissions on a case-by-case basis.

📜 [View the full license text](https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode)
