using BcloudAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PlymouthAPI.Attributes;
using PlymouthAPIData;
using PlymouthAPIData.DTO;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using static PlymouthAPI.Controllers.CompanyController;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlymouthAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiKeyAuth]
    public class CompanyController : ControllerBase
    {
        private readonly PlymouthAPIdbcontext APIDbContext;


        public CompanyController(PlymouthAPIdbcontext APIDbContext)
        {
            this.APIDbContext = APIDbContext;
        }
        [HttpGet("Company")]
        public async Task<ActionResult> Company()
        {
            var header = new List<Header>
            {
                new Header
                {
                    headerName = "RecordID",
                    field = "RecordID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header
                {

                    headerName = "Code",
                    field = "Code",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                },
                new Header
                {
                    headerName = "Name",
                    field = "Name",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                }

            };
            List<Company> lstResultData = new List<Company>();
            try
            {
                //lstResultData = APIDbContext.dtCustPriceList.Where(f => string.IsNullOrEmpty(fstrCode) || f.CPL_CUSTNMBR == fstrCode).Select(c => new clsCustPriceList

                //lstResultData = APIDbContext.dtPriceBook.Select(item => new PreparePriceBook
                lstResultData = APIDbContext.dtComp.Select(item => new Company
                {
                    RecordID = item.CO_RECID,
                    Code = item.CO_CODE,
                    Name = item.CO_NAME,
                }).ToList();

                var Data = new
                {
                    Status = "Y",
                    headers = header,
                    rows = lstResultData
                };
                var results = JsonConvert.SerializeObject(Data);
                return Content(results, "application/json");



            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }


        //[HttpGet("Route")]
        [HttpGet("Route/{id?}")]
        public async Task<ActionResult> Route(int? id)
        {
            var header = new List<Header>
            {
                new Header{
                    headerName = "RecordID",
                    field = "RecordID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                 new Header{
                    headerName = "CompanyID",
                    field = "CompanyID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                 new Header{
                    headerName = "Code",
                    field = "Code",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                },
                 new Header{
                    headerName = "Name",
                    field = "Name",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                }

            };
            List<LRoute> lstResultData = new List<LRoute>();
            try
            {
                //lstResultData = APIDbContext.dtCustPriceList.Where(f => string.IsNullOrEmpty(fstrCode) || f.CPL_CUSTNMBR == fstrCode).Select(c => new clsCustPriceList

                //lstResultData = APIDbContext.dtPriceBook.Select(item => new PreparePriceBook
                if (!id.HasValue)
                {
                    lstResultData = APIDbContext.dtRoute.Select(item => new LRoute
                    {
                        RecordID = item.R_RECID,
                        CompanyID = item.R_CORECID,
                        Code = item.R_CODE,
                        Name = item.R_NAME,

                    }).ToList();
                }
                if (id.HasValue)
                {
                    lstResultData = APIDbContext.dtRoute.Where(f => f.R_CORECID == id.Value).Select(item => new LRoute
                    {
                        RecordID = item.R_RECID,
                        CompanyID = item.R_CORECID,
                        Code = item.R_CODE,
                        Name = item.R_NAME,

                    }).ToList();
                }
                var Data = new
                {
                    Status = "Y",
                    headers = header,
                    rows = lstResultData
                };
                var results = JsonConvert.SerializeObject(Data);
                return Content(results, "application/json");



            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }

        }

        [HttpGet("Truck/{id?}")]
        public async Task<ActionResult> Truck(int? id)
        {
            var header = new List<Header>
            {
                new Header {
                    headerName = "RecordID",
                    field = "RecordID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header {
                    headerName = "CompanyID",
                    field = "CompanyID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                },
                new Header {
                    headerName = "Code",
                    field = "Code",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                },
                new Header {
                    headerName = "Name",
                    field = "Name",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = false
                }
            };

            List<LRoute> lstResultData = new List<LRoute>();
            try
            {
                if (!id.HasValue)
                {
                    lstResultData = APIDbContext.dtTruck.Select(item => new LRoute
                    {
                        RecordID = item.T_RECID,
                        CompanyID = item.T_CORECID,
                        Code = item.T_CODE,
                        Name = item.T_NAME,

                    }).ToList();
                }
                if (id.HasValue)
                {
                    lstResultData = APIDbContext.dtTruck.Where(f => f.T_CORECID == id.Value).Select(item => new LRoute
                    {
                        RecordID = item.T_RECID,
                        CompanyID = item.T_CORECID,
                        Code = item.T_CODE,
                        Name = item.T_NAME,

                    }).ToList();
                }

                var Data = new
                {
                    Status = "Y",
                    headers = header,
                    rows = lstResultData
                };
                var results = JsonConvert.SerializeObject(Data);
                return Content(results, "application/json");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpGet("Driver/{id?}")]
        public async Task<ActionResult> Driver(int? id)
        {
            var header = new List<Header>
            {
                new Header{
                    headerName = "RecordID",
                    field = "RecordID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header{
                    headerName = "CompanyID",
                    field = "CompanyID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header{
                    headerName = "First Name",
                    field = "Code",
                    align = "left",
                    width = "300",
                    headerAlign = "left",
                    hide = false
                },
                new Header{
                    headerName = "Last Name",
                    field = "Name",
                    align = "left",
                    width = "300",
                    headerAlign = "left",
                    hide = false
                }
            };

            List<LRoute> lstResultData = new List<LRoute>();
            try
            {
                if (!id.HasValue)
                {
                    lstResultData = APIDbContext.dtDriver.Select(item => new LRoute
                    {
                        RecordID = item.D_RECID,
                        CompanyID = item.D_CORECID,
                        Code = item.D_CODE,
                        Name = item.D_NAME,

                    }).ToList();
                }
                if (id.HasValue)
                {
                    lstResultData = APIDbContext.dtDriver.Where(f => f.D_CORECID == id.Value).Select(item => new LRoute
                    {
                        RecordID = item.D_RECID,
                        CompanyID = item.D_CORECID,
                        Code = item.D_CODE,
                        Name = item.D_NAME,

                    }).ToList();
                }

                var Data = new
                {
                    Status = "Y",
                    headers = header,
                    rows = lstResultData
                };
                var results = JsonConvert.SerializeObject(Data);
                return Content(results, "application/json");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpGet("Door/{id?}")]
        public async Task<ActionResult> Door(int? id)
        {
            var header = new List<Header>
            {
                new Header{
                    headerName = "RecordID",
                    field = "RecordID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header{
                    headerName = "CompanyID",
                    field = "CompanyID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
                },
                new Header{
                     headerName = "Door Code",
                     field = "Code",
                     align = "left",
                     width = "100",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Door Name",
                     field = "Name",
                     align = "left",
                     width = "100",
                     headerAlign = "left",
                     hide = false
                }
            };

            List<LRoute> lstResultData = new List<LRoute>();
            try
            {
                if (!id.HasValue)
                {
                    lstResultData = APIDbContext.dtDoor.Select(item => new LRoute
                    {
                        RecordID = item.DO_RECID,
                        CompanyID = item.DO_CORECID,
                        Code = item.DO_CODE,
                        Name = item.DO_NAME,

                    }).ToList();
                }
                if (id.HasValue)
                {
                    lstResultData = APIDbContext.dtDoor.Where(f => f.DO_CORECID == id.Value).Select(item => new LRoute
                    {
                        RecordID = item.DO_RECID,
                        CompanyID = item.DO_CORECID,
                        Code = item.DO_CODE,
                        Name = item.DO_NAME,

                    }).ToList();
                }

                var Data = new
                {
                    Status = "Y",
                    headers = header,
                    rows = lstResultData
                };
                var results = JsonConvert.SerializeObject(Data);
                return Content(results, "application/json");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }


        [HttpDelete("DeleteDriver/{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    var error = new { Status = "N", Message = "Invalid Data" };
                    var errorresult = JsonConvert.SerializeObject(error);
                    return new ContentResult
                    {
                        Content = errorresult,
                        ContentType = "application/json",
                        StatusCode = 400
                    };
                }

                var GroupEntity = await APIDbContext.dtDriver
                   .Where(entity => entity.D_RECID == id).FirstOrDefaultAsync();


                if (GroupEntity == null)
                {
                    var error = new { Status = "N", Message = "No Records found for deletion." };
                    var errorresult = JsonConvert.SerializeObject(error);
                    return new ContentResult
                    {
                        Content = errorresult,
                        ContentType = "application/json",
                        StatusCode = 400
                    };
                }
                APIDbContext.dtDriver.RemoveRange(GroupEntity);

                await APIDbContext.SaveChangesAsync();
                var success = new
                {
                    Status = "Y",
                    Message = "Deleted sucessfully."
                };
                var successresult = JsonConvert.SerializeObject(success);
                return Content(successresult, "application/json");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpDelete("DeleteTruck/{id}")]
        public async Task<ActionResult> DeleteTruck(int id)
        {
            try
            {
                if (id == 0)
                {
                    var error = new { Status = "N", Message = "Invalid Data" };
                    var errorresult = JsonConvert.SerializeObject(error);
                    return new ContentResult
                    {
                        Content = errorresult,
                        ContentType = "application/json",
                        StatusCode = 400
                    };
                }

                var GroupEntity = await APIDbContext.dtTruck
                   .Where(entity => entity.T_RECID == id).FirstOrDefaultAsync();


                if (GroupEntity == null)
                {
                    var error = new { Status = "N", Message = "No Records found for deletion." };
                    var errorresult = JsonConvert.SerializeObject(error);
                    return new ContentResult
                    {
                        Content = errorresult,
                        ContentType = "application/json",
                        StatusCode = 400
                    };
                }
                APIDbContext.dtTruck.RemoveRange(GroupEntity);

                await APIDbContext.SaveChangesAsync();
                var success = new
                {
                    Status = "Y",
                    Message = "Deleted sucessfully."
                };
                var successresult = JsonConvert.SerializeObject(success);
                return Content(successresult, "application/json");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpPost("PostDriver")]
        public async Task<ActionResult> PostDriver([FromBody] PostData data)
        {

            if (data == null ||
         string.IsNullOrWhiteSpace(data.Code) ||
         string.IsNullOrWhiteSpace(data.Name) ||
         data.CompanyID == 0)
            {
                var ErOutput = new
                {
                    Status = "N",
                    Code = "105",
                    Message = "Incomplete Data"
                };
                var Erresults = JsonConvert.SerializeObject(ErOutput);
                return Content(Erresults, "application/json");
            }

            try
            {
                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                int MaxSort = APIDbContext.dtDriver.Max(c => c.D_SORTORDER) + 1;
                var BinsEntity = new tblDriver
                {
                    D_CORECID = data.CompanyID,
                    D_CODE = data.Code,
                    D_NAME = data.Name,
                    D_CREATEDDATE = formattedDateTime,
                    D_MODIFIEDDATE = formattedDateTime,
                    D_SORTORDER = MaxSort,
                    D_DISABLE = "N"
                };

                await APIDbContext.dtDriver.AddRangeAsync(BinsEntity);
                var result = await APIDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    var Output = new
                    {
                        Status = "Y",
                        Code = "Ex01",
                        Message = "Inserted Successfully"
                    };
                    var results = JsonConvert.SerializeObject(Output);
                    return Content(results, "application/json");
                }
                else
                {
                    var ErOutput = new
                    {
                        Status = "N",
                        Code = "1001",
                        Message = "Record not inserted"
                    };
                    var Erresults = JsonConvert.SerializeObject(ErOutput);
                    return Content(Erresults, "application/json");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };
                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpPost("PostTruck")]
        public async Task<ActionResult> PostTruck([FromBody] PostData data)
        {

            if (data == null ||
         string.IsNullOrWhiteSpace(data.Code) ||
         string.IsNullOrWhiteSpace(data.Name) ||
         data.CompanyID == 0)
            {
                var ErOutput = new
                {
                    Status = "N",
                    Code = "105",
                    Message = "Incomplete Data"
                };
                var Erresults = JsonConvert.SerializeObject(ErOutput);
                return Content(Erresults, "application/json");
            }

            try
            {
                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                int MaxSort = APIDbContext.dtTruck.Max(c => c.T_SORTORDER) + 1;
                var BinsEntity = new tblTruck
                {
                    T_CORECID = data.CompanyID,
                    T_CODE = data.Code,
                    T_NAME = data.Name,
                    T_CREATEDDATE = formattedDateTime,
                    T_MODIFIEDDATE = formattedDateTime,
                    T_SORTORDER = MaxSort,
                    T_DISABLE = "N"
                };

                await APIDbContext.dtTruck.AddRangeAsync(BinsEntity);
                var result = await APIDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    var Output = new
                    {
                        Status = "Y",
                        Code = "Ex01",
                        Message = "Inserted Successfully"
                    };
                    var results = JsonConvert.SerializeObject(Output);
                    return Content(results, "application/json");
                }
                else
                {
                    var ErOutput = new
                    {
                        Status = "N",
                        Code = "1001",
                        Message = "Record not inserted"
                    };
                    var Erresults = JsonConvert.SerializeObject(ErOutput);
                    return Content(Erresults, "application/json");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };
                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpPost("PostTruckHeader")]
        public async Task<ActionResult> PostTruckHeader([FromBody] PostTruckHeaderData data)
        {
            List<tblTruckDetail> lstTruckDetail = new List<tblTruckDetail>();

            if (data == null ||
         string.IsNullOrWhiteSpace(data.Date) ||
         data.CompanyID == 0)
            {
                var ErOutput = new
                {
                    Status = "N",
                    Code = "105",
                    Message = "Incomplete Data"
                };
                var Erresults = JsonConvert.SerializeObject(ErOutput);
                return Content(Erresults, "application/json");
            }

            try
            {
                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                var TruckHeaderEntity = new tblTruckHeader
                {
                    TH_COMPID = data.CompanyID,
                    TH_DATE = data.Date

                };

                await APIDbContext.dttruckHeaders.AddRangeAsync(TruckHeaderEntity);
                var result = await APIDbContext.SaveChangesAsync();
                int TruckRecid = TruckHeaderEntity.TH_RECID;

                var TruckDetailEntity = APIDbContext.dtRoute.Where(f => f.R_CORECID == data.CompanyID);
                foreach (var item in TruckDetailEntity)
                {
                    var TruckEntityData = new tblTruckDetail
                    {
                        TD_HEADERID = TruckRecid,
                        TD_ROUTEID = item.R_RECID,
                        TD_TRUCKID = 0,
                        TD_DOORNO = 0,
                        TD_DRIVERID = 0,

                    };
                    lstTruckDetail.Add(TruckEntityData);
                }
                await APIDbContext.dtTruckDetail.AddRangeAsync(lstTruckDetail);
                var resultData = await APIDbContext.SaveChangesAsync();

                if (resultData > 0)
                {
                    var Output = new
                    {
                        Status = "Y",
                        Code = "Ex01",
                        Message = "Inserted Successfully"
                    };
                    var results = JsonConvert.SerializeObject(Output);
                    return Content(results, "application/json");
                }
                else
                {
                    var ErOutput = new
                    {
                        Status = "N",
                        Code = "1001",
                        Message = "Record not inserted"
                    };
                    var Erresults = JsonConvert.SerializeObject(ErOutput);
                    return Content(Erresults, "application/json");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };
                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpPost("PostTruckDetail")]
        public async Task<ActionResult> PostTruckDetail([FromBody] PostTruckHeaderDetail data)
        {

            if (data == null || data.RecordID==0||
         data.HeaderID == 0)
            {
                var ErOutput = new
                {
                    Status = "N",
                    Code = "105",
                    Message = "Incomplete Data"
                };
                var Erresults = JsonConvert.SerializeObject(ErOutput);
                return Content(Erresults, "application/json");
            }

            try
            {
                List<tblTruckDetail> lstTruckDetail = new List<tblTruckDetail>();

                var HeaderEntities = await APIDbContext.dtTruckDetail.FirstOrDefaultAsync(x => x.TD_RECID == data.RecordID);

                int TruckIDInt = string.IsNullOrWhiteSpace(data.TruckID) ? 0 : int.TryParse(data.TruckID, out var truckId) ? truckId : 0;

                int DoorNOInt = string.IsNullOrWhiteSpace(data.DoorNO) ? 0 : int.TryParse(data.DoorNO, out var doorNo) ? doorNo : 0;

                int DriverIDInt = string.IsNullOrWhiteSpace(data.DriverID) ? 0 : int.TryParse(data.DriverID, out var driverId) ? driverId : 0;

                if (HeaderEntities != null)
                {
                    HeaderEntities.TD_TRUCKID = TruckIDInt;
                    HeaderEntities.TD_ROUTEID = DoorNOInt;
                    HeaderEntities.TD_DRIVERID = DriverIDInt;
                }
                var result = await APIDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    var Output = new
                    {
                        Status = "Y",
                        Code = "Ex01",
                        Message = "Inserted Successfully"
                    };
                    var results = JsonConvert.SerializeObject(Output);
                    return Content(results, "application/json");
                }
                else
                {
                    var ErOutput = new
                    {
                        Status = "N",
                        Code = "1001",
                        Message = "Record not inserted"
                    };
                    var Erresults = JsonConvert.SerializeObject(ErOutput);
                    return Content(Erresults, "application/json");
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };
                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        [HttpGet("EnquiryListView")]
        public async Task<IActionResult> EnquiryListView([FromQuery] string filter, string All)
        {
            var header = new List<Header>
            {
               new Header{
                     headerName = "Company Name",
                     field = "CompanyName",
                     align = "left",
                     width = "150",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Route Code",
                     field = "RouteCode",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Route Name",
                     field = "RouteName",
                     align = "left",
                     width = "150",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Truck",
                     field = "TruckID",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = true
                },
                new Header{
                     headerName = "Truck Code",
                     field = "TruckCode",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Truck Name",
                     field = "TruckName",
                     align = "left",
                     width = "150",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Driver",
                     field = "DriverID",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = true
                },
                new Header{
                     headerName = "First Name",
                     field = "DriverCode",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Last Name",
                     field = "DriverName",
                     align = "left",
                     width = "150",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Door",
                     field = "DoorNO",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = true
                },
                new Header{
                     headerName = "Door Code",
                     field = "DoorCode",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Door Name",
                     field = "DoorName",
                     align = "left",
                     width = "150",
                     headerAlign = "left",
                     hide = false
                },
                new Header{
                     headerName = "Date",
                     field = "Date",
                     align = "left",
                     width = "80",
                     headerAlign = "left",
                     hide = false
                }
            };

            List<TruckListView> lstResultData = new List<TruckListView>();
            try
            {

                var truckDetailsQuery = APIDbContext.dtTruckListView.ToList();
                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Replace("RouteID", "TD_ROUTEID")
                                   .Replace("TruckID", "TD_TRUCKID")
                                   .Replace("CompanyID", "T_CORECID")
                                   .Replace("DriverID", "TD_DRIVERID")
                                   .Replace("DoorNO", "TD_DOORNO")
                                   .Replace("Date", "TH_DATE");

                    var parts = filter.Split(new[] { " LIKE " }, StringSplitOptions.RemoveEmptyEntries);


                    string lstrSQLST = $"SELECT * from VI_TRUCKLISTVIEW where {filter}";
                    var truckDetails = APIDbContext.dtTruckListView.FromSqlRaw(lstrSQLST).ToList();

                    if (All == "N")
                    {
                        lstResultData = truckDetails.Select(item => new TruckListView
                        {
                            RecordID = item.TD_RECID,
                            RouteID = item.TD_ROUTEID,
                            RouteCode = item.R_CODE,
                            RouteName = item.R_NAME,
                            TruckID = item.TD_TRUCKID,
                            TruckCode = item.T_CODE,
                            TruckName = item.T_NAME,
                            DoorNO = item.TD_DOORNO,
                            DoorCode = item.DO_CODE,
                            DoorName = item.DO_NAME,
                            DriverID = item.TD_DRIVERID,
                            DriverCode = item.D_CODE,
                            DriverName = item.D_NAME,

                        }).ToList();
                    }
                    if (All == "Y")
                    {
                        lstResultData = APIDbContext.dtTruckListView.Where(f=>f.DO_RECID!=0&&f.D_RECID!=0&&f.T_RECID!=0&&f.R_RECID!=0).Select(item => new TruckListView
                        {
                            RecordID = item.TD_RECID,
                            RouteID = item.TD_ROUTEID,
                            RouteCode = item.R_CODE,
                            RouteName = item.R_NAME,
                            TruckID = item.TD_TRUCKID,
                            TruckCode = item.T_CODE,
                            TruckName = item.T_NAME,
                            DoorNO = item.TD_DOORNO,
                            DoorCode = item.DO_CODE,
                            DoorName = item.DO_NAME,
                            DriverID = item.TD_DRIVERID,
                            DriverCode = item.D_CODE,
                            DriverName = item.D_NAME,

                        }).ToList();
                    }
                    var Data = new
                    {
                        Status = "Y",
                        headers = header,
                        rows = lstResultData
                    };
                    var results = JsonConvert.SerializeObject(Data);
                    return Content(results, "application/json");

                    // Use Dynamic LINQ to apply the filter
                }

                return Ok(truckDetailsQuery);
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException != null)
                {
                    var handler = new SqlErrorHandling();
                    var jsonResponse = handler.HandleSqlException(sqlException);

                    return Content(jsonResponse, "application/json");
                }
                else
                {
                    var innerMessage = ex.InnerException?.Message;
                    var errorMessage = $"Database update failed: {ex.Message}" + (innerMessage != null ? $" Inner Exception: {innerMessage}" : "");

                    var errorResponse = new
                    {
                        Status = "E",
                        Message = $"An error occurred. Please try again later.{errorMessage}"
                    };

                    return Content(JsonConvert.SerializeObject(errorResponse), "application/json");
                }

            }
            catch (Exception ex)
            {
                var errorMessage = $"API failure: {ex.Message}";
                var Error = new
                {
                    Status = "N",
                    Message = errorMessage,
                    //InnerException = ex.InnerException.Message
                };

                var Erresults = JsonConvert.SerializeObject(Error);
                return Content(Erresults, "application/json");
            }
        }

        public class Header
        {
            public string? headerName { get; set; }
            public string? field { get; set; }
            public string? align { get; set; }
            public string? width { get; set; }
            public string? headerAlign { get; set; }
            public bool hide { get; set; }
        }
        public class PostData
        {
            public string? RecordID { get; set; }
            public int CompanyID { get; set; }
            public string? Code { get; set; }
            public string? Name { get; set; }
        }
        public class PostTruckHeaderData
        {
            public string? RecordID { get; set; }
            public int CompanyID { get; set; }
            public string? Date { get; set; }
        }
        public class PostTruckHeaderDetail
        {
            public int RecordID { get; set; }
            public int HeaderID { get; set; }
            public string? TruckID { get; set; }
            public string? DoorNO { get; set; }
            public string? DriverID { get; set; }
        }
        public class TruckDetail
        {
            public int TD_RECID { get; set; }
            public int TD_ROUTEID { get; set; }
            public int TD_TRUCKID { get; set; }
            public int TD_DOORNO { get; set; }
            public int TD_DRIVERID { get; set; }
            public int TD_HEADERID { get; set; }

            public virtual tblLRoute Route { get; set; }
            public virtual tblDriver Driver { get; set; }
            public virtual tblTruck Truck { get; set; }
            public virtual tblTruckHeader TruckHeader { get; set; }
        }


    }
}
