using BlabberApp.Domain.Common.Interfaces;
using DataStore.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _repo;

        public UsersController(ILogger<UsersController> logger, IUserRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet(Name = "User")]
        public IEnumerable<User> GetAll()
        {
            _logger.LogInformation("Retrieving all the users");
            return _repo.GetAll();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            _logger.LogInformation("Retrieving user " + id);
            User u;
            try
            {
                u = _repo.GetById(id);
            } catch (NotFoundException e)
            {
                return NotFound();
            }
                
            return Ok(u);
        }


        [HttpGet]
        [Route("Username/{username}")]
        public IActionResult GetByUsername([FromRoute] string username)
        {
            _logger.LogInformation("Retrieving user " + username);
            User u;
            try
            {
                u = _repo.GetByUsername(username);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }

            return Ok(u);
        }

        [HttpGet]
        [Route("Email/{email}")]
        public IActionResult GetByEmail([FromRoute] string email)
        {
            _logger.LogInformation("Retrieving user " + email);
            User u;
            try
            {
                u = _repo.GetByEmail(email);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }

            return Ok(u);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto? uDto)
        {
            try
            {
                if (uDto is null)
                {
                    _logger.LogError("User object is null");
                    return BadRequest("User object is null");
                }

                User u = new(uDto.Email, uDto.Username, uDto.FirstName, uDto.LastName);

                _repo.Add(u);

                return CreatedAtRoute("User", new { id = u.Id }, u);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("Remove")]
        public IActionResult Remove([FromBody] UserDto? uDto)
        {
            try
            {
                if (uDto is null)
                {
                    _logger.LogError("User object is null");
                    return BadRequest("User object is null");
                }

                User user;
                try
                {
                    user = _repo.GetByEmail(uDto.Email);
                } 
                catch (NotFoundException e)
                {
                    return NotFound();
                }
                _repo.Remove(user);

                return AcceptedAtRoute("User", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]UserDto? uDto)
        {
            try
            {
                if (uDto is null)
                {
                    _logger.LogError("User object is null");
                    return BadRequest("User object is null");
                }

             
                
                try
                {
                    var user = _repo.GetByEmail(uDto.Email);
                    
                } catch (NotFoundException e)
                {
                    return BadRequest("User to update does not exist");
                }

                User u = new(uDto.Email, uDto.Username, uDto.FirstName, uDto.LastName);
                u.Validate();

                _repo.Update(u);

                return AcceptedAtRoute("User", new { id = u.Id }, u);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
