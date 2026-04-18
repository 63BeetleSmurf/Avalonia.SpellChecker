using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.SpellChecker.Demo;

public partial class MainWindow : Window
{
    private readonly TextBoxSpellChecker textBoxSpellChecker;

    public MainWindow()
    {
        InitializeComponent();

        cbGlobalSpellCheckingEnable.IsCheckedChanged += cbGlobalSpellCheckingEnable_IsCheckedChanged;
        cbUserNameSpellCheckingEnable.IsCheckedChanged += cbUserNameSpellCheckingEnable_IsCheckedChanged;
        cbNotesSpellCheckingEnable.IsCheckedChanged += cbNotesSpellCheckingEnable_IsCheckedChanged;

        tbNotes.UpdateLayout();
        //    TbNotes.Text

        var textBox = this.FindControl<TextBox>("tbUserName");


        textBoxSpellChecker = new TextBoxSpellChecker(SpellCheckerConfig.Create(/*"pt_BR", */"en_GB"));
        textBoxSpellChecker.Initialize(textBox);
        textBoxSpellChecker.Initialize(this.FindControl<TextBox>("tbNotes"));

    }

    public void CustomAction_Click(object? sender, RoutedEventArgs args)
    {

    }

    private void cbGlobalSpellCheckingEnable_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
        {
            return;
        }

        textBoxSpellChecker.IsEnabled = (checkBox.IsChecked == true);
    }

    private void cbUserNameSpellCheckingEnable_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
        {
            return;
        }

        if (checkBox.IsChecked == true)
        {
            textBoxSpellChecker.Enable(tbUserName);
        }
        else
        {
            textBoxSpellChecker.Disable(tbUserName);
        }
    }

    private void cbNotesSpellCheckingEnable_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
        {
            return;
        }

        if (checkBox.IsChecked == true)
        {
            textBoxSpellChecker.Enable(tbNotes);
        }
        else
        {
            textBoxSpellChecker.Disable(tbNotes);
        }
    }

}