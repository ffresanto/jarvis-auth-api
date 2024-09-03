using System.Data;

namespace JarvisAuth.Core.Messages
{
    public static class GlobalMessages
    {
        #region [StatusCode]

        public const string GLOBAL_EXCEPTION_500 = "An unexpected error occurred while processing your request.";
        public const string OPERATION_SUCCESS_200 = "Operation completed successfully.";
        public const string REQUEST_NOT_FOUND_404 = "The requested resource was not found.";
        public const string REQUEST_CONFLICT_409 = "The resource could not be created due to a conflict with the current state of the system.";
        public const string ACCESS_DENIED_403 = "Access denied.";
        public const string UNAUTHORIZED_ACCESS_401 = "Unauthorized access.";
        public const string VALIDATION_ERRORS_422 = "The operation could not be processed due to validation errors.";

        #endregion

        #region [Errors]

        public const string DATABASE_RECORD_NOT_FOUND = "No records were found in the database.";
        public const string AUTHENTICATION_FAILED = "Authentication error.";
        public const string DATABASE_SAVE_FAILED = "A failure occurred while saving in the database.";
        public const string DATABASE_DELETE_FAILED = "A failure occurred while delete in the database.";


        #endregion

        #region [Validations]

        public const string INVALID_EMAIL = "The email provided is invalid.";
        public const string INCORRECT_PASSWORD = "The password provided is incorrect.";
        public const string ACCOUNT_DISABLED = "This account has been disabled.";
        public const string NAME_ALREADY_EXISTS = "The name provided already exists.";
        public const string EMAIL_ALREADY_EXISTS = "The email provided already exists.";
        public const string NAME_REQUIRED = "A name is required.";
        public const string USER_ID_REQUIRED = "A User Id is required.";
        public const string APPLICATION_ID_REQUIRED = "A Application Id is required.";
        public const string PERMISSION_ID_REQUIRED = "A Permission Id is required.";
        public const string NAME_LENGTH_2_TO_100 = "The name must be between 2 and 100 characters.";
        public const string EMAIL_REQUIRED = "An email is required.";
        public const string PASSWORD_REQUIRED = "A password is required.";
        public const string PASSWORD_MIN_LENGTH_6 = "The password must be at least 6 characters long.";
        public const string MANDATORY_EMAIL = "The user's email is mandatory.";
        public const string MANDATORY_APPLICATION_NAME = "The Application Name is mandatory.";
        public const string MANDATORY_PASSWORD = "The User password is mandatory.";
        public const string USER_NOT_EXISTS = "The User provided does not exist.";
        public const string USER_PERMISSION_ALREADY = "This permission is already registered for the user.";
        public const string PERMISSION_NOT_EXISTS = "The Permission provided does not exist.";
        public const string APPLICATION_NOT_EXISTS = "The Application provided does not exist.";
        public const string USER_IS_LINKED_TO_APPLICATION = "User is already linked to the specified application.";
        public const string PROVIDER_APPLICATION_AND_PERMISSION = "You must provide either an applicationId or a permissionName.";
        public const string PERMISSION_LINKED_USER = "The permission is linked to a user. To remove this permission, you must first detach it from the associated user.";

        #endregion

        #region [Token]

        public const string INVALID_TOKEN_OR_REFRESH_TOKEN = "The token or refresh token provided is invalid.";

        #endregion

        #region [Success] 

        public const string RECORD_SAVED_SUCCESSFULLY = "Record saved successfully.";
        public const string RECORD_UPDATED_SUCCESSFULLY = "Record updated successfully.";
        public const string RECORD_DELETE_SUCCESSFULLY = "Record delete successfully.";

        #endregion
    }
}
