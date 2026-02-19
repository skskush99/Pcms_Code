using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.MISReport.NextHearing;
using Report.Repository.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.MISReport.NextHearing
{
    public class NextHearingUpdateRepository : SqlRepository, INextHearingUpdateRepository
    {
        private readonly System.Data.IDbConnection Con;
        public NextHearingUpdateRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<UpdateNextHearingHistory> GetNextHearingUpdateReport(DataPagingModel TablePaging)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    #region Sorting 1 Over
                    string SortingFilter = "ORDER BY trn_NextHearinghistory.Id";
                    switch (TablePaging.SortingColumn.Trim().ToLower())
                    {
                        case "id":
                            if (TablePaging.SortingOrder == SortingOrder.Ascending)
                            {
                                SortingFilter = " ORDER BY trn_NextHearinghistory.Id";
                            }
                            else
                            {
                                SortingFilter = " ORDER BY trn_NextHearinghistory.Id DESC";
                            }
                            break;
                        //case "recievername":
                        //    if (TablePaging.SortingOrder == SortingOrder.Ascending)
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.RecieverName";
                        //    }
                        //    else
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.RecieverName DESC";
                        //    }
                        //    break;
                        //case "mobileno":
                        //    if (TablePaging.SortingOrder == SortingOrder.Ascending)
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.MobileNo";
                        //    }
                        //    else
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.MobileNo DESC";
                        //    }
                        //    break;
                        //case "message":
                        //    if (TablePaging.SortingOrder == SortingOrder.Ascending)
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.Message";
                        //    }
                        //    else
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.Message DESC";
                        //    }
                        //    break;
                        //case "shortdescription":
                        //    if (TablePaging.SortingOrder == SortingOrder.Ascending)
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.ShortDescription";
                        //    }
                        //    else
                        //    {
                        //        SortingFilter = " ORDER BY SMS_History.ShortDescription DESC";
                        //    }
                        //    break;
                        case "createddate":
                            if (TablePaging.SortingOrder == SortingOrder.Ascending)
                            {
                                SortingFilter = " ORDER BY trn_NextHearinghistory.CreatedDate";
                            }
                            else
                            {
                                SortingFilter = " ORDER BY trn_NextHearinghistory.CreatedDate DESC";
                            }
                            break;
                    }
                    #endregion

                    #region Search Filter
                    #region Search Filter Parameters
                    string SearchString = String.Empty;
                    string cfromDateParameter = String.Empty;
                    string ctoDateParameter = String.Empty;
                    //string ShortDescriptionParameter = String.Empty;
                    //string MobileNoParameter = String.Empty;
                    //string RoleIdParameter = String.Empty;
                    #endregion

                    foreach (var item in TablePaging.SearchParameter)
                    {
                        string value = item.Value.Trim();

                        if (item.Key.ToLower() == "cfromdate" && !String.IsNullOrEmpty(value))
                        {
                            SearchString += " AND trn_NextHearinghistory.CreatedDate >= @cfromdate ";
                            cfromDateParameter = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        }
                        if (item.Key.ToLower() == "ctodate" && !String.IsNullOrEmpty(value))
                        {
                            SearchString += " AND trn_NextHearinghistory.CreatedDate <= @ctodate ";
                            ctoDateParameter = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        }

                        //if (item.Key.ToLower() == "shortdescription" && !String.IsNullOrEmpty(value))
                        //{
                        //    SearchString += " AND SMS_History.ShortDescription LIKE @ShortDescription";
                        //    ShortDescriptionParameter = value;
                        //}

                        //if (item.Key.ToLower() == "mobileno" && !String.IsNullOrEmpty(value))
                        //{
                        //    SearchString += " AND SMS_History.MobileNo LIKE @MobileNo";
                        //    MobileNoParameter = value;
                        //}

                        //if (item.Key.ToLower() == "roleid" && !String.IsNullOrEmpty(value))
                        //{
                        //    SearchString += " AND SMS_History.RoleId LIKE @RoleId";
                        //    RoleIdParameter = value;
                        //}
                    }
                    #endregion

                    #region Query :GetList

                    string CommandText = "SELECT * FROM (SELECT row_number() over (" + SortingFilter + @") as RowNum,
                                                                  trn_NextHearinghistory.CaseId, trn_NextHearinghistory.CNR AS CNRNumber, mst_AdmDept.AdmDeptName as AdmDepttName
                                                                 ,Mst_Units.UnitName, Mst_Offices.OfficeName
                                                                 ,Mst_CourtNames.CourtName,CONVERT(varchar(10),trn_NextHearinghistory.CreatedDate, 103) AS CreatedDate
                                                                 ,CONVERT(varchar(10), Trn_CaseHearings.NextHearing_Date, 103) AS NextHearing_Date
                                                                 ,(convert(varchar(10),(Trn_CaseRegistrations.CaseNo))+ '/' + Mst_CaseAbbrevation.AbbrevationShort+ '/' + convert(varchar(10),(Trn_CaseRegistrations.CaseYear))) As  [CaseDetail] 
                                        FROM [trn_NextHearinghistory]
                                        INNER JOIN Trn_CaseHearings ON trn_NextHearinghistory.CaseId = Trn_CaseHearings.CaseId AND trn_NextHearinghistory.Hearing_SNO = Trn_CaseHearings.Hearing_SNo 
                                        INNER JOIN Mst_CourtNames
                                        INNER JOIN Trn_CaseRegistrations ON Mst_CourtNames.CourtId = Trn_CaseRegistrations.CourtId ON trn_NextHearinghistory.CaseId = Trn_CaseRegistrations.CaseId 
                                        INNER JOIN Mst_CaseAbbrevation ON Trn_CaseRegistrations.AbbreviationId = Mst_CaseAbbrevation.AbbrevationId 
                                        LEFT OUTER JOIN Mst_Units 
                                        INNER JOIN mst_AdmDept ON Mst_Units.AdmDeptId = mst_AdmDept.AdmDeptId
                                        INNER JOIN Mst_Offices ON Mst_Units.UnitId = Mst_Offices.UnitId ON Trn_CaseRegistrations.OfficeId = Mst_Offices.OfficeId
	                                    WHERE 1=1 " + SearchString + " AND (Trn_CaseRegistrations.Active = 1) AND (Trn_CaseHearings.Active = 1) AND (Mst_Units.Active = 1) AND (mst_AdmDept.Active = 1) AND (Mst_Offices.Active = 1)) as tbl WHERE RowNum BETWEEN @RecordFrom AND @RecordTo";
                    #endregion

                    var parmeters = new
                    {
                        cfromDate = cfromDateParameter,
                        ctoDate = ctoDateParameter,
                        RecordFrom = TablePaging.CurrentPageID.TableSkipRecord(TablePaging.PageSize) + 1,
                        RecordTo = TablePaging.PageSize + TablePaging.CurrentPageID.TableSkipRecord(TablePaging.PageSize),
                    };

                    var objResult = Con.Query<UpdateNextHearingHistory>(CommandText, parmeters);

                    //********* Get Total Records and Total Pages(Count of List) *********//

                    CommandText = @"SELECT Count(*) 
                                        FROM [trn_NextHearinghistory]
                                        INNER JOIN Trn_CaseHearings ON trn_NextHearinghistory.CaseId = Trn_CaseHearings.CaseId AND trn_NextHearinghistory.Hearing_SNO = Trn_CaseHearings.Hearing_SNo 
                                        INNER JOIN Mst_CourtNames
                                        INNER JOIN Trn_CaseRegistrations ON Mst_CourtNames.CourtId = Trn_CaseRegistrations.CourtId ON trn_NextHearinghistory.CaseId = Trn_CaseRegistrations.CaseId 
                                        INNER JOIN Mst_CaseAbbrevation ON Trn_CaseRegistrations.AbbreviationId = Mst_CaseAbbrevation.AbbrevationId 
                                        LEFT OUTER JOIN Mst_Units 
                                        INNER JOIN mst_AdmDept ON Mst_Units.AdmDeptId = mst_AdmDept.AdmDeptId
                                        INNER JOIN Mst_Offices ON Mst_Units.UnitId = Mst_Offices.UnitId ON Trn_CaseRegistrations.OfficeId = Mst_Offices.OfficeId
	                                    WHERE 1=1 " + SearchString + " AND (Trn_CaseRegistrations.Active = 1) AND (Trn_CaseHearings.Active = 1) AND (Mst_Units.Active = 1) AND (mst_AdmDept.Active = 1) AND (Mst_Offices.Active = 1)";

                    int TotalRecords = Convert.ToInt32(Con.ExecuteScalar(CommandText, parmeters, commandType: CommandType.Text));
                    objResult.ToList().ForEach(x => x.TotalRecords = TotalRecords);

                    DisposeCurrentSqlConnection();
                    return objResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
