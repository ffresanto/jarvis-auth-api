﻿using JarvisAuth.Core.Validations;

namespace JarvisAuth.Core.Requests.Application
{
    public class PostCreateApplicationRequest
    {
        public string? Name { get; set; }

        public List<string> Validate(PostCreateApplicationRequest data)
        {
            var errors = new List<string>();

            if (GlobalValidations.IsNullOrEmptyCustom(data.Name)) errors.Add("Name is required.");

            return errors;
        }
    }
}
