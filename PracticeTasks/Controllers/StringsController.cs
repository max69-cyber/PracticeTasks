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
        
        [HttpGet("mirror")] //HTTP GET
        public async Task<IActionResult> MirrorString(string input, string sortAlgorithm)
        {
            try
            {
                var result = _stringsService.MirrorString(input);
                var charCount = _stringsService.GetCharacterCount(result);
                var longestVowelSubstring = _stringsService.GetLongestVowelSubstring(result);
                var sortedResult = _stringsService.SortString(result, sortAlgorithm);
                var randomResult = await _stringsService.GetStringWithRemovedSymbol(result);
                
                return Ok(new // JSON
                {
                    Result = result, //Обработанная строка
                    SymbolsCount = charCount, //Информация о том, сколько раз входил в обработанную строку каждый символ
                    VowelsSubstring = longestVowelSubstring, //Самая длинная подстрока начинающаяся и заканчивающаяся на гласную
                    SortedResult = sortedResult, //Отсортированная обработанная строка
                    RandomResult = randomResult //«Урезанная» обработанная строка – обработанная строка без одного символа
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  //HTTP ошибка 400 Bad Request
            }
            
        }
    }
}