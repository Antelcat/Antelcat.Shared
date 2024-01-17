namespace Antelcat.Server.Configs;

public class AntelcatFilterConfig
{
    public bool OutputTrace { get; set; }

    internal static readonly AntelcatFilterConfig Default = new();
}