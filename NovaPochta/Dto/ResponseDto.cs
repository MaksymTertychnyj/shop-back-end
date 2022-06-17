namespace NovaPochta.Dto
{
    public class ResponseDto<TEntity>
        where TEntity : class
    {
#pragma warning disable
        public List<TEntity> data { get; set; } = new List<TEntity>();

#pragma warning enable
    }
}
