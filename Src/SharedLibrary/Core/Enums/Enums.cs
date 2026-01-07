using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum Language
    {
        [Description("Default-English")]
        DefaultEnglish = 0,
        [Description("English")]
        English = 1,
        [Description("Hindi")]
        Hindi = 2,
        [Description("Punjabi")]
        Punjabi = 3
    }

    public enum ConversionType
    {
        [Description("None")] None = 0,
        [Description("Indian Rupees")] IndianRupees = 1,
        [Description("Month Name")] MonthName = 2,
        [Description("Percentage")] Percentage = 3
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
        [Description("Invalid Parameters Or Data Does Not Exist For Requested Parameters.")]
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
        [Description("Application submitted successfully.")]
        ApplicationSubmittedScuccessfully = 16,
        [Description("Application Already Submitted.")]
        ApplicationAlreadySubmitted = 17,
        [Description("Record Already Exist.")]
        RecordAlreadyExists = 18,
        [Description("Record Deleted successfully.")]
        RecordDeleted = 19,
        [Description(" Records Found. ")]
        RecordsFound = 20,
        [Description("Sorry ! An error occured while processing your request.")]
        SorryAnErrorOccuredWhileProcessingYourRequest = 21,
        [Description("Email already exists.")]
        EmailAlreadyExists = 22,
        [Description("Internal Id already exists.")]
        InternalIdAlreadyExists = 23,
        [Description("SSO detail not found.")]
        SSODetailNotFound = 24,
        [Description("SSO details fetching successfully.")]
        SSODetailsFetchingSuccessfully = 25,
        [Description("Session data (authKey) do not match.")]
        AuthKeyMismatch = 26,
        [Description("Session data do not match (authId tempered).")]
        AuthIdMismatch = 27,
        [Description("Login id and password is mandatory.")]
        LoginPasswordmandatory = 28,
        [Description("User details not found.")]
        UserDetailNotFound = 29,
        [Description("Session expired or not created yet.")]
        SessionExpired = 30,
        [Description("Internal Id Generate Successfully")]
        InternalIdGenerateSuccessfully = 31,
        [Description("please enter description first.")]
        pleaseEnterDescriptionFirst = 32,
        [Description("Please select departmenttype first!")]
        PleaseSelectDepartmentTypeFirst = 33,
        [Description("Please fill Name of Proposed Plan, if Any..")]
        PleaseFillNameOfProposedPlanIfAny = 34,
        [Description("Please Select Idea..")]
        PleaseSelectIdea = 35,
        [Description("Please Select Idea evolved or implemented..")]
        PleaseSelectIdeaEvolvedOrImplemented = 36,
        [Description("Please fill Description..")]
        PleaseFillDescription = 37,
        [Description("Invalid Description!")]
        InvalidDescription = 38,
        [Description("Word Limit Is Crossed...")]
        PleaseSelectAtLeastOneNatureOfInnovation = 39,
        [Description("Please Select Requirements..")]
        PleaseSelectRequirements = 40,
        [Description("Please Select Status..")]
        PleaseSelectStatus = 41,
        [Description("File is too large, upload upto 10 MB!")]
        FileIsTooLargeUploadUpto10MB = 42,
        [Description("File should be in .doc,.pdf,.jpg format only!")]
        FileShouldBeInDocPdfJpgFormatOnly = 43,
        [Description("Please Select Your Rating, if Any..")]
        PleaseSelectYourRatingIfAny = 44,
        [Description("Please fill Title..")]
        PleaseFillTitle = 45,
        [Description("Word Limit Is Crossed")]
        WordLimitIsCrossed = 46,
        [Description("File not uploaded!")]
        FileNotUploaded = 47,
        [Description("File name not exists!")]
        FileNameNotExists = 48,
        [Description("File should be in .pptx,.pdf,.jpg,.ppt format only!")]
        FileShouldBeInPptxPdfJpgPptFormatOnly = 49,
        [Description("File should be in .jpg format only!")]
        FileShouldBeInJpgFormatOnly = 50,
        [Description("Record Cannot be saved.")]
        RecordCanNotBeSaved = 51,
        [Description("Officer Application Closed Successfully.")]
        OfficerApplicationClosedSuccessfully = 52,
        [Description("Please provide officer name first.")]
        PleaseProvideOfficerNameFirst = 53,
        [Description("Please provide designation first.")]
        PleaseProvideDesignationFirst = 54,
        [Description("Please provide office location first.")]
        PleaseProvideOfficeLocationFirst = 55,
        [Description("Please provide employee number first.")]
        PleaseProvideEmployeeNumberFirst = 56,
        [Description("Please provide gpf number first.")]
        PleaseProvideGPFNumberFirst = 57,
        [Description("Please provide mobile number first.")]
        PleaseProvideMobileNumberFirst = 58,
        [Description("Please provide phone number first.")]
        PleaseProvidePhoneNumberFirst = 59,
        [Description("Please provide email first.")]
        PleaseProvideEmailFirst = 60,
        [Description("Please provide SSO first.")]
        PleaseProvideSSOFirst = 61,
        [Description("Please select photograph jpeg,jpg,png only.")]
        PleaseSelectPhotographJPEGJPGPNGOnly = 62,
        [Description("Please provide office name first.")]
        PleaseProvideOfficeNameFirst = 63,
        [Description("RajMaster data sync successfully.")]
        RajMasterDataSyncSuccessfully = 64,
        [Description("Time expired must be less then 30 mins.")]
        MaxTime = 65,
        [Description("Please enter project name first.")]
        PleaseEnterProjectNameFirst = 66,
        [Description("Please Enter Category Name First.")]
        PleaseEnterCategoryNameFirst = 67,
        [Description("Please Enter Role Name First.")]
        PleaseEnterRoleNameFirst = 68,
        [Description("Please Select Status First.")]
        PleaseSelectStatusFirst = 69,
        [Description("Please Select Role First.")]
        PleaseSelectRoleFirst = 70,
        [Description("Please Select Service Request Project First.")]
        PleaseSelectServiceRequestProjectFirst = 71,
        [Description("Please Select Service Request Scheme First.")]
        PleaseSelectServiceRequestSchemeFirst = 72,
        [Description("Please Enter Description First.")]
        PleaseEnterDescriptionFirst = 73,
        [Description("Please Enter Description 2000 Character Only.")]
        PleaseEnterDescription2000CharacterOnly = 74,
        [Description("Please select department first!")]
        PleaseSelectDepartmentFirst = 75,
        [Description("Please select officer level first!")]
        PleaseSelectOfficerLevelFirst = 76,
        [Description("Please select officer first!")]
        PleaseSelectOfficerFirst = 77,
        [Description("Please select nodal officer first!")]
        PleaseSelectNodalOfficerFirst = 78,
        [Description("Please select office first!")]
        PleaseSelectOfficeFirst = 79,
        [Description("Please select zone first!")]
        PleaseSelectZoneFirst = 80,
        [Description("Please select section first!")]
        PleaseSelectSectionFirst = 81,
        [Description("Email not verified.")]
        EmailNotVerified = 82,
        [Description("Session expired.")]
        SessionTimeout = 83,
        [Description("Please select originating authority")]
        OriginatingAuthority = 84,
        [Description("Due to un-availability of mapping grievance can't be register")]
        AutoAllocationFailed = 85,
        [Description("Please enter Janaadhar number")]
        PleaseEnterJanaadharNumber = 86,
        [Description("Please select Janaadhar member id")]
        PleaseSelectJanaadharMemberId = 87,
        [Description("GrievanceId Is Verified")]
        VerifiedPrevGrievance = 88,
        [Description("GrievanceId is not verified please enter valid grievanceId")]
        NotVerifiedPrevGrievance = 89,
        [Description("Grievance Form Submitted Successfully")]
        GrievanceFormSubmitted = 90,
        [Description("Please Select Available Action")]
        PleaseSelectAvailableAction = 91,
        [Description("First configure the team for selected project.")]
        NodalOfficerNotFound = 92,
        [Description("Login successfully")]
        LoginSuccessfully = 93,
        [Description("User not authenticated")]
        UserNotAuthenticated = 94,
        [Description("Record already deleted")]
        RecordAlreadyDeleted = 95,
        [Description("Service Request is already disposed...!")]
        ServiceRequestIsAlreadyDisposed = 96,
        [Description("Service Request Forwarded Successfully!")]
        ServiceRequestForwardedSuccessfully = 97,
        [Description("Please select Priority.!")]
        PleaseSelectPriority = 98,
        [Description("Service Request Pullback Successfully!")]
        ServiceRequestPullbackSuccessfully = 99,
        [Description("Service Request Transfer Successfully!")]
        ServiceRequestTransferSuccessfully = 100,
        [Description("Service Request Dispose Successfully!")]
        ServiceRequestDisposeSuccessfully = 101,
        [Description("Service Request Reply Successfully!")]
        ServiceRequestReplySuccessfully = 102,
        [Description("Service Request Clarification Successfully!")]
        ServiceRequestClarificationSuccessfully = 103,
        [Description("Service Request Sent For Approval Successfully!")]
        ServiceRequestSentForApprovalSuccessfully = 104,
        [Description("You have already taken action on this...!")]
        Youhavealreadytakenactiononthis = 105,
        [Description("Service Request Approval Successfully!")]
        ServiceRequestApprovalSuccessfully = 106,
        [Description("Service Request is not disposed, so it cannot be reopened...!")]
        ServiceRequestIsNotDisposedSoItCanNotBeReOpened = 107,
        [Description("Service Request Reopened Successfully!")]
        ServiceRequestReopenedSuccessfully = 108,
        [Description("Please Enter Role Name Hindi First.")]
        PleaseEnterRoleNameHindiFirst = 109,
        [Description("Please Enter Project Name Hindi First.")]
        PleaseEnterProjectNameHindiFirst = 110,
        [Description("Please Enter Category Name Hindi First.")]
        PleaseEnterCategoryNameHindiFirst = 111,
        [Description("Citizen data not found. Please check Citizen data")]
        CitizenDataNotFound = 112,
        [Description("Scheme/Service data found. Please check Scheme/Service data.")]
        SchemeServiceDataNotFound = 113,
        [Description("Application data found. Please check Application data.")]
        SchemeServiceApplicationDataNotFound = 114,
        [Description("Please check Scheme/Service destination office data.")]
        SchemeServiceDestinationOfficeDataNotFound = 115,
        [Description("Application record does not exists.")]
        ApplicationRecordDoesNotExists = 116,
        [Description("Application E-Form not submitted.")]
        ApplicationEFormNotSubmitted = 117,
        [Description("Application documents not submitted")]
        ApplicationDocumentNotSubmitted = 118,
        [Description("Citizen details saved successfully.")]
        CitizenProfileSavedSuccessfully = 119,
        [Description("Citizen details updated successfully.")]
        CitizenProfileUpdatedSuccessfully = 120,
        [Description("Form details saved successfully.")]
        ApplicationEFormDataSavedSuccessfully = 121,
        [Description("Form details updated successfully.")]
        ApplicationEFormDataUpdatedSuccessfully = 122,
        [Description("Documents uploaded successfully.")]
        ApplicationDocumentSavedSuccessfully = 123,
        [Description("Documents uploaded successfully.")]
        ApplicationDocumentUpdatedSuccessfully = 124,
        [Description("Thanks for submitting the feedback for your grievance")]
        FeedBackSubmit = 125,
        [Description("Thanks for your Reopen request")]
        ReopenRequest = 126,
        [Description("Grievance Under Process")]
        GrievanceUnderProcess = 127,
        [Description("Please select office type name hindi.")]
        PleaseSelectOfficetypename = 128,
        [Description("Please select office type name.")]
        PleaseSelectOfficetypenamehindi = 129,
        [Description("Please select department.")]
        PleaseSelectDepartment = 130,
        [Description("Please select location .")]
        PleaseSelectLocation = 131,
        [Description("Please select Designation .")]
        PleaseSelectDesignation = 132,
        [Description("Please select panel name .")]
        PleaseSelectPanelname = 133,
        [Description("Please select SSOId name .")]
        PleaseSelectSSOId = 134,
        [Description("Please select User.")]
        PleaseSelectUser = 135,
        [Description("Please select Language .")]
        PleaseSelectLanguage = 136,
        [Description("Please select office  name hindi.")]
        PleaseSelectOfficenamehindi = 137,
        [Description("Please select office  name.")]
        PleaseSelectOfficename = 138,
        [Description("Please select Section  name.")]
        PleaseSelectSectioname = 139,
        [Description("Please select Section  name hindi.")]
        PleaseSelectSectionamehindi = 140,
        [Description("Please Select User.")]
        PleaseSelectuser = 141,
        [Description("Please select delegate user .")]
        PleaseSelectDelegateuser = 142,
        [Description("Please select Category .")]
        PleaseSelectCategory = 143,
        [Description("Please select Authority .")]
        PleaseSelectAuthority = 144,
        [Description("Please select Subject .")]
        PleaseSelectSubject = 145,
        [Description("Please select Level .")]
        PleaseSelectLevel = 146,
        [Description("Please select User Profile .")]
        PleaseSelectUserprofile = 147,
        [Description("Please select  grievance level .")]
        PleaseSelectgrievancelevel = 148,
        [Description("Please select current status .")]
        PleaseSelectcurrentstatus = 149,
        [Description("Please select available acton .")]
        PleaseSelectavailableaction = 150,
        [Description("Please select meeting date .")]
        PleaseSelectMeetingDate = 151,
        [Description("Please select location level .")]
        PleaseSelectLocationLevel = 152,
        [Description("Please select district .")]
        PleaseSelectDistrict = 153,
        [Description("Please select title .")]
        PleaseSelectTitle = 154,
        [Description("Please select start date .")]
        PleaseSelectStartDate = 155,
        [Description("Please select end date .")]
        PleaseSelectEndDate = 156,
        [Description("Please select panel .")]
        PleaseSelectPanel = 157,
        [Description("Please select panel name hindi .")]
        PleaseSelectPanelNameHindi = 158,
        [Description("Please select officer name .")]
        PleaseSelectOfficerName = 159,
        [Description("Please select country name .")]
        PleaseSelectCountryName = 160,
        [Description("Please select country name regional .")]
        PleaseSelectCountryNameRegional = 161,
        [Description("Please select state name.")]
        PleaseSelectStateName = 161,
        [Description("Please select state name regional .")]
        PleaseSelectStateNameRegional = 162,
        [Description("Please select location type name.")]
        PleaseSelectExtendedLocationTypeName = 163,
        [Description("Please select location type name regional .")]
        PleaseSelectExtendedLocationTypeRegionalName = 164,
        [Description("Please select role name .")]
        PleaseSelectRoleName = 165,
        [Description("Please select role name hindi.")]
        PleaseSelectRoleNameHindi = 166,
        [Description("Please select dept type name .")]
        PleaseSelectDeptTypeName = 167,
        [Description("Department and Parent Department can not be same.")]
        PleaseSelectParentDeptId = 168,
        [Description("please enter reference type.")]
        PleaseSelectRefType = 169,
        [Description("please Select description.")]
        PleaseSelectDescription = 170,
        [Description("please enter description in regional.")]
        PleaseSelectDescriptionInRegional = 171,
        [Description("please select version .")]
        PleaseSelectVersion = 172,
        [Description("please select publish date .")]
        PleaseSelectPublishDate = 173,
        [Description("please select validity date .")]
        PleaseSelectValiditydate = 174,
        [Description("Please select location type name.")]
        PleaseSelectExtendedLocationName = 175,
        [Description("Please select location  name regional .")]
        PleaseSelectExtendedLocationRegionalName = 176,
        [Description("Please select department type name.")]
        PleaseSelectDepartmentTypeName = 177,
        [Description("Please Select Valid Schedule Start Date Range.")]
        PleaseSelectValidScheduleStartDateRange = 178,
        [Description("Please Select Valid Schedule End Date Range.")]
        PleaseSelectValidScheduleEndDateRange = 179,
        [Description("Tour Already Planned Between These Date Range.")]
        TourAlreadyPlanned = 180,
        [Description("Start Date should be greater than end date.")]
        StartDateGreaterThenEndDate = 181,
        [Description("Invalid tour dates.")]
        InvalidTourDates = 182,
        [Description("Other Department Office First.")]
        OtherDeptOfcFirst = 183,
        [Description("Please Select Main Purpose of Tour.")]
        PleaseSelectMainPurposeOfTour = 184,
        [Description("Please Select Valid Inspection Start Date.")]
        PleaseSelectValidInspectionStartDate = 185,
        [Description("Please Select Valid Inspection End Date.")]
        PleaseSelectValidInspectionEndDate = 186,
        [Description("Please enter name of service request role, not more than 100 Characters.")]
        NotAccedMoreThen100Above = 187,
        [Description("Please enter the name of the project is less than 250 Characters.")]
        PleaseEnterProjectName250Characters = 188,
        [Description("Please enter the hindi name of the project is less than 350 Characters.")]
        PleaseEnterProjectHindiName350Characters = 189,
        [Description("Please enter the name of the project scheme is less than 250 Characters.")]
        PleaseEnterProjectSchemeName250Characters = 190,
        [Description("Please enter the hindi name of the project scheme is less than 350 Characters.")]
        PleaseEnterProjectSchemeHindiName350Characters = 191,
        [Description("Scheme is not mapped with the selected project.")]
        SchemeIsNotMappedWithSelectedProject = 192,
        [Description("Service request role record updated successfully.")]
        SRRoleRecordUpdatedSuccessfully = 200,
        [Description("Service request role add successfully.")]
        SRRoleRecordAddSuccessfully = 201,
        [Description("Service request project record updated successfully.")]
        SRProjectRecordUpdatedSuccessfully = 202,
        [Description("Service request project add successfully.")]
        SRProjectRecordAddSuccessfully = 203,
        [Description("Service request project team record updated successfully.")]
        SRProjectTeamRecordUpdatedSuccessfully = 204,
        [Description("Service request project team add successfully.")]
        SRProjectTeamRecordAddSuccessfully = 205,
        [Description("Service request scheme record updated successfully.")]
        SRSchemeRecordUpdatedSuccessfully = 206,
        [Description("Service request scheme record add successfully.")]
        SRSchemeRecordAddSuccessfully = 207,
        [Description("This office type already used in office master.")]
        ThisOfficeTypeAlreadyusedInOfficeMaster = 208,
        [Description("Api executed successfully")]
        ApiExecutedSuccessfully = 209,
        [Description("Do Not Enter Dublicate Data, Entry Already Exists. Please Select Another Start Date or District.")]
        DuplicateDataEntryAlreadyExists = 210,
        [Description("New role added successfully")]
        NewRoleaddedSuccessfully = 211,
        [Description("Role details updated successfully")]
        RoledetailsupdatedSuccessfully = 212,
        [Description("New menu added successfully")]
        NewMenuaddedSuccessfully = 213,
        [Description("Menu details updated successfully")]
        MenudetailsupdatedSuccessfully = 214,
        [Description("Country added successfully")]
        Countryaddedsuccessfully = 215,
        [Description("Country updated successfully")]
        Countryupdatedsuccessfully = 216,
        [Description("State added successfully")]
        Stateaddedsuccessfully = 217,
        [Description("State updated successfully")]
        Stateupdatedsuccessfully = 218,
        [Description("Delegate added successfully")]
        Delegateaddedsuccessfully = 221,
        [Description("Delegate updated successfully")]
        Delegateupdatedsuccessfully = 222,
        [Description("Grievance allocation added successfully")]
        GrievanceAllocationaddedsuccessfully = 223,
        [Description("Grievance allocation updated successfully")]
        GrievanceAllocationupdatedsuccessfully = 224,
        [Description("Grievance allocation deleted successfully")]
        GrievanceAllocationdeletedsuccessfully = 225,
        [Description("Grievance action flow  added successfully")]
        GrievanceActionFlowaddedsuccessfully = 226,
        [Description("Grievance action flow  updated successfully")]
        GrievanceActionFlowupdatedsuccessfully = 227,
        [Description("Grievance action flow  deleted successfully")]
        GrievanceActionFlowdeletedsuccessfully = 228,
        [Description("Minister master  added successfully")]
        MinisterMasteraddedsuccessfully = 229,
        [Description("Minister master  updated successfully")]
        MinisterMasterupdatedsuccessfully = 230,
        [Description("Minister master  deleted successfully")]
        MinisterMasterdeletedsuccessfully = 231,
        [Description("Jansunwai added successfully")]
        Jansunwaiaddedsuccessfully = 232,
        [Description("Jansunwai updated successfully")]
        Jansunwaiupdatedsuccessfully = 233,
        [Description("Jansunwai deleted successfully")]
        Jansunwaideletedsuccessfully = 234,
        [Description("Document updated successfully")]
        Documentaddedsuccessfully = 235,
        [Description("Document added successfully")]
        Documentupdatedsuccessfully = 236,
        [Description("Slno mapping deleted successfully")]
        SlnoMappingdeletedsuccessfully = 237,
        [Description("Slno mapping updated successfully")]
        SlnoMappingaddedsuccessfully = 238,
        [Description("Slno mapping added successfully")]
        SlnoMappingupdatedsuccessfully = 239,
        [Description("New Post Assignee added successfully in Panel List.")]
        Paneladdedsuccessfully = 240,
        [Description("Post Assignee details updated successfully in Panel List.")]
        Panelupdatedsuccessfully = 241,
        [Description("Record activated successfully.")]
        RecordActivatedSuccessfully = 242,
        [Description("Record deactivated successfully.")]
        RecordInActivatedSuccessfully = 243,
        [Description("Section added successfully")]
        Sectionaddedsuccessfully = 244,
        [Description("Section updated successfully")]
        Sectionupdatedsuccessfully = 245,
        [Description("Citizen Profile already submitted")]
        CitizenProfileAlreadySubmitted = 246,
        [Description("EForm already submitted")]
        EFormAlreadySubmitted = 247,
        [Description("Document already submitted")]
        DocumentAlreadySubmitted = 248,
        [Description("Status history not found")]
        ApplicationTransactionIdNotGenerated = 249,
        [Description("Application deleted successfully")]
        ApplicationDeletedSuccessfully = 250,
        [Description("Draft applications can only be deleted.")]
        DraftApplicationCanBeDeleteOnly = 251,
        [Description("This grievance not found registered with your mobile no")]
        NotVerifiedPrevGrievanceWithMobileNumber = 252,
        [Description("OTP is verified")]
        OTPIsVerified = 253,
        [Description("Offices data not found")]
        OfficesDataNotFound = 254,
        [Description("Some technical issue while getting Profile Registration number")]
        SomeTechnicalIssueOnProfileRegistrationNumber = 255,
        [Description("Subject Master Record Saved Successfully.")]
        SubjectMasterSaveSuccessfully = 256,
        [Description("Subject Master Record Update Successfully.")]
        SubjectMasterUpdateSuccessfully = 257,
        [Description("Project and parent Project can not be same.")]
        ProjectAndParentProjectCanNotBeSame = 258,
        [Description("Project Master Record Save Successfully.")]
        ProjectMasterRecordSaveSuccessfully = 259,
        [Description("Project Master Record Update Successfully.")]
        ProjectMasterRecordUpdateSuccessfully = 260,
        [Description("Project Master Record Delete Successfully.")]
        ProjectMasterRecordDeleteSuccessfully = 261,
        [Description("Please Enter Authority Name.")]
        PleaseEnterAuthorityName = 262,
        [Description("Please Enter Authority Name Regional.")]
        PleaseEnterAuthorityNameRegional = 263,
        [Description("Please Select Originating Authority Type.")]
        PleaseSelectOriginatingAuthorityType = 264,
        [Description("Originating Authority record updated successfully.")]
        OriginatingAuthorityUpdateSuccessfully = 265,
        [Description("Originating Authority record add successfully.")]
        OriginatingAuthorityAddSuccessfully = 266,
        [Description("Please Enter Valid Mobile Number.")]
        PleaseEnterValidMobileNumber = 267,
        [Description("Please Select Assistance.")]
        PleaseSelectAssistance = 268,
        [Description("Grievance Complaint Request Record Add Successfully.")]
        GrievanceComplaintRequestAddSuccessfully = 269,
        [Description("Grievance Complaint Request Record Update Successfully.")]
        GrievanceComplaintRequestUpdateSuccessfully = 270,
        [Description("Please Enter Complainant Name.")]
        PleaseEnterComplainantName = 271,
        [Description("Please Select Gender.")]
        PleaseSelectGender = 272,
        [Description("Please Enter Father Name.")]
        PleaseEnterFatherName = 273,
        [Description("Please Select Rural Urban.")]
        PleaseSelectRuralUrban = 274,
        [Description("Please Select City.")]
        PleaseSelectCity = 275,
        [Description("Please Select Ward.")]
        PleaseSelectWard = 276,
        [Description("Please Select Panchayat Samiti.")]
        PleaseSelectPanchayatSamiti = 277,
        [Description("Please Select Gram Panchayat.")]
        PleaseSelectGramPanchayat = 278,
        [Description("Please Select Village.")]
        PleaseSelectVillage = 279,
        [Description("Citizen Information Record Add Successfully.")]
        CitizenInformationRecordAddSuccessfully = 280,
        [Description("Citizen Information Record Update Successfully.")]
        CitizenInformationRecordUpdateSuccessfully = 281,
        [Description("Please Enter Relief Required.")]
        PleaseEnterReliefRequired = 282,
        [Description("Please Enter Grievance Galary.")]
        PleaseEnterGrievanceGalary = 283,
        [Description("Please Enter Grievance Issue.")]
        PleaseEnterGrievanceIssue = 284,
        [Description("Grievance Detail New Record Add Successfully.")]
        GrienvanceDetailNewRecordAddSuccessfully = 285,
        [Description("Grievance Detail New Record Update Successfully.")]
        GrienvanceDetailNewRecordUpdateSuccessfully = 286,
        [Description("Please Select Grivance Area.")]
        PleaseSelectGrivanceArea = 287,
        [Description("Please Select Disposal Level.")]
        PleaseSelectDisposalLevel = 288,
        [Description("Grievance Area New Record Add Successfully.")]
        GrienvanceAreaNewRecordAddSuccessfully = 289,
        [Description("Grievance Area New Record Update Successfully.")]
        GrienvanceAreaNewRecordUpdateSuccessfully = 290,
        [Description("Please Select Event From Date.")]
        PleaseSelectEventFromDate = 291,
        [Description("Please Select Event To Date.")]
        PleaseSelectEventToDate = 292,
        [Description("Grievance Event New Record Add Successfully.")]
        GrienvanceEventNewRecordAddSuccessfully = 293,
        [Description("Grievance Event New Record Update Successfully.")]
        GrienvanceEventNewRecordUpdateSuccessfully = 294,
        [Description("Cannot Add More Then Five Grievance Request.")]
        CannotAddMoreThenFiveGrievanceRequest = 295,
        [Description("Please Fill Satisfaction Rating.")]
        PleaseFillSatisfactionRating = 296,
        [Description("Please Enter Grievance Id.")]
        PleaseEnterGrievanceId = 297,
        [Description("Feedback Description can not be left blank.")]
        FeedbackDescriptionCanNotBeLeftBlank = 298,
        [Description("Reason Description can not be left blank.")]
        ReasonDescriptionCanNotBeLeftBlank = 299,
        [Description("You have already sent reopen request on this grievance.")]
        YouHaveAlreadySentReopenForRequestOnThisGrievance = 300,
        [Description("Your Feedback already has been submitted With Request. So the New feedback can be submitted after grievance disposed.")]
        YourFeedbackAlreadyHasBeenSubmitted = 301,
        [Description("Feedback New Record Add Successfully.")]
        FeedbackNewRecordAddSuccessfully = 302,
        [Description("Feedback New Record Update Successfully.")]
        FeedbackNewRecordUpdateSuccessfully = 303,
        [Description("Suggestion Description can not be left blank.")]
        SuggestionDescriptionCanNotBeLeftBlank = 304,
        [Description("Suggestions New Record Add Successfully.")]
        SuggestionsNewRecordAddSuccessfully = 305,
        [Description("Suggestions New Record Update Successfully.")]
        SuggestionsNewRecordUpdateSuccessfully = 306,
        [Description("You have already sent a reminder for this grievance. You can send a reminder only after.")]
        AlreadySentAReminderForGrievance = 307,
        [Description("Comp Reminder New Record Add Successfully.")]
        CompReminderNewRecordAddSuccessfully = 308,
        [Description("Comp Reminder New Record Update Successfully.")]
        CompReminderNewRecordUpdateSuccessfully = 309,
        [Description("Please Select Event Type.")]
        PleaseSelectEventType = 310,
        [Description("Please Enter Event Name.")]
        PleaseEnterEventName = 311,
        [Description("Please Enter From Date.")]
        PleaseEnterFromDate = 312,
        [Description("Please Enter To Date.")]
        PleaseEnterToDate = 313,
        [Description("Please Enter To Description.")]
        PleaseEnterToDescription = 314,
        [Description("Event Master New Record Add Successfully.")]
        EventMasterNewRecordAddSuccessfully = 315,
        [Description("Event Master New Record Update Successfully.")]
        EventMasterNewRecordUpdateSuccessfully = 316,
        [Description("Grienvance Update Successfully.")]
        GrienvanceUpdateSuccessfully = 317,
        [Description("No Complain Found.")]
        NoComplainFound = 318,
        [Description("No Documemnt Found.")]
        NoDocumentFound = 319,
        [Description("Invalid Parameter.")]
        InvalidParameter = 320,
        [Description("Originating Authority record deleted successfully.")]
        OriginatingAuthorityDeletedSuccessfully = 321,
        [Description("Service Request Processing Rights New record saved successfully.")]
        ServiceRequestProcessingRightsNewRecordSavedSuccessfully = 322,
        [Description("Service Request Processing Rights record Update successfully.")]
        ServiceRequestProcessingRightsRecordUpdateSuccessfully = 323,
        [Description("Service Request Processing Rights record Delete successfully.")]
        ServiceRequestProcessingRightsRecordDeleteSuccessfully = 324,
        [Description("Selected record(s) deleted successfully.")]
        RoleRecordDeletedSuccessfully = 325,
        [Description("ExtendedLocationType Record Update Successfully.")]
        ExtendedLocationTypeUpdateSuccessfully = 326,
        [Description("ExtendedLocationType Record Added Successfully.")]
        ExtendedLocationTypeAddedSuccessfully = 327,
        [Description("CitizenGroup Record Added Successfully.")]
        CitizenGroupAddedSuccessfully = 328,
        [Description("CitizenGroup Record Update Successfully.")]
        CitizenGroupUpdateSuccessfully = 329,
        [Description("Please Enter DeptTypeName.")]
        PleaseEnterDeptTypeName = 330,
        [Description("Department updated Successfully")]
        Departmentupdatedsuccessfully = 331,
        [Description("Department Added Successfully")]
        DepartmentAddedsuccessfully = 332,
        [Description("Please select extended loc name .")]
        PleaseSelectExtendedLocName = 333,
        [Description("Please select extended loc name hindi  name.")]
        PleaseSelectExtendedLocNameHindi = 334,
        [Description("Department type updated Successfully")]
        DepartmentTypeupdatedsuccessfully = 335,
        [Description("Department Type Added Successfully")]
        DepartmentTypeAddedsuccessfully = 336,
        [Description("Subject Master Record Delete Successfully.")]
        SubjectMasterRecordDeleteSuccessfully = 337,
        [Description("Complain Master Sub Ex. Info. New Record Add Successfully.")]
        ComplainMasterSubExInfoNewRecordAddSuccessfully = 338,
        [Description("Complain Master Sub Ex. Info. New Record Update Successfully.")]
        ComplainMasterSubExInfoNewRecordUpdateSuccessfully = 339,
        [Description("Complain Master New Record Add Successfully.")]
        ComplainMasterNewRecordAddSuccessfully = 340,
        [Description("Complain Master New Record Update Successfully.")]
        ComplainMasterNewRecordUpdateSuccessfully = 341,
        [Description("Please enter complainant name.")]
        PleaseEnterComplaintName = 342,
        [Description("Please enter grievance area.")]
        PleaseEnterGrievanceArea = 343,
        [Description("You cannot take action on this complaint.")]
        Youcannottakeactiononthiscomplaint = 344,
        [Description("Mapped User not found")]
        MappedUserNotFound = 345,
        [Description("Subject Designation Configuration New Record Add Successfully.")]
        SubjectDesgConfgNewRecordAddSuccessfully = 346,
        [Description("Subject Designation Configuration Record Update Successfully.")]
        SubjectDesgConfgNewRecordUpdateSuccessfully = 347,
        [Description("Subject Designation Configuration Record Delete Successfully.")]
        SubjectDesgConfgNewRecordDeleteSuccessfully = 348,
        [Description("Department is not Provided.")]
        DepartmentisnotProvided = 349,
        [Description("Please enter Duration.")]
        PleaseenterDuration = 350,
        [Description("Please Select Duration Type.")]
        PleaseSelectDurationType = 351,
        [Description("Selected Duration Type is not valid.")]
        SelectedDurationTypeisnotvalid = 352,
        [Description("Subject Time Limit New Record Add Successfully.")]
        SubjectTimeLimitNewRecordAddSuccessfully = 353,
        [Description("Subject Time Limit Record Update Successfully.")]
        SubjectTimeLimitRecordUpdateSuccessfully = 354,
        [Description("Subject Time Limit Record Delete Successfully.")]
        SubjectTimeLimitRecordDeleteSuccessfully = 355,
        [Description("Role is not selected.")]
        RoleIsNotSelected = 356,
        [Description("Role Level Mapping New Record Add Successfully.")]
        RoleLevelMappingNewRecordAddSuccessfully = 357,
        [Description("Role Level Mapping Record Update Successfully.")]
        RoleLevelMappingRecordUpdateSuccessfully = 358,
        [Description("Role Level Mapping Record Delete Successfully.")]
        RoleLevelMappingRecordDeleteSuccessfully = 359,
        [Description("Grivance Exists with this mapping. So you can not delete it.")]
        GrivanceExistswiththismapping = 360,
        [Description("Notice Board Added Successfully")]
        NoticeBoardAddedsuccessfully = 361,
        [Description("Notice Board updated Successfully")]
        NoticeBoardupdatedsuccessfully = 362,
        [Description("Notice Board  deleted successfully")]
        NoticeBoarddeletedsuccessfully = 363,
        [Description("Parent Project is required")]
        ParentProjectIsRequired = 364,
        [Description("Application E-Form data resubmitted successfully")]
        ApplicationEFormDataResubmittedSuccessfully = 365,
        [Description("Application E-Form not resubmitted")]
        ApplicationEFormNotResubmitted = 366,
        [Description("E-Form Header Id not found")]
        EFormHeaderIdNotFound = 367,
        [Description("Project Scheme Status Is Changed successfully.")]
        ProjectSchemeStatusChangedSuccessfully = 368,
        [Description("Project Status Is Changed successfully.")]
        ProjectStatusIsChangedSuccessfully = 369,
        [Description("Service Request Role Status Changed successfully.")]
        ServiceRequestRoleStatusChangedSuccessfully = 370,
        [Description("Grievance generated successfully against service delivery failure.")]
        GrievanceGeneratedSuccessfully = 371,
        [Description("No Scheme/Service found for Grievance.")]
        NoServiceFoundForGrievance = 372,
        [Description("First change the scheme nodal officer, after that you have to remove this officer.")]
        Firstchangetheschemenodalofficer = 373,
        [Description("Reference Already Exist.")]
        ReferenceExist = 374,
        [Description("Please Configure Grievance Type in Subject.")]
        PleaseConfigureGrvType = 375,
        [Description("User already exists for following Role and Section -")]
        UserExistFor = 376,
        [Description("Email does not exists.")]
        Emaildoesnotexists = 377,
        [Description("Name does not exists.")]
        Namedoesnotexists = 378,
        [Description("MobileNo does not exists.")]
        MobileNodoesnotexists = 379,
        [Description("Enter SSOId.")]
        EnterSsoId = 380,
        [Description("Selected Officer Is Already Configure With Selected Role And Project.")]
        OfficerRecordAlreadyExists = 381,
        [Description("Name cannot be empty.")]
        Namecannotbeempty = 382,
        [Description("Service mapping not exists.")]
        ServiceMappingNotExists = 383,
        [Description("User have area mapping.")]
        Userhaveareamapping = 384,
        [Description("Please enter form JSON")]
        PleaseEnterFormJSON = 385,
        [Description("Project Name Already Exist")]
        ProjectNameAlreadyExist = 386,
        [Description("Project Name Hindi Already Exist")]
        ProjectNameHindiAlreadyExist = 387,
        [Description("Service Reqeust Role Already Exist")]
        ServiceReqeustRoleAlreadyExist = 388,
        [Description("Service Reqeust Role Hindi Already Exist")]
        ServiceReqeustRoleHindiAlreadyExist = 389,
        [Description("Please Enter Subject Name First.")]
        PleaseEnterSubjectName = 390,
        [Description("Please Enter Subject Name In Hindi First.")]
        PleaseEnterSubjectNameHindi = 391,
        [Description("Please select marital status")]
        PleaseSelectMaritalStatus = 392,
        [Description("Please enter current address")]
        PleaseEnterCurrentAddress = 393,
        [Description("Please enter permanent address")]
        PleaseEnterPermanentAddress = 394,
        [Description("E-Mitra Merchant Token not generated")]
        EMitraMerchantTokenNotGenerated = 395,
        [Description("Gender data not found")]
        GenderDataNotFound = 396,
        [Description("Category data not found")]
        CategoryDataNotFound = 397,
        [Description("Marital Status data not found")]
        MaritalStatusDataNotFound = 398,
        [Description("Service Request Assigned Successfully.")]
        SRAssignedSuccessfully = 399,
        [Description("Service Request Not Assigned Yet.")]
        SRNotAssignedYet = 400,
        [Description("E-Form data not saved.")]
        EFormDataNotSaved = 401,
        [Description("Already have an user for this role in same Department.")]
        RecordAlreadyExistForSameRole = 402,
        [Description("Application not submitted due to technical issues... Please try again in some time.")]
        TechnicalIssueApplicationNotSubmitted = 403,
        [Description("Please select atleast one grievance record.")]
        Pleaseselectatleastonegrievancerecord = 404,
        [Description("User exist on same section ")]
        UserExistForSameSection = 405,
        [Description("eJanSunwai cancelled successfully.")]
        CancelEjanSunwai = 406,
        [Description("You can't cancel the eJanSunwai schedule within 24 hours before the meeting starts.")]
        CanNotCancelEjanSunwai = 407,
        [Description(" Name already exist.")]
        NameAlreadyExists = 408,
        [Description(" Name Hindi already exist.")]
        NameHindiAlreadyExists = 409,

    }
}
