using DhuwaniSewa.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Domain
{
    public interface IOtpService
    {
        Task<bool> GenerateSendRegistrationOtpAsync(OtpViewModel request);
        Task<string> GenerateSendPasswordResetOtpAsync(OtpViewModel request);
        Task<bool> VerifyOtpAsync(VerifyOtpViewModel request);
    }
}
