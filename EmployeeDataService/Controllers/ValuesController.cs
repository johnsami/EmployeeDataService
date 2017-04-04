using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataLib;
namespace EmployeeDataService.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values

        public IEnumerable<Customer> Get(string City = "all")
        {
            using (EmployeeDataLib.NORTHWNDEntities EntityObj = new NORTHWNDEntities())
            {


                return EntityObj.Customers.Where(C => City == "all" || C.City == City).Take(10).ToList();
            }
        }


        // GET api/values/5
        //public HttpResponseMessage Get(string id)
        //{
        //    using (EmployeeDataLib.NORTHWNDEntities EntityObj = new NORTHWNDEntities())
        //    {
        //        var Entity = EntityObj.Customers.Where(C => C.CustomerID == id).FirstOrDefault();
        //        if (Entity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, Entity);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
        //        }

        //    }
        //}

        // POST api/values

        public HttpResponseMessage Post([FromBody]Customer CustomerObj)
        {
            using (var Context = new NORTHWNDEntities())
            {
                try
                {
                    Context.Customers.Add(CustomerObj);
                    Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, CustomerObj);
                    message.Headers.Location = new Uri(Request.RequestUri + CustomerObj.CustomerID);
                    return message;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }

            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(string id)
        {
            using (var Context = new NORTHWNDEntities())
            {
                var Custobj = Context.Customers.Where(C => C.CustomerID == id).FirstOrDefault();
                if (Custobj != null)
                {
                    Context.Customers.Remove(Custobj);
                    Context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
                Context.SaveChanges();
            }
        }
    }
}
