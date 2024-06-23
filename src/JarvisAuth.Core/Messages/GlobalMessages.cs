namespace JarvisAuth.Core.Messages
{
    public static class GlobalMessages
    {
        #region [Status]

        public const string GLOBAL_EXCEPTION = "An unexpected error occurred while processing your request.";
        public const string OPERATION_SUCESSS = "Operation completed successfully.";
        public const string OPERATION_FAILED = "Failed to complete the operation.";
        public const string OPERATION_REQUEST_NOT_FOUND = "The requested resource was not found.";
        public const string OPERATION_REQUEST_CONFLICT = "The resource could not be created due to a conflict with the current state of the system";

        #endregion

        #region [Errors]

        public const string OPERATION_VALIDATIONS_ERROS = "The operation could not be processed due to validation errors.";
        public const string RECORDS_NOT_FOUND_IN_DATABASE = "No records were found in the database.";
        public const string AUTHENTICATION_ERROR = "Authentication Error";

        #endregion
    }
}
