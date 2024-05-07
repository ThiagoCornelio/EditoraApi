using Flunt.Notifications;

namespace ProjetoEditoraApi.Extensions;
public class ValidationExtensions
{
    public static Dictionary<string, string> GetNotifications(IReadOnlyCollection<Notification> notifications)
    {
        var result = new Dictionary<string, string>();

        foreach (var item in notifications)
            result.Add(item.Key, item.Message);

        return result;
    }
}
