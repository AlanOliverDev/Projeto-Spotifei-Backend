namespace Spotifei.Model;

public interface Notificavel
{
    void EnviarNotificacao(string mensagem);
    void ReceberNotificacao(string mensagem);
}