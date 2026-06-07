using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Sales.Domain.Entities;

public class UserSaleEntity
{
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; } 
    public string LastName { get; private set; } 
    public CpfVO Cpf { get; private set; } 
    public EmailVO Email { get; private set; } 

    public UserSaleEntity(
        string firstName,
        string lastName,
        string cpf,
        string email,
        Guid userId)
    {

        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Cpf = new CpfVO(cpf);
        Email = new EmailVO(email);
    }
    
}
