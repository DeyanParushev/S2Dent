namespace S2Dent.Services.Automapper
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration);
    }
}
