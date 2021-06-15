#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_WorkerPerProd
//   Form Name    : 자재 재고관리 
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC_POPUP;

using DC00_assm;
using DC00_WinForm;
using DC_POPUP;
using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class PP_WorkerPerProd : DC00_WinForm.BaseMDIChildForm
    {
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;
        
        public PP_WorkerPerProd()
        {
            InitializeComponent();
        }
        
        private void PP_WorkerPerProd_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",	   "공장",      true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",	   "작업자" ,   true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODDATE",	   "생산일자",  true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장" ,   true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명" , true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목",      true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",	   "품명",      true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",        "생산수량",  true, GridColDataType_emu.Double,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",         "불량수량",  true, GridColDataType_emu.Double,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TOTALQTY",       "총생산량",  true, GridColDataType_emu.Double,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ERRORPER",        "불량율",   true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",       "생산일시",  true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            
            
            this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VirtualRect;
            this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKERNAME"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["PRODDATE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle = MergedCellStyle.Always;


            #endregion


            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            #endregion
        }

     
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()  // 작업 지시 조회
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sStratDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate   = string.Format  ("{0:yyyy-MM-dd}", dtpEnd.  Value);
                string sWorkerName = this.txtWorkerName.Text;
                
                rtnDtTemp = helper.FillTable("16PP_WorkerPerProd_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",      sPlantCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE",      sStratDate,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",        sEndDate,    DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKERNAME",    sWorkerName,  DbType.String, ParameterDirection.Input)
                                    );

                if (rtnDtTemp.Rows.Count != 0)
                {
                    // sub-total
                    DataTable dtSubTotal = rtnDtTemp.Clone();  //데이터 데이블의 서식 복사

                    string sWorkerRow = Convert.ToString(rtnDtTemp.Rows[0]["WORKERNAME"]);
                    double sProdQty   = Convert.ToDouble(rtnDtTemp.Rows[0]["PRODQTY"]);
                    double sBadQty    = Convert.ToDouble(rtnDtTemp.Rows[0]["BADQTY"]);
                    double sTotalQty  = Convert.ToDouble(rtnDtTemp.Rows[0]["TOTALQTY"]);
                    dtSubTotal.Rows.Add(new object[] {Convert.ToString(rtnDtTemp.Rows[0]["PLANTCODE"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["WORKERNAME"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["PRODDATE"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["WORKCENTERCODE"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["WORKCENTERNAME"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["ITEMCODE"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["ITEMNAME"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["PRODQTY"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["BADQTY"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["TOTALQTY"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["ERRORPER"]),
                                                      Convert.ToString(rtnDtTemp.Rows[0]["MAKEDATE"])});
                    for (int i =1; i < rtnDtTemp.Rows.Count; i++)
                    {
                        if (sWorkerRow == Convert.ToString(rtnDtTemp.Rows[i]["WORKERNAME"]))
                        {
                            sProdQty  = sProdQty  + Convert.ToDouble(rtnDtTemp.Rows[i]["PRODQTY"]);
                            sBadQty   = sBadQty   + Convert.ToDouble(rtnDtTemp.Rows[i]["BADQTY"]);
                            sTotalQty = sTotalQty + Convert.ToDouble(rtnDtTemp.Rows[i]["TOTALQTY"]);

                            dtSubTotal.Rows.Add(new object[] {Convert.ToString(rtnDtTemp.Rows[i]["PLANTCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKERNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["PRODDATE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKCENTERCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKCENTERNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ITEMCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ITEMNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["PRODQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["BADQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["TOTALQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ERRORPER"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["MAKEDATE"])});
                            continue;
                        }
                        else
                        {
                            dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });
                            dtSubTotal.Rows.Add(new object[] {Convert.ToString(rtnDtTemp.Rows[i]["PLANTCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKERNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["PRODDATE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKCENTERCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["WORKCENTERNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ITEMCODE"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ITEMNAME"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["PRODQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["BADQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["TOTALQTY"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["ERRORPER"]),
                                                              Convert.ToString(rtnDtTemp.Rows[i]["MAKEDATE"])});
                            sWorkerRow = Convert.ToString(rtnDtTemp.Rows[i]["WORKERNAME"]);
                        }
                    }
                    dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });
                    this.grid1.DataSource = dtSubTotal;
                }

            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(),DialogForm.DialogType.OK);    
            }
            finally
            {
                helper.Close();
            }
        }
    }
}