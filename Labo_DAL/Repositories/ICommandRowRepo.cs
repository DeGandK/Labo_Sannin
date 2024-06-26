﻿using Labo_Domain.Models;

namespace Labo_DAL.Repositories
{
    public interface ICommandRowRepo
    {
        List<CommandRow> GetByCommandId(int id);
        void Create(CommandRow cr);
    }
}