namespace metafar_atm.Interfaces
{
    public interface IAuthService
    {
        string EsValido(string cardNumber, string pin);
        string GenerarTokenJWT(string cardNumber);
    }
}
