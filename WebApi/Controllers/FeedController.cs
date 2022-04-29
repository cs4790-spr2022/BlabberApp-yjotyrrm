using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedController : Controller
    {
        private readonly ILogger<FeedController> _logger;
        private readonly IBlabRepository _repo;
        private readonly IUserRepository _userRepo;

        public FeedController(ILogger<FeedController> logger, IBlabRepository repo, IUserRepository userRepo)
        {
            _logger = logger;
            _repo = repo;
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("ByDate")]
        public IEnumerable<Blab> GetFeedByDate()
        {
            _logger.LogInformation("Retrieving all the blabs sorted by date");
            List<Blab> list = _repo.GetAll().ToList();
            return list.OrderByDescending(o => o.DttmCreated);
        }


        [HttpGet]
        [Route("ByDate/GroupedByUser")]
        public IEnumerable<Blab> GetFeedByDateGroupedByUser()
        {
            _logger.LogInformation("Retrieving all the blabs sorted by date");
            List<Blab> list = _repo.GetAll().ToList();
            //first orders them by date, then by user ID; because ones from the same user should have same user ID, 
            //the original ordering of datetimes within each user group should be preserved.
            return list.OrderByDescending(o => o.DttmCreated).OrderBy(o => o.User.Id);
        }

        [HttpGet]
        [Route("GroupedByUser")]
        public IEnumerable<Blab> GetFeedGroupedByUser()
        {
            _logger.LogInformation("Retrieving all the blabs grouped by user");
            List<Blab> list = _repo.GetAll().ToList();
            //ordering by ID will put all blabs with the same user together, essentially grouping them by user.
            return list.OrderBy(o => o.User.Id);
        }


    }
}
