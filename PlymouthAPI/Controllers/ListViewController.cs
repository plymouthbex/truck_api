using BcloudAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlymouthAPI.Attributes;
using PlymouthAPIData;
using PlymouthAPIData.DTO;
using static PlymouthAPI.Controllers.CompanyController;

namespace PlymouthAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[ApiKeyAuth]
    public class ListViewController : ControllerBase
    {
        private readonly PlymouthAPIdbcontext APIDbContext;


        public ListViewController(PlymouthAPIdbcontext APIDbContext)
        {
            this.APIDbContext = APIDbContext;
        }
        [HttpGet("ListView/{id}")]
        public async Task<ActionResult> ListView(int id)
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
                    headerName = "HeaderID",
                    field = "HeaderID",
                    align = "left",
                    width = "100",
                    headerAlign = "left",
                    hide = true
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
                    headerName = "TruckID",
                    field = "TruckID",
                    align = "left",
                    width = "150",
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
                    headerName = "DriverID",
                    field = "DriverID",
                    align = "left",
                    width = "80 ",
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
                    headerName = "Door(GateNo)",
                    field = "DoorNO",
                    align = "left",
                    width = "150",
                    headerAlign = "left",
                    hide = false
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
                }
            };

            List<TruckListView> lstResultData = new List<TruckListView>();
            try
            {
                if (id==0)
                {
                    var error = new { Status = "N", data = "No records found for the given header ID" };
                    var errorResult = JsonConvert.SerializeObject(error);
                    return new ContentResult
                    {
                        StatusCode = 404, // Not Found
                        Content = errorResult,
                        ContentType = "application/json"
                    };
                }
                if (id!=0)
                {
                    lstResultData = APIDbContext.dtTruckListView.Where(f => f.TD_HEADERID == id).Select(item => new TruckListView
                    {
                        RecordID = item.TD_RECID,
                        RouteID = item.TD_ROUTEID,
                        RouteCode = item.R_CODE,
                        RouteName = item.R_NAME,
                        TruckID = item.TD_TRUCKID,
                        TruckCode = item.T_CODE,
                        TruckName = item.T_NAME,
                        DoorNO = item.TD_DOORNO,
                        DoorCode=item.DO_CODE,
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
        public async Task<ActionResult> EnquiryListView(string? All ,string? filter)
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
                string FilterQuery = "";
                string? FilterValue = filter;
                string lstrSQLST = "SELECT * from VI_TRUCKLISTVIEW  where td_headerid=61";
                var truckDetails = APIDbContext.dtEnquiryList.FromSqlRaw(lstrSQLST).ToList();

                return Ok(truckDetails);
                if (All == "N")
                {

                }
                if(All == "Y")
                {

                }
                else
                {

                }
                if (!string.IsNullOrEmpty(FilterValue))
                {
                    lstResultData = APIDbContext.dtTruckListView.Select(item => new TruckListView
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
    }
}