namespace FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData
{
    public class CreateFlexibleDataCommandVm
    {
        public Guid Id { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
