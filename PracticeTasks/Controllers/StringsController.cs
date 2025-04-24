using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        [HttpGet("mirror")]
        public IActionResult StringMirror(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest("String can't be empty");
            }
            
            var errorSymbols = input
                .Where(c => c < 'a' || c > 'z')
                .ToList();
            
            if (errorSymbols.Any())
            {
                var errorString = new StringBuilder("В строке присутсвуют невалидные символы: ");
                foreach (var c in errorSymbols)
                {
                    errorString.Append($"{c}, ");
                }
                errorString.Remove(errorString.Length - 2, 2);
                errorString.Append(".");
                return BadRequest(errorString.ToString());
            }
            
            var result = new StringBuilder();
            int length = input.Length;
            
            if (length % 2 == 0)
            {
                for (int i = length / 2 - 1; i >= 0; i--)
                {
                    result.Append(input[i]);
                }
                for (int i = length - 1; i >= length / 2; i--)
                {
                    result.Append(input[i]);
                }
            }
            else
            {
                for (int i = length - 1; i >= 0; i--)
                {
                                
                    result.Append(input[i]);
                }
                result.Append(input);
            }
            
            return Ok(result.ToString());
        }
    }
}