sing Microsoft.AspNetCore.Mvc;
using prog_part_2.Database;
using prog_part_2.Models;
using System.Data.SqlClient;


namespace prog_part_2.Controllers
{

    [SessionCheck] 
        [Route("api/[controller]")]
        [ApiController]
        public class EnrolmentsController : ControllerBase
        {//start of class
            
        
            Enrolments enrol1;
            store_info store = new store_info();
            get_info get = new get_info();

        
            [HttpGet]
            public string getEnrolments(string userID)
            {//start of method
             //if organiser - views all enrolments for his events, if participant - views own
                return get.get_enrolments_info(userID);
            }//end of method

        
            [HttpPost]
            public string addEnrolment(string userID, string eventID, string categoryID, string enrolmemtDate, string status)
            {//start of method
                enrol1 = new Enrolments()
                {
                    userID = Convert.ToInt32(userID),
                    eventID = Convert.ToInt32(eventID),
                    categoryID = Convert.ToInt32(categoryID),
                    enrolmemtDate = enrolmemtDate,
                    status = status
                };
                return store.store_enrolments_info(enrol1.userID.ToString(), enrol1.eventID.ToString(), enrol1.categoryID.ToString(), enrol1.enrolmemtDate, enrol1.status);
            }//end of method

            [HttpPut]
            public string updateEnrolment(string userID, string enrolmentID, string status)
            {//start of method
                string message = "";
                try
                {
                    using (System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(@"Data source=(localdb)\api_both;database=races_prog;"))
                    {
                        connect.Open();
                        System.Data.SqlClient.SqlCommand check = new System.Data.SqlClient.SqlCommand(@"select role from Users where userID='" + userID + "'", connect);
                        System.Data.SqlClient.SqlDataReader r = check.ExecuteReader();
                        string role = ""; if (r.Read()) role = r["role"].ToString(); r.Close();
                        if (role != "participant") return "Access denied - participants only can update own enrolment";

                        string query = @"update Enrolments set status='" + status + "' where enrolmentID='" + enrolmentID + "' and userID='" + userID + "'";
                        System.Data.SqlClient.SqlCommand run_query = new System.Data.SqlClient.SqlCommand(query, connect);
                        run_query.ExecuteNonQuery();
                        message = "enrolment updated successfully";
                        connect.Close();
                    }
                }
                catch (Exception error) { message = error.Message; }
                return message;
            }//end of method

            [HttpDelete]
            public string deleteEnrolment(string userID, string enrolmentID)
            {//start of method
                string message = "";
                try
                {
                    using (System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(@"Data source=(localdb)\events_guide;database=Race;"))
                    {
                        connect.Open();
                        string query = @"delete from Enrolments where enrolmentID='" + enrolmentID + "' and userID='" + userID + "'";
                        System.Data.SqlClient.SqlCommand run_query = new System.Data.SqlClient.SqlCommand(query, connect);
                        run_query.ExecuteNonQuery();
                        message = "enrolment deleted successfully";
                        connect.Close();
                    }
                }
                catch (Exception error) { message = error.Message; }
                return message;
            }//end of method

        }//end of class
    }//end of namespace





    
