using Flunt.Notifications;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Shared
{
    public abstract class BaseContractInt : Notifiable<Notification>
    {

        protected abstract void Validate(int entity);
    }
}
