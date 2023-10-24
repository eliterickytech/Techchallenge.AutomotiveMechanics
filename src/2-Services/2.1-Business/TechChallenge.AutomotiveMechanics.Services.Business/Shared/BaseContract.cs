using Flunt.Notifications;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Shared
{
    public abstract class BaseContract<Entity> : Notifiable<Notification> where Entity : class
    {
        protected abstract void Validate(Entity entity);
    }
}
