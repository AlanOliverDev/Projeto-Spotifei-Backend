namespace Spotifei.Model;

public interface Reprodutor
{
    void Reproduzir();
    void Pausar();
    void Avancar();
    void Retroceder();
    void AjustarVolume(int nivel);
}


