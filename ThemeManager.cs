using System.Windows.Media;

namespace ValheimServerManager;

public static class ThemeManager
{
    public static void ApplyTheme(string? theme, string? accentColor, string? language = "English")
    {
        var resources = System.Windows.Application.Current.Resources;
        var isLight = string.Equals(theme, "Light", StringComparison.OrdinalIgnoreCase);

        SetColor(resources, "WindowBackgroundBrush", isLight ? "#F2F5F8" : "#0D1820");
        SetColor(resources, "SidebarBackgroundBrush", isLight ? "#E7EDF2" : "#0B1320");
        SetColor(resources, "CardBackgroundBrush", isLight ? "#FFFFFF" : "#162B3B");
        SetColor(resources, "BorderBrush", isLight ? "#C5D0D9" : "#25415A");
        SetColor(resources, "PrimaryTextBrush", isLight ? "#17212B" : "#EFF7FF");
        SetColor(resources, "SecondaryTextBrush", isLight ? "#405363" : "#B7C9D7");
        SetColor(resources, "MutedTextBrush", isLight ? "#657887" : "#7C8FA1");
        SetColor(resources, "AccentBrush", GetAccent(accentColor));
        SetColor(resources, "AccentDarkBrush", GetAccentDark(accentColor));
        SetColor(resources, "ConfigInputBackgroundBrush", isLight ? "#FFFFFF" : "#122331");
        SetColor(resources, "HeaderBackgroundBrush", isLight ? "#DCE5EC" : "#0F1D29");
        SetColor(resources, "HeaderPrimaryTextBrush", isLight ? "#17212B" : "#E9F4FF");
        SetColor(resources, "HeaderSecondaryTextBrush", isLight ? "#263746" : "#E6EEF6");
        SetColor(resources, "HoverBackgroundBrush", isLight ? "#D2DEE7" : "#102534");
        SetColor(resources, "SecondaryButtonBackgroundBrush", isLight ? "#E8EEF2" : "#1A2E3F");
        SetColor(resources, "AccentButtonBackgroundBrush", isLight ? "#367FBD" : "#1F73B9");

        SetText(resources, "DashboardText", IsRussian(language) ? "Панель управления" : "Dashboard");
        SetText(resources, "ConfigurationText", IsRussian(language) ? "Конфигурация" : "Configuration");
        SetText(resources, "ServerConnectedText", IsRussian(language) ? "Сервер подключён" : "Server connected");
        SetText(resources, "SaveConfigurationText", IsRussian(language) ? "Сохранить конфигурацию" : "Save Configuration");
        SetText(resources, "ConfigurationTitleText", IsRussian(language) ? "Конфигурация" : "Configuration");
        SetText(resources, "ConfigurationSubtitleText", IsRussian(language) ? "Настройки приложения, сервера и дополнительные параметры" : "Application, server and advanced settings");
    }

    private static bool IsRussian(string? language) => string.Equals(language, "Русский", StringComparison.OrdinalIgnoreCase);

    private static void SetText(System.Windows.ResourceDictionary resources, string key, string value)
    {
        resources[key] = value;
    }

    private static string GetAccent(string? value) => value switch
    {
        "Green" => "#42C77A",
        "Amber" => "#E6A23C",
        _ => "#4AA3FF"
    };

    private static string GetAccentDark(string? value) => value switch
    {
        "Green" => "#27945A",
        "Amber" => "#B97816",
        _ => "#2E7ACB"
    };

    private static void SetColor(System.Windows.ResourceDictionary resources, string key, string value)
    {
        var color = (global::System.Windows.Media.Color)global::System.Windows.Media.ColorConverter.ConvertFromString(value)!;
        resources[key] = new global::System.Windows.Media.SolidColorBrush(color);
    }
}
