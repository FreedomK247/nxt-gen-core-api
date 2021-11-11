using AutoMapper;
using NxtGen.Account.API.BusinessLogic.Contracts;
using NxtGen.Account.API.Data.Contracts;
using NxtGen.Account.API.ViewModels;
using Enteties = NxtGen.Account.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NxtGen.Account.API.Data.Enums;
using System.Security.Cryptography;
using NxtGen.Account.API.BusinessLogic.Helpers;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;

namespace NxtGen.Account.API.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountService> _logger;

        private readonly AppSettings _appSettings;
        public AccountService(IUnitOfWork uow,
            IMapper mapper,
            IEmailService emailService,
            ILogger<AccountService> logger,
            IOptions<AppSettings> appSettings)
        {
            _uow = uow;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public bool Register(RegisterRequestViewModel model, string origin)
        {
            // validate user before creating
            if (ValidateUserEmail(model.Email))
            {
                // TODO : Send Already registered email

                _logger.LogError("Email already exists in our system.");
                return false;
            }

            // Map our model to the account entity.
            var account = _mapper.Map<Enteties.Account>(model);


            // TODO make this assignment dynamic.

            account.Role = Role.User;

            account.Created = DateTime.UtcNow;
            account.VerificationToken = RandomTokenString();

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            _uow.Add(account);
            _uow.Commit();

            // TODO send verification email

            // Tell me whats good
            return true;
        }

        public void VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }

        #region Private account methods
        private bool ValidateUserEmail(string email)
        {
            return _uow.Query<Enteties.Account>().FirstOrDefault(x => x.Email == email) != null;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40]; // TODO add such value to Appsettings
            rngCryptoServiceProvider.GetBytes(randomBytes);

            // converts to a hex string value.
            return BitConverter.ToString(randomBytes).Replace("-", "");

        }

        //private void RemoveOldRefreshTokens(Enteties.Account model)
        //{
        //    model.RefreshTokens.RemoveAll(x => !x.IsActive
        //    && !x.IsActive
        //    && x.CreateDate.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        //}

        #endregion
    }
}
