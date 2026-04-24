using Microsoft.AspNetCore.Mvc.Rendering;

public static class ViewContextExtensions {
    public static string IsActive(this ViewContext vc, string controller, string action) {
        var c = vc.RouteData.Values["controller"]?.ToString();
        var a = vc.RouteData.Values["action"]?.ToString();
        return c == controller && a == action ? "active" : "";
    }
    public static string GroupIsActive(this ViewContext vc, params string[] controllers) {
        var c = vc.RouteData.Values["controller"]?.ToString();
        return controllers.Contains(c) ? "active" : "";
    }
}