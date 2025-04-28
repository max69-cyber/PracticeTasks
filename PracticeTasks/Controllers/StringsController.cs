using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeTasks.Services;
using PracticeTasks.Services.Interfaces;

namespace PracticeTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        private IStringsService _stringsService;

        public StringsController(IStringsService stringsService)
        {
            _stringsService = stringsService;
        }
        
        [HttpGet("mirror")]
        public IActionResult MirrorString(string input)
        {
            try
            {
                var result = _stringsService.MirrorString(input);
                var charCount = _stringsService.GetCharacterCount(input);
                return Ok(new
                {
                    Result = result.ToString(),
                    SymbolsCount = charCount
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}