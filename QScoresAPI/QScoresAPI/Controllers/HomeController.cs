using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QScores.Domain.QScoresWebDbModels.ViewModels;
using QScores.Infrastructure.Context;
using System.Data;

namespace QScoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly QscoresWebDbContext _context;

        public HomeController(QscoresWebDbContext context)
        {
            _context = context;
        }


        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<SearchViewModel>>> GetSearchResults([FromBody] SearchRequestModal requestModel)
        {
            try
            {
                if (requestModel == null || string.IsNullOrEmpty(requestModel.SearchData))
                {
                    return BadRequest("Invalid request model or empty search data.");
                }
                string decryptedData = DecryptSearch(requestModel.SearchData);
                List<SearchViewModel> searchResults = new List<SearchViewModel>();

                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpCustomX001", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Searchstring", SqlDbType.NVarChar) { Value = decryptedData });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new SearchViewModel
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    StudyType = reader.GetString(reader.GetOrdinal("StudyType")),
                                    Rank = reader.GetInt32(reader.GetOrdinal("Rank"))
                                };

                                searchResults.Add(result);
                            }
                        }
                    }
                }

                if (searchResults.Count == 0)
                {
                    return NotFound("No results found for the provided search criteria.");
                }

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        private string DecryptSearch(string encryptedData)
        {
            return Uri.UnescapeDataString(
                new string(encryptedData.ToCharArray().Select(c => (char)(c - 1)).ToArray())
            );
        }


        // Search Detail Controller
        [HttpGet("SearchDetail")]
        public async Task<ActionResult<IEnumerable<SearchDetailView>>> GetSearchDetail(string SearchName, string StudyType)
        {
            List<SearchDetailView> searchDetails = new List<SearchDetailView>();
            string decryptedSearchName = DecryptSearch(SearchName);
            string decryptedStudyType = DecryptSearch(StudyType);
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SpCustomX001Detail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Searchstring", SqlDbType.NVarChar, 2000) { Value = decryptedSearchName });
                    command.Parameters.Add(new SqlParameter("@Study", SqlDbType.NVarChar, 2000) { Value = decryptedStudyType });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var result = new SearchDetailView
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Code = reader.GetInt32(reader.GetOrdinal("Code")),
                                Study = reader.GetString(reader.GetOrdinal("Study")),
                                StudyType = reader.GetString(reader.GetOrdinal("StudyType")),
                                Rank = reader.GetInt32(reader.GetOrdinal("Rank"))
                            };
                            searchDetails.Add(result);
                        }
                    }
                }
            }
            return searchDetails;
        }

    }
}
