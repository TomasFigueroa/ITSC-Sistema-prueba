namespace INTITUTO1.Server.Servicios
{
    public class AutenticacionService
    {
        private bool isAuthenticated;

        public bool IsAuthenticated => isAuthenticated;

        public void Login(string username, string password)
        {
            if (username == "AdministradorITSC" && password == "ITSCrionegro77.")
            {
                isAuthenticated = true;
            }
        }

        public void Logout()
        {
            isAuthenticated = false;
        }
    }
}
