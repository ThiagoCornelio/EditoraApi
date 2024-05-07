namespace ProjetoEditoraApi;
public static class Configuration
{
    public static string JwtKey = "qOg+cKS#O$JN@!P,x#$!asK*IA&A:|x$rmqM;c[&z"; /*Json Web Token (JWT)*/
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_TH_IlTevUM/z0!G3@)CV/uÇnWg==";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; } = 25;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
