using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.DTOs;

public sealed class UpdateProductoRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [MaxLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")]
    public decimal Precio { get; set; }
}
