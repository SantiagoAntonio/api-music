using api_music.Validation;
using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.CDDTOS
{
    public class CDCreateDTO : CDPatchDTO
    {
        
        [ValidateFileSize(SizeMaxInMegaBytes: 4)]
        [ValidateFileType(groupTypeFile: GroupTypeFile.Image)]
        public IFormFile Poster { get; set; }
        
        public List<int> GendersIDs { get; set; }
    }
}
