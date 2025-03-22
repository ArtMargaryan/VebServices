namespace Lesson1Homework
{
    public class UsersDataResponse
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public List<User> Data { get; set; }
        public Support Support { get; set; }
    }
    public class UserDataResponse
    {
        public User Data { get; set; }
        public Support Support { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }

    public class Support
    {
        public string Url { get; set; }
        public string Text { get; set; }
    }

}
