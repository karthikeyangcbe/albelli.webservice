namespace albelli.configuration.library
{
    /// <summary>
    /// Defines all the properties related to reading all the configuration.
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}