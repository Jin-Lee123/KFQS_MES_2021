﻿#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : 
//   Form Name    : 생산출고 등록/취소
//   Name Space   : 
//   Created Date : 
//   Made By      : 
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region <USING AREA>
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DC00_assm;
using DC_POPUP;
using DC00_WinForm;

#endregion

namespace KFQS_Form
{
    public partial class PP_StockPP : DC00_WinForm.BaseMDIChildForm
    {
        #region <MEMBER AREA>

        DataTable table         = new DataTable();
        DataTable rtnDtTemp     = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();
        #endregion

        #region < CONSTRUCTOR >

        public PP_StockPP()
        {
            InitializeComponent();
        }
        #endregion

        #region  PP_StockPP
        private void PP_StockPP_Load(object sender, EventArgs e)
        {
            //그리드 객체 생성
            #region 
            _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",             "원자재출고취소", false, GridColDataType_emu.CheckBox,170, 100, Infragistics.Win.HAlign.Center, true, true, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장",           false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "품목",           false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",        "품목명",         false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,   true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE",        "품목구분",       false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",        "LOTNO",          false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",          "입고창고",       false, GridColDataType_emu.VarChar,  70, 100, Infragistics.Win.HAlign.Right,  true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",        "재고수량",       false, GridColDataType_emu.VarChar,  50, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",        "단위",           false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region 콤보박스
            Common _Common = new Common();
            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("ITEMTYPE ");  // 품목구분
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            string sPlantCode = Convert.ToString(this.cboPlantCode.Value);
            this.cboPlantCode.Value = "1000";
            #endregion
        }
        #endregion  PP_StockPP_Load

        #region <TOOL BAR AREA >




        public override void DoInquire()
        {   
         
            this._GridUtil.Grid_Clear(grid1);

            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sItemCode = Convert.ToString(cboItemCode.Value);
                string sLotNo     = this.txtLotNo.Text;

                rtnDtTemp = helper.FillTable("16PP_StockPP_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE",  sPlantCode,      DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMCODE",   sItemCode,       DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("MATLOTNO",   sLotNo,          DbType.String, ParameterDirection.Input)
                                              );
                
                grid1.DataSource = rtnDtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

       
        #region <METHOD AREA>
        #endregion
        public override void DoSave()
        {
            
            DataTable dt = new DataTable();
            dt = grid1.chkChange();
            if (dt == null)
                return;
            DBHelper helper = new DBHelper("", false);
            try
            {
                if (this.ShowDialog("출고를 취소 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    helper.ExecuteNoneQuery("16PP_StockPP_I1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", Convert.ToString(dt.Rows[i]["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO",     Convert.ToString(dt.Rows[i]["MATLOTNO"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE",  Convert.ToString(dt.Rows[i]["ITEMCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("QTY",       Convert.ToString(dt.Rows[i]["STOCKQTY"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UNITCODE",  Convert.ToString(dt.Rows[i]["UNITCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKERID", this.WorkerID, DbType.String, ParameterDirection.Input));

                    if (helper.RSCODE == "E")
                    {
                        this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
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
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

    }
}

