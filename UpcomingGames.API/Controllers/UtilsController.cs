using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace UpcomingGames.API.Controllers;

[ApiController]
[Route("api/v1/utils")]
public class UtilsController : ControllerBase
{
    private Color GetContrastColor(Stream photoAsStream, bool getBw)
    {
        using var image = Image.Load<Rgba32>(photoAsStream);
        var rect = new Rectangle((int)(image.Width/1.5), (int)(image.Height/1.5), 100, 100);

        // Reduce the color palette to the the dominant color without dithering.
        var quantizer = new OctreeQuantizer();
        image.Mutate( // No need to clone.
            img => img.Crop(rect) // Intial crop
                .Quantize(quantizer) // Find the dominant color, cheaper and more accurate than resizing.
                .Crop(new Rectangle(new Point(img.GetCurrentSize().Width/2, img.GetCurrentSize().Height/2), new Size(1, 1)))); // Crop again so the next command is faster

        if(getBw)
            image.Mutate(img => img.BinaryThreshold(.5F, Color.Black, Color.White)); // Threshold to High-Low color. // Threshold to High-Low color, default White/Black);

        return image[0, 0];
    }
    
    [HttpGet("color")]
    public async Task<IActionResult> GetImageColor([FromQuery] string imageUrl, [FromQuery] bool isBw)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(imageUrl);

        if (!response.IsSuccessStatusCode)
            return BadRequest();

        await using var stream = await response.Content.ReadAsStreamAsync();
        return Ok(GetContrastColor(stream, isBw).ToHex());
    }
}