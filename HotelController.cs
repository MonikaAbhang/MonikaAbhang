using BLLogic;
using BLLogic.HotelBL;
using HotelManagment.Common;
using HotelManagment.Messages;
using HotelManagment.Models.Hotel;
using HotelManagment.Models.HotelUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Channels;
using System.Threading.Tasks;
using Utitlity;
using WebavenBusinessRulesMongo;
using static BLLogic.HotelBL.HotelBL;
using AdminConfiguration = BLLogic.AdminConfiguration;
using UserIPAddressDBModel = BLLogic.UserIPAddressDBModel;
using UserLoginArgs = BLLogic.UserLoginArgs;

namespace HotelManagment.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private static readonly NLog.Logger Infolog = NLog.LogManager.GetLogger("MerchantControllerInfologger");
        private static readonly NLog.Logger Errorlog = NLog.LogManager.GetLogger("MerchantControllerErrorlogger");

        //[Authorize]
        [HttpPost]
        [Route("")]
        public BaseResponse addhotel(Hotel objData)
        {
            try
            {

                if (objData == null)
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }
                if (objData.Name == null && String.IsNullOrEmpty(objData.Name))
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }
                if (objData.Address == null && String.IsNullOrEmpty(objData.Address))
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }

                var currentUser = HttpContext.User;
                string email = null, role = null;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    role = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                }
                //if (email == null && String.IsNullOrEmpty(email))
                //{
                //    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_TOKEN.ToString(), null);

                //}

                HotelDBModel _hotel = new HotelDBModel();
                _hotel.Name = objData.Name;
                _hotel.Address = objData.Address;
                _hotel.Phone = objData.Phone;
                _hotel.Staf = objData.Staf;
                _hotel.StartDate = objData.StartDate;
                _hotel.EndDate = objData.EndDate;

                new HotelBL().addhotel(_hotel);

                return new BaseResponse(HttpStatusCode.OK, "Success", "New Hotel Data is Created", objData);

            }
            catch (Exception ex)
            {
                String exMessage = "-------------------------------------------";
                exMessage += "\n\n Error occurs on + " + DateTime.Now.ToString("dd/MMM/yyyy");
                exMessage += "\n\n Exception Message : " + ex.Message;

                if (ex.InnerException != null)
                    exMessage += "\n\n Inner Exception Message : " + ex.InnerException;

                exMessage += "\n\n";

                Errorlog.Error("Error in Hospital api :" + exMessage);
                return new BaseResponse(HttpStatusCode.InternalServerError, "failed", messages_en.SERVER_ERROR.ToString(), null);
            }
        }


        [HttpPut]
        [Route("")]
        public BaseResponse updatehotel(Hotel hotelData)
        {
            try
            {
                if (hotelData == null)
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }
                if (hotelData.Name == null && String.IsNullOrEmpty(hotelData.Name))
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }
                if (hotelData.Address == null && String.IsNullOrEmpty(hotelData.Address))
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }

                var currentUser = HttpContext.User;
                string email = null, role = null;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    role = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                }
                //if (email == null && String.IsNullOrEmpty(email))
                //{
                //    return new BaseResponse(HttpStatusCode.BadRequest, "failed",messages_en.INVALID_TOKEN.ToString(), null);

                //}

                HotelDBModel _hotel = new HotelDBModel();
                _hotel.Name = hotelData.Name;
                _hotel.Address = hotelData.Address;
                _hotel.Phone = hotelData.Phone;
                _hotel.Staf = hotelData.Staf;
                _hotel.Status = hotelData.Status;
                _hotel.StartDate = hotelData.StartDate;
                _hotel.EndDate = hotelData.EndDate;
                _hotel.CreatedDate = hotelData.CreatedDate;

                new HotelBL().updatehotel(_hotel,hotelData._id);

                return new BaseResponse(HttpStatusCode.OK, "Success", "Hotel Data is Updated Succesfully", _hotel);

            }
            catch (Exception ex)
            {
                String exMessage = "-------------------------------------------";
                exMessage += "\n\n Error occurs on + " + DateTime.Now.ToString("dd/MMM/yyyy");
                exMessage += "\n\n Exception Message : " + ex.Message;

                if (ex.InnerException != null)
                    exMessage += "\n\n Inner Exception Message : " + ex.InnerException;

                exMessage += "\n\n";

                Errorlog.Error("Error in add coupon api :" + exMessage);

                return new BaseResponse(HttpStatusCode.InternalServerError, "failed", messages_en.SERVER_ERROR.ToString(), null);
            }

        }
        // [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public BaseResponse deletehotel(string id)
        {
            try
            {
                if (id == null)
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }
                if (id == null && String.IsNullOrEmpty(id))
                {
                    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_POST_MODEL.ToString(), null);
                }

                var currentUser = HttpContext.User;
                string email = null, role = null;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    role = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                }
                //if (email == null && String.IsNullOrEmpty(email))
                //{
                //    return new BaseResponse(HttpStatusCode.BadRequest, "failed", messages_en.INVALID_TOKEN.ToString(), null);

                //}
                
                new HotelBL().Deletehotel(id);

                return new BaseResponse(HttpStatusCode.OK, "Success", "Hotel Data is Deleted Sucessfully","DELETED");

            }
            catch (Exception ex)
            {
                String exMessage = "-------------------------------------------";
                exMessage += "\n\n Error occurs on + " + DateTime.Now.ToString("dd/MMM/yyyy");
                exMessage += "\n\n Exception Message : " + ex.Message;

                if (ex.InnerException != null)
                    exMessage += "\n\n Inner Exception Message : " + ex.InnerException;

                exMessage += "\n\n";

                Errorlog.Error("Error in add coupon api :" + exMessage);

                return new BaseResponse(HttpStatusCode.InternalServerError, "failed", messages_en.SERVER_ERROR.ToString(), null);
            }

        }


        [HttpGet]
        [Route("{id}")]
        public BaseResponse GetHotelDataById(string id)
        {
            try
            {
                var currentUser = HttpContext.User;
                string email = null, role = null;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    role = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                }
               
                AdminConfiguration getconfigure = BLLogic.Constants.GetConfiguration();

                var data = new HotelBL().GetHotelDataById(id);

                return new BaseResponse(HttpStatusCode.OK, "Success","Data get succesfully....", data);
            }
            catch (Exception ex)
            {
                String exMessage = "-------------------------------------------";
                exMessage += "\n\n Error occurs on + " + DateTime.Now.ToString("dd/MMM/yyyy");
                exMessage += "\n\n Exception Message : " + ex.Message;

                if (ex.InnerException != null)
                    exMessage += "\n\n Inner Exception Message : " + ex.InnerException;

                exMessage += "\n\n";

                Errorlog.Error("Error in GetallmerchantProfilee Api:" + exMessage);

                return new BaseResponse(HttpStatusCode.InternalServerError, "failed", messages_en.SERVER_ERROR.ToString(), null);
            }

        }


        [HttpGet]
        [Route("")]
        public BaseResponse GetHotelData()
        {
            try
            {
                var currentUser = HttpContext.User;
                string email = null, role = null;
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }
                if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    role = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                }

                var list = new HotelBL().GetAllHotelData();
                //BaseResponse SuccessResponse = new BaseResponse(HttpStatusCode.OK, "Success", "Data get succesfully....",list);
                //return SuccessResponse;
                return new BaseResponse(HttpStatusCode.OK, "Success", "Data get succesfully....",list);

            }
            catch (Exception ex)
            {
                String exMessage = "-------------------------------------------";
                exMessage += "\n\n Error occurs on + " + DateTime.Now.ToString("dd/MMM/yyyy");
                exMessage += "\n\n Exception Message : " + ex.Message;

                if (ex.InnerException != null)
                    exMessage += "\n\n Inner Exception Message : " + ex.InnerException;

                exMessage += "\n\n";

                Errorlog.Error("Error in GetallmerchantProfilee Api:" + exMessage);

                return new BaseResponse(HttpStatusCode.InternalServerError, "failed", messages_en.SERVER_ERROR.ToString(), null);
            }

        }

    }
}
