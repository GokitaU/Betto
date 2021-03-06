﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Betto.Model.Entities;

namespace Betto.DataAccessLayer.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task<UserEntity> SignUpAsync(UserEntity newUser);
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<UserEntity> GetUserByMailAsync(string mailAddress);
        Task<UserEntity> GetUserByIdAsync(int userId, bool includePayments, bool includeTickets);
        Task<ICollection<UserEntity>> GetUsersAsync();
    }
}
