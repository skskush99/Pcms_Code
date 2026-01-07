using System.ComponentModel;

namespace Core.Enums.User
{
    [Serializable]
    public enum UserType
    {
        [Description("Deparmental")]
        Deparmental = 1,
        [Description("Citizen")]
        Citizen = 2,
        [Description("Call Center")]
        CallCenter = 3,
        [Description("Minister")]
        Minister = 4

    }
    public enum LoginType
    {
        [Description("SsoId")]
        SsoId = 1,
        [Description("MobileNo")]
        Citizen = 2,
        [Description("LoginId")]
        CallCenter = 3,
    }

    [Serializable]
    public enum RoleType
    {
        [Description("System Admin")]
        SystemAdmin = 3
    }

    public enum Status
    {
        [Description("Alert")]
        Alert = 1,
        [Description("Success")]
        Success = 2,
        [Description("Error")]
        Error = 3,
        [Description("SessionExpired")]
        SessionExpired = 4
    }

    public enum ReturnMessage
    {
        [Description("New record saved successfully.")]
        NewRecordSavedSuccessfully = 1,
        [Description("Record updated successfully.")]
        RecordUpdatedSuccessfully = 2,
        [Description("Selected record(s) deleted successfully.")]
        SelectedRecordDeletedSuccessfully = 3,
        [Description("Oops! Record you want to delete/Update does not exist or deleted by some other user.")]
        RecordDoesNotExist = 4,
        [Description("Oops! You don't have suffucient privileges to delete this record.")]
        InsuffucientPrivilegesToDeleteRecord = 5,
        [Description("No record found.")]
        NoRecordFound = 6,
        [Description("Invalid parameters/ data does not exist for requested paremeters.")]
        InvalidParametersOrDataDoesNotExistForRequestedParameters = 7,
        [Description("Oops! You don't have sufficient privileges to access this form.")]
        InsuffucientPrivilegesToAccessThisForm = 8,
        [Description("Oops! Record you want to update is already updated by some other user. Please open the record and update again.")]
        RecordAlreadyUpdated = 9,
        [Description("Invalid Input Value")]
        ModelError = 10,
        [Description("You are not Authorized to Perform This Action!")]
        InsuffucientPrivilegesToPerformThisAction = 11,
        [Description("Record Cannot be Deleted.")]
        RecordCantBeDeleted = 12,
        [Description("Password Reset successfully.")]
        SelectedRecordResetPasswordSuccessfully = 13,
        [Description("Status Changed successfully.")]
        SelectedRecordChangeStatusSuccessfully = 14,
        [Description("Selected record(s) Submitted successfully.")]
        SelectedRecordSubmittedSuccessfully = 15,
        [Description("Application Submitted successfully.")]
        ApplicationSubmittedScuccessfully = 16,
        [Description("Application Already Submitted.")]
        ApplicationAlreadySubmitted = 17,
        [Description("Record Already Exist.")]
        RecordAlreadyExists = 18,
        [Description("Record Deleted successfully.")]
        RecordDeleted = 19,
        [Description("Records Found.")]
        RecordsFound = 20,
        [Description("Sorry ! An error occured while processing your request.")]
        ErrorOccured = 21,
        [Description("Sorry ! This Mobile No. is not Registered.   ")]
        MobileNotRegistered = 22,
        [Description("This Mobile No. is Registered.   ")]
        MobileRegistered = 23,
        [Description("User Not Exist.  ")]
        UserNotExist = 24,
        [Description("OTP Verified ")]
        OTPVerified = 25,
        [Description("OTP Not Sent ")]
        OTPNotSent = 26,
        [Description("OTP Sent ")]
        OTPSent = 27,
        [Description("OTP Not Verified.")]
        OTPNotVerified = 28,
        [Description("OTP Generated")]
        OTPGenerated = 29,
        [Description("Invalid Mobile No.")]
        InvalidMobileNo = 30,
        [Description("Mobile No. Shall Contain 10 digit")]
        MobileNoShallContain10digit = 31,
        [Description("OTP Not Generated")]
        OTPNotGenerated = 32,
        [Description("Invalid OTP. Please try again")]
        OTPWrong = 33,
        [Description("OTP Expired.")]
        OTPExpired = 34,
        [Description("OTP Verified Already")]
        OTPVerifiedAlready = 35,
        [Description("Limit Exceeds For Failed.")]
        LimitExceed = 36,
        [Description("The Mobile Number is Banned for Now, Please Try Again Later. ")]
        MobileBanned = 37,
        [Description("Session Is Changed for OTP Validation ")]
        SessionIsChanged = 38,
        [Description("Session Can not be null")]
        SessionIsNull = 39,
        [Description("Module Is Not Valid")]
        ModuleIsNotValid = 40,
        [Description("OTP can not be blank")]
        OtpIsBlank = 41,
        [Description("TransactionNumber can not be blank")]
        TransactionNumberIsBlank = 42,
        [Description("OTP Sent And Data Saved Successfully ")]
        OTPSentAndDataSaved = 43,
        [Description("Api configuration url not found")]
        ApiConfigurationUrlNotFound = 44,
        [Description("Api executed successfully")]
        ApiExecutedSuccessfully = 45,
        [Description("Your Service request has been registered successfully with Service Request No")]
        ServiceRequestRegistrationSuccessfully = 46,
        [Description("TransactionNumber is not available")]
        TransactionNumberIsNotAvailable = 47,
        [Description("Please register your self in sso first.")]
        PleaseRegisterYourSelfInSsoFirst = 48,
        [Description("Sms sent on your Registered mobile number.")]
        SmsSentAndDataSaved = 49,
        [Description("Notification not sent but Data Saved.")]
        SmsnotSent = 50,
        [Description("Mail sent on your Registered mail id.")]
        MailSentAndDataSaved = 51,
        [Description("Mail not sent but Data Saved.")]
        MailnotSent = 52,
        [Description("This role is already mapped with other service request process.")]
        RecordAllreadyInUse = 53,
        [Description("Duplicate record please connect to administration.")]
        DuplicateRecord = 54,
        [Description("Tour Details Already Exist.")]
        TourAlreadyExist = 55,
        [Description("Schedule Tour Details Already Exist.")]
        ScheduleTourAlreadyExist = 56,
        [Description("Project Status Changed successfully.")]
        ProjectChangeStatusSuccessfully = 57,
        [Description("Project Scheme Status Changed successfully.")]
        SchemeChangeStatusSuccessfully = 58,
        [Description("Service Request Role Status Changed successfully.")]
        RoleChangeStatusSuccessfully = 59,
        [Description("Scheme Name Already Exist With Selected Project.")]
        SchemeNameAlreadyExistWithSelectedProject = 60,
        [Description("Record Active Status Updated successfully")]
        RecordActiveStatusUpdated = 61,
        [Description("Service Request Project Team Record Delete Successfully.")]
        SRProjectTeamDeleteSuccessfully = 62,
        [Description("Service Request Category Record Delete Successfully.")]
        SRCategoryDeleteSuccessfully = 63,
        [Description("First Complete The Pendency Of Particular Scheme In Service Request,After That You Are Able To Delete This Scheme Record.")]
        SchemePendencyInSR = 64,
        [Description("This Delegate User Already Have Permission With Same Department, Section And Role. Please Select Another Delegate User!")]
        SameDeptSectionRoleDelegateUserAlreadyExist = 65,
        [Description("Grvnc Request For Callback Record Add Successfully.")]
        GrvncRequestForCallbackAddSuccessfully = 66,
        [Description("Schedule is Saved.")]
        ScheduleSaved = 67,
        [Description("Scheme Name Hindi Already Exist With Selected Project.")]
        SchemeNameHindiAlreadyExistWithSelectedProject = 68,
        [Description("Schedular executed successfully")]
        SchedularExecutedSuccessfully = 69,
    }
}
