using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EventosPeloBrasil.Models;

namespace EventosPeloBrasil.Controllers
{
    public class EventosApiController : ApiController
    {
        private eventospelobrasilEntities db = new eventospelobrasilEntities();

        // GET: api/EventosApi
        public IQueryable<eventos> Geteventos()
        {
            return db.eventos;
        }

        // GET: api/EventosApi/5
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Geteventos(int id)
        {
            eventos eventos = db.eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            return Ok(eventos);
        }

        // PUT: api/EventosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puteventos(int id, eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventos.Id)
            {
                return BadRequest();
            }

            db.Entry(eventos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventosExists(id))
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

        // POST: api/EventosApi
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Posteventos(eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.eventos.Add(eventos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventos.Id }, eventos);
        }

        // DELETE: api/EventosApi/5
        [ResponseType(typeof(eventos))]
        public IHttpActionResult Deleteeventos(int id)
        {
            eventos eventos = db.eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            db.eventos.Remove(eventos);
            db.SaveChanges();

            return Ok(eventos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool eventosExists(int id)
        {
            return db.eventos.Count(e => e.Id == id) > 0;
        }
    }
}