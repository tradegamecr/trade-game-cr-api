using System.Threading.Tasks;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToekn);

        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
