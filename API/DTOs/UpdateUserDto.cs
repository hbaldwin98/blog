using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UpdateUserDto
    {
        [Required] public string Name { get; set; }
    }
}