using CarRental.Entities;
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

        private BrazilTaxService _brazilTaxService = new BrazilTaxService();
        public RentalService(double pricePerHour, double pricePerDay)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
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
            double tax = new BrazilTaxService().Tax(basicPayment);
            vehicleRental.Invoice = new Invoice(basicPayment, tax);
        }
    }
}
