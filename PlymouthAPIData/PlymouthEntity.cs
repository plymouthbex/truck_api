using Microsoft.Data.SqlClient;
using PlymouthAPIData.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlymouthAPIData
{
    public class PlymouthEntity
    {
    }

    public class tblTruck
    {
        [Key]
        public int T_RECID { get; set; }
        public string? T_CODE { get; set; }
        public string? T_NAME { get; set; }
        public string? T_CREATEDDATE { get; set; }
        public string? T_MODIFIEDDATE { get; set; }
        public int T_SORTORDER { get; set; }
        public string? T_DISABLE {  get; set; }  
        public int T_CORECID { get; set; }
    }

    public class tblRoute
    {
        [Key]
        public int R_RECID { get; set; }
        public string? R_CODE { get; set; }
        public string? R_NAME { get; set; }
        public string? R_CREATEDDATE { get; set; }
        public string? R_MODIFIEDDATE { get; set; }
        public int R_SORTORDER { get; set; }
        public string? R_DISABLE { get; set; }
        public int R_CORECID { get; set; }
    }
    public class tblDriver
    {
        [Key]
        public int D_RECID { get; set; }
        public string? D_CODE { get; set; }
        public string? D_NAME { get; set; }
        public int D_CORECID { get; set; }
        public string? D_CREATEDDATE { get; set; }
        public string? D_MODIFIEDDATE { get; set; }
        public int D_SORTORDER { get; set; }
        public string? D_DISABLE { get; set; }
    }

    public class tblCompany
    {
        [Key]

        public int CO_RECID { get; set; }
        public string? CO_CODE { get; set; }
        public string? CO_NAME { get; set; }
        public int CO_SORTORDER { get; set; }
        public string? CO_DISABLE { get; set; }
    }
    public class tblLRoute
    {
        [Key]

        public int R_RECID { get; set; }
        public string? R_CODE { get; set; }
        public string? R_NAME { get; set; }
        public int R_CORECID { get; set; }
    }

    public class tblDoor
    {
        [Key]
        public int DO_RECID { get; set; }
        public int DO_CORECID { get; set; }
        public string? DO_CODE { get; set; }
        public string? DO_NAME { get; set; }
        public string? DO_CREATEDDATE { get; set; }
        public string? DO_MODIFIEDDATE { get; set; }
        public int DO_SORTORDER { get; set; }
        public string? DO_DISABLE { get; set; }
    }
    public class tblTruckDoorDriverRecord
    {
        [Key]
        public int TD_RECID { get; set; }
        public string? TH_DATE { get; set; }
        public int TD_HEADERID { get; set; }
        public int TD_ROUTEID { get; set; }
        public int TD_TRUCKID { get; set; }
        public int TD_DOORNO { get; set; }
        public int TD_DRIVERID { get; set; }

        public int T_RECID { get; set; }
        public int T_CORECID { get; set; }
        public string? T_CODE { get; set; }
        public string? T_NAME { get; set; }

        public int D_RECID { get; set; }
        public string? D_CODE { get; set; }
        public string? D_NAME { get; set; }

        public int R_RECID { get; set; }
        public string? R_CODE { get; set; }
        public string? R_NAME { get; set; }

        public int DO_RECID { get; set; }
        public string? DO_CODE { get; set; }
        public string? DO_NAME { get; set; }
    }


    public class tblEnquiytList
    {
        [Key]
        public int RecordID { get; set; } 
        public int? RouteID { get; set; } 
        public string? RouteCode { get; set; } 
        public string? RouteName { get; set; } 
        public int? TruckID { get; set; } 
        public string? TruckCode { get; set; } 
        public string? TruckName { get; set; } 
        public int? DoorNO { get; set; } 
        public int? DriverID { get; set; } 
        public string? DriverCode { get; set; } 
        public string? DriverName { get; set; } 
        public int? DoorID { get; set; } 
        public string? DoorCode { get; set; } 
        public string? DoorName { get; set; } 
        public string? TRDate { get; set; } 
        public string? TruckDate { get; set; } 
        public int? CompanyID { get; set; } 
        public string? CompanyName { get; set; } 
    }
    public class tblTruckHeader
    {
        [Key]
        public int TH_RECID { get; set; } 
        public int TH_COMPID { get; set; }
        public string? TH_DATE { get; set; }
    }
    public class tblTruckDetail
    {
        [Key]
        public int TD_RECID { get; set; }
        public int TD_HEADERID { get; set; }
        public int TD_ROUTEID { get; set; }
        public int TD_TRUCKID { get; set; }
        public int TD_DOORNO { get; set; }
        public int TD_DRIVERID { get; set; }
    }
    //public class TruckDetail
    //{
    //    [Key]
    //    public int TD_RECID { get; set; }
    //    public int TD_ROUTEID { get; set; }
    //    public int TD_TRUCKID { get; set; }
    //    public int TD_DOORNO { get; set; }
    //    public int TD_DRIVERID { get; set; }
    //    public int TD_HEADERID { get; set; }

    //    public virtual tblRoute Route { get; set; }
    //    public virtual tblDriver Driver { get; set; }
    //    public virtual Truck Truck { get; set; }
    //    public virtual tblTruckHeader TruckHeader { get; set; }
    //}



}
