﻿using Web.Models;

namespace Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool isBearer = true);
    }
}
