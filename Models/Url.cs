namespace UrlShorteners.Models;

public class Url
{
    public int Id { get; set; } // Identificador único
    public string OriginalUrl { get; set; } = string.Empty; // URL original
    public string ShortCode { get; set; } = string.Empty; // Código corto generado
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Fecha de creación
}