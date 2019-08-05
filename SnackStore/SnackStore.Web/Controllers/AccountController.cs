using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnackStore.Core.Helpers;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using SnackStore.Web.Helpers;
using SnackStore.Web.ViewModels;

namespace SnackStore.Web.Controllers
{
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenFactory _tokenFactory;

        public AccountController(IAccountRepository accountRepository, ITokenFactory tokenFactory)
        {
            _accountRepository = accountRepository;
            _tokenFactory = tokenFactory;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountViewModel item)
        {
            var account = await _accountRepository.FindByUserName(item.UserName);
            if (account != null)
                return Error($"Account with username :{item.UserName} already registered.");

            await _accountRepository.Register(new Account
            {
                User = item.UserName,
                Password = item.Password.ToSha256(),
                Role = item.Role
            });
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel item)
        {
            var account = await _accountRepository.FindByCredentials(item.UserName, item.Password.ToSha256());
            if (account == null)
                return Error($"Account with username :{item.UserName} not found.");

            var token = _tokenFactory.GenerateToken(account.User, account.Role);

            return token == null ? Unauthorized() : Ok(token);
        }
    }
}