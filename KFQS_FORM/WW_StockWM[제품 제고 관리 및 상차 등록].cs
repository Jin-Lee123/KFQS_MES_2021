
#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WW_StockWM
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
using System.Drawing;
using System.Security;
using DC_POPUP;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class WW_StockWM : DC00_WinForm.BaseMDIChildForm
    {
        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion
        #region < CONSTRUCTOR >
        public WW_StockWM()
        {
            InitializeComponent();
        }
        #endregion
        #region < FORM EVENTS >
        private void WW_StockWM_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀

            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "상차등록", true, GridColDataType_emu.CheckBox,  100, 120, Infragistics.Win.HAlign.Left,  true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",     true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "SHIPFLAG",  "상차여부", true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목",     true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품목명",   true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",     "LOTNO",    true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",    "입고창고", true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "재고수량", true, GridColDataType_emu.Double,    100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BASEUNIT",  "단위",     true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INDATE",    "입고일자", true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "등록일시", true, GridColDataType_emu.DateTime24,100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",     "등록자",   true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp =_Common.Standard_CODE("SHIPFLAG");  //상차여부
            Common.FillComboboxMaster(this.cboShipFlag, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "SHIPFLAG", rtnDtTemp, "CODE_ID", "CODE_NAME");
            

            rtnDtTemp = _Common.Standard_CODE("WHCODE"); //입고창고
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            #endregion

            #region ▶ POP-UP ◀
            // 품목 팝업 호출
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtItemCode_H,txtItemName_H, "ITEM_MASTER", new object[] { "1000", "" });
            //작업자 팝업 호출
            btbManager.PopUpAdd(txtWorker_H, txtWorkerName_H, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            // 거래처 팝업 호출
            btbManager.PopUpAdd(txtCustCode_H, txtCustName_H, "CUST_MASTER", new object[] { cboPlantCode_H, "", "", "" });
            #endregion
        
        }
        #endregion

      
        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sItemCode  = this.txtItemName_H.Text;
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);
                string sLotNo     = this.txtLotNo.Text;
                string sShipFlag  = Convert.ToString(cboShipFlag.Value);

                rtnDtTemp = helper.FillTable("16WW_StockWM_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ITEMCODE",  sItemCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",   sEndDate,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("LOTNO",     sLotNo,     DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("SHIPFLAG",  sShipFlag,  DbType.String, ParameterDirection.Input)
                                    );
                grid1.DataSource = rtnDtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();
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
       
        public override void DoNew()
        { 
        }
        
        public override void DoDelete()
        {   
           
        }
       
        public override void DoSave()
        {
            DataTable dt = new DataTable();
            dt = grid1.chkChange();
            
            if (dt == null) return;
            //작업자 등록 여부 확인
            string sWorkId = txtWorker_H.Text.ToString();
            if (sWorkId == "")
            {
                ShowDialog("작업자를 등록 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            //거래처 등록 여부 확인
            string sCustCode = txtCustCode_H.Text.ToString();
            if (sCustCode == "")
            {
                ShowDialog("거래처를 등록 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }


            DBHelper helper = new DBHelper("", false);
            try
            {
                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;
                    helper.ExecuteNoneQuery("16WM_StockWM_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", Convert.ToString(dt.Rows[i]["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE",  Convert.ToString(dt.Rows[i]["ITEMCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO",     Convert.ToString(dt.Rows[i]["LOTNO"]),    DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CARNO",     Convert.ToString(txtCarNo.Value),         DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CUSTCODE",  Convert.ToString(txtCustCode_H.Value),    DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKER",    Convert.ToString(txtWorker_H.Value),      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STOCKQTY",  Convert.ToString(dt.Rows[i]["STOCKQTY"]),    DbType.String, ParameterDirection.Input)
                                            );
                    
                    if (helper.RSCODE == "E")
                    {
                        this.ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                        helper.Rollback();
                        return;
                    }
                }

                helper.Commit();
                this.ShowDialog("데이터가 저장 되었습니다.", DialogForm.DialogType.OK);
                this.ClosePrgForm();
                DoInquire();
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
        #endregion

       
    }
}




