# Avalonia Spell Checker

[![NuGet](https://img.shields.io/nuget/v/GHSoftware.Avalonia.SpellChecker.svg)](https://www.nuget.org/packages/GHSoftware.Avalonia.SpellChecker)
[![NuGet Avalonia 11](https://img.shields.io/nuget/v/GHSoftware.Avalonia.SpellChecker.Avalonia11.svg)](https://www.nuget.org/packages/GHSoftware.Avalonia.SpellChecker.Avalonia11)

Avalonia Spell Checker adds Hunspell-based spell checking to Avalonia `TextBox` controls. It highlights misspelled words while the user types and adds spelling suggestions to the existing context menu without requiring a custom text input control.

<p float="left">

![Spell checker demonstration](avalonia-spell-checker-demo3.gif)
![Ignore word demonstration](avalonia-spell-checker-ignore-demo.gif)
![macOS screenshot](demo-screenshot-mac.png)

</p>

## Features

- Real-time spell checking for Avalonia `TextBox`.
- Hunspell `.aff` and `.dic` dictionary support through `WeCantSpell.Hunspell`.
- Multiple enabled languages at the same time.
- Context menu suggestions for misspelled words.
- Custom ignored words.
- Automatic style registration when `TextBoxSpellChecker.Initialize` is called.
- Included dictionaries for `en_GB`, `es_MX`, and `pt_BR`.

## NuGet Packages

Use the package that matches your Avalonia major version:

| Avalonia version | Package | Target frameworks |
| --- | --- | --- |
| Avalonia 12.x | [`GHSoftware.Avalonia.SpellChecker`](https://www.nuget.org/packages/GHSoftware.Avalonia.SpellChecker) | `net8.0`, `net10.0` |
| Avalonia 11.1+ | [`GHSoftware.Avalonia.SpellChecker.Avalonia11`](https://www.nuget.org/packages/GHSoftware.Avalonia.SpellChecker.Avalonia11) | `net6.0`, `net8.0`, `net10.0` |

Install the current Avalonia package:

```bash
dotnet add package GHSoftware.Avalonia.SpellChecker
```

Install the Avalonia 11 package:

```bash
dotnet add package GHSoftware.Avalonia.SpellChecker.Avalonia11
```

The two packages expose the same `Avalonia.SpellChecker` namespace and API. The separate package IDs keep NuGet dependency resolution explicit and avoid mixing Avalonia 11 and Avalonia 12 assets in the same package.

## Usage

Create a `TextBoxSpellChecker` and initialize each `TextBox` that should be checked:

```csharp
using Avalonia.Controls;
using Avalonia.SpellChecker;

public partial class MainWindow : Window
{
    private readonly TextBoxSpellChecker _spellChecker;

    public MainWindow()
    {
        InitializeComponent();

        _spellChecker = new TextBoxSpellChecker(
            SpellCheckerConfig.Create("pt_BR", "en_GB"));

        var textBox = this.FindControl<TextBox>("DescriptionTextBox");
        if (textBox is not null)
        {
            _spellChecker.Initialize(textBox);
        }
    }
}
```

By default, `SpellCheckerConfig.Create(...)` looks for dictionaries in:

```text
<application output directory>/Dictionaries
```

The NuGet packages include the bundled dictionaries as content files and copy them to the output directory automatically.

After `Initialize` is called, the `TextBoxSpellChecker` starts checking the target `TextBox` as soon as its template is applied. Misspelled words are underlined, and suggestions are added to the context menu when the user right-clicks a misspelled word.

When the `TextBox` is detached from the logical tree, the spell checker removes its event handlers and clears its reference to that control.

## Dictionaries

Hunspell dictionaries use a pair of files per language:

```text
<language>.aff
<language>.dic
```

For example:

```text
Dictionaries/pt_BR.aff
Dictionaries/pt_BR.dic
```

The bundled dictionaries are enough to start using the library. To add or replace dictionaries, use Hunspell-compatible files from maintained dictionary projects such as:

- [LibreOffice dictionaries](https://github.com/LibreOffice/dictionaries)
- [Mozilla dictionaries](https://addons.mozilla.org/firefox/language-tools/)
- [Apache OpenOffice dictionaries](https://extensions.openoffice.org/)

Then configure the folder explicitly if needed:

```csharp
var config = SpellCheckerConfig.Create("en_GB");
config.DictionariesFolder = @"C:\path\to\Dictionaries";
```

## Building Packages

The repository builds two NuGet packages: one for Avalonia 12 and one for Avalonia 11.

```powershell
.\scripts\pack-all.ps1 -Configuration Release -Clean
```

To override the package version:

```powershell
.\scripts\pack-all.ps1 -Configuration Release -Version 0.3.1 -Clean
```

Packages are written to:

```text
artifacts/nuget
```

## Current Limitations

- Keyboard-triggered context menus do not currently show spelling suggestions because suggestions are resolved from a pointer position.
- The included dictionaries are limited to `en_GB`, `es_MX`, and `pt_BR`.

## Contributing

Issues and pull requests are welcome.

## License

This project is licensed under the MIT License.
