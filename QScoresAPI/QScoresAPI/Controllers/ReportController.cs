using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QScores.Domain.QScoresAppsModels.ViewModels;
using QScores.Infrastructure.Context;
using System.ComponentModel;
using System.Data;

namespace QScoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly QscoresAppsContext _qscoresAppsDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportController(QscoresAppsContext qscoresAppsDbContext, UserManager<IdentityUser> userManager)
        {
            _qscoresAppsDbContext = qscoresAppsDbContext;
            _userManager = userManager;
        }

        [Route("GetReports")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewReport>>> GetReports()
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                int userId = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User not found in the authentication table." });
                }

                List<ViewReport> viewReports = new List<ViewReport>();

                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpGetReports", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new ViewReport
                                {
                                    RecID = reader.GetInt32(reader.GetOrdinal("RecID")),
                                    ReportName = reader.GetString(reader.GetOrdinal("ReportName")),
                                    Datetime = reader.GetDateTime(reader.GetOrdinal("Datetime"))
                                };
                                viewReports.Add(result);
                            }
                        }
                    }
                }

                return viewReports;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [Route("Delete/Report/{recID}")]
        [HttpDelete]
        public IActionResult DeleteReport(int recID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SpDeleteReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set the stored procedure parameter
                        command.Parameters.AddWithValue("@RecID", recID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { message = "Record with RecID {recID} deleted successfully." });
                        }
                        else
                        {
                            return NotFound($"Record with RecID {recID} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return BadRequest("An error occurred: " + ex.Message);
            }
        }


        [Route("GetZipFiles")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZipView>>> GetZipFiles()
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User not found in the authentication table." });
                }

                List<ZipView> data = new List<ZipView>();

                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpGetZipFiles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new ZipView
                                {
                                    ZipFile = reader.GetString(reader.GetOrdinal("ZipFile"))
                                };
                                data.Add(result);
                            }
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [Route("GetZipFilesData/{fileName}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ZipView>>> GetZipData(string fileName)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();
                var authenticationRecord = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email);

                if (authenticationRecord == null || authenticationRecord.EmailAddress == null)
                {
                    return NotFound("User not found or email address is null.");
                }
                int userId = authenticationRecord.RecId;

                List<ZipView> data = new List<ZipView>();
                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SpGetZipFileData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar, 2000) { Value = fileName });


                        string filePath = Path.Combine(@"D:\ProjectQScores\qscores-net-core\QScoresAPI\QScores.Infrastructure\ZipFolder", fileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            string fileExtension = Path.GetExtension(fileName);
                            string contentType = GetContentType(fileExtension);
                            return PhysicalFile(filePath, contentType, fileName);
                        }
                        else
                        {
                            return NotFound("ZIP file not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        // Content Type 
        private string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".zip":
                    return "application/zip";
                case ".xls":
                    return "application/vnd.ms-excel";
                default:
                    return "application/octet-stream"; // Default content type for other file types
            }
        }


        [Authorize]
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> InsertReport([FromBody] CreateReport model)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User not found in the authentication table." });
                }

                if (model.application == null)
                {
                    return BadRequest(new { message = "Database not found!" });
                }
                if (model.rptType == null) { return BadRequest(new { message = " rptType not found!" }); }
                if (model.ranking == null) { return BadRequest(new { message = " ranking not found!" }); }
                string miscellaneousParams = await MiscellenousParms(model.application, model.rptType, model.ranking);

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    int reportId = 0;
                    var queryOne = "SELECT RecID FROM [" + model.application + "].dbo.tblReportTypes WHERE ReportType = @ReportType";
                    using (var cmdReportType = new SqlCommand(queryOne, connection))
                    {
                        cmdReportType.Parameters.Add(new SqlParameter("@ReportType", SqlDbType.NVarChar, 50) { Value = model.rptType });
                        object result = cmdReportType.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            reportId = Convert.ToInt32(result);
                        }
                    }

                    using (SqlCommand command = new SqlCommand("SpSaveReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@ReportName", model.reportName);
                        command.Parameters.AddWithValue("@RptType", reportId);
                        command.Parameters.AddWithValue("@Studydates", model.studydates);
                        command.Parameters.AddWithValue("@ScalePoints", model.scalePoints);
                        command.Parameters.AddWithValue("@Targets", model.targets);
                        command.Parameters.AddWithValue("@DemoCodes", model.demoCodes);
                        command.Parameters.AddWithValue("@Miscellaneous", miscellaneousParams);
                        command.Parameters.AddWithValue("@Application", model.application);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    int recId = (int)reader.GetDecimal(reader.GetOrdinal("RecID"));
                                    if(recId > 0) 
                                    {
                                        return Ok(recId);
                                    }
                                    
                                }
                            }
                        }

                        return NotFound();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [Route("GetReportDataID")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewReportDataId>>> GetReportDatawithId(int RecId)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User not found in the authentication table." });
                }

                List<ViewReportDataId> data = new List<ViewReportDataId>();

                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpGetReportData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@RecID", SqlDbType.Int) { Value = RecId });
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new ViewReportDataId
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    ReportName = reader.GetString(reader.GetOrdinal("ReportName")),
                                    RptType = reader.GetString(reader.GetOrdinal("RptType")),
                                    Studydates = reader.GetString(reader.GetOrdinal("Studydates")),
                                    ScalePoints = reader.GetString(reader.GetOrdinal("ScalePoints")),
                                    Targets = reader.GetString(reader.GetOrdinal("Targets")),
                                    DemoCodes = reader.GetString(reader.GetOrdinal("DemoCodes")),
                                    Miscellaneous = reader.GetString(reader.GetOrdinal("Miscellaneous")),
                                    Application = reader.GetString(reader.GetOrdinal("Application")),
                                    Datetime = reader.GetDateTime(reader.GetOrdinal("Datetime")),
                                    DBaseName = reader.GetString(reader.GetOrdinal("DBaseName"))
                                };
                                data.Add(result);
                            }
                        }
                    }
                }

                return data;
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }



        [Authorize]
        [HttpPut]
        [Route("Update/reports/{recID}")]
        public async Task<IActionResult> UpdateReport(int recID, CreateReport model)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User not found in the authentication table." });
                }

                // Validate the input model
                if (model == null)
                {
                    return BadRequest("Invalid input model.");
                }

                var studywaves = new List<string>();
                var scalepoints = new List<string>();
                var Target = new List<object>();
                var Demo = new List<string>();
                var TargetCat = new List<int>();

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    int reportdId = 0;
                    await connection.OpenAsync();

                    // Report Type Convet String to integer 
                    var QueryOne = "SELECT RecID FROM [" + model.application + "].dbo.tblReportTypes WHERE ReportType = @ReportType";
                    using (var cmdReportType = new SqlCommand(QueryOne, connection))
                    {
                        cmdReportType.Parameters.Add(new SqlParameter("@ReportType", SqlDbType.NVarChar, 50) { Value = model.rptType });
                        object result =  cmdReportType.ExecuteScalar(); // Use asynchronous execute

                        if (result != null && result != DBNull.Value)
                        {
                            reportdId = Convert.ToInt32(result);
                        }
                    }
                    if (model.studydates == null)
                    {
                        return BadRequest(new { message = "Study dates  not found!" });
                    }
                    // StudyDates convert string to integer 
                    string[] studywaveValues = model.studydates.Split(',');
                    studywaves.AddRange(studywaveValues);

                    // Scale Points Fetch using String 
                    if(model.scalePoints == null)
                    {
                        return BadRequest(new { message = "Scale point not found!" });
                    }
                    string[] scalepointsValue = model.scalePoints.Split(',');
                    scalepoints.AddRange(scalepointsValue);
                    var RecIdScale = new List<int>();
                    string CombineScale = string.Join(",", scalepointsValue.Select(scal => $"'{scal.Trim()}'"));
                    var QueryThree = "SELECT RecID FROM [" + model.application + "].dbo.tblScalePoints WHERE ScalePoint IN (" + CombineScale + ")";

                    using (var cmda = new SqlCommand(QueryThree, connection))
                    {
                        // No need for the @StudyDate parameter here
                        SqlDataReader reader = cmda.ExecuteReader();

                        while (reader.Read())
                        {
                            int scalId = reader.GetInt32(0);
                            RecIdScale.Add(scalId);
                        }
                        reader.Close();
                    }

                    //ENd of the Query Three
                    // Demogprahics 
                    if(model.demoCodes == null)
                    {
                        return BadRequest(new { message = "DemoCodes  not found!" });
                    }
                    string[] DemoPointsValue = model.demoCodes.Split(',');
                    Demo.AddRange(DemoPointsValue);

                    // Using Target StoredProcedure and codes 
                    string recIdScaleCsv = string.Join(",", RecIdScale);
                    string DemoCSV = string.Join(",", Demo);
                    string Studywavers = string.Join(",", studywaves);

                    string TargetCSV = string.Join(",", TargetCat);

                    using (SqlCommand command = new SqlCommand("SpSaveReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set the stored procedure parameters
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@ReportName", model.reportName);
                        command.Parameters.AddWithValue("@RptType", model.rptType);
                        command.Parameters.AddWithValue("@Studydates", Studywavers);
                        command.Parameters.AddWithValue("@ScalePoints", recIdScaleCsv);
                        command.Parameters.AddWithValue("@Targets", model.targets);
                        command.Parameters.AddWithValue("@DemoCodes", DemoCSV);
                        command.Parameters.AddWithValue("@Miscellaneous", model.miscellaneous);
                        command.Parameters.AddWithValue("@Application", model.application);
                        command.Parameters.AddWithValue("@RecID", recID); // Pass the RecID for update

                        // Execute the query
                        await command.ExecuteNonQueryAsync();

                        return Ok(new { message = "Record with RecID {recID} Updated successfully." });
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // Get Miscellenous 
        private async Task<string> MiscellenousParms(string TypeName, string ReportType, string Ranking)
        {
            try
            {
                string IsTV = "";
                string Tar = "";
                var RankingParameter = Ranking;
                var RankingLabel = Ranking == "-1" ? "" : Ranking;
                bool CanSelectCategories = true;

                // TargetName
                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    // Query to get TargetName
                    string targetQuery = $"SELECT TargetName FROM [{TypeName}].dbo.tblReportTypes WHERE ReportType = @ReportType";
                    using (var targetCmd = new SqlCommand(targetQuery, connection))
                    {
                        targetCmd.Parameters.AddWithValue("@ReportType", ReportType);

                        using (var targetReader = await targetCmd.ExecuteReaderAsync())
                        {
                            while (await targetReader.ReadAsync())
                            {
                                Tar = targetReader.GetString(targetReader.GetOrdinal("TargetName"));
                            }
                        }
                    }

                    // Query to get IsTVQ
                    string isTVQQuery = "SELECT IsTVQ FROM tblApplications WHERE DBaseName = @TypeName";
                    using (var isTVQCmd = new SqlCommand(isTVQQuery, connection))
                    {
                        isTVQCmd.Parameters.AddWithValue("@TypeName", TypeName);

                        using (var isTVQReader = await isTVQCmd.ExecuteReaderAsync())
                        {
                            while (await isTVQReader.ReadAsync())
                            {
                                bool isTVQ = isTVQReader.GetBoolean(isTVQReader.GetOrdinal("IsTVQ"));
                                IsTV = isTVQ.ToString(); // Convert boolean to string
                            }
                        }
                    }
                }

                string miss = $"Target={Tar},RankingParameter={RankingParameter},RankingLabel={RankingLabel},IsTVQReport={IsTV},CanSelectCategories={CanSelectCategories}";
                return miss;
            }
            catch (Exception ex)
            {

                return $"Error in MiscellenousParms: {ex.Message}";
            }
        }

    }
}

