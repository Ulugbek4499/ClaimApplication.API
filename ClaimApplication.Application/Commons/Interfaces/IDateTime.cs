﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimApplication.Application.Commons.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}