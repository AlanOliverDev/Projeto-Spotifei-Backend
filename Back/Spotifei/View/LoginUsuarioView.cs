
static class LoginView
{

    public static void Exibir()
    {
        string email = "";
        string senha = "";

        System.Console.WriteLine("Bem-vind@ ao Spotifei!\n");
        System.Console.WriteLine("Por favor, inisra suas Credenciais: ");
        System.Console.WriteLine("Email: ");
        email = Console.ReadLine();
        System.Console.WriteLine("Senha: ");
        senha = console.ReadLine();

        if (new LoginController().Autenticar(email, senha))
        {
            System.Console.WriteLine("Usuario autenticado com sucesso! Pressione Enter para continuar.");
            Console.ReadKey();
            FeedbacksView.Exibir();

        }else
        {
            System.Console.WriteLine("Login e/ou Senha incorretos. Pressione Enter para tentar novamente.");
            Console.ReadKey();
            Console.Clear();
            Exibir();
        }
    }
}