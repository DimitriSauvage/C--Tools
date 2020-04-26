namespace DimitriSauvageTools.Domain.Abstractions
{
    /// <summary>
    /// Entité générique qui sera mappée dans un ORM
    /// Ce type sera à utiliser pour les classes qui ont une clé primaire composée
    /// </summary>
    public abstract class EntityWithCompositeId : Entity, IEntityWithCompositeId
    {
    }
}