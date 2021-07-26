#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_LotTracking
//   Form Name    : Lot Tracking
//   Name Space   : KFQS_Form
//   Created Date : 2021/07
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
    public partial class PP_LotTracking : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_LotTracking()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_LotTracking_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장",           true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",         "작업지시번호",   true, GridColDataType_emu.VarChar,   160, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장",         true, GridColDataType_emu.VarChar,   140, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME",  "작업장명",       true, GridColDataType_emu.VarChar,   120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "생산품목",       true, GridColDataType_emu.VarChar,   150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",        "생산품목명",     true, GridColDataType_emu.VarChar,   150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",           "생산LOT",        true, GridColDataType_emu.VarChar,   160, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",         "생산수량",       true, GridColDataType_emu.Double,    100, 120, Infragistics.Win.HAlign.Right,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",        "단위",           true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CLOTNO",          "투입LOT",        true, GridColDataType_emu.VarChar,   160, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CITEMCODE",       "투입품목",       true, GridColDataType_emu.VarChar,   150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CITEMNAME",       "투입품명",       true, GridColDataType_emu.VarChar,   150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INQTY",           "투입수량",       true, GridColDataType_emu.Double,    100, 120, Infragistics.Win.HAlign.Right,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE",       "단위",           true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",        "등록일시",       true, GridColDataType_emu.DateTime24,160, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",           "등록자",         true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.SetInitUltraGridBind(grid1);

            this.grid1.DisplayLayout.Override.MergedCellContentArea                         = MergedCellContentArea.Default;
            this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle       = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ORDERNO"].MergedCellStyle         = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle  = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle  = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle        = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle        = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["LOTNO"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["PRODQTY"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["UNITCODE"].MergedCellStyle = MergedCellStyle.Always;


            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME"); 
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
                string sLotNo      = Convert.ToString(txtLotNo.Text);


                rtnDtTemp = helper.FillTable("00PP_LotTracking_S1", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)  
                                                                   , helper.CreateParameter("LOTNO",     sLotNo,     DbType.String, ParameterDirection.Input)
                                                                   );
                this.ClosePrgForm();
                this.grid1.DataSource = rtnDtTemp;
                if (this.grid1.Rows.Count != 0)
                {
                    for (int i = 0; i < this.grid1.Rows.Count; i++)
                    {
                        if (Convert.ToString(this.grid1.Rows[i].Cells["LOTNO"].Value) == sLotNo)
                        {
                            this.grid1.Rows[i].Activated = true;                   // 조회를 하고자 하는 LOT 의 행 선택
                            return;
                        }
                    }
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

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            //// 그리드 컬럼간 머지 합병 기능 적용
            CustomMergedCellEvalutor CM1 = new CustomMergedCellEvalutor("ITEMNAME", "LOTNO"); // 아이템 이름과 LOT NO 관계처럼 보였으면 한다.
            // 아래의 데이터 컬럼은
            e.Layout.Bands[0].Columns["PRODQTY"].MergedCellEvaluator  = CM1;
            e.Layout.Bands[0].Columns["UNITCODE"].MergedCellEvaluator = CM1; 

        }
    }
}




