namespace AspNetCoreApi.Dal.Extensions
{
    public static class DalExtensions
    {
        public static void SoftDelete(this object entity)
        {
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true, null);
        }
    }
}