using System.ComponentModel.DataAnnotations;

namespace Horeca.Blob
{
    public class SaveBlobInputDto
    {
        public byte[] Content { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
