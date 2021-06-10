#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : MM_StockOUT
//   Form Name    : 자재 생산 출고 관리
//   Name Space   : KFQS_Form
//   Created Date : 2021-06-09
//   Made By      : LJ
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
    public partial class MM_StockOUT : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public MM_StockOUT()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void MM_StockOUT_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",              "선택", true, GridColDataType_emu.CheckBox,   120, 120, Infragistics.Win.HAlign.Left,   true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",        "공장", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",     "입고일자", true, GridColDataType_emu.DateTime24, 300, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",         "품목", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME ",      "품목명", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO ",       "LOTNO", true, GridColDataType_emu.VarChar,    200, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",         "수량", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",         "단위", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",           "창고", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, " MAKER",         "입고자", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");    // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");  //콤보박스에 넣음 함수 이용해서
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");                                                             //그리드에 콤보박스

            rtnDtTemp = _Common.GET_ItemCodeFERT_Code("ROH");     //품목(조회)
            Common.FillComboboxMaster(this.cboItmeCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            rtnDtTemp = _Common.Standard_CODE("WHCODE","MINORCODE = 'WH003'"); //창고(조회,셀)
            Common.FillComboboxMaster(this.cboWhCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");  //콤보박스에 넣음 함수 이용해서
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");


            rtnDtTemp = _Common.Standard_CODE("STORAGELOCCODE","RELCODE1 = 'WH003'");        //출고저장 위치(조회)
            Common.FillComboboxMaster(this.cboWhlocation, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
           

            // 품목코드 
            //FP  : 완제품
            //OM  : 외주가공품
            //R/M : 원자재
            //S/M : 부자재(H / W)
            //SFP : 반제품
            //rtnDtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
            //UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #endregion
            // 품목코드 
            //FP  : 완제품
            //OM  : 외주가공품
            //R/M : 원자재
            //S/M : 부자재(H / W)
            //SFP : 반제품
            //rtnDtTemp = _Common.GET_ItemCodeFERT_Code("R/M");
            //Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            #endregion

            #region ▶ POP-UP ◀
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        //#endregion


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
                _GridUtil.Grid_Clear(grid1);  //그리드 초기화 하고 시작
                string sPlantCode      = Convert.ToString(cboPlantCode.Value);
                string sItemcode       = Convert.ToString(cboItmeCode.Value);
                string sMatLotNo       = txtMatLotNo.Text.ToString();
                string sWhCode         = Convert.ToString(cboWhCode.Value);
                string sWhlocation     = Convert.ToString(cboWhlocation.Value);
                string sStart = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEnd = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);



                rtnDtTemp = helper.FillTable("16MM_StockOUT_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",       sPlantCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ITEMCODE",        sItemcode,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("MATLOTNO",        sMatLotNo,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WHCODE",          sWhCode,     DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STORAGELOCCODE",  sWhlocation, DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE",       sStart,      DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",         sEnd,        DbType.String, ParameterDirection.Input)
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
        //public override void DoNew()
        //{
        //    base.DoNew();
        //    try
        //    {
        //        this.grid1.InsertRow();
        //        this.grid1.SetDefaultValue("PLANTCODE", this.plantCode);

        //        grid1.ActiveRow.Cells["PLANNO"].Activation      = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["CHK"].Activation         = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["ORDERNO"].Activation     = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["ORDERWORKER"].Activation = Activation.NoEdit;

        //        grid1.ActiveRow.Cells["MAKER"].Activation    = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["EDITOR"].Activation   = Activation.NoEdit;
        //        grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit;

        //    }
        //    catch (Exception ex)
        //    {
        //        ShowDialog(ex.ToString());    //오류 내역을 메세지박스로 볼수 있음!
        //    }
        //}
        ///// <summary>
        ///// ToolBar의 삭제 버튼 Click
        ///// </summary>
        //public override void DoDelete()
        //{   
        //   if(Convert.ToString(grid1.ActiveRow.Cells["CHK"].Value) == "1")
        //    {
        //        ShowDialog("작업지시 확정 내역을 취소후 삭제 하세요.", DialogForm.DialogType.OK);
        //        return;
        //    }
        //    base.DoDelete();
        //    grid1.DeleteRow();
        //}
        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
            // 그리드에 표현된 내용을 소스 바인딩에 포함한다.
            this.grid1.UpdateData();

            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();

            if (dtTemp == null) return;

            DBHelper helper = new DBHelper("", true);
            try
            {
                this.Focus();

                if (this.ShowDialog("출고를 신청하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

                foreach (DataRow drRow in dtTemp.Rows)
                {
                    switch (drRow.RowState)
                    {
                        
                        case DataRowState.Modified:
                            #region 수정
                            
                            helper.ExecuteNoneQuery("16MM_StockOUT_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE",      drRow["PLANTCODE"].     ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("LOTNO",          drRow["MATLOTNO"].      ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE",       drRow["ITEMCODE"].      ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("QTY",            drRow["STOCKQTY"].           ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("UNITCODE",       drRow["UNITCODE"].      ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WHCODE",         drRow["WHCODE"].        ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("STORAGELOCCODE", drRow["WHCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKERID",       drRow["MAKER"].      ToString(), DbType.String, ParameterDirection.Input)
                                                  );


                            #endregion
                            break;
                    }
                }
                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("R00002", DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
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




