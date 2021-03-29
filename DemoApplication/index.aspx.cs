//khang tran
//iqvia assignment
//comment : api only give 25 states. not sure if i called the api wrong.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemoApplication
{
    public partial class index : System.Web.UI.Page
    {
        string json = (new WebClient()).DownloadString("https://data.cdc.gov/resource/9mfq-cb36.json"); //turn json data into string

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                //gvCdcData.DataSource = info;
                //gvCdcData.DataSource = JsonConvert.DeserializeObject<DataTable>(json); //deserializes this to datatable
                //gvCdcData.DataBind();
                lblTotalDeath.Text += GetSum().ToString("#,##0");
                PopulateDdl(JsonToList());
                DisplayGridview(JsonToList());
            }
        }

        //total death within api call. 
        //for some reason api only return 25 states and not 50 as displayed on cdc website.
        protected void gvCdcData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            decimal tempSum = GetSum();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string val1 = e.Row.Cells[2].Text; //Gets the value in Column 1
                Label lblPercent = (Label)e.Row.Cells[3].FindControl("lblPercent"); //

                if (val1 == string.Empty || val1 == "")
                {
                    val1 = "0";
                }

                decimal percent = Math.Round((decimal.Parse(val1) / tempSum * 100), 2);

                lblPercent.Text = percent.ToString() + " %";
            }

        }

        //convert json string to list
        public List<CovidData> JsonToList()
        {
            List<CovidData> convertedData = JsonConvert.DeserializeObject<List<CovidData>>(json); //deserializes the json string into a collection

            var latestData = from i in convertedData
                             group i by i.state into s
                             select s.OrderByDescending(t => t.submission_date).FirstOrDefault(); //get the highest date of each state in the api call


            List<CovidData> listLatestData = latestData.OrderByDescending(o => o.tot_death).ToList();

            return listLatestData;
            /*ddlStates.DataSource = states;
            ddlStates.DataBind();
            gvCdcData.DataSource = listLatestData;
            gvCdcData.DataBind();*/
        }

        //display only selected state
        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CovidData> dataList = JsonToList();
            var res = dataList.Where(x => x.state.Contains(ddlStates.Text)).ToList();
            gvCdcData.DataSource = res;
            gvCdcData.DataBind();
        }

        //sum the total death in api call
        public decimal GetSum()
        {
            List<CovidData> tempList = JsonToList();
            decimal sum = tempList.Sum(x => decimal.Parse(x.tot_death));
            return sum;
        }

        //set datasource and bind data to gridview
        public void DisplayGridview(List<CovidData> cvData)
        {
            gvCdcData.DataSource = cvData;
            gvCdcData.DataBind();
        }

        //populate dropdownlist with states from list
        public void PopulateDdl(List<CovidData> cvData)
        {
            List<string> states = new List<string>();
            foreach (CovidData c in cvData)
            {
                states.Add(c.state);
            }
            ddlStates.DataSource = states;
            ddlStates.DataBind();
        }
    }
}