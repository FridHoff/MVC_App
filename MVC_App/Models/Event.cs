using System;
 
namespace MVC_App.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }            // название событие
        public DateTime EventDate { get; set; }     // дата и время событие
    }
}