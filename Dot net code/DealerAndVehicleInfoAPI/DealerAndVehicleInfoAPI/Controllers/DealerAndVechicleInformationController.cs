using Azure.Core;
using DealerAndVehicleInfoAPI.DataContext;
using DealerAndVehicleInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace DealerAndVehicleInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerAndVechicleInformationController : Controller
    {
        private readonly DealerAndVechicleDbContext _dealerAndVechicleDbContext;
        public DealerAndVechicleInformationController(DealerAndVechicleDbContext dealerAndVechicleDbContext)
        {
            _dealerAndVechicleDbContext = dealerAndVechicleDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetDelalerInfo()
        {
            string Jsonresult = string.Empty;

            try
            {
                List<DealerToVehicleInfo> DealerToVehicleinfo = _dealerAndVechicleDbContext.Dealers
                    .Join(_dealerAndVechicleDbContext.DealerToVehicle,
                          dealer => dealer.Id,
                          dealerToVehicle => dealerToVehicle.DealerId,
                          (dealer, dealerToVehicle) => new { Dealer = dealer, DealerToVehicle = dealerToVehicle })
                    .Join(_dealerAndVechicleDbContext.Vehicles,
                          joined => joined.DealerToVehicle.VehicleId,
                          vehicle => vehicle.Id,
                          (joined, vehicle) => new DealerToVehicleInfo
                          {
                              Id = joined.Dealer.Id,
                              Name = joined.Dealer.Name,
                              Model = vehicle.Model,
                              Year = vehicle.Year
                          })
                    .OrderBy(joined => joined.Id)
                    .ToList();



                Jsonresult = JsonSerializer.Serialize(DealerToVehicleinfo);


                if (DealerToVehicleinfo != null)
                {

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Dealer info found",
                        Data = DealerToVehicleinfo,
                        StatusCode = (long)HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Dealer info not found",
                        Data =null,
                        StatusCode = (long)HttpStatusCode.NotFound
                    }); ;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "An error occurred while Getting data"+ ex.Message.ToString(), 
                    StatusCode = (long)HttpStatusCode.ExpectationFailed
                });
            }
            finally
            {
                saveApiLogCallDetails(Jsonresult);
            }




        }

        private void saveApiLogCallDetails(string inventory)
        {
            try
            {
                string hostName = System.Net.Dns.GetHostName();  
               IPHostEntry testip= Dns.GetHostEntry(hostName);
                string IP= testip.AddressList.FirstOrDefault(x => x.AddressFamily== AddressFamily.InterNetwork).ToString();   
                APIDataLogEntry inventoryObject = new APIDataLogEntry();
                inventoryObject.ClientIP = IP;
                inventoryObject.CreatedDate = DateTime.Now;
                inventoryObject.Response = inventory;

                _dealerAndVechicleDbContext.APIDataLogEntry.Add(inventoryObject);
                _dealerAndVechicleDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw ex;
                // Handle the exception here, for example:
               
            }

        }


    }
}
