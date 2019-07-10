namespace EfCoreSample.Doman.DTO
{
    public class ProjectPutDto : ProjectDtoBase
    {
        public long Id { get; set; }
        
        public EProjectStatus? Status { get; set; }     
       
    }
}
