using Microsoft.AspNetCore.Mvc;
using UrlShorteners.Data;
using UrlShorteners.Models;
using System;

namespace UrlShorteners.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly AppDbContext _context;

    public UrlController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("shorten")]
    public IActionResult ShortenUrl([FromBody] UrlRequest request)
    {
        if (string.IsNullOrEmpty(request?.OriginalUrl))
        {
            return BadRequest("La URL original no puede estar vacía.");
        }

        var shortCode = GenerateShortCode();
        var url = new Url
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode,
            CreatedAt = DateTime.UtcNow
        };

        _context.Urls.Add(url);
        _context.SaveChanges();

        return Ok(new { ShortUrl = $"{Request.Scheme}://{Request.Host}/u/{shortCode}" });
    }

    [HttpGet("u/{shortCode}")]
    public IActionResult RedirectToOriginalUrl(string shortCode)
    {
        var url = _context.Urls.FirstOrDefault(u => u.ShortCode == shortCode);

        if (url == null)
        {
            return NotFound("La URL acortada no existe.");
        }

        return Redirect(url.OriginalUrl);
    }

    private string GenerateShortCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

public class UrlRequest
{
    public string OriginalUrl { get; set; }
}