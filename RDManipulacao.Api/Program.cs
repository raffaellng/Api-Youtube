
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDManipulacao.Application.Service;
using RDManipulacao.Application.Service.InsertVideos;
using RDManipulacao.Application.Video.CreateVideo;
using RDManipulacao.Application.Video.DeleteVideo;
using RDManipulacao.Application.Video.GetVideo;
using RDManipulacao.Application.Video.UpdateVideo;
using RDManipulacao.Domain.Interfaces;
using RDManipulacao.Infrastructure.Data;
using RDManipulacao.Infrastructure.Repositories;
using System.Reflection;

namespace RDManipulacao.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddMediatR(typeof(Program).Assembly);
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IVideoRepository, VideoRepository>();
            builder.Services.AddScoped<IYouTubeApiService, YouTubeApiService>();

            builder.Services.AddMediatR(typeof(CreateVideoCommandHandler).Assembly);
            builder.Services.AddMediatR(typeof(DeleteVideoCommandHandler).Assembly);
            builder.Services.AddMediatR(typeof(UpdateVideoCommandHandler).Assembly);
            builder.Services.AddMediatR(typeof(GetFilteredVideosQueryHandler).Assembly);
            builder.Services.AddMediatR(typeof(FetchAndInsertVideosCommandHandler).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
