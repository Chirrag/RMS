using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QScores.Domain.QScoresAppsModels.ViewModels;
using QScores.Infrastructure.Context;
using System.Data;

namespace QScoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        private readonly QscoresAppsContext _qscoresAppsDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportServiceController(QscoresAppsContext qscores, UserManager<IdentityUser> userManager)
        {
            _qscoresAppsDbContext = qscores;
            _userManager = userManager;
        }

        //Get Report Sources mean Get Application Name
        [Authorize]
        [HttpGet("ReportSource")]
        public async Task<ActionResult<List<object>>> ReportSource()
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
                    return NotFound(new { message = "User not found!" });
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User ID not found!" });
                }

                var applicationView = new List<object>();
                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SpGetApplications", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    Application = reader.GetString(reader.GetOrdinal("Application")),
                                    SubApplication = reader.GetString(reader.GetOrdinal("SubApplication")),
                                    IsTVQ = reader.GetBoolean(reader.GetOrdinal("IsTVQ")),
                                    DBaseName = reader.GetString(reader.GetOrdinal("DBaseName"))
                                };
                                applicationView.Add(result);
                            }
                        }
                    }
                }

                if (applicationView.Count == 0)
                {
                    return NotFound(new { message = "No applications found for the user." });
                }

                return applicationView;
            }
            catch (SqlException sqlEx)
            {
                return BadRequest($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the general exception
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // Get Report Types of using ReporApplication(Database) Name
        [Authorize]
        [HttpGet("ReportType/{typeName}")]
        public async Task<ActionResult<List<object>>> GetReportType(string typeName)
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
                    return NotFound(new { message = "User not found!" });
                }

                var reportType = new List<object>();
                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SpGetReportTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ReportApplication", SqlDbType.NVarChar, 50) { Value = typeName });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    RecID = reader.GetInt32(reader.GetOrdinal("RecID")),
                                    ReportType = reader.GetString(reader.GetOrdinal("ReportType")),
                                    TargetName = reader.GetString(reader.GetOrdinal("TargetName"))
                                };

                                reportType.Add(result);
                            }
                        }
                    }
                }

                if (reportType.Count == 0)
                {
                    return NotFound(new { message = "No report types found for the specified application." });
                }

                return reportType;
            }
            catch (SqlException sqlEx)
            {
                return BadRequest($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the general exception
                return BadRequest($"Error: {ex.Message}");
            }
        }



        // These study dates means Study waves 
        [Authorize]
        [HttpGet("StudyDates/{typeName}")]
        public async Task<ActionResult<List<Object>>> GetStudyDates(string typeName)
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
                    return NotFound(new { message = "User not found!" });
                }

                int userId = _qscoresAppsDbContext.TblAuthentications
                    .FirstOrDefault(x => x.EmailAddress == user.Email)?.RecId ?? 0;

                if (userId == 0)
                {
                    return NotFound(new { message = "User ID not found!" });
                }

                var studyDates = new List<object>();
                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SpGetStudyDates", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@ReportApplication", SqlDbType.NVarChar, 50) { Value = typeName });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    RecID = reader.GetInt32(reader.GetOrdinal("RecID")),
                                    Studydate = reader.GetString(reader.GetOrdinal("Studydate")),
                                    Studyyear = reader.GetValue(reader.GetOrdinal("Studyyear"))?.ToString()
                                };

                                studyDates.Add(result);
                            }
                        }
                    }
                }

                if (studyDates.Count == 0)
                {
                    return NotFound(new { message = "No study dates found for the specified application." });
                }

                return studyDates;
            }
            catch (SqlException sqlEx)
            {
                return BadRequest($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the general exception
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [Authorize]
        [HttpGet("Targets/{ReportApplication}/{ReportType}/{Studywave}/{recID}")]
        public async Task<ActionResult<List<object>>> GetTargets(int recID, string ReportApplication, string ReportType, string Studywave)
        {
            if (User?.Identity?.Name == null)
            {
                return BadRequest(new { message = "User identity not found!" });
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound(new { message = "User not found!" });

            //int userID = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email).RecId;
            var authenticationRecord = _qscoresAppsDbContext.TblAuthentications.FirstOrDefault(x => x.EmailAddress == user.Email);

            if (authenticationRecord == null || authenticationRecord.EmailAddress == null)
            {
                return NotFound("User not found or email address is null.");
            }

            int userId = authenticationRecord.RecId;
            string miscell = "";
            try
            {
                var Target = new List<object>();
                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    int ReportypeInt = Convert.ToInt32(ReportType);

                    string Miscel = "select Miscellaneous from tblReports where RecID =" + recID;

                    using (var command = new SqlCommand(Miscel, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", recID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                miscell += reader["Miscellaneous"].ToString();

                            }
                        }
                    }
                    // Fetch the Ranking Parameter value 
                    string[] Ranklist = miscell.Split(',');
                    string Rankparameter = Ranklist[1];
                    string value = Rankparameter.Substring(Rankparameter.LastIndexOf("=") + 1);
                    int Rank = int.Parse(value);

                    using (var command = new SqlCommand("SpGetTargets", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int) { Value = userId });
                        command.Parameters.Add(new SqlParameter("@ReportApplication", SqlDbType.NVarChar, 50) { Value = ReportApplication });
                        command.Parameters.Add(new SqlParameter("@ReportType", SqlDbType.Int) { Value = ReportypeInt });

                        // Use the RecID values obtained earlier in the SQL query
                        //string recIdList = string.Join(",", RecIDValues);
                        command.Parameters.Add(new SqlParameter("@Studywave", SqlDbType.NVarChar, 200) { Value = Studywave });
                        command.Parameters.Add(new SqlParameter("@RankingIndicator", SqlDbType.Int) { Value = Rank });

                        // Continue with your existing code to retrieve targets
                        // ...

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var catebool = "";


                                if (reader.GetDataTypeName("CanRequestCategories") == "bit") { catebool = reader.IsDBNull(reader.GetOrdinal("CanRequestCategories")) ? false.ToString() : reader.GetBoolean(reader.GetOrdinal("CanRequestCategories")).ToString(); }
                                else
                                {
                                    catebool = reader.IsDBNull(reader.GetOrdinal("CanRequestCategories")) ? string.Empty : reader.GetString(reader.GetOrdinal("CanRequestCategories"));
                                }

                                var result = new
                                {
                                    Code = reader.IsDBNull(reader.GetOrdinal("Code")) ? 0 : reader.GetInt32(reader.GetOrdinal("Code")),
                                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                                    AlphaSort = reader.IsDBNull(reader.GetOrdinal("AlphaSort")) ? string.Empty : reader.GetString(reader.GetOrdinal("AlphaSort")),
                                    CanRequestCategories = catebool
                                };

                                Target.Add(result);
                            }
                        }
                    }
                }
                return Target;
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("ScalePoints/{ReportApplication}")]
        public async Task<ActionResult<List<Object>>> GetScalePoints(string ReportApplication)
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
                    return NotFound(new { message = "User not found!" });
                }

                var ScalePoints = new List<object>();

                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpGetScalePoints", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ReportApplication", SqlDbType.NVarChar, 50) { Value = ReportApplication });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    RecID = reader.GetInt32(reader.GetOrdinal("RecID")),
                                    ScalePoint = reader.GetString(reader.GetOrdinal("ScalePoint"))
                                };

                                ScalePoints.Add(result);
                            }
                        }
                    }
                }

                if (ScalePoints.Count == 0)
                {
                    return NotFound(new { message = "No scale points found for the specified ReportApplication." });
                }

                return ScalePoints;
            }
            catch (SqlException ex)
            {
                // Handle SQL-related exceptions
                return StatusCode(500, $"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return BadRequest($"Error: {ex.Message}");
            }
        }


        // Demographics Paritculer DemoType name
        [Authorize]
        [HttpPost("DemographicsDemoType")]
        public async Task<ActionResult<List<DemoTypeModel>>> GetDemoNames(DemoTypeModel model)
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
                    return NotFound(new { message = "User not found!" });
                }

                var DemoList = new List<object>();
                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    string Query = "SELECT DemoCode, DemoName FROM [" + model.DatabaseName + "].dbo.tblDemographics WHERE DemoType ='" + model.DemoTypeName + "'";
                    using (var democmd = new SqlCommand(Query, connection))
                    {
                        using (var reader = await democmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    DemoCode = reader.GetInt32(reader.GetOrdinal("DemoCode")),
                                    DemoName = reader.GetString(reader.GetOrdinal("DemoName"))
                                };
                                DemoList.Add(result);
                            }
                        }
                    }
                }

                return Ok(DemoList);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the general exception
                return StatusCode(500, $"An error occurred while fetching demographics. Details: {ex.Message}");
            }
        }


        // Report Type By RecId from tblReports 
        [Authorize]
        [HttpGet("ReportTypeByRecID/{recID}")]
        public async Task<ActionResult<string>> ReportByRecID(int recID)
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
                    return NotFound(new { message = "User not found!" });
                }

                string reportType = "";

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT RptType FROM tblReports WHERE RecID = @RecID";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@RecID", SqlDbType.Int) { Value = recID });

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                reportType = reader.GetString(reader.GetOrdinal("RptType"));
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(reportType))
                {
                    return NotFound(new { message = "Report type not found for the specified RecID." });
                }

                return Ok(reportType);
            }
            catch (SqlException ex)
            {
                // Handle SQL-related exceptions
                return StatusCode(500, $"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // Database Name by RecID from tblApplication  
        [Authorize]
        [HttpGet("DatabaseName/{recID}")]
        public async Task<ActionResult<string>> DatabaseName(int recID)
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
                    return NotFound(new { message = "User not found!" });
                }

                var ApplicationID = 0;
                string DBaseName = "";

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    string applicationIdQuery = "SELECT ApplicationID FROM tblReports WHERE RecID =" + recID + "";
                    using (var applicationIdCmd = new SqlCommand(applicationIdQuery, connection))
                    {
                        using (var reader = await applicationIdCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int application = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                                ApplicationID += application;
                            }
                        }
                    }

                    string databaseNameQuery = "SELECT DBaseName FROM tblApplications WHERE RecID =" + ApplicationID + "";
                    using (var databaseNameCmd = new SqlCommand(databaseNameQuery, connection))
                    {
                        using (var reader = await databaseNameCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string database = reader.GetString(reader.GetOrdinal("DBaseName"));
                                DBaseName += database;
                            }
                        }
                    }
                }

                return Ok(DBaseName);
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        // Report Type Name by Database Name and RptType 
        [Authorize]
        [HttpGet("ReportTypeName")]
        public async Task<ActionResult<string>> GetReportType(string typeName, string RptName)
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
                    return NotFound(new { message = "User not found!" });
                }

                string ReportType = "";

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT ReportType FROM [" + typeName + "].dbo.tblReportTypes WHERE RecID = @RptName";
                    using (var democmd = new SqlCommand(query, connection))
                    {
                        democmd.Parameters.AddWithValue("@RptName", RptName);

                        using (var reader = await democmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string reportName = reader.GetString(reader.GetOrdinal("ReportType"));
                                ReportType += reportName;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(ReportType))
                {
                    return NotFound(new { message = "Report type not found!" });
                }

                return Ok(ReportType);
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // Demograp hics By name 
        [Authorize]
        [HttpGet("Demographics")]
        public async Task<ActionResult<List<object>>> GetDemographics(string typeName)
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
                    return NotFound(new { message = "User not found!" });
                }

                var Demographics = new List<object>();

                using (var connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SpGetDemographics", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ReportApplication", SqlDbType.NVarChar, 50) { Value = typeName });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    DemoType = reader.GetString(reader.GetOrdinal("DemoType")),
                                    DemoName = reader.GetString(reader.GetOrdinal("DemoName")),
                                    DemoCode = reader.GetInt32(reader.GetOrdinal("DemoCode"))
                                };

                                Demographics.Add(result);
                            }
                        }
                    }
                }

                if (Demographics.Count == 0)
                {
                    return NotFound(new { message = "No demographics found for the specified report application." });
                }

                return Demographics;
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [Authorize]
        [HttpGet("DemoTypeName/{typeName}")]
        public async Task<ActionResult<List<object>>> GetDemoTypeName(string typeName)
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
                    return NotFound(new { message = "User not found!" });
                }

                var DemoList = new List<object>();

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT DISTINCT DemoType FROM [" + typeName + "].dbo.tblDemographics";
                    using (var democmd = new SqlCommand(query, connection))
                    {
                        using (var reader = await democmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    DemoType = reader.GetString(reader.GetOrdinal("DemoType"))
                                };
                                DemoList.Add(result);
                            }
                        }
                    }
                }

                if (DemoList.Count == 0)
                {
                    return NotFound(new { message = "No demographics found for the specified type." });
                }

                return Ok(DemoList);
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [Authorize]
        [HttpGet("QScoreData/{TypeName}")]
        public async Task<ActionResult<List<object>>> GetQScoreData(string TypeName, int RecID, int ReportFormat, int CategoriesAverage)
        {
            try
            {
                if (User?.Identity?.Name == null)
                {
                    return BadRequest(new { message = "User identity not found!" });
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null) return NotFound(new { message = "User not found!" });
                string ReportType = "";
                string TargetS = "";
                string Study = "";
                string Scale = "";
                string Demo = "";
                string miscell = "";
                var target = new List<object>();

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();
                    string ReportTypeQ = "SELECT RptType FROM tblReports WHERE RecID = @RecID";
                    string StudyDateQ = "SELECT Studydates FROM tblReports WHERE RecID = " + RecID;
                    string ScaleQ = "SELECT ScalePoints FROM tblReports WHERE RecID = " + RecID;
                    string TargetQ = "SELECT Targets FROM tblReports WHERE RecID = " + RecID;
                    string DemoQ = "SELECT DemoCodes FROM tblReports WHERE RecID = " + RecID;
                    string Miscel = "select Miscellaneous from tblReports where RecID =" + RecID;
                    using (var command = new SqlCommand(ReportTypeQ, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                ReportType += reader["RptType"].ToString();

                            }
                        }
                    }
                    // Target 
                    using (var command = new SqlCommand(TargetQ, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                TargetS += reader["Targets"].ToString();

                            }
                        }
                    }
                    // StudyDates
                    using (var command = new SqlCommand(StudyDateQ, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                Study += reader["Studydates"].ToString();

                            }
                        }
                    }
                    // ScalePoints 
                    using (var command = new SqlCommand(ScaleQ, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                Scale += reader["ScalePoints"].ToString();

                            }
                        }
                    }
                    // DemoCodes 
                    using (var command = new SqlCommand(DemoQ, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                Demo += reader["DemoCodes"].ToString();

                            }
                        }
                    }
                    // Miscellnenous 
                    using (var command = new SqlCommand(Miscel, connection))
                    {
                        command.Parameters.AddWithValue("@RecID", RecID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {

                                miscell += reader["Miscellaneous"].ToString();

                            }
                        }
                    }
                    // Fetch the Ranking Parameter value 
                    string[] Ranklist = miscell.Split(',');
                    string Rankparameter = Ranklist[1];
                    string value = Rankparameter.Substring(Rankparameter.LastIndexOf("=") + 1);
                    int Rank = int.Parse(value);


                    // Continue with your existing code to retrieve the targets
                    using (var command = new SqlCommand($"{TypeName}.dbo.spGetQScoresData", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@RptType", SqlDbType.NVarChar, 50) { Value = ReportType });
                        command.Parameters.Add(new SqlParameter("@Studydates", SqlDbType.NVarChar, 2000) { Value = Study });
                        command.Parameters.Add(new SqlParameter("@ScalePoints", SqlDbType.NVarChar, 2000) { Value = Scale });
                        command.Parameters.Add(new SqlParameter("@Targets", SqlDbType.NVarChar, 2000) { Value = TargetS });
                        command.Parameters.Add(new SqlParameter("@DemoCodes", SqlDbType.NVarChar, 2000) { Value = Demo });
                        command.Parameters.Add(new SqlParameter("@ReportFormat", SqlDbType.NVarChar, 200) { Value = ReportFormat });
                        command.Parameters.Add(new SqlParameter("@CategoryAverage", SqlDbType.NVarChar, 200) { Value = CategoriesAverage });
                        if (TypeName == "QSTVQLateNight" || TypeName == "QSTVQPrimeTime" || TypeName == "QSTVQSports" || TypeName == "QSTVQWeekendDaytime")
                        {
                            command.Parameters.Add(new SqlParameter("@ProgramAttribute", SqlDbType.NVarChar, 200) { Value = Rank });
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@RankingParameter", SqlDbType.NVarChar, 200) { Value = Rank });
                        }


                        var readers = command.ExecuteReader();

                        var columns = new Dictionary<string, bool>();

                        for (int i = 0; i < readers.FieldCount; i++)
                        {
                            columns[readers.GetName(i)] = true;
                        }
                        readers.Close();

                        // pagination Logic 

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            //if (reader.GetOrdinal("One of My Favorites") != null)
                            while (await reader.ReadAsync())
                            {
                                var qScoreData = new
                                {
                                    Name = columns.ContainsKey("Name") ? reader["Name"] as string : null,
                                    AlphaSort = columns.ContainsKey("AlphaSort") ? reader["AlphaSort"] as string : null,
                                    Type = columns.ContainsKey("Type") ? reader["Type"] as string : null,
                                    StudyDate = columns.ContainsKey("Studydate") ? reader["Studydate"] as string : null,
                                    StudyYear = columns.ContainsKey("StudyYear") ? reader["StudyYear"] as int? : null,
                                    ReportNo = columns.ContainsKey("ReportNo") ? reader["ReportNo"] as int? : null,
                                    Demographic = columns.ContainsKey("Demographic") ? reader["Demographic"] as string : null,
                                    OneOfMyFavorites = columns.ContainsKey("One of My Favorites") ? reader["One of My Favorites"] as string : null,
                                    VeryGood = columns.ContainsKey("Very Good") ? reader["Very Good"] as string : null,
                                    Good = columns.ContainsKey("Good") ? reader["Good"] as string : null,
                                    FairPoor = columns.ContainsKey("Fair/Poor") ? reader["Fair/Poor"] as string : null,
                                    TotalFamiliar = columns.ContainsKey("Total Familiar") ? reader["Total Familiar"] as string : null,
                                    PositiveQScore = columns.ContainsKey("Positive Q Score") ? reader["Positive Q Score"] as string : null,
                                    NegativeQScore = columns.ContainsKey("Negative Q Score") ? reader["Negative Q Score"] as string : null,
                                    DemoCode = columns.ContainsKey("DemoCode") ? reader["DemoCode"] as int? : null,
                                    CategoryList = columns.ContainsKey("CategoryList") ? reader["CategoryList"] as string : null,

                                    //CableQDatabase 
                                    Fair = columns.ContainsKey("Fair") ? reader["Fair"] as string : null,
                                    Poor = columns.ContainsKey("Poor") ? reader["poor"] as string : null,
                                    Favorite = columns.ContainsKey("Favorite") ? reader["Favorite"] as string : null,
                                    TotalOpinion = columns.ContainsKey("Total Opinion") ? reader["Total Opinion"] as string : null,
                                    TotalNetworkViewersPositiveQScore = columns.ContainsKey("Total Network Viewers Positive Q Score") ? reader["Total Network Viewers Positive Q Score"] as string : null,
                                    TotalNetworkViewersNegativeQScore = columns.ContainsKey("Total Network Viewers Negative Q Score") ? reader["Total Network Viewers Negative Q Score"] as string : null,
                                    RegularNetworkViewersPositiveQScore = columns.ContainsKey("Regular Network Viewers Positive Q Score") ? reader["Regular Network Viewers Positive Q Score"] as string : null,
                                    RegularNetworkViewersNegativeQScore = columns.ContainsKey("Regular Network Viewers Negative Q Score") ? reader["Regular Network Viewers Negative Q Score"] as string : null,
                                    OccasionalNetworkViewersPositiveQScore = columns.ContainsKey("Occasional Network Viewers Positive Q Score") ? reader["Occasional Network Viewers Positive Q Score"] as string : null,
                                    OccasionalNetworkViewersNegativeQScore = columns.ContainsKey("Occasional Network Viewers Negative Q Score") ? reader["Occasional Network Viewers Negative Q Score"] as string : null,


                                    // QSTVQLateNight QSTVQPrimeTime QSTVQSports [QSTVQWeekendDaytime]
                                    NotSeen = columns.ContainsKey("Not Seen") ? reader["Not Seen"] as string : null,
                                    AirDay = columns.ContainsKey("AirDay") ? reader["AirDay"] as string : null,
                                    AirTime = columns.ContainsKey("AirTime") ? reader["AirTIme"] as string : null,
                                    Network = columns.ContainsKey("Network") ? reader["network"] as string : null,
                                    ProgramType = columns.ContainsKey("Program Type") ? reader["Program Type"] as string : null,
                                };

                                target.Add(qScoreData);
                            }
                        }
                    }

                }
                return target.Count > 0 ? Ok(target) : NotFound(new { message = "No data found for the specified criteria." });
            }

            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [Authorize]
        [HttpGet("RankingParamter/{TypeName}")]
        public async Task<ActionResult<List<object>>> GetRankingReport(string TypeName)
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
                    return NotFound(new { message = "User not found!" });
                }

                var target = new List<object>();

                using (SqlConnection connection = new SqlConnection(_qscoresAppsDbContext.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = $"SELECT Code, Name, CanRequestCategories FROM [{TypeName}].dbo.tblRankings";

                    using (var democmd = new SqlCommand(query, connection))
                    {
                        using (var reader = await democmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var result = new
                                {
                                    Code = reader.GetInt32(reader.GetOrdinal("Code")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    CanRequestCategories = reader.GetBoolean(reader.GetOrdinal("CanRequestCategories"))
                                };
                                target.Add(result);
                            }
                        }
                    }
                }

                return Ok(target);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
