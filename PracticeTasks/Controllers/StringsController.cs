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
        public async Task<IActionResult> MirrorString(string input, string sortAlgorithm)
        {
            try
            {
                var result = _stringsService.MirrorString(input);
                var charCount = _stringsService.GetCharacterCount(input);
                var longestVowelSubstring = _stringsService.GetLongestVowelSubstring(result);
                var sortedResult = _stringsService.SortString(result, sortAlgorithm);
                var randomResult = await _stringsService.GetStringWithRemovedSymbol(result);
                
                return Ok(new
                {
                    Result = result,
                    SymbolsCount = charCount,
                    VowelsSubstring = longestVowelSubstring,
                    SortedResult = sortedResult,
                    RandomResult = randomResult
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}