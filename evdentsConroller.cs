using Microsoft.AspNetCore.Mvc;
using prog_part_2.Database;
using prog_part_2.Models;
using System.Data.SqlClient;
namespace prog_part_2.Controllers
{
    [SessionCheck]
        [Route("api/[controller]")]
        [ApiController]
        public class EventsController : ControllerBase
        {
            
            

            Events event1;
            store_info store = new store_info();
            get_info get = new get_info();

            [HttpGet] public string getEvents() { return get.get_events_info(); }

            [HttpPost]
            public string addEvent(string userID, string EventName, string description, string EventDate, string location, string eventType)
            {
                event1 = new Events() { userID = Convert.ToInt32(userID), EventName = EventName, description = description, EventDate = EventDate, location = location, eventType = eventType };
                return store.store_events_info(event1.userID.ToString(), event1.EventName, event1.description, event1.EventDate, event1.location, event1.eventType);
            }
        }
    }
