using DC_POPUP;
using DC00_assm;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DC_POPUP;

namespace KFQS_Form
{
    public partial class PP_ActureOutPut : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public PP_ActureOutPut()
        {
            InitializeComponent();
        }

        private void PP_ActureOutPut_Load(object sender, EventArgs e)
        {   
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"      , "공장"                , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERNO"        , "작업 지시 번호"      , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"       , "품목 코드"           , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANQTY"        , "계획 수량"           , true, GridColDataType_emu.Double     , 120, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PRODQTY"        , "양품 수량"           , true, GridColDataType_emu.Double     , 120, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "BADQTY"         , "불량 수량"           , true, GridColDataType_emu.Double     , 120, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"       , "단위"                , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MATLOTNO"       , "투입LOT"             , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "COMPONENT"      , "투입 품목"           , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "COMPONENTQTY"   , "투입 수량"           , true, GridColDataType_emu.Double     , 120, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "CUNITCODE"      , "투입 단위"           , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERCODE" , "작업장"              , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKSTATUSCODE" , "가동/비가동 상태"    , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKSTATUS"     , "가동/비가동 상태"    , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ERRORFLAG"      , "고장/정상 상태"      , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKER"         , "작업자"              , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKERNAME"     , "작업자명"            , true, GridColDataType_emu.VarChar    , 120, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDSTARTDATE"   , "최초 가동 시작 시간" , true, GridColDataType_emu.DateTime24 , 160, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDENDDATE"     , "작업 지시 종료 시간" , true, GridColDataType_emu.DateTime24 , 160, 130, Infragistics.Win.HAlign.Left, true, false);


                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);


                //콤보 박스 셋팅
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                
                //공장 콤보 박스
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");
              

