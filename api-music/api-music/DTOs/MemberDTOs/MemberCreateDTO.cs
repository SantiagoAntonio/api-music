using api_music.Validation;
using System.ComponentModel.DataAnnotations;

namespace api_music.DTOs.MemberDTOs
{
    public class MemberCreateDTO :MemberPatchDTO
    {
        [ValidateFileSize(SizeMaxInMegaBytes:4)]
        [ValidateFileType(groupTypeFile:GroupTypeFile.Image)]
        public IFormFile Avatar { get; set; }
    }
}
