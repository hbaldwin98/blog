using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateCommentDto
    {
        [Required] public string CommenterName { get; set; }
        [Required] public string Contents { get; set; }
        public DateTime DateCommented { get; set; }

        
    }
}