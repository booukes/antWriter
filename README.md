# Index
- [Full Changelog](#changelog)
- [ETR Log](#ETRlog)
- [ETR Log PL](#ETRPLlog)

---

## <a id="changelog"></a>Full Changelog (Current version: **0.7.2-rc3**)

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

# <a id="ETRlog"></a>ETR Log (Current version: **0.7.2-rc3**)

## 0.7.0-rc [28-05-2025]
- The editor was almost entirely rebuilt from the ground up to make it cleaner, faster, and easier to improve in the future.
- Opening and saving files is now handled asynchronously. This means those actions happen in the background while you continue working, so the app doesn’t freeze even with large documents.
- The app’s internal structure has been updated to support more asynchronous features in future updates. These changes make it more reliable and better prepared for upcoming improvements.
- One of the only things left without asynchronous operation is the logging system, as asynchronous programming can produce very unexpected behaviour, and the logging system the last thing we want to do unexpected things.
- Your work now autosaves at regular intervals using asynchronous saving. This protects your progress without interrupting your work or slowing the app down.
- The app’s logging system was largely improved to better handle errors and ensure important activity is always recorded—even when something goes wrong.
- The character count now runs asynchronously too. That means it updates smoothly and instantly, no matter how large your document is, without affecting performance.
- Several new features, with more to come were added thanks to asynchronous operations: file loading, file saving, autosaving, and live character counting all now run in the background to improve speed and responsiveness.
- ZenMode was added allowing you to fully endulge in your writing tasks.
- Fixed some issues with how the app’s styling was applied, so the user interface now looks more consistent and polished across different screens.
- Added new messages to guide users, like reminders to "Save As" and confirmations when loading files, making the app easier and clearer to use.
- "Asynchronous" (or "async") means the app can do multiple things at the same time—like saving a file or counting characters—without making you wait. Under the hood, this works by letting the app start a task (like reading a file) and then move on to other work instead of waiting for that task to finish. When the task is done, the app gets notified (via something called a callback, promise, or event), and it continues processing the result. This helps keep everything fast and smooth, even when handling big or time-consuming tasks.

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
- Minor code fixes, optimized the script that builds the app.
- You can now create new files directly from the app.
- The app will warn you if you are NOT writing to a file, and your work is unsafe.
- There is now a panel which displays the current file.


## 0.3.0-pre-alpha [24-05-2025]
This update focused on getting the app’s core structure in place:

- The app’s navigation and routing are now fully working, so moving between different parts of the program is smooth.
- I improved how windows open, close, and manage themselves to avoid bugs.
- There’s now a fully functional **Settings window** where you can change various options.
- Fonts and colors got a big upgrade with a new system that makes customizing easier.
- I fixed how the program adjusts to different screen sizes so everything looks right.
- The editor’s navigation bar is set up and ready for future features.
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
- Added comments throughout the code to make it easier to understand and edit in the future.

*Last updated: 31-05-2025*

---

## <a id="ETRPLlog"></a> Lista ETR (Obecna wersja: **0.7.2-rc3**)

## 0.7.0-rc [28-05-2025]  
- Edytor został niemal całkowicie przebudowany od podstaw, aby był czystszy, szybszy i łatwiejszy do dalszego rozwoju.  
- Otwieranie i zapisywanie plików odbywa się teraz asynchronicznie. Oznacza to, że te operacje działają w tle, podczas gdy Ty możesz kontynuować pracę, dzięki czemu aplikacja nie zawiesza się nawet przy dużych dokumentach.  
- Struktura wewnętrzna aplikacji została zaktualizowana, aby obsługiwać więcej funkcji asynchronicznych w przyszłych aktualizacjach. Zmiany te zwiększają niezawodność i przygotowują aplikację na kolejne ulepszenia.  
- Jednym z nielicznych elementów, który nadal działa synchronicznie, jest system logowania — ponieważ programowanie asynchroniczne może powodować bardzo nieoczekiwane zachowania, a system logów to ostatnie miejsce, gdzie chcemy takich niespodzianek.  
- Twoja praca jest teraz automatycznie zapisywana w regularnych odstępach czasu dzięki asynchronicznemu zapisywaniu. Chroni to postępy bez przerywania pracy i spowalniania aplikacji.  
- System logowania aplikacji został znacznie ulepszony, aby lepiej obsługiwać błędy i zapewniać zapisywanie ważnych informacji — nawet jeśli coś pójdzie nie tak.  
- Liczenie znaków również działa teraz asynchronicznie. Oznacza to, że wynik aktualizuje się płynnie i natychmiastowo, niezależnie od wielkości dokumentu, bez wpływu na wydajność.  
- Dodano kilka nowych funkcji, z których wiele możliwe było właśnie dzięki operacjom asynchronicznym: ładowanie plików, zapisywanie, autosave i liczenie znaków na żywo działają teraz w tle, zwiększając szybkość i responsywność aplikacji.  
- Naprawiono kilka problemów związanych z wyglądem aplikacji — interfejs użytkownika wygląda teraz bardziej spójnie i estetycznie na różnych ekranach.  
- Dodano nowe komunikaty ułatwiające korzystanie z aplikacji, takie jak przypomnienia o użyciu „Zapisz jako” i potwierdzenia podczas ładowania plików. Dzięki temu obsługa aplikacji jest prostsza i bardziej przejrzysta.
- ZenMode zostal dodany, dzieki czemu możesz się teraz lepiej skupić na pisaniu, bez zbędnych przeszkadzajek.
- Implementacja struktur związanych z asynchronicznością wprowadziło szereg nowych problemów, i utrudniło pracę z kodem, co wyrównałem znaczną poprawą struktury kodu oraz znaczną próbą uproszczenia go. Niestety programowanie asynchroniczne jest tak niemiłym stworem, że pracowanie nad programem jest teraz ledwo znośne. Gdyby nie było popraw struktury, program zapewne by umarł bardzo prędko.
- **„Asynchroniczność” (lub „async”)** oznacza, że aplikacja może wykonywać wiele rzeczy jednocześnie — na przykład zapisywać plik lub liczyć znaki — bez konieczności czekania. **Od strony technicznej wygląda to tak, że aplikacja uruchamia dane zadanie (np. odczyt pliku), a następnie kontynuuje inne operacje, zamiast czekać na jego zakończenie. Gdy zadanie się zakończy, aplikacja zostaje o tym powiadomiona (np. poprzez tzw. callback, promisy lub zdarzenia) i wtedy przetwarza wynik.** Dzięki temu wszystko działa szybko i płynnie, nawet w przypadku dużych lub czasochłonnych zadań.


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

## 0.3.0-pre-alpha [24-05-2025]
- Nawigacja i przełączanie między różnymi częściami programu działają już płynnie i bez błędów.
- Poprawiłem sposób zarządzania oknami, aby uniknąć problemów przy ich otwieraniu i zamykaniu.
- Dodałem w pełni funkcjonalne, modułowe okno Ustawień, gdzie możesz dostosować różne opcje.
- Zmodernizowałem system czcionek i kolorów, aby personalizacja była łatwiejsza i bardziej elastyczna.
- Naprawiłem problemy z dopasowaniem interfejsu do różnych rozmiarów ekranu.
- Przygotowałem pasek nawigacji edytora pod przyszłe rozszerzenia funkcjonalności.


## 0.2.0-pre-pre-alpha [23-05-2025]
- Znacząco uprościłem strukturę kodu, żeby praca nad projektem była szybsza.
- Usunąłem niepotrzebne komponenty, które spowalniały działanie aplikacji.
- Rozpocząłem tworzenie podstawowej nawigacji między stronami.

## 0.1.0-pre-pre-alpha [21-05-2025]
- Dodano prostą wersję menu, która jeszcze nie działa.
- Stworzyłem pierwsze modułowe komponenty do dalszego wykorzystania.
- Ulepszyłem proces budowania aplikacji, żeby był bardziej niezawodny.
- Dodałem komentarze w kodzie, co znacznie ułatwia jego zrozumienie i przyszłą pracę nad projektem.

*Ostatnia aktualizacja: 31-05-2025*
