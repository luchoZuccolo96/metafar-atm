using metafar_atm.Models;

public interface IAuthRepository
{
    string EsValido(string cardNumber, string pin);
    Auth GetCard(string cardNumber);
}
