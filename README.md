# Index
- [Technical Changelog](#changelog)
- [ETR Log](#ETRlog)
- [ETR Log PL](#ETRPLlog)
- [Todo](#todo)
- [antWriter](#antwriter)

---

## <a id="changelog"></a>Technical Changelog (Current version: **0.6.2-beta**)

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

# <a id="ETRlog"></a>ETR Log (Current version: **0.6.0-beta**)

## 0.6.0-alpha [26-05-2025]

- Built a powerful new resource system that loads design settings (like colors, spacing, and fonts) from a JSON file, giving the app more flexibility and easier theming.  
- Gave the entire interface a visual refresh with better layout, cleaner colors, and improved responsiveness for different screen sizes.  
- Added support for two logos — including a dynamically generated one — and let users switch between them in real time.  
- Standardized the app’s look by making nearly all elements use shared styles and static resources.  
- Included more system fonts in the app for improved internal styling and compatibility.  
- Removed the GitHub titlebar link to reduce clutter and keep the interface clean.  
- Let users know when there are no recent files with a friendly message instead of an empty list.  
- Fixed a major issue where creating a new file with no current document would cause infinite duplicates.  
- The app now greets you by name using your Windows username at startup.  
- Simplified and cleaned up menu logic to make it easier to update and maintain.  
- Removed extra code from SettingsWindow that wasn’t needed anymore.  
- Fixed several minor bugs and polished up various parts of the app.


## 0.5.0-alpha [25-05-2025]

- Switched the app’s core framework to MahApps.Metro, giving it a modern look and smoother, more consistent design.  
- Added new icons (including GitHub) that look sharp and work well with buttons.  
- Improved settings controls for easier and better user interaction.  
- Created a system that automatically saves and loads your preferences (like font size and style) every time you open or close the app.  
- Set up advanced logging to keep track of app activity and errors, helping with troubleshooting.  
- Opened a special debug window to show real-time technical info during development.  
- Added extra checks at startup to record important system info like .NET version and memory use.  
- Cleaned up and organized the app’s startup code to make it easier to manage and update in the future.  


## 0.4.0-pre-alpha [24-05-2025]
This update made the editor fully functional for the first time, adding essential features to manage your files easily and safely!

- You can now **save your work** directly from the editor, **load files** you’ve saved before, and use **Save As** to create new files or save your work in different locations. It uses the standard Windows file dialog for a familiar experience.
- To protect your progress, the app now **autosaves your work automatically** when you switch between recently used files—no more worrying about losing unsaved changes!
- A **recent files list** was added that updates automatically as you open and save files. It shows buttons with the file names, and clicking any button loads that file immediately.
- The currently active file’s button is highlighted with a different color so you always know which file you’re working on.
- Buttons that shouldn’t be clickable are now disabled but keep their normal appearance—no more confusing grayed-out buttons.
- You can customize button colors using HEX color codes, giving you a nicer and more flexible look.
- Under the hood, the code was cleaned up and organized to make future improvements easier and keep everything neat.

#### 0.4.3-pre-alpha, 0.4.4-pre-alpha [25-05-2025]
- Minor code fixes, optimized the script that builds the app.

#### 0.4.2-pre-alpha [24-05-2025]
- You can now create new files directly from the app.
- The app will warn you if you are NOT writing to a file, and your work is unsafe.
- There is now a panel which displays the current file.

#### 0.4.1-pre-alpha [24-05-2025]
- Improved autosave function to be more reliable and smoother.

## 0.3.0-pre-alpha [24-05-2025]
This update focused on getting the app’s core structure in place:

- The app’s navigation and routing are now fully working, so moving between different parts of the program is smooth.
- I improved how windows open, close, and manage themselves to avoid bugs.
- There’s now a fully functional **Settings window** where you can change various options.
- Fonts and colors got a big upgrade with a new system that makes customizing easier.
- I fixed how the program adjusts to different screen sizes so everything looks right.
- The editor’s navigation bar is set up and ready for future features.

#### 0.3.1-pre-alpha [24-05-2025]
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

#### 0.1.1-pre-pre-alpha [22-05-2025]
- Added comments throughout the code to make it easier to understand and edit in the future.

*Last updated: 24-05-2025*

---

## <a id="ETRPLlog"></a> Lista ETR (Obecna wersja: **0.6.0-beta**)

## 0.6.0-beta [25-05-2025]

- Zbudowano zaawansowany system zasobów, który ładuje ustawienia wyglądu (kolory, odstępy, czcionki itp.) z pliku JSON, co umożliwia łatwiejsze dostosowywanie aplikacji.  
- Odświeżono cały interfejs: poprawiono układ, kolory i responsywność na różnych rozdzielczościach ekranu.  
- Dodano obsługę dwóch logo — w tym jedno generowane dynamicznie — oraz możliwość przełączania się między nimi w czasie rzeczywistym.  
- Ujednolicono wygląd aplikacji, sprawiając, że niemal wszystkie elementy korzystają ze wspólnych stylów i zasobów statycznych.  
- Dodano więcej czcionek systemowych do wewnętrznego użytku aplikacji, co poprawia kompatybilność i spójność stylistyczną.  
- Usunięto link do GitHuba z paska tytułu, by uprościć interfejs i zmniejszyć wizualny bałagan.  
- Dodano informację o braku ostatnich plików, dzięki czemu użytkownik nie widzi pustej listy bez kontekstu.  
- Naprawiono poważny błąd, w którym kliknięcie przycisku „Nowy plik” bez aktywnego dokumentu powodowało nieskończone duplikowanie.  
- Aplikacja teraz wita użytkownika, wyświetlając jego nazwę z systemu Windows przy uruchomieniu.  
- Uproszczono logikę menu, co ułatwia dalszy rozwój i konserwację.  
- Usunięto zbędne argumenty z konstruktora `SettingsWindow`, co poprawia czytelność i zmniejsza nadmiarowy kod.  
- Naprawiono szereg drobnych błędów oraz przeprowadzono ogólne poprawki i optymalizacje.


## 0.5.0-alpha [25-05-2025]

- Przełączyliśmy główny framework aplikacji na MahApps.Metro, dzięki czemu wygląda nowocześnie i ma spójny, płynny design.  
- Dodaliśmy nowe ikony (w tym GitHub), które są wyraźne i dobrze działają z przyciskami.  
- Ulepszyliśmy elementy sterujące w ustawieniach, aby korzystanie z nich było prostsze i wygodniejsze.  
- Stworzyliśmy system, który automatycznie zapisuje i wczytuje Twoje preferencje (np. rozmiar i styl czcionki) przy uruchamianiu i zamykaniu aplikacji.  
- Wdrożyliśmy zaawansowane logowanie, które śledzi działanie aplikacji i błędy, co pomaga w rozwiązywaniu problemów.  
- Otwieramy specjalne okno debugowania pokazujące techniczne informacje na żywo podczas pracy nad aplikacją.  
- Dodaliśmy dodatkowe kontrole przy starcie aplikacji, które rejestrują ważne dane systemowe, takie jak wersja .NET i zużycie pamięci.  
- Posprzątaliśmy i uporządkowaliśmy kod odpowiedzialny za uruchamianie aplikacji, żeby łatwiej było go zarządzać i rozwijać.  


## 0.4.0-pre-alpha [24-05-2025]
W tej wersji edytor stał się w pełni funkcjonalny i gotowy do codziennego użytku. Dodałem wiele istotnych funkcji, które znacząco ułatwiają i zabezpieczają pracę z plikami:

- Teraz możesz **zapisywać swoją pracę** bezpośrednio w edytorze, **otwierać wcześniej zapisane pliki** oraz korzystać z funkcji **Zapisz jako**, która pozwala tworzyć nowe pliki lub zapisać je w innym miejscu. Całość działa przez standardowe okno dialogowe Windows, co sprawia, że wszystko jest intuicyjne i znajome.
- Wprowadziłem mechanizm **autosave**, który automatycznie zapisuje zmiany podczas przełączania się między ostatnio używanymi plikami, abyś nie musiał się martwić o utratę danych.
- Stworzyłem dynamiczną listę **ostatnich plików**, która automatycznie się aktualizuje. Przyciski z nazwami plików umożliwiają szybkie otwieranie wybranych dokumentów.
- Aktywny plik jest wyraźnie oznaczony innym kolorem, dzięki czemu zawsze wiesz, nad czym aktualnie pracujesz.
- Przyciski, które nie powinny być klikane, są teraz wyłączone, ale zachowują swój normalny wygląd — bez klasycznego efektu „wyszarzenia”.
- Dodano wsparcie dla kolorów przycisków definiowanych w formacie HEX, co daje większą swobodę w personalizacji wyglądu interfejsu.
- Dodatkowo, w tle uporządkowałem i uprościłem kod, aby jego dalsza rozbudowa była łatwiejsza i bardziej przejrzysta.

#### 0.4.3-pre-alpha, 0.4.4-pre-alpha [25-05-2025]
- Bugfixy, optymalizacja skryptu budującego program.

#### 0.4.2-pre-alpha [24-05-2025]
- Dodana funkcja tworzenia nowych plików bezpośrednio w aplikacji.
- Aplikacja teraz ostrzega użytkownika, że jego praca nie jest zapisywana w pliku.
- Dodano panel informujący o obecnym pliku.

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
