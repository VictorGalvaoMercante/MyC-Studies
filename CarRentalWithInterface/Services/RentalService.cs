using CarRental.Entities;
using CarRentalWithInterface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services
{
    internal class RentalService
    {
        public Double PricePerHour { get; set; }
        public Double PricePerDay { get; set; }

        private ITaxService _taxService;
        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice(VehicleRental vehicleRental)
        {
            TimeSpan duration = vehicleRental.Finish.Subtract(vehicleRental.Start);
            double basicPayment = 0.0;
            if (duration.TotalHours <= 12.0)
            {
                basicPayment = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }
            double tax =  _taxService.Tax(basicPayment);
            vehicleRental.Invoice = new Invoice(basicPayment, tax);
        }
    }
}
