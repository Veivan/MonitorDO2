using Dapper;
using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitorDO2.Models
{
    public interface IDoRepository
    {
        //void Create(User user);
        //void Delete(int id);
        //User Get(int id);
        List<RDnDO2Model> GetDo2s(DateTime dt, SortState sortOrder);
        //void Update(User user);
    }

    public class DoRepository : IDoRepository
    {
        string connectionString = null;

        public DoRepository(string conn)
        {
            connectionString = conn;
        }

        public List<RDnDO2Model> GetDo2s(DateTime dt, SortState sortOrder)
        {
            if (dt == null) dt = DateTime.Now;

            var sql =
                @"SELECT RD.ID as RdId, DRD.ID as DrdId, DO.ID as Do2Id,
    A.FULL_AWB_NUMBER as FullAwbNumber, A.TECHNOLOGY as AwbTech, 
    RD.OPERATION_DATE as RdDate, ABS(RD.PIECES) as Pieces, ABS(RD.WEIGHT) as Weight
FROM DOCUSR.DOC_AWB_RECEIPT_DISPATCH RD
    INNER JOIN DOCUSR.DOC_AWB A ON A.ID = RD.DOC_ID
    LEFT JOIN DOCUSR.DOC_DO_RECEIPT_DISPATCH DRD ON DRD.AWB_REC_DISP_ID = RD.ID
    LEFT JOIN DOCUSR.DOC_DO_DO2 DO ON DO.REC_DISP_ID = DRD.ID
WHERE 
    A.TECHNOLOGY IN ('IMP', 'TRN')	
    AND A.IS_CUSTOMS_CONTROLLED = 1 
    AND A.IS_PREVENT_SVH_STORAGE = 0
    AND TO_CHAR(RD.OPERATION_DATE, 'DD-MM-YY') = :strDt
    AND RD.STATUS = 'Approved'
    AND RD.OPERATION_TYPE IN ('CLNT', 'EXP', 'DWW', 'DAA', 'DREX', 'ZTK', 'ZTKLOCK')
    AND DO.ID IS NULL ";
            using (var db = new OracleConnection(connectionString))
            {
                //var do1Id = "DO1-938595";
                //var res = db.Query<Do2Model>("SELECT ID, DOC_ID, STATUS, DO2_NUMBER as Do2Number, DO2_DATE FROM DOCUSR.DOC_DO_DO2 WHERE DOC_ID = :do1Id", new { do1Id }).ToList();

                var strDt = dt.ToString("dd-MM-yy");
                //var strDt = "22-04-21";

                //var res = db.Query<RDnDO2Model>(sql, new { strDt }).OrderBy(x => x.FullAwbNumber).ToList();
                var res = db.Query<RDnDO2Model>(sql, new { strDt });

                res = sortOrder switch
                {
                    SortState.AwbNumberDesc => res.OrderByDescending(x => x.FullAwbNumber),
                    SortState.AwbTechAsc => res.OrderBy(x => x.AwbTech),
                    SortState.AwbTechDesc => res.OrderByDescending(x => x.AwbTech),
                    _ => res.OrderBy(x => x.FullAwbNumber), // AwbNumberAsc
                };
                return res.ToList();
            }
        }
    }
}
