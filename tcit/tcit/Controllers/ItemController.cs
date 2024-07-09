using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tcit.DataAccesLayer;
using tcit.Models;
using tcit.Repository;

namespace tcit.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemController : ApiController
    {
        private IItemService itemService;

        public ItemController()
        {
            this.itemService = new ItemDal(); 
        }

        // GET: api/GetAll
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAllItems(string nombre = null)
        {
            try
            {
                var items = itemService.GetAllItems(nombre);
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST: api/AddItem
        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage AddItem([FromBody] Item item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Item data is required");
            }

            try
            {
                bool isAdded = itemService.AddItem(item);
                if (isAdded)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, item);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to add item");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE: api/RemoveItem/5
        [HttpDelete, Route("Remove/{id:int}")]
        public HttpResponseMessage DeleteItem(int id)
        {
            try
            {
                bool isDeleted = itemService.DeleteItem(id);
                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
