﻿using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Cart
{
    public class CartGetByIdResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public CartResponse CartResponse { get; set; } = new CartResponse();
    }
}
