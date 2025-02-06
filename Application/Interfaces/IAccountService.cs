using Application.DTOs;
using Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<Guid>> Registration(RegistrationModel registrationModel);
    }
}
