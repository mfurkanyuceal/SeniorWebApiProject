namespace SeniorWepApiProject.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Location = "/location";

        public const string Base = Root + "/" + Version;

        public static class SwapRoutes
        {
            public const string GetAll = Base + "/swaps";
            public const string GetAllOutGoing = Base + "/getalloutgoingswaps";
            public const string GetAllInComing = Base + "/getallincomingswaps";
            public const string Get = Base + "/swaps/{swapId}";
            public const string Update = Base + "/swaps/{swapId}";
            public const string Delete = Base + "/swaps/{swapId}";
            public const string Create = Base + "/swaps";
        }


        public static class AddressRoutes
        {
            public const string GetAll = Base + Location + "/addresses";
            public const string Get = Base + Location + "/addresses/{addressId}";
            public const string Update = Base + Location + "/addresses/{addressId}";
            public const string Delete = Base + Location + "/addresses/{addressId}";
            public const string Create = Base + Location + "/addresses";
        }

        public static class FieldOfInterestRoutes
        {
            public const string GetAll = Base + "/fieldOfInterests";
            public const string Get = Base + "/fieldOfInterest/{fieldOfInterestName}";
            public const string Update = Base + "/fieldOfInterest/{fieldOfInterestName}";
            public const string Delete = Base + "/fieldOfInterest/{fieldOfInterestName}";
            public const string Create = Base + "/fieldOfInterest";
        }

        public static class UserRoutes
        {
            public const string GetAll = Base + "/users";
            public const string Login = Base + "/user/login";
            public const string LoginWithFacebook = Base + "/user/loginwithfacebook";
            public const string Get = Base + "/user/{userId}";
            public const string Register = Base + "/user/register";
            public const string Update = Base + "/user/update";
            public const string Delete = Base + "/user/delete";
            public const string UploadPhoto = Base + "/user/uploadphoto";
            public const string ProfilePhoto = Base + "/user/profilephoto/{userId}";
            public const string ResetPassword = Base + "/user/sendresetpasswordlink/{emailOrUsername}";
            public const string Refresh = Base + "/user/refresh";
        }
    }
}