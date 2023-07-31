namespace api_music.Services
{
    public class StorageFileLocal : IFileUploader
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StorageFileLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task deleteFile(string url, string container)
        {
            if(url != null)
            {
                var nameFile = Path.GetFileName(url);
                string pathFile = Path.Combine(env.WebRootPath, container, nameFile);
                if (File.Exists(pathFile)) { File.Delete(pathFile); }
            }
            return Task.FromResult(0);
        }

        public async Task<string> editFile(byte[] content, string extension, string container, string url, string contentType)
        {
            await deleteFile(url, container);
            return await saveFile(content, extension, container, contentType);
        }

        public async Task<string> saveFile(byte[] content, string extension, string container, string contentType)
        {
            var nameFile = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string route = Path.Combine(folder, nameFile);
            await File.WriteAllBytesAsync(route, content);
            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, container, nameFile).Replace("\\", "/");
            return urlParaBD;
        }
    }
}
