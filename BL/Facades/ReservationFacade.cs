﻿using BL.DTOs.Reservation;
using BL.Services.IServices;
using BL.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class ReservationFacade
    {
        private IReservationService reservationService;
        private IBookPrintService bookPrintService;

        public ReservationFacade(IReservationService reservationService, IBookPrintService bpService)
        {
            this.reservationService = reservationService;
            this.bookPrintService = bpService;
        }

        public void ReserveBook(ReservationCreateFormDto reservationDto)
        {
            var reservedBPs = reservationService.GetReservationsInDateRangeByBookAndBranch
                (
                reservationDto.BookId,
                reservationDto.BranchId,
                reservationDto.StartDate,
                reservationDto.EndDate
                );

            var bookPrints = bookPrintService.GetBookbyBranchIDAndBookID(reservationDto.BranchId,reservationDto.BookId);

            var availableBPs = bookPrints.Where(bp => !reservedBPs.Any(r => r.BookPrintId == bp.Id));

            if (availableBPs.Count() == 0)
            {
                throw new Exception("No book print is available in given date range.");
            }

            var availableBP = availableBPs.First();

            CreateReservationDto createDto = new()
            {
                BookPrintId = availableBP.Id,
                UserId = reservationDto.UserId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate
            };

            reservationService.Insert(createDto);
        }

        public void UpdateReservationDate(ReservationUpdateFormDto reservationDto)
        {
            var bookId = bookPrintService.Find(reservationDto.BookPrintId).BookId;

            var reservedBPs = reservationService.GetReservationsInDateRangeByBookAndBranch
                (
                bookId,
                reservationDto.BranchId,
                reservationDto.StartDate,
                reservationDto.EndDate
                ).Where(r => r.Id != reservationDto.Id);

            var bookPrints = bookPrintService.GetBookbyBranchIDAndBookID(reservationDto.BranchId, bookId);

            var availableBPs = bookPrints.Where(bp => !reservedBPs.Any(r => r.BookPrintId == bp.Id));

            if (availableBPs.Count() == 0)
            {
                throw new Exception("No book print is available in given date range.");
            }

            var availableBP = availableBPs.First();

            UpdateReservationDto updateDto = new()
            {
                Id = reservationDto.Id,
                BookPrintId = availableBP.Id,
                UserId = reservationDto.UserId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate
            };

            reservationService.Update(updateDto);
        }
    }
}