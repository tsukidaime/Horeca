using System.ComponentModel.DataAnnotations;

namespace Horeca.Blob
{
    public class GetBlobRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
