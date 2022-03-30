using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class UserListModel : PageModel
    {
        [BindProperty] public List<User> Users { get; set; }

        private readonly IUserRepository _repo;

        public UserListModel(IUserRepository repo)
        {
            _repo = repo;
            Users = new List<User>();
        }
        public void OnGet()
        {
            Users = (List<User>)_repo.GetAll();
        }
    }
}
