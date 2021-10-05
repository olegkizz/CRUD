using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
