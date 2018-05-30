using CrmApi.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace CrmApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    
    public class ServersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Servers
        public IQueryable<TradingServer> GetTradingServers()
        {
            var tradingServers = db.TradingServers.Include(t => t.Brands);
            return tradingServers;
            
        }

        // GET: api/Servers/5
        [ResponseType(typeof(TradingServer))]
        public IHttpActionResult GetTradingServer(int id)
        {
            TradingServer tradingServer = db.TradingServers.Find(id);
            if (tradingServer == null)
            {
                return NotFound();
            }

            return Ok(tradingServer);
        }

        // PUT: api/Servers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTradingServer(int id, TradingServer tradingServer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tradingServer.serverId)
            {
                return BadRequest();
            }

            db.Entry(tradingServer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradingServerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Servers
        [ResponseType(typeof(TradingServer))]
        public IHttpActionResult PostTradingServer(TradingServer tradingServer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TradingServers.Add(tradingServer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tradingServer.serverId }, tradingServer);
        }

        // DELETE: api/Servers/5
        [ResponseType(typeof(TradingServer))]
        public IHttpActionResult DeleteTradingServer(int id)
        {
            TradingServer tradingServer = db.TradingServers.Find(id);
            if (tradingServer == null)
            {
                return NotFound();
            }

            db.TradingServers.Remove(tradingServer);
            db.SaveChanges();

            return Ok(tradingServer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TradingServerExists(int id)
        {
            return db.TradingServers.Count(e => e.serverId == id) > 0;
        }
    }
}