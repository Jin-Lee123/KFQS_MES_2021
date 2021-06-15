#region < HEADER AREA >
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
    public partial class PP_STockHALB : DC00_WinForm.BaseMDIChildForm
    {
        #region <MEMBER AREA>

        DataTable table         = new DataTable();
        DataTable rtnDtTemp     = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();
        #endregion

        #region < CONSTRUCTOR >

        public PP_STockHALB()
        {
            InitializeComponent();
        }
        #endregion

        #region  PP_STockHALB
        private void PP_STockHALB_Load(object sender, EventArgs e)
        {
            //그리드 객체 생성
            #region 
            
            _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",        false, GridColDataType_emu.CheckBox, 70, 100, Infragistics.Win.HAlign.Center, true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목",        false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품목명",      false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE",       "품목구분",    false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",       "LOTNO",       false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",      false, GridColDataType_emu.VarChar,  100, 100, Infragistics.Win.HAlign.Right,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",    false, GridColDataType_emu.VarChar,  100, 100, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",       "재고수량",    false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",        false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region 콤보박스
            Common _Common = new Common();
            
            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //공장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("ITEMTYPE");  //타입
            Common.FillComboboxMaster(this.cboItemType, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMTYPE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #endregion
        }
        #endregion  PP_STockHALB_Load

        #region <TOOL BAR AREA >
        public override void DoInquire()
        {   
         
            this._GridUtil.Grid_Clear(grid1);

            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sItemType = Convert.ToString(cboItemType.Value);
                string sLotNo    = this.txtLotNo.Text;
               
                rtnDtTemp = helper.FillTable("16PP_STockHALB_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMTYPE",  sItemType,  DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("MATLOTNO",  sLotNo,     DbType.String, ParameterDirection.Input)
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

    }
}

