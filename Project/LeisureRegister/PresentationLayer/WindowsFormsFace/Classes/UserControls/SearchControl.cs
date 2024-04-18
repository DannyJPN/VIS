using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.BusinessLayerLib.Classes.DataContainers;

namespace WindowsFormsFace.Classes.UserControls
{
    public partial class SearchControl : GenericControl
    {
        protected TableLayoutPanel searchboard;
        protected Button b_searcher;
        protected List<TextBox> tb_crits;
        protected List<Label> lb_crits;

        protected DataGridView dgw_searchresult;
        protected Label lb_controlname;
        protected int userID;
        public SearchControl()
        {
            InitializeComponent();
            searchboard = new TableLayoutPanel();
            searchboard.RowCount = 4;
            
            searchboard.Parent = this;
            searchboard.Width = Width;
            searchboard.Height = Height /6;
            searchboard.Top = 0;
            searchboard.Left = 0;
           
            b_searcher = new Button() { Text="Vyhledat",Name="b_searcher"};
            dgw_searchresult = new DataGridView()
            {
                Name = "dgw_searchresult",
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                ColumnHeadersVisible = true,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                ScrollBars = ScrollBars.Vertical,
                Top = searchboard.Height + 10,
                Left = 0,
                Width = Width,
                Height = Height - searchboard.Height - 10,
                ReadOnly = true,
                Visible = true,
                Parent = this
                
            };
            dgw_searchresult.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lb_controlname = new Label() { Text = "Vyhledání", Name = "lb_controlname" };
              tb_crits=new List<TextBox>();
        lb_crits = new List<Label>();

    }

        private void SearchControl_Load(object sender, EventArgs e)
        {
            this.Size = this.Parent.Size;
        }
    }
}
