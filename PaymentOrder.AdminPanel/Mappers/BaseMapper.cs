using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Entities;

namespace PaymentOrder.AdminPanel.Mappers
{
    public abstract class BaseMapper<TEntity, TModel> where TEntity : BaseEntity where TModel : BaseModel
    {
        public abstract TEntity Map(TModel model);
        public abstract TModel Map(TEntity entity);
    }
}