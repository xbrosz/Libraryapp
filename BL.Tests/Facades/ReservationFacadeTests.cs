using Autofac.Core;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.Facades;
using BL.Services.IServices;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.Facades
{
    public class ReservationFacadeTests
    {
        Mock<IReservationService> _reservationServiceMock;
        Mock<IBookPrintService> _bpServiceMock;
        public ReservationFacadeTests()
        {
            _reservationServiceMock = new Mock<IReservationService>();
            _bpServiceMock = new Mock<IBookPrintService>();
        }

    }
}
