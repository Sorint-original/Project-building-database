using Model;
using Service;

namespace ChapeauUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

        }
    }
}