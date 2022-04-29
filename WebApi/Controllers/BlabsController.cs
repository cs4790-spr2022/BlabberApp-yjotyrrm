using BlabberApp.Domain.Common.Interfaces;
using DataStore.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlabsController : Controller
    {
        private readonly ILogger<BlabsController> _logger;
        private readonly IBlabRepository _repo;
        private readonly IUserRepository _userRepo;

        public BlabsController(ILogger<BlabsController> logger, IBlabRepository repo, IUserRepository userRepo)
        {
            _logger = logger;
            _repo = repo;
            _userRepo = userRepo;
        }

        [HttpGet(Name = "Blabs")]
        public IEnumerable<Blab> GetAll()
        {
            _logger.LogInformation("Retrieving all the blabs");
            return _repo.GetAll();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            _logger.LogInformation("Retrieving blab " + id);
            try
            {
                var b = _repo.GetById(id);
                return Ok(b);
            } catch (NotFoundException e)
            {
                return NotFound();
            }        
        }

        [HttpPost]
        public IActionResult Post([FromBody] BlabDto? bDto)
        {
            if (bDto is null)
            {
                _logger.LogError("Blab object is null");
                return BadRequest("Blab object is null");
            }

            User user;
            try
            {
                user = _userRepo.GetByEmail(bDto.UserEmail);
            }
            catch (Exception e)
            {
                throw new BadHttpRequestException(e.Message);
            }

            try
            {
                Blab b = new(user, bDto.Content );

                _repo.Add(b);

                return CreatedAtRoute("Blabs", new { id = b.Id }, b);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
