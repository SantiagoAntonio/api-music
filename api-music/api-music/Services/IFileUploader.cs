namespace api_music.Services
{
    public interface IFileUploader
    {
        Task<string> saveFile(byte[] content, string extension, string container, string contentType);
        Task<string> editFile(byte[] content, string extension, string container,string url, string contentType);
        Task deleteFile(string url, string container);

    }
}
