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
using Accessor.Models;

namespace Accessor.Controllers
{
    public class challenges : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/challenges
        public IQueryable<Challenge> GetChallenges()
        {
            return db.Challenges;
        }

        // GET: api/challenges/5
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult GetChallenge(Guid id)
        {
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return NotFound();
            }

            return Ok(challenge);
        }

        // PUT: api/challenges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChallenge(Guid id, Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != challenge.Id)
            {
                return BadRequest();
            }

            db.Entry(challenge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(id))
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

        // POST: api/challenges
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult PostChallenge(Challenge challenge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Challenges.Add(challenge);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ChallengeExists(challenge.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = challenge.Id }, challenge);
        }

        // DELETE: api/challenges/5
        [ResponseType(typeof(Challenge))]
        public IHttpActionResult DeleteChallenge(Guid id)
        {
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return NotFound();
            }

            db.Challenges.Remove(challenge);
            db.SaveChanges();

            return Ok(challenge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChallengeExists(Guid id)
        {
            return db.Challenges.Count(e => e.Id == id) > 0;
        }
    }
}