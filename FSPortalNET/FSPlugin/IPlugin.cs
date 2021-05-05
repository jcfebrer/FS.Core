namespace FSPlugin
{
    /// <summary>
    ///     A public interface to be used by all custom plugins
    /// </summary>
    public interface IPlugin
    {
        string Name { get; }
        int Parameters { get; }
        string Execute(params string[] p);
    }
}