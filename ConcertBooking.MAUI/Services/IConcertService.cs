﻿using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public interface IConcertService
    {
        Task<List<Concert>?> GetConcertsAsync();
        Task GetConcertById(int concertId);
    }
}
