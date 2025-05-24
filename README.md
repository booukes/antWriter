# Index
- [Technical Changelog](#changelog)
- [ETR Log](#ETRlog)
- [ETR Log PL](#ETRPLlog)
- [Todo](#todo)
- [antWriter](#antwriter)

---

## <a id="changelog"></a>Technical Changelog (Current version: **0.4.1-pre-alpha**)

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

# <a id="ETRlog"></a>ETR Log (Current version: **0.4.1-pre-alpha**)

## 0.4.0-pre-alpha [24-05-2025]
This update made the editor fully functional for the first time, adding essential features to manage your files easily and safely!

- You can now **save your work** directly from the editor, **load files** you’ve saved before, and use **Save As** to create new files or save your work in different locations. It uses the standard Windows file dialog for a familiar experience.
- To protect your progress, the app now **autosaves your work automatically** when you switch between recently used files—no more worrying about losing unsaved changes!
- A **recent files list** was added that updates automatically as you open and save files. It shows buttons with the file names, and clicking any button loads that file immediately.
- The currently active file’s button is highlighted with a different color so you always know which file you’re working on.
- Buttons that shouldn’t be clickable are now disabled but keep their normal appearance—no more confusing grayed-out buttons.
- You can customize button colors using HEX color codes, giving you a nicer and more flexible look.
- Under the hood, the code was cleaned up and organized to make future improvements easier and keep everything neat.


##### 0.4.1-pre-alpha [24-05-2025]
- Improved autosave function to be more reliable and smoother.

## 0.3.0-pre-alpha [24-05-2025]
This update focused on getting the app’s core structure in place:

- The app’s navigation and routing are now fully working, so moving between different parts of the program is smooth.
- I improved how windows open, close, and manage themselves to avoid bugs.
- There’s now a fully functional **Settings window** where you can change various options.
- Fonts and colors got a big upgrade with a new system that makes customizing easier.
- I fixed how the program adjusts to different screen sizes so everything looks right.
- The editor’s navigation bar is set up and ready for future features.

##### 0.3.1-pre-alpha [24-05-2025]
- Added PowerShell build script with build logging functionality.

## 0.2.0-pre-pre-alpha [23-05-2025]
Simplified the app and started building its basic navigation system:

- The code was cleaned up and made simpler to speed up development.
- Removed some old components that weren’t needed anymore.
- Started work on making it possible to switch between different app pages.

## 0.1.0-pre-pre-alpha [21-05-2025]
Got the foundation ready with initial UI and reusable pieces:

- Added a basic menu, though it’s not working fully yet.
- Created modular components designed to be reused later on.
- Made improvements to the build process to make it smoother and more reliable.

##### 0.1.1-pre-pre-alpha [22-05-2025]
- Added comments throughout the code to make it easier to understand and edit in the future.

*Last updated: 24-05-2025*

---

## <a id="ETRPLlog"></a> Lista ETR (Obecna wersja: **0.4.1-pre-alpha**)

## 0.4.0-pre-alpha [24-05-2025]
W tej wersji edytor stał się w pełni funkcjonalny i gotowy do codziennego użytku. Dodałem wiele istotnych funkcji, które znacząco ułatwiają i zabezpieczają pracę z plikami:

- Teraz możesz **zapisywać swoją pracę** bezpośrednio w edytorze, **otwierać wcześniej zapisane pliki** oraz korzystać z funkcji **Zapisz jako**, która pozwala tworzyć nowe pliki lub zapisać je w innym miejscu. Całość działa przez standardowe okno dialogowe Windows, co sprawia, że wszystko jest intuicyjne i znajome.
- Wprowadziłem mechanizm **autosave**, który automatycznie zapisuje zmiany podczas przełączania się między ostatnio używanymi plikami, abyś nie musiał się martwić o utratę danych.
- Stworzyłem dynamiczną listę **ostatnich plików**, która automatycznie się aktualizuje. Przyciski z nazwami plików umożliwiają szybkie otwieranie wybranych dokumentów.
- Aktywny plik jest wyraźnie oznaczony innym kolorem, dzięki czemu zawsze wiesz, nad czym aktualnie pracujesz.
- Przyciski, które nie powinny być klikane, są teraz wyłączone, ale zachowują swój normalny wygląd — bez klasycznego efektu „wyszarzenia”.
- Dodano wsparcie dla kolorów przycisków definiowanych w formacie HEX, co daje większą swobodę w personalizacji wyglądu interfejsu.
- Dodatkowo, w tle uporządkowałem i uprościłem kod, aby jego dalsza rozbudowa była łatwiejsza i bardziej przejrzysta.

#### 0.4.1-pre-alpha [24-05-2025]
- Udoskonaliłem mechanizm autosave, aby działał jeszcze bardziej stabilnie i płynnie.

## 0.3.0-pre-alpha [24-05-2025]
W tym wydaniu skupiłem się na zbudowaniu solidnych fundamentów aplikacji:

- Nawigacja i przełączanie między różnymi częściami programu działają już płynnie i bez błędów.
- Poprawiłem sposób zarządzania oknami, aby uniknąć problemów przy ich otwieraniu i zamykaniu.
- Dodałem w pełni funkcjonalne, modułowe okno Ustawień, gdzie możesz dostosować różne opcje.
- Zmodernizowałem system czcionek i kolorów, aby personalizacja była łatwiejsza i bardziej elastyczna.
- Naprawiłem problemy z dopasowaniem interfejsu do różnych rozmiarów ekranu.
- Przygotowałem pasek nawigacji edytora pod przyszłe rozszerzenia funkcjonalności.

#### 0.3.1-pre-alpha [24-05-2025]
- Dodano skrypt do budowania projektu w PowerShell z możliwością zapisywania logów procesu.

## 0.2.0-pre-pre-alpha [23-05-2025]
- Znacząco uprościłem strukturę kodu, żeby praca nad projektem była szybsza.
- Usunąłem niepotrzebne komponenty, które spowalniały działanie aplikacji.
- Rozpocząłem tworzenie podstawowej nawigacji między stronami.

## 0.1.0-pre-pre-alpha [21-05-2025]
- Dodano prostą wersję menu, która jeszcze nie działa.
- Stworzyłem pierwsze modułowe komponenty do dalszego wykorzystania.
- Ulepszyłem proces budowania aplikacji, żeby był bardziej niezawodny.

#### 0.1.1-pre-pre-alpha [22-05-2025]
- Dodałem komentarze w kodzie, co znacznie ułatwia jego zrozumienie i przyszłą pracę nad projektem.

*Ostatnia aktualizacja: 24-05-2025*


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
