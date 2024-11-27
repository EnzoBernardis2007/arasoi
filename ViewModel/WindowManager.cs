using System.Linq;
using System.Windows;
using System.Windows.Controls;

public static class WindowManager
{
    public static void ShowOrFocusWindow<T>() where T : Window, new()
    {
        // Check if the window is already open
        var existingWindow = Application.Current.Windows.OfType<T>().FirstOrDefault();

        if (existingWindow != null)
        {
            // Focus on the existing window
            existingWindow.Activate();
        }
        else
        {
            // Create a new window and show
            T newWindow = new T();
            newWindow.Show();
        }
    }

    public static T GetUIElementWithName<T>(Grid grid, string name) where T : UIElement
    {
        return grid.Children
            .OfType<T>()
            .FirstOrDefault(x => x is FrameworkElement frameworkElement && frameworkElement.Name == name);
    }
}
