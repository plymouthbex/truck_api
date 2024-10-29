using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;

namespace BcloudAPI.Helper
{
    public class SqlErrorHandling
    {
        public string HandleSqlException(Exception sqlException)
        {
            // Extract the error message
            var errorMessage = sqlException.Message;

            // Define the regex pattern to extract the constraint name
            string pattern = @"UNIQUE KEY constraint '([^']*)'";

            // Match the pattern against the error message
            Match match = Regex.Match(errorMessage, pattern);

            // Check if a match is found and extract the constraint name
            string constraintName = match.Success ? match.Groups[1].Value : "Constraint name not found.";
            string firstChar = "";
            string lastChar = "";

            if (constraintName != "Constraint name not found." && constraintName.Length > 0)
            {
                // Extract the first and last characters of the constraint name
                firstChar = constraintName[0].ToString();
                lastChar = constraintName[constraintName.Length - 1].ToString();
            }
            if( !string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
            {
                if (firstChar =="3" &&  lastChar =="3")
                {
                    var Coderesponse = new
                    {
                        Status = "U",
                        Message = "This code already exists."
                    };
                    var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                    // Return the JSON response
                    return jsonCodeResponse;
                }
            }
            if (!string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
            {
                if (firstChar == "3" && lastChar == "3")
                {
                    var Coderesponse = new
                    {
                        Status = "U",
                        Message = "This code already exists."
                    };
                    var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                    // Return the JSON response
                    return jsonCodeResponse;
                }
            }
            if (!string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
            {
                if (firstChar == "3" && lastChar == "6")
                {
                    var Coderesponse = new
                    {
                        Status = "U",
                        Message = "This Email already exists."
                    };
                    var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                    // Return the JSON response
                    return jsonCodeResponse;
                }
            }
            //FOR  FOREIGN KEY
            pattern = @"FOREIGN KEY constraint ""([^""]*)""";

            // Perform the match
            Match fOREIGNmatch = Regex.Match(errorMessage, pattern, RegexOptions.IgnoreCase);

            // Extract the constraint name
            constraintName = fOREIGNmatch.Success ? fOREIGNmatch.Groups[1].Value : "Constraint name not found.";
            if (constraintName != "Constraint name not found." && constraintName.Length > 0)
            {
                // Extract the first and last characters of the constraint name
                firstChar = constraintName[0].ToString();
                lastChar = constraintName[constraintName.Length - 1].ToString();
                if (!string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
                {
                    if (firstChar == "4" && lastChar == "5")
                    {
                        var Coderesponse = new
                        {
                            Status = "N",
                            Message = "Action failed due to conflicting data. Please check and update the related data, then try again."
                        };
                        var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                        // Return the JSON response
                        return jsonCodeResponse;
                    }
                }
            }



            // Define the regex pattern to match the constraint name
            pattern = @"REFERENCE constraint ""([^""]*)""";
            Match Deletematch = Regex.Match(errorMessage, pattern, RegexOptions.IgnoreCase);

            // Extract the constraint name from the error message
            constraintName = Deletematch.Success ? Deletematch.Groups[1].Value : "Constraint name not found.";
            if (constraintName != "Constraint name not found." && constraintName.Length > 0)
            {
                // Extract the first and last characters of the constraint name
                firstChar = constraintName[0].ToString();
                lastChar = constraintName[constraintName.Length - 1].ToString();
                if (!string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
                {
                    if (firstChar == "4" && lastChar == "5")
                    {
                        var Coderesponse = new
                        {
                            Status = "U",
                            Message = "Cannot delete the record because it is referenced by one or more child records. Please delete or update the dependent records first."
                        };
                        var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                        // Return the JSON response
                        return jsonCodeResponse;
                    }
                }
            }
            //FOR CHECK CONSTRAIN
            pattern = @"CHECK constraint ""([^""]*)""";
            Match MatchCheckConstrain = Regex.Match(errorMessage, pattern, RegexOptions.IgnoreCase);
            constraintName = MatchCheckConstrain.Success ? MatchCheckConstrain.Groups[1].Value : "Constraint name not found.";
            
            if (constraintName != "Constraint name not found." && constraintName.Length > 0)
            {
                // Extract the first and last characters of the constraint name
                firstChar = constraintName[0].ToString();
                lastChar = constraintName[constraintName.Length - 1].ToString();
            }
            if (!string.IsNullOrEmpty(firstChar) && !string.IsNullOrEmpty(lastChar))
            {
                if (firstChar == "2" && lastChar == "4")
                {
                    var Coderesponse = new
                    {
                        Status = "N",
                        Message = "Please ensure value of Disable"
                    };
                    var jsonCodeResponse = JsonConvert.SerializeObject(Coderesponse);

                    // Return the JSON response
                    return jsonCodeResponse;
                }
            }
            // Create a response object with the extracted constraint name
            var response = new 
            {
                Status = "U",
                Message =  "An unexpected error occurred while processing your request. Please try again later."

            };

            // Serialize the response object to JSON
            var jsonResponse = JsonConvert.SerializeObject(response);

            // Return the JSON response
            return jsonResponse;
        }
        public static IActionResult HandleSqlError(SqlException sqlException)
        {
            if (sqlException != null)
            {
                switch (sqlException.Number)
                {
                    case 2627: // Unique constraint violation
                    case 2601: // Unique index violation
                        var uniqueErrors = new
                        {
                            Status = "U",
                            Message = "This code already exists."
                        };
                        var erResultss = JsonConvert.SerializeObject(uniqueErrors);
                        return new ContentResult
                        {
                            Content = erResultss,
                            ContentType = "application/json",
                            StatusCode = 400 // Bad Request
                        };

                    case 547: // Foreign key constraint violation
                        var foreignKeyErrors = new
                        {
                            Status = "F",
                            Message = "A foreign key constraint was violated. Ensure related data exists."
                        };
                        var foreignKeyResult = JsonConvert.SerializeObject(foreignKeyErrors);
                        return new ContentResult
                        {
                            Content = foreignKeyResult,
                            ContentType = "application/json",
                            StatusCode = 400 // Bad Request
                        };

                    default:
                        if (sqlException.Message.Contains("CHECK constraint"))
                        {
                            var checkConstraintErrors = new
                            {
                                Status = "C",
                                Message = "A constraint violation occurred. Please check the provided values."
                            };
                            var checkConstraintResult = JsonConvert.SerializeObject(checkConstraintErrors);
                            return new ContentResult
                            {
                                Content = checkConstraintResult,
                                ContentType = "application/json",
                                StatusCode = 400 // Bad Request
                            };
                        }

                        // For any other SQL exceptions
                        var sqlErrorResponse = new
                        {
                            Status = "S",
                            Message = $"A SQL error occurred: {sqlException.Message}"
                        };
                        var sqlErrorResult = JsonConvert.SerializeObject(sqlErrorResponse);
                        return new ContentResult
                        {
                            Content = sqlErrorResult,
                            ContentType = "application/json",
                            StatusCode = 400 // Bad Request
                        };
                }
            }

            // Handle non-SQL exceptions
            var innerMessage = sqlException?.InnerException?.Message;
            var errorMessage = $"Database update failed: {sqlException.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

            var errorResponse = new
            {
                Status = "E",
                Message = $"An error occurred. Please try again later.{errorMessage}"
            };

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(errorResponse),
                ContentType = "application/json",
                StatusCode = 500 // Internal Server Error
            };
        }
    }
}
