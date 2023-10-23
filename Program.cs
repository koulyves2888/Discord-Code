
using Dapr;
using SlideX.Core.Contracts;

namespace Communication.API
{
    public class Program
    {
      
        public static void Main(string[] args)
        {
            var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3501";
            var daprGrpcPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT") ?? "50002";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDaprClient(builder => builder
           .UseHttpEndpoint($"http://172.18.0.7:{daprHttpPort}")
           .UseGrpcEndpoint($"http://172.18.0.7:{daprGrpcPort}"));
            builder.Services.AddControllers().AddDapr();
            

            var app = builder.Build();
            //app.MapPost("ConfirmRegistrationEmail", [Topic("azpubsub", "userregistration")] (User user) =>
            //{
            //    Console.WriteLine("Subscriber received : " + user);
            //    return Results.Ok(user);
            //});
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCloudEvents();
            app.UseEndpoints(endpoints => {       
                
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();

            });
                
            

            app.Run();
        }
    }
}
