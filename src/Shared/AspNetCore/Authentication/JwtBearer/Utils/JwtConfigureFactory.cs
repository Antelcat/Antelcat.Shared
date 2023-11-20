namespace Antelcat.Utils;

public class JwtConfigureFactory
{
    internal readonly Dictionary<string, JwtConfigure> Configs = new();
    internal JwtConfigureFactory() { }
}