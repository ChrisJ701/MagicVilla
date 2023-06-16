using MagicVilla_API.Modelos.Dto;


namespace MagicVilla_API.Datos
    //esta clase simula objetos almacenados como en una base de datos 
    //para probar inicialmente el proyecto sin necesidad de crear base de datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id=1, Nombre="Vista a la piscina"},
            new VillaDto{Id=2, Nombre="Vista a la playa"}
        };
    }
}
