﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Betto.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Betto.DataAccessLayer.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(BettoDbContext dbContext) 
            : base(dbContext)
        {
                
        }

        public async Task<ICollection<GameEntity>> GetLeagueGamesAsync(int leagueId) =>
            await DbContext.Games.Where(g => g.LeagueId == leagueId).ToListAsync();
    }
}