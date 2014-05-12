using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using skjatextar.BLL;
using skjatextar.Models;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace skjatextar.Controllers
{
	public class HomeController : ApplicationController
	{
 
        public ActionResult Index()
		{
           
            var bll = new SkjatextiRepository();
            var query = new BLL.SkjatextiRepository().GetTopTenSrt();
           
      
            //return View(users);
            return View(query);
		}
        
		public ActionResult About()
		{
			//ViewBag.Message = "Your application description page.";
            //var bll = new SkjatextiBLL();
            //var request = bll.GetRequests();
            //return View(request);

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
        
       //[HttpGet]
        public ActionResult Upload()
        {
            
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
        */
        /*private void frmMain_Load(object sender, EventArgs e)
        {
            objConn.ConnectionString = strSqlConn; //set connection params
            FillDataGrid(gridViewMain, strQuery_AllAttachments);
        }
        */
        /*private void FillDataGrid(DataGridView objGrid, string strQuery)
        {
            DataTable tbl1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = objConn;  // use connection object
            cmd1.CommandText = strQuery; // set query to use
            adapter1.MissingSchemaAction = MissingSchemaAction.AddWithKey;  //grab schema
            adapter1.SelectCommand = cmd1; //
            adapter1.Fill(tbl1);  // fill the data table as specified
            objGrid.DataSource = tbl1;  // set the grid to display data
        }*/
        
        /*private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() != DialogResult.Cancel)
            {
                CreateAttachment(ofdMain.FileName);  //upload the attachment
            }
            FillDataGrid(gridViewMain, strQuery_AllAttachments);  // refresh grid
        }*/

       /*[HttpPost]
       public ActionResult Upload(string strFile)
        {
             SqlConnection objConn = new SqlConnection();
            string strQuery_AllAttachments_AllFields = "select * from [tblAttachments]";
            
            SqlDataAdapter objAdapter =
                new SqlDataAdapter(strQuery_AllAttachments_AllFields, objConn);
            objAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            SqlCommandBuilder objCmdBuilder = new SqlCommandBuilder(objAdapter);
            DataTable objTable = new DataTable();
            FileStream objFileStream =
                new FileStream(strFile, FileMode.Open, FileAccess.Read);
            int intLength = Convert.ToInt32(objFileStream.Length);
            byte[] objData;
            objData = new byte[intLength];
            DataRow objRow;
            string[] strPath = strFile.Split(Convert.ToChar(@"\"));
            objAdapter.Fill(objTable);

            objFileStream.Read(objData, 0, intLength);
            objFileStream.Close();

            objRow = objTable.NewRow();
            //clip the full path - we just want last part!
            objRow["fileName"] = strPath[strPath.Length - 1];
            objRow["fileSize"] = intLength / 1024; // KB instead of bytes
            objRow["attachment"] = objData;  //our file
            objTable.Rows.Add(objRow); //add our new record
            objAdapter.Update(objTable);

            return RedirectToAction("Index");
        }

        /*public static void SaveFile(string name, string contentType,
            int size, byte[] data)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                OpenConnection(connection);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 0;

                string commandText = "INSERT INTO Files VALUES(@Name, @ContentType, ";
                commandText = commandText + "@Size, @Data)";
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@ContentType", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@size", SqlDbType.Int);
                cmd.Parameters.Add("@Data", SqlDbType.VarBinary);

                cmd.Parameters["@Name"].Value = name;
                cmd.Parameters["@ContentType"].Value = contentType;
                cmd.Parameters["@size"].Value = size;
                cmd.Parameters["@Data"].Value = data;
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }*/
	}
}