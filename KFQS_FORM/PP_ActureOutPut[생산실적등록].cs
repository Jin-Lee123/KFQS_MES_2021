#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_ActureOutPut
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
    public partial class PP_ActureOutPut : DC00_WinForm.BaseMDIChildForm
    {
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;
        
        public PP_ActureOutPut()
        {
            InitializeComponent();
        }
        
        private void PP_ActureOutPut_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",	  "공장",              true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",	      "작업지시 번호" ,    true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",	  "품목코드",          true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY",	      "계획 수량" ,        true, GridColDataType_emu.Double,     120, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",       "양품 수량" ,        true, GridColDataType_emu.Double,     120, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",        "불량수량",          true, GridColDataType_emu.Double,     120, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",	  "단위",              true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",      "투입 LOT",          true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "COMPONENT",     "투입 품목",         true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTY",  "투입 수량",         true, GridColDataType_emu.Double,     120, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE",     "투입단위",          true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE","작업장",            true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE","상태코드",          true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS",    "상태",              true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER",        "작업자",            true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",    "작업자명",          true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STARTDATE",     "최초가동시작",      true, GridColDataType_emu.DateTime24, 160, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ENDDATE",       "작업지시종료시간",  true, GridColDataType_emu.DateTime24, 160, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion


            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_Workcenter_Code(); // 작업장     //작업장
            Common.FillComboboxMaster(this.cboWorkCenterCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");
           
            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            #endregion
        }

        #region < TOOL BAR AREA >
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
                string sWorkerCenterCode = Convert.ToString(cboWorkCenterCode.Value);
                string sStratDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format  ("{0:yyyy-MM-dd}", dtpEnd.  Value);
                string sOrderNo = Convert.ToString(txtOrderNo.Value);
 
                rtnDtTemp = helper.FillTable("16PP_ActureOutPut_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",      sPlantCode,        DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKCENTERCODE", sWorkerCenterCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE",      sStratDate,        DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",        sEndDate,          DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ORDERNO",        sOrderNo,          DbType.String, ParameterDirection.Input)
                                    );

               this.ClosePrgForm();
               this.grid1.DataSource = rtnDtTemp;
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
        
        #endregion

        private void btnWorker_Click(object sender, EventArgs e)
        {
            //작업자 등록 시작
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            string sWorkId = txtWorkerID.Text.ToString();
            if(sWorkId =="")
            {
                ShowDialog("작업자를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            
            // DB에 등록하기 위한 변수 지정
            string sOrderNo        = grid1.ActiveRow.Cells["ORDERNO"].Value.ToString();
            string sWorkCenterCode = grid1.ActiveRow.Cells["WORKCENTERCODE"].Value.ToString();

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("16PP_ActureOutput_I2", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE",      "1000",          DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKER",         sWorkId,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO",        sOrderNo,        DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkCenterCode, DbType.String, ParameterDirection.Input)
                                        );
                if(helper.RSCODE =="S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally 
            { 
                helper.Close();
            }
        }
        private void btnLotIn_Click(object sender, EventArgs e)
        {
            // LOT 투입
            if (this.grid1.ActiveRow == null) return;
            DBHelper helper = new DBHelper("",true);
            try
            {
                string sItemCode         = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotNo            = Convert.ToString(txtInLotNo.Text);
                string sWorkerCenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo          = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode         = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);
                string sInFlag           = Convert.ToString(btnLotIn.Text);
                string sWorker           = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
                
                if (sInFlag == "투입")
                {
                    sInFlag = "IN";
                }
                else sInFlag = "OUT";
                
                helper.ExecuteNoneQuery("16PP_ActureOutput_I1", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE",     "1000",             DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ITEMCODE",       sItemCode,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("LOTNO",          sLotNo,            DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkerCenterCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO",        sOrderNo,          DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("UNITCODE",       sUnitCode,         DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("INFLAG",         sInFlag,           DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("MAKER",          sWorker,           DbType.String, ParameterDirection.Input)
                                        );

                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            if (Convert.ToString(this.grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop.Text = "비가동";
            }
            else btnRunStop.Text = "가동";

            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotno != "")
            {
                txtInLotNo.Text = sMatLotno;
                btnLotIn.Text = "투입취소";
            }
            else
            {
                txtInLotNo.Text = "";
                btnLotIn.Text   = "투입";
            }

            txtWorkerID.Text   = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);

        }

        private void btnRunStop_Click(object sender, EventArgs e)
        {
            // 가동 / 비가동 등록
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sStatus = "R";
                if (btnRunStop.Text == "비가동") sStatus = "S";
                helper.ExecuteNoneQuery("16PP_ActureOutput_U1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE",       "1000",                                                              DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO",        Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value),        DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE",       Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value),       DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("UNITCODE",       Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value),      DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("STATUS",         sStatus,                                                              DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("오류발생." +helper.RSMSG ,DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
              
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            //생산 실적 등록
            if (this.grid1.ActiveRow == null)    //ActiveRow가 널이면 그리드안에서 뭐고를지 클릭을 안해놓은거
            {
                ShowDialog("작업지시를 선택하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            double dProdQty = 0; //누적 양품수량
            double dErrorQty = 0; //누적 불량수량
            double dTProdQty = 0; //입력 양품수량
            double dTErrorQty = 0; //입력 불량수령
            double dOrderQty = 0; //작업지시 수량
            double dInQty = 0; //투입 LOT 잔량

            string sProdQty = Convert.ToString(this.grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
            double.TryParse(sProdQty, out dProdQty);

            string sBadQty = Convert.ToString(this.grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            double.TryParse(sBadQty, out dErrorQty);

            string sTProdQty = Convert.ToString(txtProduct.Text);
            double.TryParse(sTProdQty, out dTProdQty);

            string sTBadQty = Convert.ToString(txtBad.Text);
            double.TryParse(sTBadQty, out dTErrorQty);

            string sOrderQty = Convert.ToString(this.grid1.ActiveRow.Cells["PLANQTY"].Value).Replace(",", "");
            double.TryParse(sOrderQty, out dOrderQty);

            string sInQty = Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENTQTY"].Value).Replace(",", "");
            double.TryParse(sInQty, out dInQty);


            if (dInQty == 0)
            {
                ShowDialog("투입한 LOT이 존재하지 않습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            if ((dTProdQty + dTErrorQty) == 0)
            {
                ShowDialog("실적수량을 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            if (dOrderQty < (dProdQty + dErrorQty) + (dTProdQty + dTErrorQty))
            {
                ShowDialog("생산수량 및 불량수량의 합계가 지시수량보다 많습니다", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("08PP_ActureOutput_U2", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ITEMCODE", Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("UNITCODE", Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("PRODQTY", dTProdQty, DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ERRORQTY", dTErrorQty, DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("MATLOTNO", Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CITEMCODE", Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENT"].Value), DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("CUNITCODE", Convert.ToString(this.grid1.ActiveRow.Cells["CUNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                   );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("생산실적 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                //txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";

            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }

        }

        private void btnOrderClose_Click(object sender, EventArgs e)
        {
            // 작업지시 종료

            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("LOT 투입 취소후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            // 가동일 경우 종료 안되도록 확인
            if(Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                ShowDialog("비가동 등록 후 진행 하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("16PP_ActureOutput_U3", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", "1000",                                                                    DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO",        Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value),        DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("지시 종료 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString() , DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }


        }
    }
}