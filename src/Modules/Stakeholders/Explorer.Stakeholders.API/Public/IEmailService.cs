using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Stakeholders.API.Public
{
    public interface IEmailService
    {
        void SendEmail(AccountRegistrationDto account, string tokenData);
        void SendPasswordResetEmail(string userName, string userEmail, string secureTokenData);
        void SendRecommendedToursEmail(PersonDto person, List<TourPreviewDto> recommendedTours);
    }
}
