#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_STockHALBRec
//   Form Name    : 재공 재고 입출고 이력 조회
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

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class PP_STockHALBRec : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_STockHALBRec()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_STockHALBRec_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",      true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",          "LOTNO",     true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목코드",  true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품명",      true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTDATE",      "입/출일자", true, GridColDataType_emu.VarChar,    150, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",    true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTCODE",      "입출유형",  true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTFLAG",      "입출구분",  true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTQTY",       "입출수량",  true, GridColDataType_emu.Double,     100, 120, Infragistics.Win.HAlign.Right,true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BASEUNIT",       "단위",      true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",          "등록자",    true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",       "등록일시",  true, GridColDataType_emu.DateTime24, 100, 120, Infragistics.Win.HAlign.Left, true, false);

            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀

            Common _Common = new Common();
            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("UNITCODE");  //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "BASEUNIT", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_Workcenter_Code();  //작업장
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("INOUTCODE");  //입출 유형
            UltraGridUtil.SetComboUltraGrid(this.grid1, "INOUTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("INOUTTYPE");  //입출 구분
            UltraGridUtil.SetComboUltraGrid(this.grid1, "INOUTFLAG", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Get_ItemForCus("1000");  //품목 코드(품목 전체)
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            #endregion

            #region ▶ POP-UP ◀

            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode  = Convert.ToString(cboPlantCode.Value);
                string sItemCode   = Convert.ToString(cboItemCode.Value);
                string sLotNo      = Convert.ToString(txtLotNo.Text);
                string sStartDate  = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate    = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);

                rtnDtTemp = helper.FillTable("16PP_STockHALBRec_S1", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)                  
                                                                   , helper.CreateParameter("ITEMCODE",  sItemCode,  DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("LOTNO",     sLotNo,     DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                                                   , helper.CreateParameter("ENDDATE",   sEndDate,   DbType.String, ParameterDirection.Input)
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
        /// <summary>
        /// ToolBar의 신규 버튼 클릭
        /// </summary>
        public override void DoNew()
        {
            
        }
        /// <summary>
        /// ToolBar의 삭제 버튼 Click
        /// </summary>
        public override void DoDelete()
        {   
           
        }
        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
        }
        #endregion
        
    }
}




