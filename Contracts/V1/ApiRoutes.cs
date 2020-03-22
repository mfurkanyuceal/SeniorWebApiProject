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
            public const string Get = Base + "/swaps/{swapId}";
            public const string Update = Base + "/swaps/{swapId}";
            public const string Delete = Base + "/swaps/{swapId}";
            public const string Create = Base + "/swaps";
        }

        public static class Cities
        {
            public const string GetAll = Base + Location + "/cities";
            public const string Get = Base + Location + "/cities/{cityId}";
            public const string Update = Base + Location + "/cities/{cityId}";
            public const string Delete = Base + Location + "/cities/{cityId}";
            public const string Create = Base + Location + "/cities";
        }

        public static class Districts
        {
            public const string GetAll = Base + Location + "/districts/{cityId}";
            public const string Get = Base + Location + "/districts/{districtId}";
            public const string Update = Base + Location + "/districts/{districtId}";
            public const string Delete = Base + Location + "/districts/{districtId}";
            public const string Create = Base + Location + "/districts";
        }

        public static class Neighborhoods
        {
            public const string GetAll = Base + Location + "/neighborhoods/{districtId}";
            public const string Get = Base + Location + "/neighborhoods/{neighborhoodId}";
            public const string Update = Base + Location + "/neighborhoods/{neighborhoodId}";
            public const string Delete = Base + Location + "/neighborhoods/{neighborhoodId}";
            public const string Create = Base + Location + "/neighborhoods";
        }

        public static class Addresses
        {
            public const string GetAll = Base + Location + "/addresses";
            public const string Get = Base + Location + "/addresses/{addressId}";
            public const string Update = Base + Location + "/addresses/{addressId}";
            public const string Delete = Base + Location + "/addresses/{addressId}";
            public const string Create = Base + Location + "/addresses";
        }

        public static class User
        {
            public const string GetAll = Base + Location + "/users";
            public const string Login = Base + "/user/login";
            public const string Get = Base + "/user/{userId}";
            public const string Register = Base + "/user/register";
            public const string Update = Base + "/user/update";
            public const string Delete = Base + "/user/delete";
            public const string UploadPhoto = Base + "/user/uploadphoto";
            public const string ProfilePhoto = Base + "/user/profilephoto";
        }
    }
}