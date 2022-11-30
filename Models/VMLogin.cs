namespace AuthProjectMVC6.Models
{
    //------------------Adding Model Class For Login -----------------------------
    public class VMLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool KeepLoggedIn { get; set; }

    }

    //-----------------------------------------------
}
