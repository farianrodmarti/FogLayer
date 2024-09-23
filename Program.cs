using FogLayer.DataAccess.Contexts;
using FogLayer.Domain.Entities;
using FogLayer.Domain.Entities.EdgeModules;
using FogLayer.Domain.Entities.LinkCenters;
using FogLayer.Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace FogLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Borrando BD en caso de existir una antigua,
            // esto se hace pq para que este programa en particular
            // funcione correctamente, debe iniciar con la BD vacía.
            if (File.Exists("Data.sqlite"))
                File.Delete("Data.sqlite");

            // Definiendo string de conexión.
            string connectionString = "Data Source = Data.sqlite";

            // Creando contexto a usar por repositorios.
            ApplicationContext context = new ApplicationContext("Data Source=FogLayerDb.sqlite");

            //Generando la Bd en caso de no existir
            if (!context.Database.CanConnect())
                context.Database.Migrate();

            LinkCenter linkCenter = new LinkCenter(Guid.NewGuid(), "0123");
            Collector collector = new Collector(Guid.NewGuid(), "456", 4);
            Fog fog = new Fog(Guid.NewGuid(), "789", 5, collector, linkCenter);
            ExecutionMonitor executionMonitor = new ExecutionMonitor(Guid.NewGuid(), "qwerty", 6, linkCenter.Id, ExecutionStatus.Completed);
            DataAcquisition dataAcquisition = new DataAcquisition(Guid.NewGuid(), "asdf", 7, linkCenter.Id, TimeSpan.Zero);

            context.LinkCenters.Add(linkCenter);
            context.Collectors.Add(collector);
            context.EdgeModules.Add(dataAcquisition);
            context.EdgeModules.Add(executionMonitor);
            context.Fogs.Add(fog);
            context.SaveChanges();

        }
    }

}