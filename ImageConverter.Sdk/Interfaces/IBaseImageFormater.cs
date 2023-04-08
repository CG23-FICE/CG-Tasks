namespace ImageConverter.Sdk.Interfaces
{
    public interface IBaseImageFormater
    {
        string Title { get; }
        string Description { get; }
        /// <summary>
        /// Write name in ".ext" format
        /// </summary>
        string FormatName { get; }
    }
}
