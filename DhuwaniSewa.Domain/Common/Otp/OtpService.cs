using DhuwaniSewa.Database.Repository;
using DhuwaniSewa.Model.DbEntities;
using DhuwaniSewa.Model.ViewModel;
using DhuwaniSewa.Utils;
using DhuwaniSewa.Utils.CustomException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public class OtpService : IOtpService
    {
        private readonly IMailService _mailService;
        private readonly IRepositoryService<ApplicationUsers, int> _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OtpService(IMailService mailService,
            IRepositoryService<ApplicationUsers, int> userRepo,
            IUnitOfWork unitOfWork)
        {
            _mailService = mailService;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GenerateSendPasswordResetOtpAsync(OtpViewModel request)
        {
            try
            {
                bool success = false;
                var aspUser = await _userRepo.GetQueryable().Include(a => a.AppUsers).FirstOrDefaultAsync(a =>
                (
                a.UserName == request.EmalMobileNumber
                || (a.EmailConfirmed && request.EmalMobileNumber.ToLower().Trim() == a.Email.ToLower().Trim())
                || (a.PhoneNumberConfirmed && request.EmalMobileNumber.Trim() == a.PhoneNumber.Trim())
                )
                && a.IsActive);
                if (aspUser == null || aspUser.AppUsers == null)
                    throw new CustomException($"{request.EmalMobileNumber} is not linked to your account.");
                success = await GenerateSendOtpAsync(aspUser, request);

                var userName = aspUser.UserName;
                return userName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> GenerateSendRegistrationOtpAsync(OtpViewModel request)
        {
            try
            {
                bool success = false;
                var aspUser = await _userRepo.GetQueryable().Include(a => a.AppUsers).FirstOrDefaultAsync(a => string.Equals(a.UserName, request.UserName) && a.IsActive);
                if (aspUser == null && aspUser.AppUsers == null)
                    throw new CustomException($"User does not exit.");

                request.EmalMobileNumber = aspUser.UserName;
                success = await GenerateSendOtpAsync(aspUser, request);
                return success;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> VerifyOtpAsync(VerifyOtpViewModel request)
        {
            try
            {
                bool validOtp = false;
                var aspUser = await _userRepo.GetQueryable().Include(a => a.AppUsers).FirstOrDefaultAsync(a => a.UserName == request.UserName);
                if (aspUser == null && aspUser.AppUsers == null)
                    throw new CustomException($"{request.UserName} does not exist.");
                double timeElapsed = DateTime.Now.Subtract(aspUser.AppUsers.OtpCreatedDate).TotalMinutes;
                if (string.Equals(aspUser.AppUsers.Otp, request.Otp) && timeElapsed < 10 && aspUser.AppUsers.IsFreshOtp)
                {
                    validOtp = true;
                    aspUser.AppUsers.IsFreshOtp = false;
                    _userRepo.Update(aspUser);
                    await _unitOfWork.CommitAsync();
                }
                return validOtp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<bool> GenerateSendOtpAsync(ApplicationUsers user, OtpViewModel request)
        {
            try
            {
                var otp = OtpGenerator();
                user.AppUsers.Otp = otp;
                user.AppUsers.OtpCreatedDate = DateTime.Now;
                user.AppUsers.IsFreshOtp = true;
                _userRepo.Update(user);
                await _unitOfWork.CommitAsync();

                if (CustomValidator.IsEmail(request.EmalMobileNumber.Trim()))
                {
                    var mailMesage = new MailViewModel();
                    mailMesage.To.Add(request.EmalMobileNumber);
                    mailMesage.Subject = request.MailSubject;
                    mailMesage.Body = string.Format(request.MailBody, otp);
                    await _mailService.SendMailAsync(mailMesage);
                }

                ///TODO: Implement mobile service for otp

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string OtpGenerator()
        {
            StringBuilder otp = new StringBuilder();
            string[] allowedChars = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int number = random.Next(0, allowedChars.Length);
                otp.Append(allowedChars[number]);
            }
            return otp.ToString();
        }
    }
}
