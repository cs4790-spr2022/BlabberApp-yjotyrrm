using BlabberApp.Domain.Common.Interfaces;
using DataStore.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class FeedModel : PageModel
    {
        [BindProperty] public List<Blab> Blabs { get; set; }
        [BindProperty] public string? Message { get; set; }
        [BindProperty] public string? Email { get; set; }
        private readonly ILogger<FeedModel> _log;
        private readonly IBlabRepository _repo;
        private readonly IUserRepository _userRepo;
        public FeedModel(ILogger<FeedModel> logger, IBlabRepository repository, IUserRepository userRepo)
        {
            _log = logger;
            _repo = repository;
            _userRepo = userRepo;
            Blabs = new List<Blab>();
            _log.LogInformation("Injected the repository");
        }
        public void OnGet()
        {
            Blabs = (List<Blab>)_repo.GetAll();
        }

        public void OnPost()
        {
            //this is temporary, would in final version be based on user session
            try
            {
                User poster = _userRepo.GetByEmail(Email);
                Blab b = new(poster, Message) { DttmCreated = DateTime.Now, DttmModified = DateTime.Now };
                if(b.Validate())
                {
                    _repo.Add(b);
                }
                else
                {
                    //TODO: handle invalid blab, which means invalid message
                }
                
                
            }
            catch(NotFoundException e)
            {
                //TODO: handle invalid user
            }
            
            
            
            
        }
    }
}
