using Microsoft.AspNetCore.Mvc;
using prog_part_2.Database;
using prog_part_2.Models;
using System.Data.SqlClient;


namespace prog_part_2.Controllers
{
    [SessionCheck]
    
        [Route("api/[controller]")]
        [ApiController]
        public class eventRouteController : ControllerBase
        {//start of class
           
            eventRoute route1;
            store_info store = new store_info();
            get_info get = new get_info();

            [HttpGet]
            public string getRoute()
            {//start of method
                string message = "";
                try
                {
                    using (System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(@"Data source=(localdb)\api_both;database=races_prog;"))
                    {
                        connect.Open();
                        string query = @"select * from eventRoute;";
                        System.Data.SqlClient.SqlCommand run_query = new System.Data.SqlClient.SqlCommand(query, connect);
                        System.Data.SqlClient.SqlDataReader found_data = run_query.ExecuteReader();
                        while (found_data.Read())
                        {
                            message += found_data["routeName"].ToString() + " Distance:" + found_data["distance"].ToString() + " | ";
                        }
                        if (message == "") message = "no route found";
                        connect.Close();
                    }
                }
                catch (Exception error) { message = error.Message; }
                return message;
            }//end of method

            [HttpPost]
            public string addRoute(string userID, string eventID, string routeName, string distance)
            {//start of method
                route1 = new eventRoute()
                {
                    eventID = Convert.ToInt32(eventID),
                    routeName = routeName,
                    distance = distance
                };
                return store.store_eventRoute_info(userID, route1.eventID.ToString(), route1.routeName, route1.distance);
            }//end of method

            [HttpPut]
            public string updateRoute(string userID, string routeID, string routeName, string distance)
            {//start of method
                string message = "";
                try
                {
                    using (System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(@"Data source=(localdb)\api_both;database=Race;"))
                    {
                        connect.Open();
                        System.Data.SqlClient.SqlCommand check = new System.Data.SqlClient.SqlCommand(@"select role from Users where userID='" + userID + "'", connect);
                        System.Data.SqlClient.SqlDataReader r = check.ExecuteReader();
                        string role = ""; if (r.Read()) role = r["role"].ToString(); r.Close();
                        if (role != "organiser") return "Access denied - organisers only";

                        string query = @"update eventRoute set routeName='" + routeName + "', distance='" + distance + "' where routeID='" + routeID + "'";
                        System.Data.SqlClient.SqlCommand run_query = new System.Data.SqlClient.SqlCommand(query, connect);
                        run_query.ExecuteNonQuery();
                        message = "route updated successfully";
                        connect.Close();
                    }
                }
                catch (Exception error) { message = error.Message; }
                return message;
            }//end of method

        }//end of class
    }//end of namespace