                //공정 창고 콤보 박스
                dtTemp = _Common.GET_Workcenter_Code();
                Common.FillComboboxMaster(this.cboWorkCentercode, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

                BizTextBoxManager btManager = new BizTextBoxManager();
                btManager.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
        }

        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            try 
            {
                //작업 지시 사항 조회
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkcentercode = Convert.ToString(cboWorkCentercode.Value);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sOrderNo = Convert.ToString(txtOrderNO.Text);


                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19PP_ActureOutPut_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE"      , sPlantCode       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKCENTERCODE" , sWorkcentercode  , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STARTDATE"      , sStartDate       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ENDDATE"        , sEndDate         , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ORDERNO"        , sOrderNo         , DbType.String, ParameterDirection.Input)
                                          );
                grid1.DataSource = dtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();

            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally { helper.Close(); }
        }

        private void btnWorker_Click(object sender, EventArgs e)
        {
            // 작업자 등록 시작
            
            #region Validation Check
            // 작업 지시서가 있는 즉, 그리드에 나타난 행이 없을 경우 버튼 클릭 무효화
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            // 작업자를 선택하지 않았을 경우 버튼 클릭 무효화
            string sWorkerID = txtWorkerID.Text.ToString();

            if (sWorkerID == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            #endregion

            //작업자 등록 변수 지정
            string sOrderNo        = grid1.ActiveRow.Cells["ORDERNO"].Value.ToString();
            string sWorkcentercode = grid1.ActiveRow.Cells["WORKCENTERCODE"].Value.ToString();

            DBHelper helper = new DBHelper("", true);
            try
            {
                // 어떤 작업자가 어떤 작업 지시 번호를 어떤 작업장에서 담당할지를 입력.

                helper.ExecuteNoneQuery("19PP_ActureOutput_I2", CommandType.StoredProcedure
                                                              ,helper.CreateParameter("PLANTCODE"      ,"1000"           , DbType.String, ParameterDirection.Input)
                                                              ,helper.CreateParameter("WORKER"         , sWorkerID       , DbType.String, ParameterDirection.Input)
                                                              ,helper.CreateParameter("ORDERNO"        , sOrderNo        , DbType.String, ParameterDirection.Input)
                                                              ,helper.CreateParameter("WORKCENTERCODE" , sWorkcentercode , DbType.String, ParameterDirection.Input)
                                                              );
                if (helper.RSCODE=="S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                DoInquire();
            }
            catch (Exception ex)
            {
                ShowDialog("오류가 발생했습니다.", DC00_WinForm.DialogForm.DialogType.OK);
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
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemcode       = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotno          = Convert.ToString(txtInLotNo.Text);
                string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderno        = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode       = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);
                string sInFlag         = Convert.ToString(btnLotIn.Text);
                string sWorker         = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value); 

                if (sInFlag == "투입")
                {
                    sInFlag = "IN";
                }
                else sInFlag = "OUT";

                helper.ExecuteNoneQuery("19PP_ActureOutput_I1", CommandType.StoredProcedure
                                                              , helper.CreateParameter("PLANTCODE"       , "1000"           , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ITEMCODE"        , sItemcode        , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("LOTNO"           , sLotno           , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("WORKCENTERCODE"  , sWorkcenterCode  , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ORDERNO"         , sOrderno         , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("UNITCODE"        , sUnitCode        , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("INFLAG"          , sInFlag          , DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("MAKER"           , sWorker          , DbType.String, ParameterDirection.Input)
                                                              );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
                else
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                helper.Rollback();
            }
            finally
            { helper.Close(); }
            
        }

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop.Text = "비가동";
            }
            else
                btnRunStop.Text = "가동";
            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotno != "")
            {
                txtInLotNo.Text = sMatLotno;
                btnLotIn.Text = "투입취소";
            }
            else
            {
                txtInLotNo.Text = "";
                btnLotIn.Text = "투입";
            }
            txtWorkerID.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);
        }
       
        //가동 비가동 등록
        private void btnRunStop_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(grid1.ActiveRow.Cells["ERRORFLAG"].Value) == "Y")
            {
                ShowDialog("해당 작업장은 고장 상태입니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sStatus = "R";
                if (btnRunStop.Text == "비가동") sStatus = "S";
                helper.ExecuteNoneQuery("19PP_ActureOutput_U1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE"      , "1000"                                                               , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE" , Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value) , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO"        , Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value)        , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE"       , Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value)       , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("UNITCODE"       , Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value)      , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("STATUS"         , sStatus                                                              , DbType.String, ParameterDirection.Input)
                                                                    );

                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
                else
                { 
                    helper.Rollback();
                    ShowDialog("데이터 등록 중 오류가 발생하였습니다."+ helper.RSMSG , DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            { helper.Close(); }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            // 생산 실적 등록
            #region Validation Check
            // Validation Check
            if (this.grid1.ActiveRow == null) // 선택을 안한경우
            {
                ShowDialog("작업지시를 선택하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            #endregion

            #region Variables Dec
            double dProdQty = 0;   // 누적 양품수량
            double dErrorQty = 0;   // 누적 불량품수량
            double dTProdQty = 0;   // 입력 양품수량
            double dTErrorQty = 0;   // 입력 양품수량
            double dOrderQty = 0;   // 입력 양품수량
            double dInQty = 0;   // 투입 LOT 잔량

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
            #endregion

            #region Validation Check
            if (dInQty == 0)
            {
                ShowDialog("투입한 LOT의 잔량이 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if ((dTProdQty + dTErrorQty) == 0)
            {
                ShowDialog("실적 수량을 입력하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if (dOrderQty<(dProdQty+dErrorQty)+(dTProdQty+dTErrorQty))
            {
                ShowDialog("생산수량 및 불량 수량의 합계가 지시수량보다 많습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            #endregion

            DBHelper helper = new DBHelper("",true);
            try
            {
                helper.ExecuteNoneQuery("19PP_ActureOutput_U2", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE"      , "1000"                                                               , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE" , Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value) , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO"        , Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value)        , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE"       , Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value)       , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("UNITCODE"       , Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value)      , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("PRODQTY"        , dTProdQty                                                            , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("BADQTY"         , dTErrorQty                                                           , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("MATLOTNO"       , Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value)       , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("CITEMCODE"      , Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENT"].Value)      , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("CUNITCODE"      , Convert.ToString(this.grid1.ActiveRow.Cells["CUNITCODE"].Value)      , DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("생산 실적 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                //txtInLotNo.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK) ;
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnOrderClose_Click(object sender, EventArgs e)
        {
            //작업지시 종료
            #region Validation Check
            // Validation Check
            if (this.grid1.Rows.Count == 0) 
            {
                return;
            }
            if (this.grid1.ActiveRow == null) 
            {
                return;
            }
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("LOT 투입 취소 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                ShowDialog("비가동 등록 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            #endregion

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("19PP_ActureOutput_U3", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE"      , "1000"                                                               , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE" , Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value) , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO"        , Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value)        , DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("상태 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNo.Text = "";
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
        private void btnError_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(this.grid1.ActiveRow.Cells["ERRORFLAG"].Value) == "Y")
            {
                ShowDialog("해당 작업장은 이미 고장상태입니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
       
                helper.ExecuteNoneQuery("03PP_ActureOutput_U33", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE"      , "1000"                                                               , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE" , Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value) , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO"        , Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value)        , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE"       , Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value)       , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKER"         , Convert.ToString(this.grid1.ActiveRow.Cells["WORKER"].Value)         , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("MAKER"          , LoginInfo.UserID                                                     , DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
                else 
                {
                    ShowDialog(helper.RSCODE, DC00_WinForm.DialogForm.DialogType.OK);
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);

                    helper.Rollback();
                    
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
