using Microsoft.AspNetCore.Mvc.Filters;
using StudentsAPI.Models.v2;
using StudentsAPI.Services.v2;

namespace StudentsAPI.Filters
{
    public class EventsLoggingFilter : ResultFilterAttribute
    {
        public EventsLoggingFilter()
        {

        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var currentRequest = context.HttpContext.Request;
            var eventsService = context.HttpContext.RequestServices.GetService(typeof(IEventsService));

            Event executedEvent = new Event
            {
                Path = currentRequest.Path,
                Type = currentRequest.Method,
                HttpStatusCode = context.HttpContext.Response.StatusCode

            };

            ((IEventsService)eventsService).Add(executedEvent);
        }
    }
}
