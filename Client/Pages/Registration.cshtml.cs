using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class RegistrationModel : PageModel
    {

        [BindProperty] public string? FirstName { get; set; }
        [BindProperty] public string? LastName { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Username { get; set; }
        private readonly ILogger<RegistrationModel> _log;
        private readonly IUserRepository _repo;

        public RegistrationModel(ILogger<RegistrationModel> logger, IUserRepository repository)
        {
            _log = logger;
            _repo = repository;
            _log.LogInformation("Injected the repository");
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            User usr = new(Email, Username, FirstName, LastName);

            _repo.Add(usr);

            _log.LogInformation("Add user into repo");

            IEnumerable<User> users = _repo.GetAll();

            foreach (var u in users)
            {
                if (u.Username != null)
                {
                    // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                    _log.LogInformation(u.Username + ", " + u.Email);
                }
            }
        }
    }
}
