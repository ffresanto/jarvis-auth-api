﻿using JarvisAuth.Core.Requests.User;
using JarvisAuth.Core.Responses.Shared;
using JarvisAuth.Core.Responses.User;

namespace JarvisAuth.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        public Task<Response<PostUserResponse>> PostUser(PostUserRequest request);
        public Task<Response<PostUserLoginResponse>> PostLogin(PostUserLoginRequest request);
        public Task<Response<PostUserRefreshTokenResponse>> PostRefreshToken(PostUserRefreshTokenRequest request);
        public Task<Response<List<GetUserResponse>>> GetAllUser();
        public Task<Response<PostLinkUserToApplicationResponse>> PostLinkApplication(PostLinkUserToApplicationRequest request);
    }
}
